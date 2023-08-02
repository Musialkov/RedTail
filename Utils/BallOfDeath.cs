using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Utils
{
    public class BallOfDeath : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float radius = 5f;
        [SerializeField] private bool isMovingForward = true;
        [SerializeField] private bool shouldRotate = true;
        [SerializeField] private bool reverseRotation = true;
        [SerializeField] private float damage = 10f;
        [SerializeField] private UnityEvent onHit;

        private float angle;
        private float rotationAngle;
        private Vector3 startPosition;
        void Start()
        {
            if(isMovingForward) angle = Mathf.PI/2;
            else angle = Mathf.PI +  Mathf.PI/2;
            startPosition = transform.position;
        }

        void Update()
        {
            MoveBall();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerStatsComponent>().TakeDamage(damage);
                onHit.Invoke();
            }
        }

        private void MoveBall()
        {
            float direction = isMovingForward ? 1f : -1f;
            angle += Time.deltaTime * speed * direction;

            float z = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            transform.position = new Vector3(0f, y, z) + startPosition;
            if(shouldRotate)
            {
                float rotationDirection = reverseRotation ? 1f : -1f;
                rotationAngle += Time.deltaTime * rotationSpeed * direction * rotationDirection;
                transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);
            }

            if ((isMovingForward && angle >= Mathf.PI + Mathf.PI / 2) || (!isMovingForward && angle <= Mathf.PI / 2))
            {
                isMovingForward = !isMovingForward;
            }
        }

    }
}