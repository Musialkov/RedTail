using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Utils
{
    public class WaterBox : MonoBehaviour
    {
        [SerializeField] UnityEvent onWaterFall;
        [SerializeField] float damage;
        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PlayerStatsComponent>().TakeDamage(damage);
                if(other.GetComponent<PlayerStatsComponent>().GetStat(Stat.Health) > 0)
                {
                    StartCoroutine("StartEvent");
                }
            }
        }

        IEnumerator StartEvent()
        {
            yield return new WaitForSeconds(0.25f);
            onWaterFall.Invoke();
        }
    }
}
