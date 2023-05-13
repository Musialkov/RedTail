using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Utils
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] float XAmplitude;
        [SerializeField] float YAmplitude;
        [SerializeField] float ZAmplitude;
        [SerializeField] float frequency;

        private Vector3 initPosition;

        private void Awake() 
        {
            initPosition = transform.position;
        }

        private void Update() 
        {
            transform.position = new Vector3(Mathf.Sin(Time.time * frequency) * XAmplitude + initPosition.x,
                                             Mathf.Sin(Time.time * frequency) * YAmplitude + initPosition.y,
                                             Mathf.Sin(Time.time * frequency) * ZAmplitude + initPosition.z);
        }
    }

}