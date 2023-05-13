using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class MovingPlatfrom : MonoBehaviour
    {
        [SerializeField] private WaypointPath waypointPath;
        [SerializeField] private float movingSpeed;

        private int targetWaypointIndex;

        private Transform previousWaypoint;
        private Transform targetWaypoint;

        private float timeToWaypoint;
        private float elapsedTime;

        void Start()
        {
            TargetNextWaypoint();
        }

        void FixedUpdate()
        {
            elapsedTime += Time.deltaTime;

            float elapsedPercentage = elapsedTime / timeToWaypoint;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
            if(elapsedPercentage >= 1) TargetNextWaypoint();
        }

        private void TargetNextWaypoint()
        {
            previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
            targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
            targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

            elapsedTime = 0;

            float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
            timeToWaypoint = distanceToWaypoint / movingSpeed;
        }

        private void OnTriggerEnter(Collider other) 
        {
            other.transform.SetParent(transform);
        }

        private void OnTriggerExit(Collider other) 
        {
            other.transform.SetParent(null);
        }
    }
}
