using cakeslice;
using System.Collections.Generic;
using UnityEngine;

namespace NightAtTheRijksmuseum
{
    public class Enemy : Character
    {
        [SerializeField]
        private new MeshRenderer renderer; //To get the right colour for now. Might change later.

        [SerializeField]
        private Painting painting; //The painting this enemy belongs to.

        private int currentPatrolPointIndex; //The index of the point it's now moving towards.

        private Transform currentPatrolPoint; //The location of the patrol point it's heading for.

        [SerializeField]
        private List<Transform> patrolPoints; //The default patrol route of this enemy.

        [SerializeField]
        private float patrolPointDistance; //The distance from the patrol point the AI will allow.

        [SerializeField]
        protected Outline outline;

        [SerializeField]
        private Game game;

        private Transform movementTarget;

        public Transform CurrentPatrolPoint
        {
            get
            {
                return currentPatrolPoint;
            }

            private set
            {
                currentPatrolPoint = value;
                movementTarget = currentPatrolPoint;
            }
        }

        private void ProcessMovementDirection()
        {
            Vector3 movementTargetPosition = new Vector3(movementTarget.position.x, transform.position.y, movementTarget.position.z);
            transform.LookAt(movementTargetPosition);
        }

        public Character TargetEnemy
        {
            get
            {
                return targetEnemy;
            }
            set
            {
                targetEnemy = value;
                if(targetEnemy != null)
                {
                    movementTarget = targetEnemy.transform;
                }
            }
        }

        public bool EnemyWithinReach
        {
            get
            {
                return enemyWithinReach;
            }

            set
            {
                enemyWithinReach = value;
            }
        }

        public Painting Painting
        {
            get
            {
                return painting;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            renderer.material.color = Painting.OutlineColor;
            targetEnemy = null;
            currentPatrolPointIndex = 0;
            CurrentPatrolPoint = patrolPoints[currentPatrolPointIndex];
            currentDelay = 0;
            outline.enabled = false;
        }

        private void Update()
        {
            currentDelay += Time.deltaTime;

            if (targetEnemy)
            {
                if(enemyWithinReach)
                {
                    if(currentDelay >= attackDelay)
                    {
                        Attack();
                    }
                }
                else
                {
                    ProcessMovementDirection();
                    Move();
                }
            }
            else
            {
                //patrol the designated route
                if (Mathf.Abs(transform.position.x - CurrentPatrolPoint.position.x) > patrolPointDistance || Mathf.Abs(transform.position.z - CurrentPatrolPoint.position.z) > patrolPointDistance)
                {
                    ProcessMovementDirection();
                    Move();
                }
                else
                {
                    ChooseNextPatrolPoint();
                    ProcessMovementDirection();
                    Move();
                }
            }
        }

        private void ChooseNextPatrolPoint()
        {
            if (currentPatrolPointIndex < patrolPoints.Count - 1)
            {
                currentPatrolPointIndex++;
            }
            else
            {
                currentPatrolPointIndex = 0;
            }
            CurrentPatrolPoint = patrolPoints[currentPatrolPointIndex];
        }



        private void Move()
        {
            Vector3 movement = transform.forward * movementSpeed;
            movement.y = 0;
            controller.Move(movement * Time.deltaTime);
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                TargetEnemy = null;
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                TargetEnemy = other.GetComponent<Player>();
            }
        }

        public void OnTargeted()
        {
            outline.enabled = true;
        }

        public void OnLostTargeting()
        {
            outline.enabled = false;
        }

        public void Die()
        {
            game.OnEmemyDeath(this);
            Destroy(gameObject);
        }
    }
}