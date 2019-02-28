using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA.Video
{
    public class FullscreenHUDController : MonoBehaviour
    {
        [SerializeField] Canvas videoCanvas;
        [SerializeField] Canvas controlCanvas;

        void Start()
        {

        }

        public void Show(bool enabled)
        {
            videoCanvas.enabled = enabled;
            controlCanvas.enabled = enabled;
        }
    }
}
