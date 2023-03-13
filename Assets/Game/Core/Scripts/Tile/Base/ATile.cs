using System.Collections;
using Core.Tile.Data;
using UnityEngine;
using Utilities;
using Core.Door;
using System;
using Core.Utilities;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Core.Tile
{
    public abstract class ATile : MonoBehaviour
    {
        #region Variables

        [SerializeField] GameEnums.PositionState _positionState;

        [SerializeField] private bool isTileBeforeDoor;

        private const string DataPath = "Data/TileData/Tile";
        protected Renderer _renderer;
        protected TileData _tileData;
        private bool _isHighlighting = false;

        #endregion

        #region Enumeration

        public TileType _TileType;

        public enum TileType
        {
            Normal,
            Breakable,
        }

        #endregion

        #region Getters

        public GameEnums.PositionState PositionState => _positionState;
        public TileData TileData => _tileData;
        public bool IsTileBeforeDoor => isTileBeforeDoor;

        #endregion

        #region Built-in Methods

        protected void Awake()
        {
            _renderer = GetComponent<Renderer>();
            LoadData();
            isTileBeforeDoor = false;
        }

        protected void Update()
        {
            if (_isHighlighting)
            {
                _renderer.material.color = Color.Lerp(
                    _tileData.DefaultMaterial.color,
                    _tileData.HighLightedMaterial.color,
                    Mathf.PingPong(Time.time * _tileData.HighlightSpeed, 1));
            }
            else
            {
                _renderer.material.color = Color.Lerp(_renderer.material.color, _tileData.DefaultMaterial.color, 1f);
            }
        }

        protected void OnEnable()
        {
            Subscribe();
            // if (isTileBeforeDoor && DoorBase.Instance != null)
            //     DOVirtual.DelayedCall(1f, ()=>DoorBase.Instance.SubscribeOnDoorUnlocked(SetDoorTile, true));
        }


        protected void OnDisable()
        {
            Unsubscribe();

        }

        #endregion
        
        
        #region DoorTile

        // private void SetDoorTile()
        // {
        //     isTileBeforeDoor = false;
        //     DoorBase.Instance.SubscribeOnDoorUnlocked(SetDoorTile,false);
        // }

        #endregion

        #region Custom Methods
        protected void SetTileType(TileType type)
        {
            _TileType = type;
        }

        private void LoadData()
        {
            if (_tileData != null) return;
            _tileData = Resources.Load<TileData>(DataPath);
            if (_tileData != null)
                Debug.Log("Tile Data Loaded");
        }

        public void SetHighlight(bool value)
        {
            _isHighlighting = value;
        }


        #endregion

        #region Subscribtion Methods

        private void Subscribe()
        {
        }

        private void Unsubscribe()
        {
            
        }

        #endregion


    }
}