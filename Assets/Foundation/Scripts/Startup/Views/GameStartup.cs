using Foundation.Scripts.Ball.Views;
using Foundation.Scripts.ControlSystem.Configs;
using Foundation.Scripts.ControlSystem.Controllers;
using Foundation.Scripts.InputSystem.Configs;
using Foundation.Scripts.Movement.Configs;
using Foundation.Scripts.Ball.Presenters;
using UnityEngine;
using System;

namespace Foundation.Scripts.Startup.Views
{
    internal class GameStartup : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LineDrawer _lineDrawer;
        [SerializeField] private BallView _ballView;
        [SerializeField] private BallMovementView _ballMovementView;
        [SerializeField] private BallCompressionView _ballCompressionView;
        [SerializeField] private BallColorView _ballColorView;
        [SerializeField] private BallEffectView _ballEffectView;

        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private ControllerConfig _controllerConfig;
        [SerializeField] private MovementConfig _movementConfig;

        private BallPresenter _ballPresenter;
        private InputController _inputController;

        private IDisposable[] _disposables;

        private void Awake()
        {
            Initialize();

            _ballPresenter = new BallPresenter(_ballView, _ballMovementView, 
                _ballCompressionView, _ballColorView, _ballEffectView, _movementConfig);
            _inputController = new InputController(_camera, _lineDrawer, 
                _ballPresenter, _inputConfig, _controllerConfig);

            InitializeDisposables();
        }

        private void Update()
        {
            _inputController.UpdateInput();
        }

        private void FixedUpdate()
        {
            _ballPresenter.UpdateMovement();
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        private void Initialize()
        {
            var maxKickPower = _movementConfig.KickPower * _controllerConfig.MaxInputValue;

            _lineDrawer.Initialize();
            _lineDrawer.gameObject.SetActive(false);

            _ballMovementView.Initialize();
            _ballCompressionView.Initialize(maxKickPower);
            _ballColorView.Initialize(maxKickPower);
            _ballEffectView.Initialize(maxKickPower);
        }

        private void InitializeDisposables()
        {
            _disposables = new IDisposable[]
            {
                _ballPresenter,
                _inputController
            };
        }
    }
}