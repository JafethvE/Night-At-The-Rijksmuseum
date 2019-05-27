using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NightAtTheRijksmuseum
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Text victoryText;

        [SerializeField]
        private List<Enemy> enemies;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnEmemyDeath(Enemy enemy)
        {
            enemies.Remove(enemy);
            if(enemies.Count == 0)
            {
                Victory();
            }
        }

        private void Victory()
        {
            victoryText.text = "You Win!";
        }
    }
}