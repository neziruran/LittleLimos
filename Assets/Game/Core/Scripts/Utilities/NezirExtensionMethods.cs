using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Core.Utilities
{
    public class NezirExtensionMethods
    {
        public static Quaternion ConvertToQuaternion(Vector3 rotation)
        {
            return Quaternion.Euler(rotation);
        }

        public static LayerMask SetLayerMask(string name)
        {
            return LayerMask.GetMask(name);
        }

        public static void DelayedCall(Action action)
        {
            DOVirtual.DelayedCall(1f, action.Invoke);
        }
        
    }
}
