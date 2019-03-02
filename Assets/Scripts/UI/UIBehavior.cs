using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBehavior : MonoBehaviour
    {
        public Button startButton;
        public Button levelButton;
        public Button controlButton;
        LevelManager lvlManager;

        public Image[] knobs;

        int currentselection;

        private void Start()
        {
            lvlManager = GetComponent<LevelManager>();
            currentselection = 0;
            for (int i = 0; i < knobs.Length; i++)
            {
                knobs[i].enabled = false;
            }

            knobs[0].enabled = true;
            startButton.image.color = new Color(1, 1, 1, 0.75f);
        }

        private void Update()
        {
            HandleInput();
        }

        void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && currentselection != 2)
            {
                currentselection++;
                Selection(currentselection);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && currentselection != 0)
            {
                currentselection--;
                Selection(currentselection);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                switch (currentselection)
                {
                    case 0:
                        lvlManager.LoadLevel(1);
                        break;
                    case 1:
                        //level select

                        break;
                    case 2:
                        //controls

                        break;
                }
            }
        }

        void Selection(int cs)
        {
            switch (cs)
            {
                case 0:
                    knobs[0].enabled = true;
                    knobs[1].enabled = false;
                    knobs[2].enabled = false;
                    startButton.image.color = new Color(1, 1, 1, 0.75f);
                    levelButton.image.color = new Color(1, 1, 1, 0.50f);
                    controlButton.image.color = new Color(1, 1, 1, 0.50f);
                    break;
                case 1:
                    knobs[0].enabled = false;
                    knobs[1].enabled = true;
                    knobs[2].enabled = false;
                    startButton.image.color = new Color(1, 1, 1, 0.50f);
                    levelButton.image.color = new Color(1, 1, 1, 0.75f);
                    controlButton.image.color = new Color(1, 1, 1, 0.50f);
                    break;
                case 2:
                    knobs[0].enabled = false;
                    knobs[1].enabled = false;
                    knobs[2].enabled = true;
                    startButton.image.color = new Color(1, 1, 1, 0.50f);
                    levelButton.image.color = new Color(1, 1, 1, 0.50f);
                    controlButton.image.color = new Color(1, 1, 1, 0.75f);
                    break;
            }
        }
    }
}