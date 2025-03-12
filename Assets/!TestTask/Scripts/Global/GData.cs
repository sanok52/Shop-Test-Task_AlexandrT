using UnityEngine;
using System;
using UnityEngine.Events;

public interface IInteractive
{
    public bool Click();
}

public interface IMoveHandler
{
    public event Action<Vector2> OnMove;
}

public interface IRotateHandler
{
    public event Action<Vector2> OnRotate;
}

public interface IClickHandler
{
    public event Action<Vector2> OnClick;
}