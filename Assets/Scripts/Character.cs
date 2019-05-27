using UnityEngine;


namespace NightAtTheRijksmuseum
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected CharacterController controller;

        [SerializeField]
        protected float movementSpeed;

        protected Character targetEnemy; //The position of the player

        protected bool enemyWithinReach; //The enemy can be fought

        [SerializeField]
        protected float attackThrowDistance;

        [SerializeField]
        protected float attackDelay;

        protected float currentDelay;

        protected void Attack()
        {
            targetEnemy.IsAttacked(transform.forward);
            currentDelay = 0;
        }

        public virtual void IsAttacked(Vector3 direction)
        {
            controller.Move(direction * attackThrowDistance);
        }

        protected abstract void OnTriggerEnter(Collider other);

        protected abstract void OnTriggerExit(Collider other);
    }
}
