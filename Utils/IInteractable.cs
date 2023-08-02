using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public interface IInteractable
    {
        void Interact(GameObject trigger);
    }
}