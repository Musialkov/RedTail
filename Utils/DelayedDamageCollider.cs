using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class DelayedDamageCollider : MonoBehaviour
    {
        [Tooltip("Amount of time between take damage")]
        [SerializeField] private float damageInterval = 2f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float damageRadius = 7f;
        [SerializeField] private bool makeDamageInstantly = false;
        private List<GameObject> affectedGameObjects = new List<GameObject>();

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<StatsComponent>())
            {
                affectedGameObjects.Add(other.gameObject);
                if(makeDamageInstantly) InvokeRepeating("TakeDamage", 0, damageInterval);
                else InvokeRepeating("TakeDamage", damageInterval, damageInterval);
            }
            
        }

        private void OnTriggerExit(Collider other) 
        {
            if(affectedGameObjects.Contains(other.gameObject)) affectedGameObjects.Remove(other.gameObject);
            if(affectedGameObjects.Count == 0) CancelInvoke("TakeDamage");
        }

        private void TakeDamage()
        {
            foreach (GameObject gameObject in affectedGameObjects)
            {
                gameObject.GetComponent<StatsComponent>().TakeDamage(damage);
            }
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
    }
}