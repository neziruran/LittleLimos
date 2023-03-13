using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Camera
{
    public class FocusPointRotationFixer : MonoBehaviour
    {
        Quaternion _rotation;
    
        void Awake()
        {
            _rotation = transform.rotation;
        }
        void LateUpdate()
        {
            if(transform.rotation != _rotation)
            {
                transform.rotation = _rotation;
            }
        }

    }
 
}
