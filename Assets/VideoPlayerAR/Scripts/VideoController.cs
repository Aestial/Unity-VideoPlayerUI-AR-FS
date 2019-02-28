using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public class VideoController : Singleton<VideoController>
    {
        public UniversalMediaPlayer ump;

        [SerializeField] string url;
        [SerializeField] GameObject onTargetGO;
        [SerializeField] GameObject fullscreenGO;

        bool isDone;

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
            Load(url);
        }

        public void SwitchToMode(VideoPlayerMode mode)
        {
            switch(mode)
            {
                case VideoPlayerMode.OnTarget:
                    onTargetGO.SetActive(true);
                    onTargetGO.SetActive(false);
                    break;
                case VideoPlayerMode.Fullscreen:
                    onTargetGO.SetActive(false);
                    onTargetGO.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region MonoBehaviourMethods
        void Start()
        {

        }

        void Update()
        {
            
        }
        #endregion
    }
}
