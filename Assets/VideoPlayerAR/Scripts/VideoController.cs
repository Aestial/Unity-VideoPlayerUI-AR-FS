using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA.Video 
{
    /// <summary>
    ///  VideoController: Singleton Controller for static access
    /// </summary> 
    public class VideoController : Singleton<VideoController>
    {
        public UniversalMediaPlayer ump;

        [SerializeField] string url;

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
        public void PlayFullscreen(string url)
        {
            ump.Path = url;
            ump.Prepare();
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
