using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace gameProcess
{
    [RequireComponent(typeof(PhisicsBodyBehaviour))]
    public class Move : MonoBehaviour
    {
        PhisicsBodyBehaviour body;
        public float speed;
        public float SpeedMultiplier = 1;

        Vector3 movement;

        void Start()
        {
            body = GetComponent<PhisicsBodyBehaviour>();
        }

        void FixedUpdate()
        {
            float DeltaX = Input.GetAxis("Horizontal") * speed;
            float DeltaZ = Input.GetAxis("Vertical") * speed;

            movement = new Vector3(DeltaX, 0, DeltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement = transform.TransformDirection(movement);

            body.Move(movement * Time.fixedDeltaTime * SpeedMultiplier);
        }

        float GetLengthVectorXZ(Vector3 vect) => Mathf.Pow(vect.x * vect.x + vect.z + vect.z, 0.5f);

    }
}
