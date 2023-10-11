using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Core
{
    public class UIUnityActionHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public const float th_long_click = 0.5f;

        public event UnityAction OnClickHandler = null;
        public event UnityAction OnLongClickHandler = null;

        private bool isPointerDown = false;
        private float pointerDownTimer = 0f;

        public virtual void OnPointerUp(PointerEventData _)
        {
            if (isPointerDown == true)
            {
                OnClickHandler?.Invoke();
            }
            pointerDownTimer = 0f;
            isPointerDown = false;
        }

        public virtual void OnPointerDown(PointerEventData _)
        {
            pointerDownTimer = 0f;
            isPointerDown = true;
        }

        private void Update()
        {
            if (isPointerDown)
            {
                pointerDownTimer += Time.deltaTime;
                if (pointerDownTimer > th_long_click)
                {
                    isPointerDown = false;
                    OnLongClickHandler?.Invoke();
                }
            }
        }
    }
}
