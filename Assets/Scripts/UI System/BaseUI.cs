using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.UISystem
{
    class BaseUI : MonoBehaviour
    {
        [SerializeField] internal GameObject content;
        [Space(10), Header("Open-Close Button (Optional)")]
        [SerializeField] protected Button openButton;
        [SerializeField] protected Button closeButton;

        [HideInInspector] public Canvas canvas;
        [HideInInspector] public Coroutine uiAnimation;

        [Space(10), Header("Animations (Optional)")]
        [SerializeField] UIShowAnimation _showAnimation;
        [SerializeField] UIHideAnimation _hideAnimation;

        #region UNITY_CALLBACKs
        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        protected virtual void OnEnable()
        {
            openButton?.onClick.AddListener(Show);
            closeButton?.onClick.AddListener(Hide);
        }
        #endregion

        public virtual void Show()
        {
            if (_showAnimation)
            {
                EndHideAnimation();
                _showAnimation.Animate(this);
            }
            else
            {
                canvas.enabled = true;
            }
        }

        public virtual void Hide()
        {
            if (_hideAnimation)
            {
                EndShowAnimation();
                _hideAnimation.Animate(this);
            }
            else
            {
                canvas.enabled = false;
            }
        }

        #region ANIMATION_HANDLER
        internal void EndShowAnimation()
        {
            if (_showAnimation.isAnimationRunning)
                _showAnimation.OnAnimationEnd(this);
        }

        internal void EndHideAnimation()
        {
            if (_hideAnimation.isAnimationRunning)
                _hideAnimation.OnAnimationEnd(this);
        }

        internal void StopAnimationCoroutine()
        {
            if (uiAnimation != null)
                StopCoroutine(uiAnimation);
        }

        internal void StartAnimationCoroutine(IEnumerator anim)
        {
            uiAnimation = StartCoroutine(anim);
        }
        #endregion
    }
}