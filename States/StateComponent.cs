using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.States
{
    public class StateComponent : MonoBehaviour
    {
        private State currentState = State.Grounded;

        public void SetNewState(State newState)
        {
            if(currentState == State.Dead) return;
            currentState = newState;
        }

        public State GetCurrentState()
        {
            return currentState;
        }

        public bool IsCurrentStateEqualTo(State state)
        {
            return currentState == state;
        }

        public bool IsCurrentStateEqualTo(State[] states)
        {
           foreach (State state in states)
           {
                if(state == currentState) return true;
           }

           return false;
        }
    }

}