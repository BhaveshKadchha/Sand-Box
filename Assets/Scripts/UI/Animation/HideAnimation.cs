using System.Collections;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Hide")]
    public abstract class HideAnimation : ScriptableObject
    {
        public float _animationTime;
        public AnimationCurve _animationCurve;

        [HideInInspector] public RectTransform _rect;

        public bool IsAnimationRunning { get { return isAnimationRunning; } }
        bool isAnimationRunning;


        public virtual void StartAnimation(MonoBehaviour mono, RectTransform rect, System.Action action)
        {
            _rect = rect;
            mono.StartCoroutine(Animate(action));
        }

        IEnumerator Animate(System.Action action)
        {
            float elapsed = 0;
            float perc;
            OnAnimationStart();

            while (elapsed < _animationTime)
            {
                perc = elapsed / _animationTime;
                OnAnimationRunning(_animationCurve.Evaluate(perc));
                elapsed += Time.deltaTime;
                yield return 0f;
            }

            OnAnimationEnd();
            action();
            yield return 0f;
        }


        public virtual void OnAnimationStart()
        {
            isAnimationRunning = true;
        }

        public abstract void OnAnimationRunning(float perc);

        public virtual void OnAnimationEnd()
        {
            isAnimationRunning = false;
        }
    }
}