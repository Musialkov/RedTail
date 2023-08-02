using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Core;
using FoxRevenge.States;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class DestroyableItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ParticleSystem particlesOnDestroy;
        [SerializeField] private AudioSource soundOnDestroy;
        [SerializeField] private Collider colliderComponent;
        [SerializeField] private GameObject objectToSpawn;

        private MeshRenderer[] meshRenderers;

        private void Awake() 
        {
            if(!colliderComponent) colliderComponent = GetComponent<Collider>();
            meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }

        public void Interact(GameObject trigger)
        {
            colliderComponent.enabled = false;
            foreach(MeshRenderer mesh in meshRenderers) {mesh.enabled = false;}
            if(particlesOnDestroy) particlesOnDestroy.Play();
            if(soundOnDestroy) soundOnDestroy.Play();
            if(objectToSpawn) Instantiate(objectToSpawn, transform.position, Quaternion.Euler(0, 0, 0));

            trigger.GetComponentInChildren<HelpComponent>().CancelHelp(EHelpType.DestroyObject);

            Destroy(gameObject, 3);
        }
    }
}