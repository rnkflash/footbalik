using UnityEngine;

namespace _Game.Scripts.utils
{
    public static class TrajectoryCalculator
    {
        public static Vector3 CalculateVelocity(Vector3 projectile, Vector3 target, float yMax, float g = 9.8f)
        {
            var y0 = projectile.y - target.y;
            var x = target.x - projectile.x;
            var z = target.z - projectile.z;
            
            Vector3 displacementXZ = new Vector3(x, 0, z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * g * (yMax - y0));
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(2 * (yMax - y0) / g) + Mathf.Sqrt(2 * yMax / g));

            Vector3 velocity = velocityXZ + velocityY;
            return velocity;
        }
    }
}