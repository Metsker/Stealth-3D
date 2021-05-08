using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIButtons : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        public void NextLevel()
        {
            switch (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex+1)
            {
                case true:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                    break;
                default:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
                    break;
            }
        }
    }
}
