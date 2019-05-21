using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UMP;

namespace RA.Video 
{
    public enum VideoPlayerMode
    {
        OnTarget,
        Fullscreen
    };

    /// <summary>
    ///  VideoController: Singleton Controller for 'global' access
    /// </summary> 
    public class VideoPlayerController : Singleton<VideoPlayerController>
    {
        [SerializeField] string url;
        [SerializeField] FullscreenHUDController fullscreenHUD;
        public UniversalMediaPlayer ump;
        public bool isActive;
        bool isDone;

        UnityAction<int, int> m_OnPreparedAction;
        UnityAction m_OnEndReachedAction;

        #region MonoBehaviourMethods
        void Awake()
        {
            m_OnPreparedAction += OnPrepared;
            m_OnEndReachedAction += OnEndReached;
        }

        void Start()
        {
            //VideoDownload.Instance.DownloadCoroutine
            //PlayOnTarget(url);
            //SwitchToMode(VideoPlayerMode.OnTarget);
            StartCoroutine(Download(url));
        }

        IEnumerator Download(string url)
        {
            yield return VideoDownload.Instance.DownloadCoroutine(url);
            PlayOnTarget(VideoDownload.Instance.filePath);
        }

        void OnEnable()
        {
            ump.AddPreparedEvent(m_OnPreparedAction);
            ump.AddEndReachedEvent(m_OnEndReachedAction);
        }

        void OnDisable()
        {
            ump.RemovePreparedEvent(m_OnPreparedAction);
            ump.RemoveEndReachedEvent(m_OnEndReachedAction);
        }
        #endregion

        #region Properties
        public bool IsPlaying
        {
            get { return ump.IsPlaying; }
        }

        public bool IsLooping
        {
            get { return ump.Loop; }
        }

        public bool IsPrepared
        {
            get { return ump.AbleToPlay; }
        }

        public bool IsDone
        {
            get { return isDone; }
        }

        public long Time
        {
            get { return ump.Time; }
        }

        public long Length
        {
            get { return ump.Length; }
        }

        public float NTime
        {
            get { return Time / Length; }
        }
        #endregion

        #region PublicMethods

        public void Load(string url)
        {
            ump.Path = url;
            ump.Prepare();     
        }

        public void Play()
        {
            if (!IsPrepared) return;
            ump.Play();
        }

        public void PlayFullscreen(string url)
        {
            SwitchToMode(VideoPlayerMode.Fullscreen);
            Load(url);
        }

        public void PlayOnTarget(string url)
        {
            SwitchToMode(VideoPlayerMode.OnTarget);
            Load(url);
        }

        public void SwitchToFullscreen(bool isFullscreen)
        {
            if (isActive)
            {
                VideoPlayerMode mode = isFullscreen ? VideoPlayerMode.Fullscreen : VideoPlayerMode.OnTarget;
                SwitchToMode(mode);    
            }
        }

        public void SwitchToMode(VideoPlayerMode mode)
        {
            switch (mode)
            {
                case VideoPlayerMode.OnTarget:
                    SetOrientation(false);
                    fullscreenHUD.Show(false);
                    break;
                case VideoPlayerMode.Fullscreen:
                    SetOrientation(true);
                    fullscreenHUD.Show(true);
                    break;
                default:
                    SetOrientation(false);
                    fullscreenHUD.Show(false);
                    break;
            }
        }

        #endregion

        #region PrivateMethods

        private void OnPrepared(int width, int height)
        {
            isDone = false;
            isActive = true;
            Play();
        }

        private void OnEndReached()
        {
            isDone = true;
        }

        private void SetOrientation(bool canRotate)
        {
            if (canRotate)
            {
                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.autorotateToPortrait = true;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToPortraitUpsideDown = false;
            }
            else 
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }

        #endregion
    }
}
