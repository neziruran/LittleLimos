using System;
using UnityEngine;
using Cinemachine;
using Core.InputS;
using Core.Managers;
using Core.Player;
using Core.Raycast;
using Core.Symbol;
using DG.Tweening;

namespace Core.Camera
{
    public class CameraController : SingletonBehaviour<CameraController>
    {
        #region Variables

        [SerializeField] private CinemachineVirtualCamera _groundCam;
        [SerializeField] private CinemachineVirtualCamera _rightCam;
        [SerializeField] private CinemachineVirtualCamera _roofCam;
        [SerializeField] private CinemachineVirtualCamera _leftCam;
        [SerializeField] private CinemachineVirtualCamera _flippingCam;
        [SerializeField] private CinemachineVirtualCamera _lookUpCam;
        [SerializeField] private CinemachineVirtualCamera _lookLeftCam;
        [SerializeField] private CinemachineVirtualCamera _lookDownCam;
        [SerializeField] private CinemachineVirtualCamera _lookRightCam;
        [SerializeField] private CinemachineVirtualCamera _playerFocusCam;
        [SerializeField] private CinemachineVirtualCamera _symbolFocusCam;
        [SerializeField] private CinemachineVirtualCamera previousCam;
        
        private const float SymbolFocusUpperOffset = -2;
        private const float SymbolNormalOffset = 2;
        private bool _isFocusing = false;

        #endregion

        #region Built-in Methods

        void Start()
        {
            InitCameras();
            
            GameController.Instance.SubscribeOnLevelStart(OnStartFocus,true);
        }

        private void OnDisable()
        {
            GameController.Instance.SubscribeOnLevelStart(OnStartFocus,false);

        }

        void Update()
        {
            if (!_isFocusing)
            {
                QuickCameraTransitionCheck();
                MainCameraTransition();
                
                if (InputListener.CollectableFocusPressed && PlayerController.HasInput)
                {
                    OnSymbolFocus();
                }
            }
        }

        #endregion

        #region List methods

        #endregion

        #region Focus Methodss

        public void OnPlayerCameraFocus()
        {
            DOVirtual.DelayedCall(0f, () => SetPlayerFocus(true));

            DOVirtual.DelayedCall(2.5f, () => SetPlayerFocus(false))
                .OnComplete(() => PlayerController.SetPlayerInput(true));
        }

        private void OnStartFocus()
        {
            DOVirtual.DelayedCall(1f, () => SetPlayerFocus(true));

            DOVirtual.DelayedCall(5f, () => SetPlayerFocus(false))
                .OnComplete(() => PlayerController.SetPlayerInput(true));
        }

        private void SetPlayerFocus(bool active)
        {
            PlayerController.SetPlayerInput(false);
            if (active)
            {
                _isFocusing = true;
                _groundCam.Priority = 0;
                _rightCam.Priority = 0;
                _roofCam.Priority = 0;
                _leftCam.Priority = 0;
                _flippingCam.Priority = 0;
                _playerFocusCam.Priority = 1;
            }
            else
            {
                _isFocusing = false;
            }
        }

        #endregion

        #region Symbol Focus

        private void SetSymbolCameraTarget()
        {
            SymbolBase symbol = GameController.Instance.GetCurrentSymbol();
            var puzzle = symbol.TryGetComponent(out PuzzleSymbol puzzleSymbol);
            if (puzzleSymbol.direction == Vector3.down)
            {
                SetOffsetUpper(symbol);
            }
            else
            {
                SetOffsetNormal(symbol);
            }

            var symbolTransform = symbol.transform;
            _symbolFocusCam.m_Follow = symbolTransform;
            _symbolFocusCam.m_LookAt = symbolTransform;
        }

        private void SetOffsetUpper(SymbolBase symbol)
        {
            var currentOffset = _symbolFocusCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
            var newOffset = new Vector3(currentOffset.x, SymbolFocusUpperOffset, currentOffset.z);
            _symbolFocusCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = newOffset;
        }

        private void SetOffsetNormal(SymbolBase symbol)
        {
            var currentOffset = _symbolFocusCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
            var newOffset = new Vector3(currentOffset.x, SymbolNormalOffset, currentOffset.z);
            _symbolFocusCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = newOffset;
        }

        private void OnSymbolFocus()
        {
            PlayerController.SetPlayerInput(false);
            _isFocusing = true;
            SetSymbolCameraTarget();
            StartFocusSymbol();
            DOVirtual.DelayedCall(3f, FinishSymbolFocus);
        }

        private void StartFocusSymbol()
        {
            _symbolFocusCam.Priority = 1;
            _groundCam.Priority = 0;
            _rightCam.Priority = 0;
            _roofCam.Priority = 0;
            _leftCam.Priority = 0;
            _flippingCam.Priority = 0;
            _playerFocusCam.Priority = 0;
        }

        private void FinishSymbolFocus()
        {
            previousCam.Priority = 1;
            _groundCam.Priority = 0;
            _symbolFocusCam.Priority = 0;
            _rightCam.Priority = 0;
            _roofCam.Priority = 0;
            _leftCam.Priority = 0;
            _flippingCam.Priority = 0;
            _playerFocusCam.Priority = 0;
            PlayerController.SetPlayerInput(true);
            _isFocusing = false;
        }

        #endregion

        #region Main Methods

        private void InitCameras()
        {
            // Main Cameras
            _groundCam.Priority = 1;
            _rightCam.Priority = 0;
            _roofCam.Priority = 0;
            _leftCam.Priority = 0;
            _flippingCam.Priority = 0;

            // Quick Cameras
            _lookUpCam.Priority = 0;
            _lookLeftCam.Priority = 0;
            _lookDownCam.Priority = 0;
            _lookRightCam.Priority = 0;
        }


        private void MainCameraTransition()
        {
            if (PlayerController.IsInGround())
            {
                SetPreviousCamera(_groundCam);


                _groundCam.Priority = 1;
                _rightCam.Priority = 0;
                _leftCam.Priority = 0;
                _roofCam.Priority = 0;
            }
            else if (PlayerController.IsInRight())
            {
                SetPreviousCamera(_rightCam);

                _groundCam.Priority = 0;
                _rightCam.Priority = 1;
                _leftCam.Priority = 0;
                _roofCam.Priority = 0;
            }
            else if (PlayerController.IsInLeft())
            {
                SetPreviousCamera(_leftCam);


                _groundCam.Priority = 0;
                _rightCam.Priority = 0;
                _leftCam.Priority = 1;
                _roofCam.Priority = 0;
            }
            else if (PlayerController.IsInRoof())
            {
                SetPreviousCamera(_roofCam);


                _groundCam.Priority = 0;
                _rightCam.Priority = 0;
                _leftCam.Priority = 0;
                _roofCam.Priority = 1;
            }
        }

        private void QuickCameraTransitionCheck()
        {
            // Check for [UP] arrow key when On Ground
            if (PlayerController.IsInGround())
            {
                if (InputListener.CameraLookUpPressed)
                {
                    RaycastController.CastHighlightRay(Vector3.up);
                    PlayerController.SetLookCamera(true);

                    _lookUpCam.Priority = 1;
                    _groundCam.Priority = 0;
                }
                else
                {
                    RaycastController.DisableHighlightedTile();

                    PlayerController.SetLookCamera(false);

                    _lookUpCam.Priority = 0;
                    _groundCam.Priority = 1;
                }
            }

            // Check for [LEFT] arrow key when On Right Wall
            else if (PlayerController.IsInRight())
            {
                if (InputListener.CameraLookUpPressed)
                {
                    RaycastController.CastHighlightRay(Vector3.left);
                    PlayerController.SetLookCamera(true);

                    _lookLeftCam.Priority = 1;
                    _rightCam.Priority = 0;
                }
                else
                {
                    RaycastController.DisableHighlightedTile();
                    PlayerController.SetLookCamera(false);

                    _lookLeftCam.Priority = 0;
                    _rightCam.Priority = 1;
                }
            }

            // Check for [DOWN] arrow key when On Roof
            else if (PlayerController.IsInRoof())
            {
                if (InputListener.CameraLookUpPressed)
                {
                    RaycastController.CastHighlightRay(Vector3.down);
                    PlayerController.SetLookCamera(true);

                    _lookDownCam.Priority = 1;
                    _roofCam.Priority = 0;
                }
                else
                {
                    RaycastController.DisableHighlightedTile();
                    PlayerController.SetLookCamera(false);

                    _lookDownCam.Priority = 0;
                    _roofCam.Priority = 1;
                }
            }

            // Check for [RIGHT] arrow key when On Left Wall
            else if (PlayerController.IsInLeft())
            {
                if (InputListener.CameraLookUpPressed)
                {
                    RaycastController.CastHighlightRay(Vector3.right);
                    PlayerController.SetLookCamera(true);

                    _lookRightCam.Priority = 1;
                    _leftCam.Priority = 0;
                }
                else
                {
                    RaycastController.DisableHighlightedTile();
                    PlayerController.SetLookCamera(false);

                    _lookRightCam.Priority = 0;
                    _leftCam.Priority = 1;
                }
            }
        }

        private void SetPreviousCamera(CinemachineVirtualCamera vcam)
        {
            previousCam = vcam;
        }

        #endregion

    }
}