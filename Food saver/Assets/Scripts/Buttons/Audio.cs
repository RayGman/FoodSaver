using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private List<Sprite> spriteButtonList;
    private Image spriteButton;

    void Start()
    {
        int audio;
        spriteButton = GetComponentInParent<Image>();

        if (!PlayerPrefs.HasKey("audioPP"))
        {
            audio = 0;           
        }
        else { audio = PlayerPrefs.GetInt("audioPP"); }

        SetSpriteButton(audio);
    }

    public void Click()
    {
        int audio = PlayerPrefs.GetInt("audioPP");

        if (audio == 0)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
            audio = 1;
        }
        else if (audio != 0)
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
            audio = 0;
        }

        SetSpriteButton(audio);
        PlayerPrefs.SetInt("audioPP", audio);      
    }

    private void SetSpriteButton(int audio)
    {
        spriteButton.sprite = spriteButtonList[audio];
    }
}
