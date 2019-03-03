using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Let's just end the game here
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }

    public enum Event
    {
        //every action triggered by the observer event system
        PLAYER_DEATH,
        PLAYER_JUMP,
        PLAYER_LAND,
        PLAYER_WIN,
        COLLECT_COIN
    }
}