using Foundation.Scripts.InputSystem.Configs;
using Foundation.Scripts.InputSystem.Interfaces;
using Foundation.Scripts.InputSystem.Models.Inputs;
using UnityEngine;

namespace Foundation.Scripts.InputSystem.Factories
{
    public class InputFactory
    {
        private InputConfig _inputConfig;

        private float _maxInputValue;

        public InputFactory(InputConfig inputConfig, 
            float maxInputValue)
        {
            _inputConfig = inputConfig;
            _maxInputValue = maxInputValue;
        }

        public IInput[] GetInputSystem()
        {
            IInput[] inputSystem =
            {
                new MouseInputModel(),
                new KeyboardInputModel(_inputConfig.KeyboardInputSpeed,
                _maxInputValue)
            };

            if (Application.isMobilePlatform)
            {
                inputSystem = new IInput[]
                {
                    new TouchscreenInputModel()
                };
            }

            return inputSystem;
        }
    }
}