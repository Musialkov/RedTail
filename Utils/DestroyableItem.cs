using System.Collections;
using System.Collections.Generic;
using FoxRevenge.States;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class DestroyableItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleSystem particlesOnDestroy;
        [SerializeField] private AudioSource soundOnDestroy;
        [SerializeField] private Collider colliderComponent;
        private MeshRenderer[] meshRenderers;

        private void Awake() 
        {
            if(!colliderComponent) colliderComponent = GetComponent<Collider>();
            meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }

        public void Interact()
        {
            colliderComponent.enabled = false;
            foreach(MeshRenderer mesh in meshRenderers) {mesh.enabled = false;}
            if(particlesOnDestroy) particlesOnDestroy.Play();
            if(soundOnDestroy) soundOnDestroy.Play();

            Destroy(gameObject, 3);
        }
    }
}