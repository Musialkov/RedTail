using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class BallOfDeath : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float radius = 5f;
        [SerializeField] private bool isMovingForward = true;
        [SerializeField] private float damage = 10f;

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
            }
        }

        private void MoveBall()
        {
            float direction = isMovingForward ? 1f : -1f;
            angle += Time.deltaTime * speed * direction;
            rotationAngle += Time.deltaTime * rotationSpeed * direction * -1;

            float z = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            transform.position = new Vector3(0f, y, z) + startPosition;
            transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);

            if ((isMovingForward && angle >= Mathf.PI + Mathf.PI / 2) || (!isMovingForward && angle <= Mathf.PI / 2))
            {
                isMovingForward = !isMovingForward;
            }
        }

    }
}