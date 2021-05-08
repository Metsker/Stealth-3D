using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        private Camera _camera;
        private const float MoveSpeed = 3;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!GameManager.GameOver && GameManager.StartGame)
            {
                TouchControl();
#if UNITY_EDITOR
                EditorControl();
#endif
            }
        }

        private void TouchControl()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
                RayCasting(ray);
            }
        }
        
        private void EditorControl()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RayCasting(ray);
            }
        }

        private void RayCasting(Ray ray)
        {
            if (Physics.Raycast (ray, out RaycastHit hit)) 
            {
                //Ищу вектор направления движения, нормализирую чтобы получить постоянную скорость
                
                Vector3 direction = (hit.point - transform.position).normalized;
                direction.y = 0;
                transform.Translate (direction * (Time.deltaTime * MoveSpeed), Space.World);
            }
        }
    }
}
