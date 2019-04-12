using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RA.Video
{
    public class FullscreenHUDController : MonoBehaviour
    {
        [Header("Canvas elements for show and hide")]
        [SerializeField] Canvas videoCanvas;
        [SerializeField] Canvas controlCanvas;
        //[SerializeField] HUDAppearBehaviour m_HUDAppear;

        [Header("Skin object and customizable UI elements")]
        [SerializeField] FullscreenHUDSkin skin;
        [SerializeField] Image backgroundImage;
        [SerializeField] Image scrubBallImage;
        [SerializeField] Image[] panelsImages;
        [SerializeField] Image[] buttonsImages;

        void Start()
        {
            Debug.Log("SKIN Configuration");
            Debug.Log("Skin name: " + skin.name);
            Debug.Log("Skin description: " + skin.description);

            backgroundImage.color = skin.backgroundColor;
            scrubBallImage.color = skin.scrubBallColor;
            SetImageArrayColors(panelsImages, skin.panelsColor);
            SetImageArrayColors(buttonsImages, skin.buttonsColor);

        }

        private void SetImageArrayColors(Image[] images, Color color)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = color;
            }
        }

        public void Show(bool enabled)
        {
            videoCanvas.enabled = enabled;
            controlCanvas.enabled = enabled;
            //m_HUDAppear.Appear(enabled);
        }
    }
}
