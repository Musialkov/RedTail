using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Stats;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FoxRevenge.UI
{
    public class HUDComponent : MonoBehaviour
    {
        [SerializeField] Image healthFiller;
        [SerializeField] TextMeshProUGUI points;

        private PlayerStatsComponent statsComponent;

        private void Awake() 
        {
            statsComponent = GameObject.FindWithTag("Player").GetComponent<PlayerStatsComponent>();
        }

        private void Update() 
        {
            healthFiller.fillAmount = statsComponent.GetStat(Stat.Health) / statsComponent.GetBaseStat(Stat.Health);
            points.text = statsComponent.GetStat(Stat.Points).ToString();
        }
    }
}