using System;
using UnityEngine;

namespace Utils
{
    public static class UnityUtils
    {
        public static AnimationEvent AddAnimationEvent(this AnimationClip animationClip, float time, string callback)
        {
            if (time < 0 || time > animationClip.length) throw new ArgumentOutOfRangeException(nameof(time),"Wrong time argument!");

            AnimationEvent @event = new AnimationEvent()
            {
                time = time,
                functionName = callback
            };
        
            animationClip.AddEvent(@event);
            return @event;
        }
    
        public static void RotateTransformInDirection(this Transform transformToRotate, Vector3 direction, float maxDegreesDelta)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction, transformToRotate.up);
            Quaternion angle = Quaternion.RotateTowards(transformToRotate.rotation, targetAngle, maxDegreesDelta);
            transformToRotate.rotation = angle;
        }
    }
}