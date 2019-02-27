using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace RAInfinitum.Video
{
    public class VideoController : MonoBehaviour
    {
        [SerializeField] string url;
        [Header("UI Elements")]
        [SerializeField] RawImage videoOutputImage;
        [SerializeField] Slider slider;
        [SerializeField] Text timeText;
        [SerializeField] Image playButton;
        [SerializeField] Image pauseButton;

        AudioSource audioPlayer;
        VideoPlayer videoPlayer;

        bool isDone;

        #region Properties

        public bool IsPlaying
        {
            get { return videoPlayer.isPlaying; }
        }

        public bool IsLooping
        {
            get { return videoPlayer.isLooping; }
        }

        public bool IsPrepared
        {
            get { return videoPlayer.isPrepared; }
        }

        public bool IsDone
        {
            get { return isDone; }
        }

        public double Time
        {
            get { return videoPlayer.time; }
        }

        public double Duration
        {
            get { return videoPlayer.frameCount / videoPlayer.frameRate; }
        }

        public double NTime
        {
            get { return Time / Duration; }
        }

        #endregion

        #region PublicMethods

        public void Load(string url)
        {
            videoPlayer.url = url;
            videoPlayer.Prepare();

            // Video features
            Debug.Log("Video Controller - Can set Direct Audio Volume: " + videoPlayer.canSetDirectAudioVolume);
            Debug.Log("Video Controller - Can set Playback Speed: " + videoPlayer.canSetPlaybackSpeed);
            Debug.Log("Video Controller - Can set Skip on Drop: " + videoPlayer.canSetSkipOnDrop);
            Debug.Log("Video Controller - Can set Time Source: " + videoPlayer.canSetTimeSource);
            Debug.Log("Video Controller - Can set Time: " + videoPlayer.canSetTime);
            Debug.Log("Video Controller - Can Step: " + videoPlayer.canStep);
        }

        public void Play()
        {
            if (!IsPrepared) return;
            videoPlayer.Play();
            audioPlayer.Play();
            TogglePlayButton(true);
        }

        public void Pause()
        {
            if (!IsPrepared || !IsPlaying) return;
            videoPlayer.Pause();
            TogglePlayButton(false);
        }

        public void Restart()
        {
            if (!IsPrepared) return;
            Pause();
            Seek(0);
        }

        public void Stop()
        {
            if (!IsPrepared || !IsPlaying) return;
            videoPlayer.Stop();
        }

        public void Loop(bool toogle)
        {
            if (!IsPrepared) return;
            videoPlayer.isLooping = toogle;
        }

        public void Seek(float nTime)
        {
            if (!videoPlayer.canSetTime) return;
            if (!IsPrepared) return;
            nTime = Mathf.Clamp(nTime, 0, 1);
            videoPlayer.time = nTime * Duration;
        }

        public void SeekFromCurrent(float seconds)
        {
            if (!videoPlayer.canSetTime) return;
            if (!IsPrepared) return;
            double time = videoPlayer.time + (double)seconds;
            time = Mathf.Clamp((float)time, 0, (float)Duration);
            videoPlayer.time = time;
        }

        #endregion

        #region MonoBehaviourMethods

        void Awake()
        {
            audioPlayer = GetComponent<AudioSource>();
            videoPlayer = GetComponent<VideoPlayer>();

            videoOutputImage.enabled = false;
            slider.value = 0;

            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.controlledAudioTrackCount = 1;
            videoPlayer.EnableAudioTrack(1, true);

            TogglePlayButton(false);

            StartCoroutine(DownloadCoroutine(url));
            //videoPlayer.Prepare();

        }

        void OnEnable()
        {
            videoPlayer.errorReceived += OnErrorReceived;
            videoPlayer.frameDropped += OnFrameDropped;
            videoPlayer.frameReady += OnFrameReady;
            videoPlayer.loopPointReached += OnLoopPointReached;
            videoPlayer.prepareCompleted += OnPrepareCompleted;
            videoPlayer.seekCompleted += OnSeekCompleted;
            videoPlayer.started += OnStarted;
        }

        void OnDisable()
        {
            videoPlayer.errorReceived -= OnErrorReceived;
            videoPlayer.frameDropped -= OnFrameDropped;
            videoPlayer.frameReady -= OnFrameReady;
            videoPlayer.loopPointReached -= OnLoopPointReached;
            videoPlayer.prepareCompleted -= OnPrepareCompleted;
            videoPlayer.seekCompleted -= OnSeekCompleted;
            videoPlayer.started -= OnStarted;
        }

        void Update()
        {
            if (!IsPrepared) return;

            slider.value = (float)NTime;

            TimeSpan time = TimeSpan.FromSeconds(Time);
            TimeSpan duration = TimeSpan.FromSeconds(Duration);
            string timeStr = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
            string durationStr = string.Format("{0:D2}:{1:D2}", duration.Minutes, duration.Seconds);
            timeText.text = timeStr + " / " + durationStr;
        }

        #endregion //MonoBehaviourMethods

        #region EventHandlers

        void OnErrorReceived(VideoPlayer source, string message)
        {
            Debug.Log("VideoController - Error Received: " + message);
        }

        void OnFrameDropped(VideoPlayer source)
        {
            Debug.Log("VideoController - Frame Dropped");
        }

        void OnFrameReady(VideoPlayer source, long frame)
        {
            // CPU Heavy, use with caution
        }

        void OnLoopPointReached(VideoPlayer source)
        {
            Debug.Log("VideoController - Loop Point Reached");
            isDone = true;
            TogglePlayButton(false);
        }

        void OnPrepareCompleted(VideoPlayer source)
        {
            Debug.Log("VideoController - Video finished Prepared");
            isDone = false;
            // CHECK
            //videoPlayer.SetTargetAudioSource(1, audioPlayer);
        }

        void OnSeekCompleted(VideoPlayer source)
        {
            Debug.Log("VideoController - Video finished Seeking");
            isDone = false;
            Play();
        }

        void OnStarted(VideoPlayer source)
        {
            Debug.Log("VideoController - Video Started!");
            videoOutputImage.enabled = true;
            isDone = false;
        }

        #endregion

        #region PrivateMethods

        void TogglePlayButton(bool isPlaying)
        {
            playButton.enabled = !isPlaying;
            pauseButton.enabled = isPlaying;
        }

        IEnumerator DownloadCoroutine(string url)
        {
            Debug.Log("Downloading video :" + url);
            var www = new WWW(url);
            while (!www.isDone)
            {
                Debug.Log("Video download progress: " + www.progress);
                yield return null;
            }
            yield return www;

            string filePath = Path.Combine(Application.persistentDataPath, "DownloadedVideo.mp4");
            File.WriteAllBytes(filePath, www.bytes);
            Debug.Log("Video Saved: " + filePath);

            www.Dispose();
            www = null;

            // Play video
            Load(filePath);
        }

        #endregion
    }
}
