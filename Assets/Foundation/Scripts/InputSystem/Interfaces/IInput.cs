using System;
using UnityEngine;

namespace Foundation.Scripts.InputSystem.Interfaces
{
    public interface IInput
    {
        Vector3 GetInput();

        event Action<IInput> InputStarted;
        event Action InputFinished;

        bool IsPointerSystem { get; }
        bool IsInputStarted { get; }
    }
}