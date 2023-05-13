using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxRevenge.Utils;

namespace FoxRevenge.Core
{
    public class CheckpointController : MonoBehaviour
    {
        [SerializeField] CharacterController characterController;
        private Transform spawnTransform;

        private void Start() 
        {
            CheckpointFlag.OnActivateDelegate += SetSpawnTransform;
        }

        public void SetSpawnTransform(Transform newSpawnTransform)
        {
            spawnTransform = newSpawnTransform;
        }
        public void RespawnAtLocation()
        {
            if(spawnTransform == null) return;
            characterController.enabled = false;
            transform.position = spawnTransform.position;
            characterController.enabled = true;
        }

    }
}
