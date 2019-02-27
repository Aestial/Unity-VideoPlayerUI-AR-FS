using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RA.Video
{
    /// <summary>
    ///  HUDPlaybackBehaviour: Behaviour for the playback buttons of the control
    ///  UI.
    /// </summary> 
    public class HUDPlaybackBehaviour : MonoBehaviour
    {
        public UniversalMediaPlayer ump;

        [Header("UI Elements")]
        [SerializeField] Slider slider;
        [SerializeField] Text timeText;
        [SerializeField] Image playButton;
        [SerializeField] Image pauseButton;

        UnityAction m_OnPlayingAction;
        UnityAction m_OnPausedAction;
        UnityAction m_OnEndReachAction;
        UnityAction<float> m_OnPositionChangedAction;

        public long Time
        {
            get { return ump.Time; }
        }

        public long Length
        {
            get { return ump.Length; }
        }

        void Awake()
        {
            m_OnPlayingAction += OnPlaying;
            m_OnPausedAction += OnPaused;
            m_OnEndReachAction += OnEndReached;
            m_OnPositionChangedAction += OnPositionChanged;
        }

        void OnEnable()
        {
            ump.AddPlayingEvent(m_OnPlayingAction);
            ump.AddPausedEvent(m_OnPausedAction);
            ump.AddEndReachedEvent(m_OnEndReachAction);
            ump.AddPositionChangedEvent(m_OnPositionChangedAction);
        }

        void OnDisable()
        {
            ump.RemovePlayingEvent(m_OnPlayingAction);
            ump.RemovePausedEvent(m_OnPausedAction);
            ump.RemoveEndReachedEvent(m_OnEndReachAction);
            ump.RemovePositionChangedEvent(m_OnPositionChangedAction);
        }

        void Start()
        {
            TogglePlayButton(false);
        }


        void Update()
        {
            SetTimer();
        }

        #region EventHandlers
        private void OnPlaying()
        {
            TogglePlayButton(true);
        }

        private void OnPaused()
        {
            TogglePlayButton(false);
        }

        private void OnEndReached()
        {
            TogglePlayButton(false);
        }

        private void OnPositionChanged(float position)
        {
            slider.value = position;
        
        }
        #endregion

        private void TogglePlayButton(bool isPlaying)
        {
            playButton.enabled = !isPlaying;
            pauseButton.enabled = isPlaying;
        }

        private void SetTimer()
        {
            TimeSpan t = TimeSpan.FromMilliseconds(Time);
            TimeSpan l = TimeSpan.FromMilliseconds(Length);
            string timeStr = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            string lengthStr = string.Format("{0:D2}:{1:D2}", l.Minutes, l.Seconds);
            timeText.text = timeStr + " / " + lengthStr;
        }
    }
}
