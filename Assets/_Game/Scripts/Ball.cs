using _Game.Scripts.player;
using _Game.Scripts.sounds;
using _Game.Scripts.utils;
using UnityEngine;

namespace _Game.Scripts
{
    public class Ball : MonoBehaviour
    {
        readonly static int AnimSpeedParameter = Animator.StringToHash("Speed");

        [SerializeField] float maxDistance = 5.0f;
        [SerializeField] float maxHeight = 2.0f;
        
        Player master;
        Animator animator;
        Vector3 lastPosition;
        Rigidbody ballRigidBody;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            ballRigidBody = GetComponent<Rigidbody>();
            lastPosition = transform.position;
        }

        void FixedUpdate()
        {
            var delta = transform.position - lastPosition;
            float animSpeed = delta.magnitude > 0 ? 1.0f : 0.0f;
            animator.SetFloat(AnimSpeedParameter, animSpeed);
            
            lastPosition = transform.position;
        }

        public bool IsCaptured()
        {
            return master != null;
        }

        void SetVelocity(Vector3 velocity)
        {
            ballRigidBody.linearVelocity = velocity;
        }
        
        public void Capture(Player player)
        {
            bool capture = player != null;
            if (IsCaptured() == capture) return;
            master = player;
            ballRigidBody.isKinematic = capture;
            ballRigidBody.detectCollisions = !capture;
        }

        public void UnCapture()
        {
            Capture(null);
        }
        public void Kick(Vector3 target, double power)
        {
            var distanceToTarget = Vector3.Distance(transform.position, target);
            var yMax = Mathf.Lerp(0.0f, maxHeight, distanceToTarget / maxDistance);
            Vector3 velocity = TrajectoryCalculator.CalculateVelocity(transform.position, target, yMax);
            SetVelocity(velocity);
            SoundSystem.instance.Play(Sounds.kick);
        }
    }
}
