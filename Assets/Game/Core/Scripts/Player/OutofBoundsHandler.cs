using UnityEngine;

namespace Core.Player
{
    public class OutofBoundsHandler : MonoBehaviour
    {
        #region Variables

        private static Transform _selfTransform;
        private static Transform _targetTransform;
        private static LayerMask _layerMask;
        
        private static bool _upHit = false;
        private static bool _downHit = false;

        #endregion

        #region Built-In Methdods

        void Awake()
        {
            InitOutOfBoundsHandler();
        }
        #endregion
        

        #region CustomMethods

        private void InitOutOfBoundsHandler()
        {
            _selfTransform = transform;
            _layerMask = LayerMask.NameToLayer("Grid");
        }

        public void Init(Transform target)
        {
            _targetTransform = target;
        }
        
        
        public static void OutCheck()
        {
            _selfTransform.position = _targetTransform.position;
            _upHit = OutRay(Vector3.up);
            _downHit = OutRay(Vector3.down);
            if (!_upHit || !_downHit)
            {
                if (PlayerController.IsOutOfBounds())
                {
                    Debug.LogError("Out Of Bounds");
                    PlayerController.InvokeOnOutOfBounds();
                }
            }

        }


        private static bool OutRay(Vector3 direction)
        {
            return Physics.Raycast(_selfTransform.position, _selfTransform.TransformDirection(direction), out var hit, Mathf.Infinity, _layerMask);
        }

        #endregion
   

     
    
    }

}

