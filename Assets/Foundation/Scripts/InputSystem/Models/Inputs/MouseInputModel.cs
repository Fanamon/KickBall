using UnityEngine;

namespace Foundation.Scripts.InputSystem.Models.Inputs
{
    internal class MouseInputModel : InputModelBase
    {
        private const int LeftMouseIndex = 0;

        public MouseInputModel() : base()
        {
            IsPointerSystem = true;
        }

        public override Vector3 GetInput()
        {
            if (IsInputStarted == false)
            {
                CurrentInput = Vector3.zero;
            }
            else
            {
                CurrentInput = Input.mousePosition;
            }

            if (IsInputStarted == false && Input.GetMouseButtonDown(LeftMouseIndex))
            {
                StartInput();
            }
            else if (Input.GetMouseButtonUp(LeftMouseIndex))
            {
                FinishInput();
            }

            return CurrentInput;
        }
    }
}