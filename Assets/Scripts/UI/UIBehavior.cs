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
        private int _numberOfOptions = 3;

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

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && _currentSelection != 2)
            {
                _currentSelection++;
                Selection(Mathf.Clamp(_currentSelection, 0, knobs.Length));
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && _currentSelection != 0)
            {
                _currentSelection--;
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

        private void Selection(int cs)
        {
            ResetKnobs();
            knobs[cs].enabled = true;

            switch (cs)
            {
                case 0:
                    startButton.image.color = new Color(1, 1, 1, 0.75f);
                    levelButton.image.color = new Color(1, 1, 1, 0.50f);
                    controlButton.image.color = new Color(1, 1, 1, 0.50f);
                    break;

                case 1:
                    startButton.image.color = new Color(1, 1, 1, 0.50f);
                    levelButton.image.color = new Color(1, 1, 1, 0.50f);
                    controlButton.image.color = new Color(1, 1, 1, 0.75f);
                    break;

                case 2:
                    startButton.image.color = new Color(1, 1, 1, 0.50f);
                    levelButton.image.color = new Color(1, 1, 1, 0.75f);
                    controlButton.image.color = new Color(1, 1, 1, 0.50f);
                    break;
            }
        }

        private void ResetKnobs()
        {
            foreach (var knob in knobs)
            {
                knob.enabled = false;
            }
        }
    }
}