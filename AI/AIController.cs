using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FoxRevenge.Stats;
using FoxRevenge.States;
using UnityEngine.Events;
using FoxRevenge.Audio;

namespace FoxRevenge.AI
{
    public class AIController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private StatsComponent stats;
        [SerializeField] private StateComponent states;

        [Header("Movement")]
        [SerializeField] private float viewDistance = 10f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private UnityEvent onFootstep;

        [Header("Attack")]
        [SerializeField] private float attackInterval = 2f;
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private UnityEvent onAttack;

        [Header("Sounds")]
        [SerializeField] private SoundsRandomizer attackSounds;
        [SerializeField] private AudioSource attackAudioSource;
        [SerializeField] private SoundsRandomizer footstepsSounds;
        [SerializeField] private AudioSource footstepsAudioSource;

        private NavMeshAgent agent;
        private Animator animator;
        private Vector3 beginingPosition;
        private Transform playerTransform;
        private StateComponent playerState;
        private float timeSinceLastAttack = 0;
        private void Awake() 
        {
            agent = GetComponent<NavMeshAgent>();
            playerTransform = GameObject.FindWithTag("Player").transform;
            playerState = GameObject.FindWithTag("Player").GetComponent<StateComponent>();
            animator = GetComponent<Animator>();

            if(!stats) stats = GetComponent<StatsComponent>();
            if(!states) states = GetComponent<StateComponent>();
        }

        private void Start() 
        {
            beginingPosition = transform.position;
        }

        private void Update()
        {
            if(states.GetCurrentState() == State.Dead) return;

            UpdateMovement();
            UpdateAttack();
            UpdateAnimation();
        }

        private void UpdateMovement()
        {
            if (Vector3.Distance(transform.position, playerTransform.position) < viewDistance && playerState.GetCurrentState() != State.Dead)
            {
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                agent.SetDestination(beginingPosition);
            }
        }

         private void UpdateAttack()
        {
            if (CanAttack())
            {
                animator.SetTrigger("Attack");
                timeSinceLastAttack = 0;
            }
            else
            {
                timeSinceLastAttack += Time.deltaTime;
                animator.ResetTrigger("Attack");
            }
        }

        private bool CanAttack()
        {
            if(Vector3.Distance(transform.position, playerTransform.position) > agent.stoppingDistance) return false;
            if(timeSinceLastAttack < attackInterval) return false;
            if(playerState.GetCurrentState() == State.Dead) return false;
            return true;
        }

        private void RotateToTarget()
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f; 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("Speed", speed);

            if(CanAttack() || timeSinceLastAttack < attackInterval)
            {
                RotateToTarget();
            }
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        public void GetHit()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("GetHit");
        }

        public void PerformDeath()
        {
            animator.SetBool("Dead", true);
        }

        // Animation Event

        private void Attack()
        {
            Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.GetComponent<PlayerStatsComponent>())
                {
                    hitCollider.gameObject.GetComponent<PlayerStatsComponent>().TakeDamage(stats.GetStat(Stat.Damage));
                }
            }
        }

        private void InvokeFootstepEvent()
        {
            PrepareAudioSource(footstepsAudioSource, footstepsSounds);
            onFootstep.Invoke();
        }

        private void InvokeAttackEvent()
        {
            PrepareAudioSource(attackAudioSource, attackSounds);
            onAttack.Invoke();
        }

        private void PrepareAudioSource(AudioSource audioSource, SoundsRandomizer soundsRandomizer)
        {
            if(soundsRandomizer != null)
            {
                var audioInfo = soundsRandomizer.GetRandomAucioCLip();
                audioSource.clip = audioInfo.clip;
                audioSource.pitch = audioInfo.pitch;
                audioSource.volume = audioInfo.volume;
            }
        }
    }
}
