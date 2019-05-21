using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UMP;

namespace RA.Video
{
    /// <summary>
    ///  HUDAppearBehaviour: Appear and disappear behaviour for the control UI. 
    ///  Animates (dis)appearing effect on detected touch or after a wait time.
    /// </summary> 
    public class HUDAppearBehaviour : MonoBehaviour
    {
        public UniversalMediaPlayer ump;

        [SerializeField] CanvasGroup canvasGroup;

        [SerializeField] float waitTime = 3.0f;
        [SerializeField] float appearSpeed = 0.5f;
        [SerializeField] float disappearSpeed = 1.0f;

        bool isShown;
        bool hasTriggered;
        bool canTrigger;
        float startTime;

        UnityAction m_OnPlayingAction;
        UnityAction m_OnEndReachedAction;

        void Awake()
        {
            m_OnPlayingAction += OnPlaying;
            m_OnEndReachedAction += OnEndReached;
        }

        void Start()
        {
            isShown = true;
            hasTriggered = false;
            startTime = Time.time;
        }

        void OnEnable()
        {
            ump.AddPlayingEvent(m_OnPlayingAction);
            ump.AddEndReachedEvent(m_OnEndReachedAction);
        }

        void OnDisable()
        {
            ump.RemovePlayingEvent(m_OnPlayingAction);
            ump.RemoveEndReachedEvent(m_OnEndReachedAction);
        }

        void Update()
        {
    #if UNITY_EDITOR
            if (isShown && Input.GetMouseButton(0))
    #else
            if (isShown && Input.touchCount > 0)
    #endif
            {
                startTime = Time.time;
            }
    #if UNITY_EDITOR
            if (!isShown && Input.GetMouseButtonDown(0))
    #else
            if (!isShown && Input.touchCount > 0)
    #endif
            {
                Start();
                Appear(true);
            }
            if ((Time.time - startTime > waitTime) && canTrigger && !hasTriggered && isShown)
            {
                hasTriggered = true;
                Appear(false);
            }
    	}

        public void Appear(bool appear)
        {
            StartCoroutine(AppearCoroutine(appear));
        }

        private void OnPlaying()
        {
            canTrigger = true;
        }

        private void OnEndReached()
        {
            canTrigger = false;
        }

        IEnumerator AppearCoroutine(bool isAppearing)
        {
            float animTime = 0;
            float startValue = isAppearing ? 0.0f : 1.0f;
            float endValue = isAppearing ? 1.0f : 0.0f;
            float speed = isAppearing ? appearSpeed : disappearSpeed;
            while (animTime < 1.0f)
            {
                animTime += Time.deltaTime * speed;
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, animTime);
                yield return new WaitForEndOfFrame();
            }
            isShown = isAppearing;
            canvasGroup.interactable = isShown;
        }
    }
}
