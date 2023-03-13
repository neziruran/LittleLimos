using System.Collections;
using Core.Camera;
using Core.Door;
using Core.InputS;
using Core.Player;
using Core.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utilities;

namespace Core.Symbol
{
    public class PuzzleSymbol : SymbolBase
    {
        #region Variables

        [SerializeField] private DoorBase _door;
        [SerializeField] private DoorSymbol doorSymbol;
        [SerializeField] private RayDirections rayDirection;
        private enum RayDirections
        {
            Up,
            Left,
            Right,
            Down,
        }
        private const string Symbol = "Symbol";
        private LayerMask _layerMask;
        private Vector3 _direction;
        public Vector3 direction => _direction;
        private bool _rayStatus = true;
        private bool _isPlayerFocusing = false;


        #region Components
        
        private Renderer _renderer;
        private PlayerSymbol _playerSymbol;

        #endregion
        

        #endregion

        #region  Built-In Methods
        
        private new void Awake()
        {
            base.Awake();
            GetLayerMask();
            SetRayToDirection();
            SetMaterial();
        }
        
        private new void OnEnable()
        {
            base.OnEnable();
            
        }
        

        private new void OnDisable()
        {
            base.OnDisable();
        }

        private void FixedUpdate()
        {
            if (!_rayStatus) return;
            
            if (InputListener.CollectableFocusPressed && !_isPlayerFocusing)
            {
                HiglightSymbol();
            }
            
            if (Physics.Raycast(transform.position, _direction, out var hit, 0.1f, _layerMask))
            {
                if (IsCorrectSymbolHit(hit))
                {
                    OnCollected?.Invoke();
                    doorSymbol.OnCollected?.Invoke();
                    CameraController.Instance.OnPlayerCameraFocus();

                }

            }
        }
        
        #endregion
        
        #region  Custom Methods

        private void HiglightSymbol()
        {
            _isPlayerFocusing = true;
            GetSymbolData().PuzzleMaterial.DOFloat(GetSymbolData().EmissionActiveValue, GetSymbolData().EmissionExposureWeightKey, 1f).SetDelay(1.25f)
                .SetEase(Ease.InOutSine);
            DOVirtual.DelayedCall(4f, FinishHiglight);

        }

        private void FinishHiglight()
        {
            GetSymbolData().PuzzleMaterial.SetFloat(GetSymbolData().EmissionExposureWeightKey,GetSymbolData().EmissionInActiveValue);
            _isPlayerFocusing = false;
        }
        
        public override void ActivateSymbol()
        {
            Debug.Log("ACTIVATE SYMBOL + " + gameObject.name);
            var symbolData = GetSymbolData();
            Activated = true;
            symbolData.PuzzleMaterial.DOFloat(symbolData.EmissionActiveValue, symbolData.EmissionExposureWeightKey, 1f).SetDelay(1.5f).SetEase(Ease.InOutSine);
        }

        public override void DeactivateSymbol()
        {
            Debug.Log("DEACTIVATE SYMBOL + " + gameObject.name);
            var symbolData = GetSymbolData();
            Activated = false;
            symbolData.PuzzleMaterial.SetFloat(symbolData.EmissionExposureWeightKey,symbolData.EmissionInActiveValue);
        }
        private void SetMaterial()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material = base.GetSymbolData().PuzzleMaterial;
        }
        private void SetRayToDirection()
        {
            switch (rayDirection)
            {
                case RayDirections.Up:
                    _direction = Vector3.up;
                    break;
                case RayDirections.Down:
                    _direction = Vector3.down;
                    break;
                case RayDirections.Left:
                    _direction = Vector3.left;
                    break;
                case RayDirections.Right:
                    _direction = Vector3.right;
                    break;
            }
        }
        private bool IsMatching()
        {
            Quaternion matchAngle = NezirExtensionMethods.ConvertToQuaternion(GetSymbolData().RotationMatchAngle);
            return PlayerController.IsMatchingRotation(matchAngle);
        }

        private bool IsCorrectSymbolHit(RaycastHit hit)
        {
            if (!IsMatching()) return false;
            var hitTransform = hit.collider.TryGetComponent(out PlayerSymbol playerSymbol);
            _playerSymbol = playerSymbol;
            var playerId = _playerSymbol.GetSymbolId();
            
            if (playerId == base.GetSymbolData().SymbolId)
            {

                if (_door.Equals(_playerSymbol.GetSymbolData().SymbolOrder))
                {
                    Debug.Log("DOĞRU SEMBOL EŞLEŞMESİ - DOĞRU SIRA");
                    _playerSymbol.OnCollected?.Invoke();
                    _rayStatus = false;
                    return true;
                }
                Debug.LogWarning("DOĞRU SEMBOL EŞLEŞMESİ - YANLIŞ SIRA");
                return false;
            }

            Debug.LogError("HATALI SEMBOL EŞLEŞMESİ");


            return playerId == base.GetSymbolData().SymbolId;
        }
        private void GetLayerMask()
        {
            _layerMask = LayerMask.GetMask(Symbol);
        }
        
        #endregion
        
    }
}