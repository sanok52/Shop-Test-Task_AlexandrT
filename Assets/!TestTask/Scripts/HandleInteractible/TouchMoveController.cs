using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMoveController : MonoBehaviour, IRotateHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlayerMoveControl _playerTest;
    [SerializeField] private RectTransform _background;
    [SerializeField] private float _offsetSensitivity = 1f;
    [SerializeField] private float _offsetLimit = 1f;
    [SerializeField] private bool isInverseX;
    [SerializeField] private bool isInverseY;
    private Vector2 _delta;
    private Vector2 _previousDelta;

    private Vector2 _inputVector = Vector2.zero;

    public event Action<Vector2> OnRotate;

    void Update()
    {
        if (Mathf.Abs(_previousDelta.sqrMagnitude - _delta.sqrMagnitude) > 0.2f)
        {
            OnRotate?.Invoke(_inputVector * Time.deltaTime);
            _previousDelta = _delta;
        }
    }

    private void SetInputVector(Vector2 inputVector)
    {
        inputVector = new Vector2(isInverseX ? -inputVector.x : inputVector.y, isInverseY ? -inputVector.y : inputVector.y);
        inputVector = Vector2.ClampMagnitude(inputVector, _offsetLimit);
        _inputVector = inputVector;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        SetInputVector(Vector2.zero); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        _delta = eventData.delta;
        if (Mathf.Abs(_previousDelta.sqrMagnitude - _delta.sqrMagnitude) > 0.2f)
        {
            SetInputVector(eventData.delta * _offsetSensitivity / eventData.clickTime);
        }
        else
            SetInputVector(Vector2.zero);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetInputVector(Vector2.zero);
    }
}
