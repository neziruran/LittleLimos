
using System;
using System.Collections;
using Core.Audio;
using UnityEngine;
using Utilities;
using Core.Level;
using System.Threading.Tasks;
using Core.InputS;
using Core.Managers;
using Core.Symbol.Data;
using Core.Tile;
using Core.Utilities;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variable
        
        private const string DataPath = "Data/PlayerData/Player";

        private static bool _hasInput = false;
        public static bool CanProceed
        {
            get => _canProceed;
            set => _canProceed = value;
        }
        public static bool PlayerActive => _playerActive;
        public static bool HasInput => _hasInput;
        private static bool _canProceed;
        private static bool _isMoving;
        private static bool _lookCam = false;
        private static bool _playerActive = false;
        private static GameObject _mainCube;
        
        #endregion

        #region Components

        public static Transform PlayerTransform => _playerTransform;
        public static PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    LoadPlayerData();
                }

                return _playerData;
            }
        }

        private static ATile _currentTile;
        private static Transform _playerTransform;
        private static PlayerData _playerData;
        private static GameEnums.PositionState _currentState;

        #endregion

        #region Events

        private static UnityAction _onOutOfBounds;
        private static UnityAction _onRespawn;

        #endregion
        
        #region Built-In Methods

        void Awake()
        {
            LoadPlayerData();
            ResetData();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        public void InitPlayer()
        {
            SubscribeEvents();
            AddOutOfBoundsHandler();
        }
        
        private void ResetData()
        {
            if (_playerData == null) return;
            PlayerData.ResetPlayerData();
        }
        

        #endregion

        #region Subscribe Methods

        private void SubscribeEvents()
        {
            SubscribeOnOutOfBounds(OnOutOfBounds,true);
            SubscribeOnRespawn(OnRespawn,true);

            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,true);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelCompleted,true);
            PlayerMovement.SubscribeOnFlip(OnFlip,true);
            PlayerMovement.SubscribeOnMove(OnMove,true);
        }


        private void UnsubscribeEvents()
        {
            SubscribeOnOutOfBounds(OnOutOfBounds,false);
            SubscribeOnRespawn(OnRespawn,false);

            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,false);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelCompleted,false);
            PlayerMovement.SubscribeOnFlip(OnFlip,false);
            PlayerMovement.SubscribeOnMove(OnMove,false);

        }

        #endregion

        #region Input

        public static void SetPlayerInput(bool key)
        {
            _hasInput = key;
        }

        #endregion

        #region Handler Method

        private void AddOutOfBoundsHandler()
        {
            GameObject outOfBoundsHandler = new GameObject("Follower");
            var follower = outOfBoundsHandler.AddComponent<OutofBoundsHandler>();
            follower.Init(this.transform);
            _playerTransform = this.transform;
        }

        #endregion
        
        #region Camera Methods

        public static bool IsLookingCamera()
        {
            return _lookCam;
        }

        public static void SetLookCamera(bool key)
        {
            _lookCam = key;
        }

        #endregion

        #region Movement Methods

        public static void SetIsMoving(bool key)
        {
            _isMoving = key;
        }

        public static bool IsMoving()
        {
            return _isMoving;
        }

        #endregion

        #region State Methods

        
        public static bool IsOutOfBounds()
        {
            return GetCurrentState() is GameEnums.PositionState.OUTOFBOUNDS;
        }

        public static GameEnums.PositionState GetCurrentState()
        {
            return _currentState;
        }

        public static ATile GetCurrentTile()
        {
            return _currentTile;
        }

        public static void SetPlayerState(GameEnums.PositionState state)
        {
            _currentState = state;
        }

        public static void SetTile(ATile tile)
        {
            _currentTile = tile;
        }

        public static bool IsInGround()
        {
            return _currentState is GameEnums.PositionState.GROUND;
        }

        public static bool IsInRoof()
        {
            return _currentState is GameEnums.PositionState.ROOF;
        }

        public static bool IsInRight()
        {
            return _currentState is GameEnums.PositionState.RWALL;
        }

        public static bool IsInLeft()
        {
            return _currentState is GameEnums.PositionState.LWALL;
        }


        #endregion

        #region Data Methods
   
        private static void LoadPlayerData()
        {
            _mainCube = _playerTransform.GetChild(0).gameObject;
            Debug.Log("Main cube is " + _mainCube);
            if (_playerData != null) return;
            _playerData = Resources.Load<PlayerData>(DataPath);
            Debug.Log(_playerData != null ? "Player Data Loaded" : "Player Data is null");
        }
        
        private void ResetTransform()
        {
            _playerTransform.localScale = Vector3.one;
            _playerTransform.rotation = Quaternion.Euler(0, 0, 0);
            _playerTransform.position = GameController.Instance.GetLevelData().StartPoint;
        }

        #endregion

        #region Utils

        private void SetStatusMainCube(bool status)
        {
            _mainCube.SetActive(status);
        }
        
        public static bool IsMatchingRotation(Quaternion targetAngle)
        {
            float precision = 0.9999f;
            bool matching = Mathf.Abs(Quaternion.Dot(_playerTransform.rotation, targetAngle)) > precision;
            return matching;
        }
        
        #endregion
        
        #region AudioMethods

        private static void PlayMovementSound()
        {
            string movementSoundKey = "movement";
            AudioManager.Instance.PlaySound(movementSoundKey);
        }

        private static void PlayFlipSound()
        {
            string flipSoundKey = "flip";
            AudioManager.Instance.PlaySound(flipSoundKey);
        }

        #endregion

        #region Event Methods

        public static void SubscribeOnOutOfBounds(UnityAction action,bool subscribe)
        {
            if (subscribe)
                _onOutOfBounds += action;
            else
            {
                _onOutOfBounds -= action;
            }
        }
        public static void SubscribeOnRespawn(UnityAction action,bool subscribe)
        {
            if (subscribe)
                _onRespawn += action;
            else
            {
                _onRespawn -= action;
            }
        }
 
        public static async void InvokeOnOutOfBounds()
        {
            Debug.LogError("Game Over");
            await Task.Delay(1);
            _onOutOfBounds?.Invoke();
        }
        private void OnOutOfBounds()
        {
            SetPlayerInput(false);
            transform.DOShakeScale(.5f,1f,10,1000);
            SetStatusMainCube(false);
            ResetData();
            DOVirtual.DelayedCall(_playerData.RespawnTime, InvokeOnRespawn);
        }

        private void InvokeOnRespawn()
        {
            _onRespawn?.Invoke();
        }
        private void OnRespawn()
        {
            ResetData();
            ResetTransform();
            DOVirtual.DelayedCall(2.5f, ActivatePlayer);

        }

        private void OnFlip()
        {
            PlayFlipSound();
        }

        private void OnMove()
        {
            PlayMovementSound();
        }

    
        private void OnLevelStart()
        {
            ResetData();
            ResetTransform();
            
            DeActivatePlayer();
            
            // make sure dissolve finish then open inputs, calculate with the dissolve time
            DOVirtual.DelayedCall(3.5f, ActivatePlayer);

        }

        private void ActivatePlayer()
        {
            SetPlayerInput(true);
            SetStatusMainCube(true);
            _playerActive = true;
            
        }

        private void DeActivatePlayer()
        {
            SetPlayerInput(false);
            SetStatusMainCube(false);
            _playerActive = false;

        }
            
        
        private void OnLevelCompleted()
        {
            DeActivatePlayer();
        }

        #endregion
        
    }
}