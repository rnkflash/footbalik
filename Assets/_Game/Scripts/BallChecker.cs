using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class BallChecker: MonoBehaviour
    {
        public float ballCheckRadius = 0.04f;
        public LayerMask ballMask;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.75F);
            Gizmos.DrawSphere(transform.position, ballCheckRadius);
        }

        public Ball GetBall()
        {
            RaycastHit[] results = new RaycastHit[1];
            var size = Physics.SphereCastNonAlloc(transform.position, ballCheckRadius, Vector3.down, results, 0.0f, ballMask);
            if (size > 0)
            {
                var ball = results[0].rigidbody.GetComponent<Ball>();
                if (ball != null)
                {
                    return ball;
                }
            }
            return null;
        }
    }
}