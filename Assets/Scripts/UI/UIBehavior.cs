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
        public Image[] knobs;

        LevelManager _lvlManager;
        int _currentSelection;

        private void Start()
        {
            _lvlManager = GetComponent<LevelManager>();
            _currentSelection = 0;
            
            foreach (var knob in knobs)
            {
                knob.enabled = false;
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
            if (Input.GetKeyDown(KeyCode.DownArrow) && _currentSelection++ != 2)
            {
                Selection(_currentSelection);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && _currentSelection-- != 0)
            {
                Selection(_currentSelection);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                switch (_currentSelection)
                {
                    case 0:
                        _lvlManager.LoadLevel(1);
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