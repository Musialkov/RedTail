using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private Stat statToChange;
        [Range(1, 100)]
        [SerializeField] private float value;
        private void OnTriggerEnter(Collider other) 
        {
            if(!other.CompareTag("Player")) return;
            
            PlayerStatsComponent statsComponent = other.gameObject.GetComponent<PlayerStatsComponent>();
            if(statToChange == Stat.Health) statsComponent.AddHealth(value);
            if(statToChange == Stat.Points) statsComponent.AddPoints(value);

            Destroy(gameObject);
        }
    }

}