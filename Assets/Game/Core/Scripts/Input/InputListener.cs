using System;
using Core.Symbol;
using Core.Symbol.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Core.Player;

namespace Core.InputS
{
    public class InputListener : MonoBehaviour
    {
        #region Variables

        private static bool _inputEnabled = true;

        private static bool _forwardPressed;
        private static bool _backPressed;
        private static bool _leftPressed;
        private static bool _rightPressed;
        private static bool _flipGravityPressed;
        private static bool _gravityShiftPressed;
        private static bool _cameraLookUpPressed;
        private static bool _collectableFocusPressed;
        private static bool _escapePressed;
        #endregion

        #region Getters & Setters

        public static bool ForwardPressed => _forwardPressed;

        public static bool BackPressed => _backPressed;

        public static bool LeftPressed => _leftPressed;

        public static bool RightPressed => _rightPressed;

        public static  bool FlipGravityPressed => _flipGravityPressed;
        
        public static bool CameraLookUpPressed => _cameraLookUpPressed;
        
        public static bool CollectableFocusPressed => _collectableFocusPressed;
        
        
        public static bool EscapePressed => _escapePressed;


        #endregion

        #region Components

        private InputMaster _input;
        
        #endregion

        #region Built-In Methods

        void Awake()
        {
            OnInputInit();
        }

        #endregion
        
        
        private void OnInputInit()
        {
            _input = new InputMaster();
            _inputEnabled = true;
            EnableInputActions();
            GetInput();

        }

        private void EnableInputActions()
        {
            if (_inputEnabled)
            {
                _input.Keyboard.Enable();
            }
        }

        private void DisableInputActions()
        {
            if (!_inputEnabled)
            {
                _input.Keyboard.Disable();

            }
        }

        public static void SetInput(bool key)
        {
            _inputEnabled = key;
        }
        
        private void GetInput()
        {

            _input.Keyboard.Forward.performed += _ => _forwardPressed = true;
            _input.Keyboard.Forward.canceled += _ => _forwardPressed = false;
            
            _input.Keyboard.Back.performed += _ => _backPressed = true;
            _input.Keyboard.Back.canceled += _ => _backPressed = false;
            
            _input.Keyboard.Right.performed += _ => _rightPressed = true;
            _input.Keyboard.Right.canceled += _ => _rightPressed = false;
            
            _input.Keyboard.Left.performed += _ => _leftPressed = true;
            _input.Keyboard.Left.canceled += _ => _leftPressed = false;
            
            
            _input.Keyboard.Space.performed += _ => _flipGravityPressed = true;
            _input.Keyboard.Space.canceled += _ => _flipGravityPressed = false;

            _input.Keyboard.Tab.performed += _ => _cameraLookUpPressed = true;
            _input.Keyboard.Tab.canceled += _ => _cameraLookUpPressed = false;

            
            _input.Keyboard.F.performed += _=> _collectableFocusPressed = true;
            _input.Keyboard.F.canceled += _=> _collectableFocusPressed = false;

            _input.Keyboard.ESC.performed += _=> _collectableFocusPressed = true;
            _input.Keyboard.ESC.canceled += _=> _collectableFocusPressed = false;


        }


        #region Custom Methods

    
        #endregion

    }
}