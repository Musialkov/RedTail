using System.Collections;
using System.Collections.Generic;
using FoxRevenge.States;
using UnityEngine;

namespace FoxRevenge.AnimationBehaviour
{
    public class InAirAnimationBehaviour : StateMachineBehaviour
    {
        private StateComponent stateManager;

        private void Awake()
        {
            stateManager = GameObject.FindWithTag("Player").GetComponent<StateComponent>();
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateManager.SetNewState(State.Jumping);
        }
    }
}   