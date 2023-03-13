using UnityEngine;

namespace Core.Level
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] private string levelMusicKey;
        [SerializeField] private Transform startPoint;
        public Vector3 StartPoint => startPoint.position;
        public string LevelMusicKey => levelMusicKey;

        #region Built-In Methods


        #endregion



    }

}
