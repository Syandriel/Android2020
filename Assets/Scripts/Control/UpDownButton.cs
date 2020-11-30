using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[AddComponentMenu("UI/UpDownButton", 30)]
public class UpDownButton : Selectable, IPointerDownHandler, IPointerUpHandler {

    public class ButtonDownEvent : UnityEvent { }
    public class ButtonUpEvent : UnityEvent { }


    [SerializeField]
    private ButtonDownEvent m_OnButtonDown = new ButtonDownEvent();
    [SerializeField]
    private ButtonUpEvent m_OnButtonUp = new ButtonUpEvent();

    protected UpDownButton() { }

    public ButtonDownEvent onButtonDown {
        get { return m_OnButtonDown; }
        set { m_OnButtonDown = value; }
    }

    public ButtonUpEvent onButtonUp {
        get { return m_OnButtonUp; }
        set {m_OnButtonUp = value; }
    }

    public virtual void OnPointerDown(PointerEventData eventData) {

        if(eventData.button != PointerEventData.InputButton.Left) 
            return;
        
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("UpDownButton.onPointerDown", this);
        m_OnButtonDown.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData) {
        
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("UpDownButton.opPointerUp", this);
        m_OnButtonUp.Invoke();

    }



}
