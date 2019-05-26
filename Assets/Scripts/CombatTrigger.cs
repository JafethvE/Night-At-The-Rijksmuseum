using UnityEngine;

namespace NightAtTheRijksmuseum
{
    public class CombatTrigger : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                if (other.GetComponent<Player>() == enemy.TargetEnemy)
                {
                    enemy.EnemyWithinReach = true;
                }
                else
                {
                    enemy.EnemyWithinReach = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                if (other.GetComponent<Player>() == enemy.TargetEnemy)
                {
                    enemy.EnemyWithinReach = false;
                }
            }
        }
    }
}