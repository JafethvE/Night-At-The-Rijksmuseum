using System.Collections;
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

        private bool enemySeen; //Has this enemy seen the player.

        private int currentPatrolPointIndex; //The index of the point it's now moving towards.

        private Transform currentPatrolPoint; //The location of the patrol point it's heading for.

        [SerializeField]
        private List<Transform> patrolPoints; //The default patrol route of this enemy.

        [SerializeField]
        private float patrolPointDistance; //The distance from the patrol point the AI will allow.

        public Transform CurrentPatrolPoint
        {
            get
            {
                return currentPatrolPoint;
            }

            private set
            {
                currentPatrolPoint = value;
                transform.LookAt(CurrentPatrolPoint);
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            renderer.material.color = painting.OutlineColor;
            enemySeen = false;
            currentPatrolPointIndex = 0;
            CurrentPatrolPoint = patrolPoints[0];
        }

        private void Update()
        {
            if (enemySeen)
            {
                //Move towards enemy and attack;
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
            transform.LookAt(CurrentPatrolPoint);
        }



        private void Move()
        {
            Vector3 movement = transform.forward * movementSpeed;
            movement.y = 0;
            controller.Move(movement * Time.deltaTime);
        }
    }
}