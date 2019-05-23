using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UMP;

namespace RA.Video
{
    public class VideoQuadAppearHUD : MonoBehaviour
    {

        public UniversalMediaPlayer ump;

        [SerializeField] CanvasGroup[] canvasGroups;

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
            isShown = false;
            AppearInstant(isShown);
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
            if ((Time.time - startTime > waitTime) && canTrigger && !hasTriggered && isShown)
            {
                hasTriggered = true;
                StartCoroutine(AppearCoroutine(false));
            }
        }

        void OnMouseUp()
        {
            Restart();
            StartCoroutine(AppearCoroutine(true));
        }

        void OnMouseDown()
        {
            startTime = Time.time;
        }

        private void Restart()
        {
            isShown = true;
            hasTriggered = false;
            startTime = Time.time;
        }

        private void OnPlaying()
        {
            canTrigger = true;
        }

        private void OnEndReached()
        {
            canTrigger = false;
        }

        private void AppearInstant(bool appear)
        {
            isShown = appear;
            for (int i = 0; i < canvasGroups.Length; i++)
            {
                canvasGroups[i].alpha = isShown ? 1.0f : 0.0f;
                canvasGroups[i].interactable = isShown;
            }
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
                for (int i = 0; i < canvasGroups.Length; i++)
                {
                    canvasGroups[i].alpha = Mathf.Lerp(startValue, endValue, animTime);    
                } 
                yield return new WaitForEndOfFrame();
            }
            isShown = isAppearing;
            for (int i = 0; i < canvasGroups.Length; i++)
            {
                canvasGroups[i].interactable = isShown;
            }
        }
    }
}
