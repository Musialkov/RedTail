using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Utils
{
    public class SharpTrap : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private UnityEvent onPlayerEnter;
        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                onPlayerEnter.Invoke();
                other.GetComponent<StatsComponent>().TakeDamage(damage);
            }
        }
    }
}
