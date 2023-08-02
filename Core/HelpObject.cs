using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Core
{
    public class HelpObject : MonoBehaviour
    {
        [SerializeField] EHelpType helpType;

        public EHelpType GetHelpType() { return helpType; }
        public void SetHelpType(EHelpType helpType) { this.helpType = helpType; }
    }
}
