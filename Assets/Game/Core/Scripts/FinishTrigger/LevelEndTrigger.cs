using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using Core.Managers;
using DG.Tweening;

namespace Core
{
    public class LevelEndTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var isPlayer = other.TryGetComponent(out PlayerController player);
            if(isPlayer)
            {
                DOVirtual.DelayedCall(1f, () => GameController.Instance.InvokeOnLevelComplete());
            }
        }
    }

}
