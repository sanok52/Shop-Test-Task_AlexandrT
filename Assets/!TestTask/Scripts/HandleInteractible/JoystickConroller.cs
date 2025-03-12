using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class JoystickConroller : MonoBehaviour, IMoveHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;
    [SerializeField] private float _offset = 1f;
    [SerializeField] private float _offsetLimit = 1f;

    private Vector2 _inputVector = Vector2.zero;

    public event Action<Vector2> OnMove;

    void Update()
    {
        if(_inputVector.sqrMagnitude > 0)
          OnMove.Invoke(_inputVector * Time.deltaTime);
    }

    private void SetInputVector(Vector2 inputVector)
    {
        inputVector = Vector2.ClampMagnitude(inputVector, _offsetLimit);
        _inputVector = inputVector;
        _handle.anchoredPosition = _inputVector * (_background.sizeDelta.x / 2f) * _offset;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPosition = eventData.position - (Vector2)_background.position;
        SetInputVector(dragPosition / (_background.sizeDelta.x / 2f));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetInputVector(Vector2.zero);
    }
}
