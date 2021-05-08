using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AdditiveManager : MonoBehaviour
    {
        public static int CurrentLevel = 1;
    
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    
        private void Start()
        {
            //При запуске игры загружаю индекс сохраненного уровня и соответствующую сцену,
            //Если ее индекс не больше общего количества сцен
            
            LoadProgress();

            switch (SceneManager.sceneCountInBuildSettings > CurrentLevel)
            {
                case true:
                    SceneManager.LoadScene(CurrentLevel, LoadSceneMode.Single);
                    break;
                default:
                    SceneManager.LoadScene(CurrentLevel-1, LoadSceneMode.Single);
                    break;
            }
        }

        private void LoadProgress()
        {
            SavedData savedData = SaveProgress.LoadPlayer();
            CurrentLevel = savedData.CurrentLevel;
        }
    }
}
