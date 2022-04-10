using UnityEngine;

namespace PlayerLoop
{
    public static class PlayerUtils
    {
        public static void RotateInDirection(Transform transformToRotate, Vector3 direction, float maxDegreesDelta)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction, transformToRotate.up);
            Quaternion angle = Quaternion.RotateTowards(transformToRotate.rotation, targetAngle, maxDegreesDelta);
            transformToRotate.rotation = angle;
        }
    }
}