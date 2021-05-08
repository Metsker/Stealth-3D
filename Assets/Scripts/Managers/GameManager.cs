using Enemy;
using Player;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Image gameOverImage;
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private ParticleSystem startEffect;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform startPoint;
    
        public static bool GameOver { get; private set; }
        public static bool StartGame { get; private set; }

        private void Start()
        {
            startEffect.transform.position = startPoint.position;
        }

        private void OnEnable()
        {
            PlayerInteraction.Finish += OnFinish;
            PlayerInteraction.GameOver += SetGameOver;
            FieldOfView.PlayerDetected += SetGameOver;
        }

        private void OnDisable()
        {
            PlayerInteraction.Finish -= OnFinish;
            FieldOfView.PlayerDetected -= SetGameOver;
            PlayerInteraction.GameOver -= SetGameOver;
            GameOver = false;
            StartGame = false;
        }

        private void Update()
        {
            StartCheck();
        }
        private void StartCheck()
        {
            if (Input.touchCount > 0 && !StartGame)
            {
                OnStart();
            }
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !StartGame)
            {
                OnStart();
            }
#endif
        }

        private void SetGameOver()
        {
            GameOver = true;
            gameOverImage.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnStart()
        {
            StartGame = true;
            startText.gameObject.SetActive(false);
        
            //Появление с задержкой в секунду
        
            Invoke(nameof(InvokeDelay), 1);
        }
        private void OnFinish()
        {
            GameOver = true;
            nextLevelButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        
            //Сохранение решил производить во время успешного завершения уровня.
            //Таким образом, если игрок прошел уровень, при загрузке он окажется на следующем уровене
            
            AdditiveManager.CurrentLevel = SceneManager.GetActiveScene().buildIndex+1;
            SaveProgress.SavePlayer();
        }

        private void InvokeDelay()
        {
            startEffect.Play();
            Instantiate(player, startPoint.transform.position, player.transform.rotation);
        }
    }
}
