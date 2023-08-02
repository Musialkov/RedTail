using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace FoxRevenge.Utils
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private Stat statToChange;
        [Range(1, 100)]
        [SerializeField] private float value;
        [SerializeField] private UnityEvent onCollect;

        private bool collected = false;
        private void OnTriggerEnter(Collider other) 
        {
            if(!other.CompareTag("Player")) return;
            if(collected) return;
            
            PlayerStatsComponent statsComponent = other.gameObject.GetComponent<PlayerStatsComponent>();
            if(statToChange == Stat.Health) statsComponent.AddHealth(value);
            if(statToChange == Stat.Points) statsComponent.AddPoints(value);
            collected = true;

            GetComponentsInChildren<MeshRenderer>().ToList().ForEach(x => x.enabled = false);

            onCollect.Invoke();

            Destroy(gameObject, 3f);
        }
    }

}