using Managers;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Transform[] points;
        private int _destPoint;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            PlayerInteraction.GameOver += OnGameOver;
            FieldOfView.PlayerDetected += OnGameOver;
            PlayerInteraction.Finish += OnFinish;
        }

        private void OnDisable()
        {
            PlayerInteraction.GameOver -= OnGameOver;
            FieldOfView.PlayerDetected -= OnGameOver;
            PlayerInteraction.Finish -= OnFinish;
        }

        private void Start ()
        {
            _destPoint = 0;
            _agent.autoBraking = false;
        }
    
        private void Update ()
        {
            //Поиск пути для стража,
            //Когда текущая точка почти достигнута, назначается новая
            
            if (_agent.enabled && !_agent.pathPending && _agent.remainingDistance < 0.5f && GameManager.StartGame)
            {
                GotoNextPoint();
            }
        }
        private void GotoNextPoint() 
        {
            if (points.Length == 0) return;
        
            _agent.SetDestination(points[_destPoint].position);
            _destPoint = (_destPoint + 1) % points.Length;
        }

        private void OnGameOver()
        {
            _agent.enabled = false;
            //При обнаружении игрока, страж направляется на него
            
            transform.LookAt(FindObjectOfType<PlayerInteraction>().transform);
        }
        private void OnFinish()
        {
            _agent.enabled = false;
        }
    }
}
