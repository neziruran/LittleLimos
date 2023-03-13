using System;
using Core.InputS;
using Core.Tile;
using Core.Player;
using Core.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using System.Linq;
using Core.Managers;


namespace Core.Raycast
{
    public class RaycastController : MonoBehaviour
    {
        #region Variables

        private static ATile _highlightedTile;
        private static LayerMask _raycastLayer;
        private static bool _isHighlighting = false;
        private static bool _isAnyRayHitting;
        private static float _rayLenght = 1f;

        public static GameEnums.PlayerState PlayerState = GameEnums.PlayerState.OnTile;


        private RaycastHit _groundRay, _upRay, _rightRay, _leftRay;
        private RaycastHit[] _rayArray = new RaycastHit[4];

        #endregion


        #region Built-in Methods

        private void Awake()
        {
            _raycastLayer = NezirExtensionMethods.SetLayerMask("Grid");
        }

        void FixedUpdate()
        {
            if (!GameController.Instance.GameInstanceActive) return;
            
            if(PlayerState is GameEnums.PlayerState.OnAir)
            {
                return;
            }
            MainRays();
        }

        #endregion

        #region Raycasting NonAlloc

        private void MainRays()
        {
            _isAnyRayHitting = false;

            CheckMainRays(-transform.up, transform.up, transform.right, -transform.right);


            if (_upRay.transform && _rightRay.transform is not null)
            {
                _rightRay.transform.TryGetComponent(out TilePieceNormal rightTilePiece);

                _upRay.transform.TryGetComponent(out TilePieceNormal roofTilePiece);
                if (InputListener.RightPressed)
                {
                    OnHitTile(GameEnums.PositionState.RCORNER_UP, rightTilePiece);
                }

                if (InputListener.LeftPressed)
                {
                    OnHitTile(GameEnums.PositionState.ROOF, roofTilePiece);
                }
            }
            CheckForRayHit();
        }

        private void CheckForRayHit()
        {
            if (!_isAnyRayHitting)
            {
                PlayerController.SetPlayerState(GameEnums.PositionState.OUTOFBOUNDS);
            }
        }

        public static void CastCustomRay(out RaycastHit hit, Vector3 direction)
        {
            Transform follower = FindObjectOfType<OutofBoundsHandler>().transform;
            LayerMask gridLayer = NezirExtensionMethods.SetLayerMask("Grid");
            Physics.Raycast(follower.transform.position, follower.TransformDirection(direction), out hit,
                Mathf.Infinity, gridLayer);
        }



        private void CheckMainRays(Vector3 groundDirection, Vector3 upwardDirection, Vector3 rightDirection,
            Vector3 leftDirection)
        {
            int groundRayHits = CastRayNonAlloc(groundDirection);
            CheckRayHits(groundRayHits, _groundRay);

            int upwardRayHits = CastRayNonAlloc(upwardDirection);
            CheckRayHits(upwardRayHits, _upRay);

            int rightRayHits = CastRayNonAlloc(rightDirection);
            CheckRayHits(rightRayHits, _rightRay);

            int leftRayHits = CastRayNonAlloc(leftDirection);
            CheckRayHits(leftRayHits, _leftRay);
        }

        private void CheckRayHits(int rayLength, RaycastHit targetRay)
        {
            for (int i = 0; i < rayLength; i++)
            {
                if (_rayArray[i].transform != null)
                {
                    SetRayArray(targetRay, _rayArray, i);
                }
            }
        }

        private void SetRayArray(RaycastHit targetRay, RaycastHit[] raycasts, int index)
        {
            targetRay = raycasts[index];
            targetRay.transform.TryGetComponent(out ATile tilePiece);

            PlayerController.CanProceed = !tilePiece.IsTileBeforeDoor;

            OnHitTile(tilePiece.PositionState, tilePiece);
            // TO DO: CAST IT VIA A CUSTOM RAY check for ray length current length too short
            _isAnyRayHitting = true;

        }

        private int CastRayNonAlloc(Vector3 direction)
        {
            int hits = Physics.RaycastNonAlloc(PlayerController.PlayerTransform.position, direction, _rayArray,
                _rayLenght, _raycastLayer);
            return hits;
        }

        public static void SetRayHitting(bool key)
        {
            _isAnyRayHitting = key;
        }

        #endregion

        #region Hit Event

        private void OnHitTile(GameEnums.PositionState state, ATile hitTile)
        {

            if (PlayerController.GetCurrentTile() == hitTile) return;

            PlayerController.SetPlayerState(state);
            PlayerController.SetTile(hitTile);

            var isBreakable = hitTile._TileType is ATile.TileType.Breakable;
            hitTile.TryGetComponent(out TileBreakable tileBreakable);

            if (isBreakable)
                OnBreakableTile(tileBreakable);

        }

        private void OnBreakableTile(TileBreakable tileBreakable)
        {
            tileBreakable.InvokeOnPlayerVisit();
        }

        #endregion

        #region Higlighting

        public static void CastHighlightRay(Vector3 direction)
        {
            if (Physics.Raycast(PlayerController.PlayerTransform.position, direction, out var hit, Mathf.Infinity,
                    _raycastLayer))
            {
                hit.transform.TryGetComponent(out ATile tilePiece);
                if (tilePiece != null && !_isHighlighting)
                {
                    _isHighlighting = true;
                    _highlightedTile = tilePiece;
                    _highlightedTile.SetHighlight(true);
                }
            }
        }

        public static void DisableHighlightedTile()
        {
            if (_highlightedTile == null) return;
            _highlightedTile.SetHighlight(false);
            _highlightedTile = null;
            _isHighlighting = false;
        }

        #endregion


    }
}