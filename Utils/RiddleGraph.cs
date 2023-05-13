using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FoxRevenge.Utils.RiddleNode;

namespace FoxRevenge.Utils
{
    public class RiddleGraph : MonoBehaviour
    {
        [SerializeField] private List<RiddleNode> nodes;
        [SerializeField] private int startNodeIndex;  
        private RiddleNode currentNode;
        public RiddleNode CurrentNode { get => currentNode; set => currentNode = value; }

        private void Awake() 
        {
            currentNode = nodes[startNodeIndex];
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.cyan;
            for(int i = 0; i < nodes.Count; i++)
            {
                foreach(NeighbourNode neighbourNode in nodes[i].GetNeighbourNodes())
                {
                    Gizmos.DrawLine(nodes[i].transform.position, neighbourNode.node.transform.position);
                }
            }
        }
    }
}