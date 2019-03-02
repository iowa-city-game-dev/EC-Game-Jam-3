using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public int GetCurrentLevel()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadLevel(int lvl)
        {
            if (lvl <= SceneManager.sceneCountInBuildSettings)
            {
                StartCoroutine(Load(lvl));
            }
            else
            {
                Debug.Log("failed to load level does not exist.");
                StartCoroutine(Load(0));
            }
        }

        IEnumerator Load(int lvl)
        {
            AsyncOperation loadinglevel = SceneManager.LoadSceneAsync(lvl, LoadSceneMode.Single);
            Debug.Log("loading scene");
            yield return loadinglevel;
        }
    }
}