using System.Collections;
using System.Collections.Generic;
using Core.InputS;
using Core.Player;
using Utilities;
using Core.Tile;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Symbol.Data
{
    [CreateAssetMenu(fileName = "Player", menuName = "Create New Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        #region Variables
        
        private const int rotationDirection = 2;
        [Range(1, 15)] [SerializeField] private float rollSpeed;
        [Range(5, 15)] [SerializeField] private float flipSpeed;
        [Range(1, 5)] [SerializeField] private float respawnTime;
        
        #endregion


        #region Getter & Setters

        public int RotationDirection => rotationDirection;

        public float FlipSpeed => flipSpeed;
        public float RollSpeed => rollSpeed;

        public float RespawnTime => respawnTime;
        

        public void ResetPlayerData()
        {
            PlayerController.SetIsMoving(false);
            PlayerController.SetPlayerState(GameEnums.PositionState.GROUND); // DEFAULT
            InputListener.SetInput(true);
        }
        
        #endregion
        
   
    }
}