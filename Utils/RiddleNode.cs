using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class RiddleNode : MonoBehaviour
    {
        [SerializeField] private float gizmoRadius = 1;
        [SerializeField] private List<NeighbourNode> neighbourNodes;

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, gizmoRadius);    
        }

        public IEnumerable GetNeighbourNodes()
        {
            return neighbourNodes;
        }

        public int GetNeighbourNodesSize()
        {
            return neighbourNodes.Count;
        }

        public RiddleNode GetNeighbourOnSpecificDirection(EDirection direction)
        {
            return neighbourNodes.Find(x => x.direction == direction).node;
        }

        [System.Serializable]
        public struct NeighbourNode
        {
            public EDirection direction;
            public RiddleNode node;
        }
    }


}