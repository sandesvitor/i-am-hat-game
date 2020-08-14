using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject jukeboxPanel;
    public AudioClip[] musicPlaylist;

    void Start() {
        jukeboxPanel.SetActive(false);    
    }

    public void PickSong(int index) {

        audioSource.clip = musicPlaylist[index];
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    public void EnableJukeboxPanel(bool enable) {
        jukeboxPanel.SetActive(enable);
    }

}
