using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkBackButton : MonoBehaviour, IPointerDownHandler,  IPointerUpHandler {

    public bool PlayerMovingBackPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerMovingBackPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerMovingBackPressed = false;
    }
}

