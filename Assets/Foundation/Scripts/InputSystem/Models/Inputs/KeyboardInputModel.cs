using Foundation.Scripts.Shared;
using UnityEngine;

namespace Foundation.Scripts.InputSystem.Models.Inputs
{
    internal class KeyboardInputModel : InputModelBase
    {
        private float _inputSpeed;
        private float _maxMagnitude;

        public KeyboardInputModel(float inputSpeed, float maxMagnitude) : base()
        {
            _inputSpeed = inputSpeed;
            _maxMagnitude = maxMagnitude;
        }

        public override Vector3 GetInput()
        {
            if (IsInputStarted)
            {
                var horizontal = Input.GetAxis(Axis.Horizontal);
                var vertical = Input.GetAxis(Axis.Vertical);

                CurrentInput += (Vector3.right * horizontal + Vector3.up * vertical) * 
                    Time.deltaTime * _inputSpeed;
                CurrentInput = Vector3.ClampMagnitude(CurrentInput, _maxMagnitude);
            }
            else
            {
                CurrentInput = Vector3.zero;
            }

            if (IsInputStarted == false && CheckKeyPush())
            {
                StartInput();
            }
            else if (CheckKeyPush() == false && IsInputStarted)
            {
                FinishInput();
            }

            return CurrentInput;
        }

        private bool CheckKeyPush()
        {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S);
        }
    }
}