using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Interactor : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _dist = 15f;
    [SerializeField] private LayerMask _layers;
    private IClickHandler _clickHandler;

    [Inject]
    public void Init(IClickHandler clickHandler)
    {
        _camera = Camera.main;
        _clickHandler = clickHandler;
        _clickHandler.OnClick += TryInteractive;
    }

    public void TryInteractive (Vector2 point)
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(point), out RaycastHit hitInfo, _dist, _layers))
        {
            if (hitInfo.transform.TryGetComponent(out IInteractive interactive))
            {
                interactive.Click();
            }
        }
    }

    private void OnDestroy()
    {
        _clickHandler.OnClick -= TryInteractive;
    }
}
