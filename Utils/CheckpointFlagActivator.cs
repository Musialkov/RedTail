using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class CheckpointFlagActivator : MonoBehaviour
    {
        public delegate void TriggerEnterDelegate(Collider other);
        public TriggerEnterDelegate OnTriggerEnterDelegate;

        private void OnTriggerEnter(Collider other)
        {
            if (OnTriggerEnterDelegate != null && other.CompareTag("Player"))
            {
                OnTriggerEnterDelegate(other);
            }
        }
    }
}