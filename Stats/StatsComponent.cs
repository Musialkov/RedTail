using System.Collections;
using System.Collections.Generic;
using FoxRevenge.States;
using FoxRevenge.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Stats
{
    public class StatsComponent : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField] protected float baseHealth = 100;
        [Range(1, 100)]
        [SerializeField] protected float baseDamage = 100;
        [SerializeField] protected AudioSource damageAudioSource;
        [SerializeField] protected SoundsRandomizer damageSounds;
        
        [SerializeField] protected UnityEvent onDamage;
        [SerializeField] protected UnityEvent onDie;

        protected StateComponent stateComponent;
        protected Dictionary<Stat, float> currentStats = new Dictionary<Stat, float>();

        protected void Awake() 
        {
            currentStats.Add(Stat.Health, baseHealth);
            currentStats.Add(Stat.Damage, baseDamage);

            stateComponent = GetComponent<StateComponent>();
        }

        public float GetBaseStat(Stat stat)
        {
            if(stat == Stat.Health) return baseHealth;
            else return baseDamage;
        }
        public float GetStat(Stat stat)
        {
            return currentStats[stat];
        }

        public void TakeDamage(float damage)
        {
            if(currentStats[Stat.Health] <= 0) return;
            currentStats[Stat.Health] -= damage;

            if(currentStats[Stat.Health] > 0 )
            {
                if(damageSounds != null)
                {
                    var audioInfo = damageSounds.GetRandomAucioCLip();
                    damageAudioSource.clip = audioInfo.clip;
                    damageAudioSource.pitch = audioInfo.pitch;
                    damageAudioSource.volume = audioInfo.volume;
                }

                onDamage.Invoke();
            } 
            else 
            {
                onDie.Invoke();
                stateComponent.SetNewState(State.Dead);
            }
        }

        public void AddHealth(float healthToAdd)
        {
            if(stateComponent.GetCurrentState() != State.Dead)
            {
                currentStats[Stat.Health] = Mathf.Min(currentStats[Stat.Health] + healthToAdd, baseHealth);
            } 
        }
    }
}