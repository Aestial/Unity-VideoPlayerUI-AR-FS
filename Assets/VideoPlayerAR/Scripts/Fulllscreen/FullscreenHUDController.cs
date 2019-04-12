using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA.Video
{
    public class FullscreenHUDController : MonoBehaviour
    {
        [SerializeField] Canvas videoCanvas;
        [SerializeField] Canvas controlCanvas;
        [SerializeField] HUDAppearBehaviour m_HUDAppear;

        public void Show(bool enabled)
        {
            videoCanvas.enabled = enabled;
            controlCanvas.enabled = enabled;
            //m_HUDAppear.Appear(enabled);
        }
    }
}
