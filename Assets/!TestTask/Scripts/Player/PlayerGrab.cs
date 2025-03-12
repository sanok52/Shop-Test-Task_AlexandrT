using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private Button _buttonThrow;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private float _throwForce = 5f;
    private Grasped _propInHand;
    private Rigidbody _propRg;

    public bool IsCanGrap => _propInHand == null;

    private void Start()
    {
        _buttonThrow.onClick.AddListener(() => Throw());
        UpdateThrowButtonState();
    }

    internal bool TryGrapProp(Grasped grapable)
    {
        if (IsCanGrap == false)
            return false;

        _propInHand = grapable;
        Grab(grapable);
        UpdateThrowButtonState();
        return true;
    }

    private void Grab(Grasped prop)
    {
        _propInHand = prop;
        _propInHand.transform.position = _holdPoint.position;
        _propInHand.transform.rotation = _holdPoint.rotation;
        _propInHand.transform.parent = _holdPoint;
        SetGraspedState(prop.gameObject, true);
    }

    private void SetGraspedState (GameObject propGO, bool isGrapProp)
    {
        if (propGO.TryGetComponent(out Collider collider))
        {
            collider.isTrigger = isGrapProp;
        }
        if (_propRg != null || propGO.TryGetComponent(out _propRg))
        {
            _propRg.isKinematic = isGrapProp;
            _propRg.useGravity = !isGrapProp;         
        }
    }

    public void Throw()
    {
        if (_propInHand == null)        
            return;        

        ThrowGO(_propInHand.gameObject);
        _propInHand = null;
        _propRg = null;
        UpdateThrowButtonState();
    }

    private void ThrowGO(GameObject prop)
    {
        SetGraspedState(prop, false);
        _propInHand.transform.parent = null;
        prop.transform.position = _throwPoint.position;
        prop.transform.rotation = _throwPoint.rotation;
        if (_propRg)        
            _propRg.AddForce(_throwPoint.forward * _throwForce, ForceMode.Impulse);   
    }

    private void UpdateThrowButtonState ()
    {
        _buttonThrow.gameObject.SetActive(_propInHand != null);
    }
}