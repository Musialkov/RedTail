using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class WaypointPath : MonoBehaviour
    {
        [SerializeField] private float radiusOfGizmo = 1f;

        public Transform GetWaypoint(int waypointIndex)
        {
            return transform.GetChild(waypointIndex) ? transform.GetChild(waypointIndex) : transform.GetChild(0);
        }

        public int GetNextWaypointIndex(int currentWaypointIndex)
        {
            int nextWaypointIndex = currentWaypointIndex + 1;
            if(nextWaypointIndex == transform.childCount) nextWaypointIndex = 0;

            return nextWaypointIndex;
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextWaypointIndex(i);
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(GetWaypoint(i).position, radiusOfGizmo);
                Gizmos.DrawLine(GetWaypoint(i).position, GetWaypoint(j).position);
            }
        }
    }

}