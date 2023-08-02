using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using FoxRevenge.States;
using FoxRevenge.Utils;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using FoxRevenge.Stats;
using Cinemachine;
using FoxRevenge.Saving;

namespace FoxRevenge.Core 
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private StateComponent playerState;
        [SerializeField] private StatsComponent playerStats;
        [SerializeField] CinemachineFreeLook freeLookCinemachine;

        [Header("Movement")]
        [SerializeField] private float movementSpeed = 8;
        [SerializeField] private float slidingSpeed = 5f;
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private float changeAnimationBlendSpeedRatio = 0.1f;
        [SerializeField] private Transform cameraTransform;

        [Header("Jumping")]
        [SerializeField] private float jumpForce = 10;
        [SerializeField] private float jumpDoubleForce = 10;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private LayerMask notJumpableLayerMask;

        [Header("Dodge")]
        [SerializeField] private float dodgeRadius = 1f;

        [Header("Attack")]
        [SerializeField] private Transform attackCenter;
        [SerializeField] private Transform attackTailCenter;
        [SerializeField] private float attackRadius = 2f;
        [SerializeField] private float attackTailRadius = 3f;
  
        [Header("Events")]
        [SerializeField] private UnityEvent onPause;
        [SerializeField] private UnityEvent onUnPause;
        [SerializeField] private UnityEvent onJump;
        [SerializeField] private UnityEvent onAttack;
        [SerializeField] private UnityEvent onDodge;
        
        private Vector2 inputVector;
        private Vector3 moveDirection;
        private float verticalVelocity;
        private float rotationSmoothVelocity;
        private int jumpCount = 0;
        private bool isGamePaused = false;

        //Sliding 
        private Vector3 hitPointNormal;
        private bool IsSliding
        {
            get
            {

                if(characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 5f))
                {
                    hitPointNormal = slopeHit.normal;
                    return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsOnNotJumpableGround
        {
            get
            {
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f, notJumpableLayerMask)) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //On Ground
        public delegate void OnPlayerOnGround();
        public static OnPlayerOnGround onPlayerOnGround;

        private void Awake() 
        {
            if(!animator) animator = GetComponent<Animator>();
            if(!characterController) characterController = GetComponent<CharacterController>();
            if(!cameraTransform) cameraTransform = GameObject.FindWithTag("MainCamera").transform;
            if(!playerState) playerState = GetComponent<StateComponent>();
            if(!playerStats) playerStats = GetComponent<PlayerStatsComponent>();

            SetCursorVisibility(false);
            onPlayerOnGround += ResetJumping;
            Time.timeScale = 1f;

            float sensitivity = SavingSystem.ReadFloat(SavingKeys.MOUSE_SENSITIVITY);
            if(freeLookCinemachine)
            {
                freeLookCinemachine.m_XAxis.m_MaxSpeed = sensitivity;
                freeLookCinemachine.m_YAxis.m_MaxSpeed = sensitivity/100;
            }
        }
        private void Update()
        {
            CalculateVerticalMovement();
            CalculateMoveDirection();
            MovePlayer();
        }

        private void CalculateVerticalMovement()
        {
            if (characterController.isGrounded)
            {
                animator.SetBool("Grounded", true);
            }
            else
            {
                verticalVelocity -= gravity * -2 * Time.deltaTime;
                animator.SetBool("Grounded", false);
            }
        }
        
        private void CalculateMoveDirection()
        {
            Vector3 inputDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            if (inputDirection.magnitude >= 0.1 && playerState.IsCurrentStateEqualTo(new State[]{State.Jumping, State.Grounded}))
            {
                float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                    cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity,
                    rotationSpeed);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            }
            else if(playerState.IsCurrentStateEqualTo(State.Dodging))
            {
                moveDirection.Set(transform.forward.x, 0, transform.forward.z);
            }
            else
            {
                moveDirection.Set(0, 0, 0);
            }

            if(IsSliding)
            {
                moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slidingSpeed;
            }
        }

        private void MovePlayer()
        {
            characterController.Move(moveDirection.normalized * (movementSpeed * Time.deltaTime) +
                                     new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

            Vector3 velocity = characterController.velocity;
            float currentHorizontalSpeed = new Vector3(velocity.x, 0.0f, velocity.z).magnitude;
            animator.SetFloat("Speed", currentHorizontalSpeed, changeAnimationBlendSpeedRatio, Time.deltaTime);
        }


        public void Move(InputAction.CallbackContext context)
        {
            inputVector = context.ReadValue<Vector2>();
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed && (CanPerformJump() || CanPerformDoubleJump()))
            {
                jumpCount++;
                playerState.SetNewState(State.Jumping);
                animator.SetTrigger("Jump");
                onJump.Invoke();
            }
        }
        
        public void Dodge(InputAction.CallbackContext context)
        {
            if (context.performed && CanPerformDodge())
            {
                playerState.SetNewState(State.Dodging);
                animator.SetTrigger("Dodge");
                onDodge.Invoke();
            }
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (context.performed && CanPerformAttack())
            {
                playerState.SetNewState(State.Attacking);
                animator.SetTrigger("Attack");
            }
        }

        public void Pause(InputAction.CallbackContext context)
        {
            if (context.performed && CanPerformPause())
            {
                ManagePause();
            }
        }

        public void ManagePause()
        {
            if(isGamePaused)
                {
                    isGamePaused = false;
                    onUnPause.Invoke();
                    Time.timeScale = 1f;
                    SetCursorVisibility(false);
                }
                else
                {
                    isGamePaused = true;
                    onPause.Invoke();
                    Time.timeScale = 0f;
                    SetCursorVisibility(true);
                }
        }

        private bool CanPerformJump()
        {
            if(jumpCount != 0) return false;
            if(IsSliding) return false;
            if(playerState.GetCurrentState() != State.Grounded) return false;
            if (!characterController.isGrounded) return false;
            if(isGamePaused) return false;
            if(IsOnNotJumpableGround) return false;

            return true;
        }

        private bool CanPerformDoubleJump()
        {
            if(jumpCount != 1) return false;
            if(IsSliding) return false;
            if(playerState.GetCurrentState() != State.Jumping) return false;
            if(isGamePaused) return false;
            if(SceneManager.GetActiveScene().buildIndex < 2) return false;

            return true;
        }

        private bool CanPerformDodge()
        {
            if (playerState.GetCurrentState() != State.Grounded) return false;
            if (!characterController.isGrounded) return false;
            if(isGamePaused) return false;
            if(SceneManager.GetActiveScene().buildIndex < 3) return false;

            return true;
        }

        private bool CanPerformAttack()
        {
            if(isGamePaused) return false;
            if(SceneManager.GetActiveScene().buildIndex < 4) return false;
            if (playerState.IsCurrentStateEqualTo(new State[]{State.Attacking, State.Grounded})) return true;
                 
            return false;
        }

        private bool CanPerformPause()
        {
            if(playerState.GetCurrentState() != State.Grounded) return false;
            if(playerState.GetCurrentState() == State.Dead) return false; 

            return true;
        }

        private void ResetJumping()
        {
            jumpCount = 0;
            if(animator != null) animator.ResetTrigger("Jump");
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(attackCenter.position, attackRadius);
            Gizmos.DrawWireSphere(attackTailCenter.position, attackTailRadius);
        }

        //animation events
        public void AddJumpForce()
        {
            if(jumpCount == 1) verticalVelocity = jumpForce;
            else if(jumpCount == 2) verticalVelocity = jumpDoubleForce;
        }

        public void LookForDestroyableAndDestroyThem()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, dodgeRadius);

            foreach(Collider collider in hitColliders)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if(interactable != null) interactable.Interact(this.gameObject);
            }
        }

        public void AttackNormal()
        {
            Collider[] hitColliders = Physics.OverlapSphere(attackCenter.position, attackRadius);
            PerformAttackOnProperObjects(hitColliders);
        }

        public void AttackTail()
        {
            Collider[] hitColliders = Physics.OverlapSphere(attackTailCenter.position, attackTailRadius);
            PerformAttackOnProperObjects(hitColliders);
            
        }

        public void InvokeAttackEvent()
        {
            onAttack.Invoke();
        }

        private void PerformAttackOnProperObjects(Collider[] hitColliders)
        {
            foreach(Collider collider in hitColliders)
            {
                if(collider.CompareTag("Player")) continue;

                IInteractable interactable = collider.GetComponent<IInteractable>();
                if(interactable != null) interactable.Interact(this.gameObject);

                StatsComponent stats = collider.GetComponent<StatsComponent>();
                if(stats != null) stats.TakeDamage(playerStats.GetStat(Stat.Damage));
            }
        }

        public void SetCursorVisibility(bool visible)
        {
            Cursor.visible = visible;
            if(!visible) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }

        public void StopTime()
        {
            Time.timeScale = 0f;
        }

        public void PerformDeath()
        {
            animator.SetBool("Dead", true);
            freeLookCinemachine.GetComponent<CinemachineInputProvider>().enabled = false;
        }
    }
}