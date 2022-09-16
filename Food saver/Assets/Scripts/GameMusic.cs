using System.Collections.Generic;
using UnityEngine;


public class GameMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicList;
    private AudioSource my_audioSource;

    private void Start()
    {      
        my_audioSource = gameObject.GetComponent<AudioSource>();
        AudioOnline();
    }

    public void AudioOnline()
    {
        int audio = PlayerPrefs.GetInt("audioPP");

        if (gameObject.GetComponent<AudioSource>().clip == null)
        {
            MusicOn();
        }

        if (audio == 0)
        {
            my_audioSource.UnPause();
        }
        if (audio != 0)
        {
            my_audioSource.Pause();
        }
    }

    private void MusicOn()
    {
        int randMusic = Random.Range(0, musicList.Count - 1);
        my_audioSource.clip = musicList[randMusic];
        my_audioSource.Play();
    }
}
