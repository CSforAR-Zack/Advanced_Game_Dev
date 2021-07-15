using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] bool flying = false;

        NavMeshAgent navMeshAgent;
        Health health;

        float flyingSpeed = 0f;
        Vector3 lastPosition;

        private void Start()
        {

            navMeshAgent = this.GetComponent<NavMeshAgent>();
            health = this.GetComponent<Health>();
            lastPosition = this.transform.position;
        }

        void Update()
        {
            
             navMeshAgent.enabled = !health.IsDead() && !flying;
             UpdateAnimator();
            
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            if (!flying)
            {
                GroundTravel(destination, speedFraction);
            }
            else
            {
                Fly(destination, speedFraction);
            }
        }

        private void GroundTravel(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        private void Fly(Vector3 destination, float speedFraction)
        {
            float step = maxSpeed * Time.deltaTime * Mathf.Clamp01(speedFraction);

            Vector3 targetDir = destination - this.transform.position;
            Vector3 newDir = Vector3.RotateTowards(this.transform.forward, targetDir, step, 0.0f);
            this.transform.rotation = Quaternion.LookRotation(newDir);
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, step);            
        }

        private void CaclulateFlyingSpeed()
        {
            flyingSpeed = Vector3.Distance(this.transform.position, lastPosition) / Time.deltaTime;
            lastPosition = this.transform.position;
        }

        public void Cancel()
        {
            if (!flying)
            {
                navMeshAgent.isStopped = true;
            }
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = new Vector3(0f, 0f, 0f);
            if (!flying)
            {
                velocity = navMeshAgent.velocity;
                Vector3 localVelocity = this.transform.InverseTransformDirection(velocity);
                float speed = localVelocity.z;
                this.GetComponent<Animator>().SetFloat("forwardSpeed", speed);
            }
            else
            {
                CaclulateFlyingSpeed();
                this.GetComponent<Animator>().SetFloat("forwardSpeed", flyingSpeed);
            }            
        }        
    }
}