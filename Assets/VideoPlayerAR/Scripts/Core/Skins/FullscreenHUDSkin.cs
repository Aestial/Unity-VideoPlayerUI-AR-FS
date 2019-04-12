using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Fullscreen HUD Skin", menuName = "Fullscreen Skin")]
public class FullscreenHUDSkin : ScriptableObject
{
    public new string name;
    public string description;
    public Color backgroundColor;
    public Color buttonsColor;
    public Color panelsColor;
    public Color scrubBallColor;

    //public Sprite playButton;
    //public Sprite pauseButton;
    //public Sprite loopButton;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
