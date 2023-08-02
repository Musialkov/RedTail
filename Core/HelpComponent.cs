using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Core
{
    public class HelpComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onColumnHelp;
        [SerializeField] private UnityEvent onDestroyObjectHelp;
        [SerializeField] private UnityEvent onLeverHelp;

        [SerializeField] private UnityEvent onCancelColumnHelp;
        [SerializeField] private UnityEvent onCancelDestroyObjectHelp;
        [SerializeField] private UnityEvent onCancelLeverHelp;

        private void OnTriggerEnter(Collider other) 
        {
            HelpObject helpObject = other.GetComponent<HelpObject>();
            if(helpObject)
            {
                PerformHelp(helpObject.GetHelpType());
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            HelpObject helpObject = other.GetComponent<HelpObject>();
            if(helpObject)
            {
                CancelHelp(helpObject.GetHelpType());
            }
        }

        private void PerformHelp(EHelpType helpType)
        {
            switch (helpType)
            {
                case EHelpType.Column:
                    onColumnHelp.Invoke();
                    break;
                case EHelpType.Lever:
                    onLeverHelp.Invoke();
                    break;
                case EHelpType.DestroyObject:
                    onDestroyObjectHelp.Invoke();
                    break;
            }
        }

        public void CancelHelp(EHelpType helpType)
        {
            switch (helpType)
            {
                case EHelpType.Column:
                    onCancelColumnHelp.Invoke();
                    break;
                case EHelpType.Lever:
                    onCancelLeverHelp.Invoke();
                    break;
                case EHelpType.DestroyObject:
                    onCancelDestroyObjectHelp.Invoke();
                    break;
            }
        }
    }
}
