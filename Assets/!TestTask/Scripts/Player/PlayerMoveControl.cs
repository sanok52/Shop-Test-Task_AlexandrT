using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMoveControl : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _minMaxCameraRotate = new Vector2(-40f, 40f);
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector2 _speedRotate = new Vector2(5f, 5f);

    [Inject]
    public void Init (IMoveHandler moveHandler, IRotateHandler rotateHandler)
    {
        _controller = GetComponent<CharacterController>();
        moveHandler.OnMove += Move;
        rotateHandler.OnRotate += Rotate;
    }

    public void Move(Vector2 moveVector)
    {
        _controller.Move(_controller.transform.TransformDirection(new Vector3(moveVector.x, 0f, moveVector.y)) * _speed);
    }

    internal void Rotate(Vector2 inputVector)
    {
        _controller.transform.Rotate(0, inputVector.x * _speedRotate.x, 0, Space.Self);
        _camera.transform.Rotate(inputVector.y * _speedRotate.y, 0, 0, Space.Self);

        float x = _camera.transform.localRotation.eulerAngles.x;
        if (x > _minMaxCameraRotate.y && x < (360 + _minMaxCameraRotate.x))
        {
            float deltaX = (360 + _minMaxCameraRotate.x) - x;
            float deltaY = x - _minMaxCameraRotate.y;
            x = deltaX > deltaY ? _minMaxCameraRotate.y : (360 + _minMaxCameraRotate.x);
            _camera.transform.localRotation = Quaternion.Euler(x,
                _camera.transform.localRotation.eulerAngles.y, _camera.transform.localRotation.eulerAngles.z);
        }
    }
}
