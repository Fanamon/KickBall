using Foundation.Scripts.InputSystem.Interfaces;
using System;
using UnityEngine;

namespace Foundation.Scripts.InputSystem.Models
{
    internal abstract class InputModelBase : IInput
    {
        protected Vector3 CurrentInput;

        public bool IsPointerSystem { get; protected set; }
        public bool IsInputStarted { get; protected set; }

        public event Action<IInput> InputStarted;
        public event Action InputFinished;

        public InputModelBase()
        {
            CurrentInput = Vector3.zero;
            IsInputStarted = false;
            IsPointerSystem = false;
        }

        protected void StartInput()
        {
            IsInputStarted = true;
            InputStarted?.Invoke(this);
        }

        protected void FinishInput()
        {
            InputFinished?.Invoke();
            IsInputStarted = false;
        }

        public abstract Vector3 GetInput();
    }
}