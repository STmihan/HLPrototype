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
    
        public static void RotateTransformInDirection(this Transform transformToRotate, Vector3 direction, float maxDegreesDelta, Space relativeTo = Space.Self)
        {
            Quaternion targetAngle = Quaternion.identity;
            switch (relativeTo)
            {
                case Space.World:
                    targetAngle = Quaternion.LookRotation(direction, Vector3.up);
                    break;
                case Space.Self:
                    targetAngle = Quaternion.LookRotation(direction, transformToRotate.up);
                    break;
            }
            Quaternion angle = Quaternion.RotateTowards(transformToRotate.rotation, targetAngle, maxDegreesDelta);
            transformToRotate.rotation = angle;
        }
    }
}