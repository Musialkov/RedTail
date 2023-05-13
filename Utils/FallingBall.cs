using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Utils
{
    public class FallingBall : MonoBehaviour
    {
        [SerializeField] private float respawnDelay = 3f;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private UnityEvent onPlayerHit;
        private Vector3 startingPosition;

        private void Awake() 
        {
            startingPosition = transform.position;
            if(!rb) rb = GetComponent<Rigidbody>();
        }

        public void RespownAtStartPosition()
        {
            StartCoroutine("RespawnAtStartLocation");
        }

        private IEnumerator RespawnAtStartLocation()
        {
            yield return new WaitForSeconds(respawnDelay);
            transform.position = startingPosition;
            rb.velocity = new Vector3(0,0,0);
        }

        private void OnCollisionEnter(Collision other) 
        {
            if(other.gameObject.CompareTag("Player"))
            {
                onPlayerHit.Invoke();
            }
        }
    }
}