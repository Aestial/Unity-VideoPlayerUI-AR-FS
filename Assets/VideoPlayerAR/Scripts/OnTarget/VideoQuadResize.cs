using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RA.Video
{
    public class VideoQuadResize : MonoBehaviour
    {
        public UniversalMediaPlayer ump;

        MeshRenderer m_meshRenderer;

        UnityAction m_OnPlayingAction;
        UnityAction<int, int> m_OnPreparedAction;

        void Awake()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();

            m_OnPreparedAction += OnPrepared;
            m_OnPlayingAction += OnPlaying;
        }

        void Start()
        {
            m_meshRenderer.enabled = false;
        }

        void OnEnable()
        {
            ump.AddPreparedEvent(m_OnPreparedAction);
            ump.AddPlayingEvent(m_OnPlayingAction);
        }

        void OnDisable()
        {
            ump.RemovePreparedEvent(m_OnPreparedAction);
            ump.RemovePlayingEvent(m_OnPlayingAction);
        }

        private void OnPrepared(int width, int height)
        {
            float aspectRatio = width / (float)height;
            Debug.Log("Video apsect ratio: " + aspectRatio);
            float quadHeight = 1.0f / aspectRatio;
            transform.localScale = new Vector3(1.0f, quadHeight, 1.0f);
        }

        private void OnPlaying()
        {
            m_meshRenderer.enabled = true;
        }
    }
}
