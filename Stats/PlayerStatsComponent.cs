using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Stats
{
    public class PlayerStatsComponent : StatsComponent
    {
        private void Awake() 
        {
            base.Awake();

            currentStats.Add(Stat.Points, 0);
        }

        public void AddPoints(float pointsToAdd)
        {
            currentStats[Stat.Points] += pointsToAdd;
        }
    }
}