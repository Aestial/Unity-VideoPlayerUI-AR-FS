using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RA.Video
{
    public class VideoQuadResize : MonoBehaviour
    {
        public UniversalMediaPlayer ump;
        [SerializeField] float globalScale;

        MeshRenderer m_meshRenderer;

        UnityAction m_OnPlayingAction;
        UnityAction<int, int> m_OnPreparedAction;

        Vector2 resolution;

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

        public void Resize(float scale)
        {
            Resize((int)resolution.x, (int)resolution.y, scale);
        }

        private void Resize(int width, int height, float scale)
        {
            resolution.x = width;
            resolution.y = height;
            float aspectRatio = width / (float)height;
            float quadHeight = 1.0f / aspectRatio;
            transform.localScale = new Vector3(1.0f, quadHeight, 1.0f);
            transform.localScale *= scale;
        }

        private void OnPrepared(int width, int height)
        {
            Resize(width, height, globalScale);
        }

        private void OnPlaying()
        {
            m_meshRenderer.enabled = true;
        }
    }
}
