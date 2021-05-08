using System;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : PlayerControls
    {
        public static event Action Finish;
        public static event Action GameOver;

        private void OnTriggerEnter(Collider other)
        {
            //Вызываю разные ивенты при входе в разные триггеры
            
            if (other.gameObject.CompareTag("Finish"))
            {
                Finish?.Invoke();
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                GameOver?.Invoke();
            }
        }
    }
}
