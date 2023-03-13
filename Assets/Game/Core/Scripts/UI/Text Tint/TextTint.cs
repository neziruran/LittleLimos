using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class TextTint : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        [SerializeField] private TMP_Text text;
        

        public void OnPointerDown(PointerEventData eventData)
        { 
            text.color = Color.grey;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            text.color = Color.white;
        }
    }

}
