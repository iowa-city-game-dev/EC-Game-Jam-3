using Scripts;
using UnityEngine;
using Utility;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Observer
    {
        AudioSource _source;
        public AudioClip collectionSound;
        public AudioClip jumpSound;
        public AudioClip landSound;
        public AudioClip deathSound;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();

            Player player = FindObjectOfType(typeof(Player)) as Player;
            player.AddObserver(this);

            LaundryManager lm = FindObjectOfType(typeof(LaundryManager)) as LaundryManager;
            lm.AddObserver(this);
            lm.AddObserver(this);
        }

        void PlayVFX(AudioClip clip)
        {
            _source.pitch = Random.Range(0.9f, 1.1f);
            _source.volume = Random.Range(0.9f, 1.1f);
            _source.clip = clip;
            _source.Play();
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
                case Event.PLAYER_WIN:
                    Debug.Log("Win Sound.");
                    break;
            }
        }
    }
}