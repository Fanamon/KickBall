using Foundation.Scripts.Ball.Presenters;
using Foundation.Scripts.Ball.Views;
using Foundation.Scripts.ControlSystem.Configs;
using Foundation.Scripts.InputSystem.Configs;
using Foundation.Scripts.InputSystem.Factories;
using Foundation.Scripts.InputSystem.Interfaces;
using Foundation.Scripts.Shared;
using System;
using UnityEngine;

namespace Foundation.Scripts.ControlSystem.Controllers
{
    public class InputController : IDisposable
    {
        private const float MaxRayDistance = 100f;

        private Camera _camera;
        private LineDrawer _lineDrawer;
        private BallPresenter _ballMovement;

        private InputConfig _inputConfig;
        private ControllerConfig _controllerConfig;

        private Vector3 _currentInput;

        private IInput _currentInputSystem;
        private IInput[] _inputs;

        public InputController(Camera camera, LineDrawer lineDrawer,
            BallPresenter ballMovement, InputConfig inputConfig, 
            ControllerConfig controllerConfig)
        {
            _camera = camera;
            _lineDrawer = lineDrawer;
            _ballMovement = ballMovement;
            _inputConfig = inputConfig;
            _controllerConfig = controllerConfig;

            InitializeInputSystem();
        }

        public void Dispose()
        {
            foreach (var input in _inputs)
            {
                input.InputStarted -= OnInputStarted;
                input.InputFinished -= OnInputFinished;
            }
        }

        public void UpdateInput()
        {
            if (_currentInputSystem == null)
            {
                foreach(var input in _inputs)
                {
                    input.GetInput();

                    if (input.IsInputStarted)
                    {
                        break;
                    }
                }
            }
            else
            {
                _currentInput = GetCurrentInput();
            }

            _lineDrawer.TryDrawLine(_currentInput, _currentInputSystem == null, 
                _currentInput.magnitude > _controllerConfig.MinInputValue);
        }

        private void InitializeInputSystem()
        {
            InputFactory inputFactory = new InputFactory(_inputConfig, 
                _controllerConfig.MaxInputValue);

            _inputs = inputFactory.GetInputSystem();
            _currentInputSystem = null;

            foreach (var input in _inputs)
            {
                input.InputStarted += OnInputStarted;
                input.InputFinished += OnInputFinished;
            }
        }

        private void OnInputStarted(IInput input)
        {
            if (input.IsPointerSystem && TryGetPointerRayHit(out RaycastHit hitInfo,
                    input.GetInput(), MaxRayDistance, LayerMasks.BallLayer))
            {
                _currentInputSystem = input;
            }
            else if (input.IsPointerSystem == false)
            {
                _currentInputSystem = input;
            }
        }

        private void OnInputFinished()
        {
            if (_currentInputSystem != null && 
                _currentInput.magnitude > _controllerConfig.MinInputValue)
            {
                _ballMovement.Kick(_currentInput);
            }

            _currentInputSystem = null;
        }

        private Vector3 GetCurrentInput()
        {
            Vector3 newInput = Vector3.zero;

            try
            {
                if (_currentInputSystem.IsPointerSystem &&
                    TryGetPointerRayHit(out RaycastHit hitInfo,
                    _currentInputSystem.GetInput(), MaxRayDistance, LayerMasks.FloorLayer))
                {
                    Vector3 ballPosition = _lineDrawer.transform.position;

                    newInput = new Vector3(hitInfo.point.x - ballPosition.x,
                        hitInfo.point.z - ballPosition.z, 0);
                    newInput = Vector3.ClampMagnitude(newInput, _controllerConfig.MaxInputValue);
                }
                else if (_currentInputSystem.IsPointerSystem == false)
                {
                    newInput = _currentInputSystem.GetInput();
                }
            }
            catch (NullReferenceException) { }

            return new Vector3(newInput.x, 0, newInput.y);
        }

        private bool TryGetPointerRayHit(out RaycastHit hitInfo, Vector3 pointerPosition, 
            float maxDistance, LayerMask layerMask)
        {
            Ray ray = _camera.ScreenPointToRay(pointerPosition);

            return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask);
        }
    }
}