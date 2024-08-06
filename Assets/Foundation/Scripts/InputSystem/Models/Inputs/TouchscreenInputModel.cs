using UnityEngine;

namespace Foundation.Scripts.InputSystem.Models.Inputs
{
    internal class TouchscreenInputModel : InputModelBase
    {
        private const int PrimalTouchIndex = 0;

        public TouchscreenInputModel() : base()
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
                CurrentInput = Input.GetTouch(PrimalTouchIndex).position;
            }

            if (Input.touchCount > 0 &&
                Input.GetTouch(PrimalTouchIndex).phase == TouchPhase.Began)
            {
                StartInput();
            }
            else if (IsInputStarted && 
                (Input.GetTouch(PrimalTouchIndex).phase == TouchPhase.Canceled ||
                Input.GetTouch(PrimalTouchIndex).phase == TouchPhase.Ended))
            {
                FinishInput();
            }

            return CurrentInput;
        }
    }
}