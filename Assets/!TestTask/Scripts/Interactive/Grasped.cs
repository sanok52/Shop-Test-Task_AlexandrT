using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Grasped : MonoBehaviour, IInteractive
{
    [SerializeField] private ItemData _dataRef;
    [Inject] private PlayerGrab _playerGrab;

    public ItemData DataRef { get => _dataRef; }

    public bool Click()
    {
        if (_playerGrab.TryGrapProp(this))
        {
            Debug.Log($"Grab {gameObject.name}");
            return true;
        }
        Debug.Log($"NOT Grab {gameObject.name}");
        return false;
    }
}

