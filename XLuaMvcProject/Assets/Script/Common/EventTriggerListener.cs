using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// ÊÂ¼þ´¥·¢¼àÌýÆ÷
/// </summary>
public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDoubleClick;
    public VoidDelegate onThreeClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public static EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetOrAddComponent<EventTriggerListener>();
        return listener;
    }
    public static EventTriggerListener Get(Transform trans)
    {
        return Get(trans.gameObject);
    }
    public static EventTriggerListener Get(Button btn)
    {
        return Get(btn.gameObject);
    }
    public static EventTriggerListener Get(Image image)
    {
        return Get(image.gameObject);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount==1)
        {
            if (onClick != null) onClick(gameObject);
        }
        else if (eventData.clickCount == 2)
        {
            if (onDoubleClick != null) onDoubleClick(gameObject);
        }
        else if (eventData.clickCount == 2)
        {
            if (onThreeClick != null) onThreeClick(gameObject);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }
}