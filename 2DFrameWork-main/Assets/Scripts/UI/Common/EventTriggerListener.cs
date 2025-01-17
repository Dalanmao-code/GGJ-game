using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler 
    {
        public Action<PointerEventData> onClick;
        public Action<PointerEventData> onPointDown;
        public Action<PointerEventData> onPointUp;
        public Action<PointerEventData> onBeginDrag;
        public Action<PointerEventData> onDrag;
        public Action<PointerEventData> onEndDrag;

        public static EventTriggerListener Get(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null) listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(eventData);
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            onPointDown?.Invoke(eventData);
        }
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            onPointUp?.Invoke(eventData);
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(eventData);
        }
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(eventData);
        }
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(eventData);
        }
    }
}