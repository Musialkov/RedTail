using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class CheckpointFlag : MonoBehaviour
    {
        [SerializeField] private CheckpointFlagActivator activator;
        [SerializeField] private MeshRenderer flagMesh;
        [SerializeField] private Material activatedFlagMaterial;
        [SerializeField] private Transform checkpointTransform;

        private bool isActive = false;

        public delegate void ActivateDelegate(Transform newSpawnTransform);
        public static ActivateDelegate OnActivateDelegate;
        
        private void Start()
        {
            activator.OnTriggerEnterDelegate += OnFlagActivatorTrigger;
        }
        
        private void OnFlagActivatorTrigger(Collider other)
        {
            if(isActive == true) return;
            Material[] materials = flagMesh.materials;
            materials[1] = activatedFlagMaterial;
            flagMesh.materials = materials;

            OnActivateDelegate?.Invoke(checkpointTransform);
            isActive = true;
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(checkpointTransform.position, 0.5f);
        }
    }
}