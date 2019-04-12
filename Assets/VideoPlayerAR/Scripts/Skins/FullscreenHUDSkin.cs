using UnityEngine;

namespace RA.Video
{
    [CreateAssetMenu(fileName = "New Fullscreen HUD Skin", menuName = "Fullscreen Skin")]
    public class FullscreenHUDSkin : ScriptableObject
    {
        public new string name;
        public string description;
        public Color backgroundColor;
        public Color buttonsColor;
        public Color panelsColor;
        public Color scrubBallColor;

        // TODO: Maybe we can customize buttons too??
        //public Sprite playButton;
        //public Sprite pauseButton;
        //public Sprite loopButton;

        public void Print()
        {
            Debug.Log("SKIN Configuration");
            Debug.Log("Skin name: " + this.name);
            Debug.Log("Skin description: " + this.description);
        }
    }
}