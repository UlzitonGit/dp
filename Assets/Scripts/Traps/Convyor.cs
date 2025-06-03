using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convyor : MonoBehaviour
{
        public enum BeltForceMode
        {
            Push,
            Pull
        }

        public enum RelativeDirection
        {
            Up,
            Down,
            Left,
            Right,
            Forward,
            Backward
        }

       
        [SerializeField]
        [Tooltip("Velocity of the conveyer belt.")]
        private float velocity;

        [SerializeField]
        [Tooltip("Local direction does this push objects.")]
        private RelativeDirection direction = RelativeDirection.Down;

        
        // [SerializeField]
        // [Tooltip("Belt force mode used to push physics objects in the scene.")]
        private BeltForceMode beltMode = BeltForceMode.Push;

       
        private Rigidbody body;

      
        private Vector3 pos;

        
        public void Awake()
        {
            this.body = GetComponent<Rigidbody>();
            pos = transform.position;
        }

        
        public void FixedUpdate()
        {
            if (body != null && beltMode == BeltForceMode.Pull)
            {
                Vector3 movement = velocity * GetDirection() * Time.fixedDeltaTime;
                transform.position = pos - movement;
                body.MovePosition(pos);
            }
        }

        
        /// <param name="other">Collision event between the objects.</param>
        public void OnCollisionStay(Collision other)
        {
            if (other.rigidbody != null && !other.rigidbody.isKinematic && beltMode == BeltForceMode.Push)
            {
                Vector3 movement = velocity * GetDirection() * Time.deltaTime;
                other.rigidbody.MovePosition(other.transform.position + movement);
            }
        }

        
        public Vector3 GetDirection()
        {
            switch (this.direction)
            {
                case RelativeDirection.Up:
                    return transform.up;
                case RelativeDirection.Down:
                    return -transform.up;
                case RelativeDirection.Left:
                    return -transform.right;
                case RelativeDirection.Right:
                    return transform.right;
                case RelativeDirection.Forward:
                    return transform.forward;
                case RelativeDirection.Backward:
                    return -transform.forward;
            }
            return transform.forward;
        }
    }
