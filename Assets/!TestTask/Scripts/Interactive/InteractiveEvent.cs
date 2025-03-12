using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveEvent : MonoBehaviour, IInteractive
{
    [SerializeField] private UnityEvent _onClick;

    public bool Click()
    {
        _onClick?.Invoke();
        return true;
    }
}
