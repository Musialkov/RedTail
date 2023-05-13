using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{ 
    public class FallingBallRespawner : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<FallingBall>())
            {
                other.gameObject.GetComponent<FallingBall>().RespownAtStartPosition();
            }
        }
    }
}