using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickOnScreen : MonoBehaviour, IClickHandler
{
    [SerializeField] private float _timeToClick = 0.2f;
    private Dictionary<int, float> _beganFingers = new Dictionary<int, float>();

    public event Action<Vector2> OnClick;

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                if (!_beganFingers.ContainsKey(touch.fingerId))
                    _beganFingers.Add(touch.fingerId, _timeToClick);
                else
                    _beganFingers[touch.fingerId] = _timeToClick;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (_beganFingers.ContainsKey(touch.fingerId) && _beganFingers[touch.fingerId] > 0f)
                {
                    OnClick?.Invoke(touch.position);
                }
            }
            else
            {
                if (_beganFingers.ContainsKey(touch.fingerId))
                {
                    _beganFingers[touch.fingerId] -= Time.deltaTime;
                    if (_beganFingers[touch.fingerId] <= 0f)
                    {
                        _beganFingers.Remove(touch.fingerId);
                    }
                }
            }
        }
    }
}