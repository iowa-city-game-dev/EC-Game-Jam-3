using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : Observer
{
    AudioSource source;
    public AudioClip collectionSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip deathSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        Player player = FindObjectOfType(typeof(Player)) as Player;
        player.addObserver(this);

        LaundryManager lm = FindObjectOfType(typeof(LaundryManager))as LaundryManager;
        lm.addObserver(this);
        lm.addObserver(this);
     
    }
    void PlayVFX(AudioClip clip)
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.clip = clip;
        source.Play();

    }





    public override void OnNotify(Event thing)
    {
        Debug.Log("notes received");
        //responds to events
        switch (thing)
        {
            case Event.COLLECT_COIN:
                Debug.Log("Coin Collect Sound.");
                break;
            case Event.PLAYER_JUMP:
                Debug.Log("Jump Sound.");
                break;
            case Event.PLAYER_LAND:
                Debug.Log("Land Sound.");
                break;
            case Event.PLAYER_DEATH:
                Debug.Log("DEath Sound.");
                break;
        }
    }
}
