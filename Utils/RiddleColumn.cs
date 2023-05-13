using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class RiddleColumn : MonoBehaviour, IInteractable
    {
        [SerializeField] private RiddleGraph riddleGraph;
        [SerializeField] private float timeToWaypoint = 2f;

        private GameObject player;
        private Transform previousNodeTransform;
        private bool isMoving = false;
        
        private float elapsedTime;

        private void Awake() 
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start() 
        {
            transform.position = riddleGraph.CurrentNode.transform.position;
            previousNodeTransform = riddleGraph.CurrentNode.transform;
        }

        private void FixedUpdate() 
        {
            if(transform.position != riddleGraph.CurrentNode.transform.position)
            {
                elapsedTime += Time.deltaTime;

                float elapsedPercentage = elapsedTime / timeToWaypoint;
                elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
                transform.position = Vector3.Lerp(previousNodeTransform.position, riddleGraph.CurrentNode.transform.position, elapsedPercentage);
            }
            else
            {
                isMoving = false;
            }
        }

        public void Interact()
        {
            if(isMoving) return;
            EDirection direction = FindInteractDirection();

            RiddleNode nextNode = riddleGraph.CurrentNode.GetNeighbourOnSpecificDirection(direction);
            if(!nextNode) return;

            previousNodeTransform = riddleGraph.CurrentNode.transform;
            riddleGraph.CurrentNode = nextNode;
            elapsedTime = 0;
            isMoving = true;
        }

        private EDirection FindInteractDirection()
        {
            Vector3 toTarget = transform.position - player.transform.position;
            float angle = Vector3.Angle(toTarget, Vector3.forward);

            Vector3 cross = Vector3.Cross(toTarget, Vector3.forward);
            if (cross.y < 0)
            {
                angle = 360 - angle;
            }

            int index = Mathf.RoundToInt(angle / 90);
            EDirection direction = EDirection.North;
            switch (index)
            {
                case 0:
                    direction = EDirection.North;
                    break;
                case 1:
                    direction = EDirection.West;
                    break;
                case 2:
                    direction = EDirection.South;
                    break;
                case 3:
                    direction = EDirection.East;
                    break;
            }

            return direction;
        }
    }
}