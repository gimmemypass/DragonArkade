using System.Collections.Generic;
using System.Linq;
using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Helpers
{
    public static class ItemsHelper
    {      
        public static Entity GetAimedItem(IEnumerable<Entity> items, float targetAngle, float angle)
        {
             var minAngle = float.MaxValue;
             var minAngleItem = default(Entity);
             foreach (var item in items)
             {
                 var angleDiff = Mathf.Abs(NormalizeAngle(item.GetComponent<UnityTransformComponent>().Transform.rotation
                     .eulerAngles.y) - targetAngle);
                 if(angleDiff > angle)
                     continue;
                 if (angleDiff < minAngle)
                 {
                     minAngle = angleDiff;
                     minAngleItem = item;
                 }
             }
             return minAngleItem;
        }

        private static float NormalizeAngle(float angle) //-180 to 180
        {
            //short way to get normalized between 0 and 360 degree
            angle = Quaternion.Euler(0, angle, 0).eulerAngles.y;

            if (angle > 180) angle -= 360;
            return angle;

        }
    }
}