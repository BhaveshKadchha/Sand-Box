using System.Collections;
using UnityEngine;


namespace Game.UI
{
    [CreateAssetMenu(menuName = "Show")]
    public abstract class ShowAnimation : ScriptableObject
    {
        public float _animationTime;
        public AnimationCurve _animationCurve;

        public WaitForEndOfFrame wait = new WaitForEndOfFrame();
        [HideInInspector] public RectTransform _rect;

        bool isAnimationRunning;

        public bool IsAnimationRunning { get { return isAnimationRunning; } }


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
            yield return wait;
            action();

            while (elapsed < _animationTime)
            {
                perc = elapsed / _animationTime;
                OnAnimationRunning(_animationCurve.Evaluate(perc));
                elapsed += Time.deltaTime;
                yield return 0f;
            }

            OnAnimationEnd();
            yield return 0f;
        }

        public virtual void OnAnimationStart()
        {
            isAnimationRunning = true;
            _rect.localScale = Vector3.zero;
        }

        public abstract void OnAnimationRunning(float perc);

        public virtual void OnAnimationEnd()
        {
            isAnimationRunning = false;
            _rect.localScale = Vector3.one;
        }
    }
}