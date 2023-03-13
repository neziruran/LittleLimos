using System;
using Core.Player;
using Core.Raycast;
using Core.Tile;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Core.Tile
{
#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(TileBreakable))]
    public class TileBreakableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TileBreakable tileBreakable = (TileBreakable)target;

            if (GUILayout.Button("OnPlayerVisit"))
            {
                tileBreakable.InvokeOnPlayerVisit();
            }

        }
    }
#endif

    public class TileBreakable : ATile
    {
        #region Variables

        public bool _playerVisiting = false;
        private Tween _materialLerpTween;
        
        [SerializeField] [Range(1,3)] private int breakCount = 1;
        [SerializeField] private float fallSpeed = 10f;

        #endregion

        #region Events

        private UnityAction _onPlayerVisit;

        #endregion
        
        #region BuiltIn Methods

        new void OnEnable()
        {
            base.OnEnable();
            base.SetTileType(TileType.Breakable);
            
            PlayerMovement.SubscribeOnMove(OnPlayerHitTile,true);
            SubscribeOnPlayerVisit(OnPlayerVisit,true);
            
        }
        
        new void OnDisable()
        {
            base.OnDisable();
            PlayerMovement.SubscribeOnMove(OnPlayerHitTile,false);
            SubscribeOnPlayerVisit(OnPlayerVisit,false);
        }

        #region CustomMethods

        private void FallDown()
        {
            float fallPosition = -20f; // fall down 100 unit below
            transform.DOMoveY(fallPosition,fallSpeed).SetSpeedBased();
            DOVirtual.DelayedCall(.2f, OutofBoundsHandler.OutCheck);
        }

        private void SetPlayerVisiting(bool status)
        {
            _playerVisiting = status;
        }


        private void OnPlayerVisit()
        {
            if (_playerVisiting) return;
            
            SetPlayerVisiting(true);

            // decrease breakcount
            breakCount--;

            if (breakCount == 1)
            {
                // play an animation or material lerp in here to indicate 
                _materialLerpTween = _renderer.material.DOFloat(1f,_tileData.EmissionExposureWeightKey,1f)
                    .SetLoops(-1, LoopType.Yoyo);
            }
            if (breakCount == 0)
            {
                OnFallStart();
            }
        }
        
        private void OnFallStart()
        {
            // plau fx here
            // Call FallDown with delay
            _materialLerpTween.Kill();
            DOVirtual.DelayedCall(.5F, FallDown);
        }
        
        private void OnFallFinish()
        {
            // set active false
            gameObject.SetActive(false);
        }


        #endregion

        #region Event Methods
        

        public void InvokeOnPlayerVisit()
        {
            _onPlayerVisit?.Invoke();
            
        }
        
        private void SubscribeOnPlayerVisit(UnityAction action, bool subscribe)
        {
            if (subscribe)
            {
                _onPlayerVisit += action;
            }
            else
            {
                _onPlayerVisit -= action;
            }
        }

        private void OnPlayerHitTile()
        {
            RaycastController.CastCustomRay(out var hit ,Vector3.up);
            if (hit.transform != this.transform)
            {
                SetPlayerVisiting(false);
            }
        }
        

        #endregion
        

        #endregion
        
       
    }
}