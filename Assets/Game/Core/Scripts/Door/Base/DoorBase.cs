using System;
using System.Collections.Generic;
using System.Linq;
using Core.Symbol;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Core.Utilities;
using DG.Tweening;
using Core.Tile;
using Core.Managers;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Core.Door
{
    public abstract class DoorBase : MonoBehaviour, IDoor, IEquatable<int>
    {

        #region Variables
        
        [SerializeField] private Transform doorPivot;
        [SerializeField] private Renderer doorRenderer;
        [SerializeField] private Material openMaterial;
        [SerializeField] private List<SymbolBase> symbols;
        [SerializeField] [Range(0.1f,1f)] private float scrollSpeedY= 0.1f;

        private bool _opened = false;
        

        #endregion
        
        
        #region Events
        
        private UnityAction _onDoorUnlocked;
        
        #endregion

        #region Built In

        protected void Awake()
        {

            StartCoroutine(SymbolOrderer());
            
        }

        private void Update()
        {
            if (!_opened)
            {
                float y = Time.time * scrollSpeedY;
                doorRenderer.material.SetTextureOffset("_EmissiveColorMap",new Vector2(0f,y));
            }
            
        }
        
        #endregion

        #region Subscription Methods

        public void SubscribeOnDoorUnlocked(UnityAction action, bool subscribe)
        {
            if (subscribe)
                _onDoorUnlocked += action;
            else
            {
                _onDoorUnlocked -= action;
            }
        }

        #endregion

        #region Custom Methods
        

        public void Unlock()
        {
            Debug.Log("Door Unlocked");
            _opened = true;
            _onDoorUnlocked?.Invoke();
            doorPivot.DOScaleY(0, 1f).SetEase(Ease.InOutSine);
        }

        public void OnFinish()
        {
            Debug.Log("Door Opened process end");
        }


        public bool Equals(int other)
        {
            if (symbols[0].GetSymbolData().SymbolOrder == other)
            {
                symbols.RemoveAt(0);
                if (symbols.Count <= 0)
                {
                    UnlockCall(1.5f);
                }
                return true;
            }

            return false;
        }

        private void UnlockCall(float t)
        {
            StartCoroutine(UnlockEnumerator(t));
        }

        #region Enumerators
        
        IEnumerator UnlockEnumerator(float t)
        {
            doorRenderer.material.Lerp(doorRenderer.material,openMaterial,1f);
            yield return new WaitForSeconds(t);
            Unlock();
        }



        IEnumerator SymbolOrderer()
        {
            OrderSymbols(ref symbols);
            yield return null;
        }

        

        #endregion


        private void OrderSymbols(ref List<SymbolBase> symbolData)
        {
            symbolData = symbols.OrderBy(order => order.GetSymbolData().SymbolOrder).ToList();
        }


        #endregion

        

    }

}

