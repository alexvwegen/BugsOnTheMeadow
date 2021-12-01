using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] tracks;
    public AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isPlaying)
        {
            player.clip = tracks[Random.Range(0, tracks.Length)];
            player.Play();
        }
    }
}
