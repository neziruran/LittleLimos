using System;
using System.Collections;
using Core.InputS;
using Core.Managers;
using Core.Raycast;
using Core.Tile;
using DG.Tweening;
using UnityEngine;
using PlayerEnum = Utilities.GameEnums;


namespace Core.Player
{
    public class PlayerMovement : MonoBehaviour
    {

        #region Variables
        private Vector3 _anchor;
        private Vector3 _axis;
        #endregion

        #region Events

        private static event Action OnFlip;
        private static event Action OnMove;

        #endregion

        #region Built-in Methods

        void Awake()
        {
            OnPlayerInit();
        }

        void Update()
        {
            if (GameController.Instance.GameInstanceActive)
            {
                if (PlayerController.HasInput)
                {
                    if (CanMove())
                    {
                        MovementInput();
                    }

                    if (CanFlip())
                    {
                        FlipInput();
                    } 
                }
                
            }
            
        }


        #endregion

        #region CustomMethods

        private void OnPlayerInit()
        {
            _anchor = Vector3.zero;
            _axis = Vector3.zero;
        }

        private bool CanMove()
        {
            return !PlayerController.IsMoving() && !PlayerController.IsLookingCamera();
        }
        private bool CanFlip()
        {
            return !PlayerController.IsMoving() && !PlayerController.IsLookingCamera();
        }

        #region Movement Methods

        private void MovementInput()
        {
            if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.GROUND)
            {
                if (InputListener.LeftPressed)
                    StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
                else if (InputListener.RightPressed)
                    StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
                else if (InputListener.ForwardPressed && PlayerController.CanProceed)
                    StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
                else if (InputListener.BackPressed)
                    StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked


            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.ROOF)
            {
                if (InputListener.LeftPressed)
                    StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
                else if (InputListener.RightPressed)
                    StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
                else if (InputListener.ForwardPressed)
                    StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
                else if (InputListener.BackPressed)
                    StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.RWALL)
            {
                if (InputListener.LeftPressed)
                    StartCoroutine(Roll(Vector3.down)); // rotate to the left when A clicked
                else if (InputListener.RightPressed)
                    StartCoroutine(Roll(Vector3.up)); // rotate to the right when D clicked
                else if (InputListener.ForwardPressed)
                    StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
                else if (InputListener.BackPressed)
                    StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.LWALL)
            {
                if (InputListener.LeftPressed)
                    StartCoroutine(Roll(Vector3.up)); // rotate to the left when A clicked
                else if (InputListener.RightPressed)
                    StartCoroutine(Roll(Vector3.down)); // rotate to the right when D clicked
                else if (InputListener.ForwardPressed)
                    StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
                else if (InputListener.BackPressed)
                    StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
            }

            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.RCORNER)
            {
                if (InputListener.LeftPressed)
                {
                    _anchor = transform.localPosition + Vector3.left / 2 + Vector3.down / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.up, Vector3.left); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate to the left when A clicked
                }
                else if (InputListener.RightPressed)
                {
                    _anchor = transform.localPosition + Vector3.up / 2 + Vector3.right / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.left, Vector3.up); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate to the right when D clicked
                }
                else if (InputListener.ForwardPressed)
                {
                    _anchor = transform.localPosition + Vector3.forward / 2 + Vector3.down / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.up, Vector3.forward); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate forward when W clicked
                }
                else if (InputListener.BackPressed)
                {
                    _anchor = transform.localPosition + Vector3.back / 2 + Vector3.down / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.up, Vector3.back); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate back when S clicked
                }
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.LCORNER)
            {
                if (InputListener.LeftPressed)
                {
                    _anchor = transform.localPosition + Vector3.up / 2 + Vector3.left / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.right, Vector3.up); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate to the right when A clicked
                }
                else if (InputListener.RightPressed)
                {
                    _anchor = transform.localPosition + Vector3.right / 2 + Vector3.down / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.up, Vector3.right); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate to the right when D clicked
                }
                else if (InputListener.ForwardPressed)
                {
                    _anchor = transform.localPosition + Vector3.forward / 2 + Vector3.down / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.up, Vector3.forward); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate forward when W clicked
                }
                else if (InputListener.BackPressed)
                {
                    _anchor = transform.localPosition + Vector3.back / 2 +
                              Vector3.down / 2; // direction of the rotation
                    _axis = Vector3.Cross(Vector3.up,
                        Vector3.back); // compute the rotation access based on the direction and y axis
                    StartCoroutine(Roll_Custom()); // rotate back when S clicked
                }
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.RCORNER_UP)
            {
                if (InputListener.LeftPressed)
                {
                    StartCoroutine(Roll(Vector3.left));
                }
                else if (InputListener.RightPressed)
                {
                    // ERROR
                }
                else if (InputListener.ForwardPressed)
                {
                    StartCoroutine(Roll(Vector3.forward));
                }
                else if (InputListener.BackPressed)
                {
                    StartCoroutine(Roll(Vector3.back));
                }
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.LCORNER_UP)
            {
                if (InputListener.LeftPressed)
                {
                    // DONT DO ANYTHİNG
                    //PLAY ERROR SOUND MAYBE
                }
                else if (InputListener.RightPressed)
                {
                    StartCoroutine(Roll(Vector3.right));
                }
                else if (InputListener.ForwardPressed)
                {
                    StartCoroutine(Roll(Vector3.forward));
                }
                else if (InputListener.BackPressed)
                {
                    StartCoroutine(Roll(Vector3.back));
                }
            }

        }

        private void SetRotationAnchor(Vector3 direction)
        {
            //if statement that changes the axes and the rotation depending on which wall is the cube touching 

            if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.ROOF or PlayerEnum.PositionState
                .LCORNER_UP or PlayerEnum.PositionState
                .RCORNER_UP)
            {
                _anchor = transform.localPosition + direction / PlayerController.PlayerData.RotationDirection + Vector3.up / PlayerController.PlayerData.RotationDirection; // direction of the rotation
                _axis = Vector3.Cross(Vector3.down,
                    direction); // compute the rotation access based on the direction and y axis
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.RWALL or PlayerEnum.PositionState.RCORNER_UP)
            {
                _anchor = transform.localPosition + direction / PlayerController.PlayerData.RotationDirection + Vector3.right / PlayerController.PlayerData.RotationDirection; // direction of the rotation
                _axis = Vector3.Cross(Vector3.left,
                    direction); // compute the rotation access based on the direction and y axis
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.LWALL or PlayerEnum.PositionState.LCORNER_UP)
            {
                _anchor = transform.localPosition + direction / PlayerController.PlayerData.RotationDirection + Vector3.left / PlayerController.PlayerData.RotationDirection; // direction of the rotation
                _axis = Vector3.Cross(Vector3.right,
                    direction); // compute the rotation access based on the direction and y axis
            }
            else if (PlayerController.GetCurrentState() is PlayerEnum.PositionState.GROUND)
            {
                _anchor = transform.localPosition + direction / PlayerController.PlayerData.RotationDirection + Vector3.down / PlayerController.PlayerData.RotationDirection; // direction of the rotation
                _axis = Vector3.Cross(Vector3.up,
                    direction); // compute the rotation access based on the direction and y axis
            }
        }

        #endregion

        #region Flip Methods

        private void FlipInput()
        {
            if (InputListener.FlipGravityPressed)
            {
                InvokeOnFlip();

                Vector3 yOffset = new Vector3(0, .5f, 0);
                Vector3 xOffset = new Vector3(.5f, 0f, 0);

                switch (PlayerController.GetCurrentState())
                {
                    case PlayerEnum.PositionState.GROUND:
                        FlipPlayer(Vector3.up, -yOffset);
                        break;
                    case PlayerEnum.PositionState.ROOF:
                        FlipPlayer(Vector3.down, yOffset);
                        break;
                    case PlayerEnum.PositionState.RWALL:
                        FlipPlayer(Vector3.left, xOffset);
                        break;
                    case PlayerEnum.PositionState.LWALL:
                        FlipPlayer(Vector3.right, -xOffset);
                        break;

                }
            }

        }
        private void FlipPlayer(Vector3 direction, Vector3 offset)
        {
            RaycastController.CastCustomRay(out var hit, direction);
            if (hit.transform == null) return;
            var hasHit = hit.transform.TryGetComponent(out TilePieceNormal tilePiece);
            if (hasHit && hit.transform != null)
            {
                Vector3 finalPosition = hit.transform.position + offset;
                ExecuteFlipOperation(finalPosition);
            }

        }

        private void ExecuteFlipOperation(Vector3 targetPosition)
        {
            PlayerController.PlayerTransform.DOMove(targetPosition, PlayerController.PlayerData.FlipSpeed).SetEase(Ease.Linear)
                .SetSpeedBased().OnStart(OnFlipStart).OnComplete(OnFlipEnd);
        }

        private void OnFlipStart()
        {
            RaycastController.PlayerState = PlayerEnum.PlayerState.OnAir;
            PlayerController.SetPlayerInput(false);
            PlayerController.SetIsMoving(true);
        }

        private void OnFlipEnd()
        {
            RaycastController.PlayerState = PlayerEnum.PlayerState.OnTile;
            DOVirtual.DelayedCall(.5f, () => PlayerController.SetIsMoving(false));
            DOVirtual.DelayedCall(.5f, () => PlayerController.SetPlayerInput(true));
            // DOVirtual.DelayedCall(.5f, () => RaycastController.PlayerState = PlayerEnum.PlayerState.OnTile); bu fix bazı durumlarda daha kötğ bug çıkardı tekrar bakalım
        }

        #endregion

        #endregion

        #region Enumerators

        IEnumerator Roll(Vector3 direction)
        {

            InvokeOnMove();

            PlayerController.SetIsMoving(true);
            SetRotationAnchor(direction);

            for (var i = 0; i < 90 / PlayerController.PlayerData.RollSpeed; i++)
            {
                transform.RotateAround(_anchor, _axis, PlayerController.PlayerData.RollSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            PlayerController.SetIsMoving(false);
            OutofBoundsHandler.OutCheck();

        }

        IEnumerator Roll_Custom()
        {

            InvokeOnMove();

            PlayerController.SetIsMoving(true);

            for (var i = 0; i < 90 / PlayerController.PlayerData.RollSpeed; i++)
            {
                transform.RotateAround(_anchor, _axis, PlayerController.PlayerData.RollSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            PlayerController.SetIsMoving(false);
            OutofBoundsHandler.OutCheck();

        }

        #endregion

        #region Event Methods
        public static void SubscribeOnMove(Action action, bool subscribe)
        {
            if (subscribe)
            {
                OnMove += action;
            }
            else
            {
                OnMove -= action;
            }
        }

        private void InvokeOnMove()
        {
            OnMove?.Invoke();
        }

        public static void SubscribeOnFlip(Action action, bool subscribe)
        {
            if (subscribe)
            {
                OnFlip += action;
            }
            else
            {
                OnFlip -= action;
            }
        }

        private void InvokeOnFlip()
        {
            OnFlip?.Invoke();
        }

        #endregion

    }

}

