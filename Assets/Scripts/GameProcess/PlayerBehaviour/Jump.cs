using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameProcess
{
    [RequireComponent(typeof(PhisicsBodyBehaviour))]
    public class Jump : MonoBehaviour
    {

        [SerializeField] private float JumpVelocity = 1;

        bool CanJump;
        private PhisicsBodyBehaviour body;
        //private Vector3 

        void Start()
        {
            body = GetComponent<PhisicsBodyBehaviour>();
            CanJump = true;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space) && body.IsGrounded)
            {
                if (CanJump)
                {
                    StartCoroutine(jump());
                }
            }
        }

        IEnumerator jump()
        {
            body.AddImpuls(UnityEngine.Vector3.up * JumpVelocity);
            CanJump = false;
            yield return new WaitForSeconds(0.1f);
            CanJump = true;
        }
    }
}