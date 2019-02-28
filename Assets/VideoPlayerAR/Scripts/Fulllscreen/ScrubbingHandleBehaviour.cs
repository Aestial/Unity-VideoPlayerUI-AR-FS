using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RA.Video
{
    /// <summary>
    ///  ScrubbingHandleBehaviour: Shows or hides scrubbing handle image when
    ///  using scrubbing slider.
    /// </summary> 
    public class ScrubbingHandleBehaviour : MonoBehaviour
    {
        [SerializeField] Image handleImage;
        [SerializeField] Color normalColor;
        [SerializeField] Color dragColor;

        private void Start()
        {
            handleImage.color = normalColor;
        }

        public void OnBeginDrag()
        {
            handleImage.color = dragColor;
        }

        public void OnEndDrag()
        {
            handleImage.color = normalColor;
        }
    }
}
