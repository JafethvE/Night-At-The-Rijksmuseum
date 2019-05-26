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

        public Transform CurrentPatrolPoint
        {
            get
            {
                return currentPatrolPoint;
            }

            private set
            {
                currentPatrolPoint = value;
                Vector3 target = new Vector3(currentPatrolPoint.position.x, transform.position.y, currentPatrolPoint.position.z);
                transform.LookAt(target);
                currentDelay = 0;
            }
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
                    Vector3 target = new Vector3(targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z);
                    transform.LookAt(target);
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
            if (targetEnemy)
            {
                if(enemyWithinReach)
                {
                    if(currentDelay >= attackDelay)
                    {
                        Attack();
                    }
                    else
                    {
                        currentDelay += Time.deltaTime;
                    }
                }
                else
                {
                    Move();
                }
            }
            else
            {
                //patrol the designated route
                if (Mathf.Abs(transform.position.x - CurrentPatrolPoint.position.x) > patrolPointDistance || Mathf.Abs(transform.position.z - CurrentPatrolPoint.position.z) > patrolPointDistance)
                {
                    Move();
                }
                else
                {
                    ChooseNextPatrolPoint();
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

        protected override void OnTriggerStay(Collider other)
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
            Destroy(gameObject);
        }
    }
}