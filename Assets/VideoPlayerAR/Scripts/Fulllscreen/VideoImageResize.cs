using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UMP;

namespace RA.Video
{
    /// <summary>
    ///  VideoImageBehaviour: Set aspect ratio for video image and shows/hiddes it.
    /// </summary> 
    public class VideoImageResize : MonoBehaviour
    {
        public UniversalMediaPlayer ump;

        [Header("UI Elements")]
        [SerializeField] RawImage videoOutputImage;
        [SerializeField] AspectRatioFitter aspectRatioFitter;

        UnityAction<int, int> m_OnPreparedAction;

        void Awake()
        {
            m_OnPreparedAction += EnableVideoImage;
            videoOutputImage.enabled = false;
        }

        void OnEnable()
        {
            ump.AddPreparedEvent(m_OnPreparedAction);
        }

        void OnDisable()
        {
            ump.RemovePreparedEvent(m_OnPreparedAction);
        }

        void EnableVideoImage(int width, int height)
        {
            videoOutputImage.enabled = true;
            float aspectRatio = width / (float)height;
            aspectRatioFitter.aspectRatio = aspectRatio;
            Debug.Log("Video apsect ratio: " + aspectRatio);
        }
       
    }
}
