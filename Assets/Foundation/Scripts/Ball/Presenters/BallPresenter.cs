using Foundation.Scripts.Ball.Views;
using Foundation.Scripts.Movement.Configs;
using Foundation.Scripts.Movement.Models;
using System;
using UnityEngine;

namespace Foundation.Scripts.Ball.Presenters
{
    public class BallPresenter : IDisposable
    {
        private BallView _ballView;
        private BallMovementView _movementView;
        private BallCompressionView _compressionView;
        private BallColorView _colorView;
        private BallEffectView _effectView;

        private CustomPhysicsModel _customPhysicsModel;

        private MovementConfig _movementConfig;

        public BallPresenter(BallView ballView, BallMovementView ballMovementView, 
            BallCompressionView ballCompressionView, BallColorView ballColorView, 
            BallEffectView ballEffectView, MovementConfig movementConfig)
        {
            _ballView = ballView;
            _movementView = ballMovementView;
            _compressionView = ballCompressionView;
            _colorView = ballColorView;
            _effectView = ballEffectView;

            _customPhysicsModel = new CustomPhysicsModel();
            _movementConfig = movementConfig;

            _ballView.WallContacted += OnWallContacted;
        }

        public void Dispose()
        {
            _ballView.WallContacted -= OnWallContacted;
        }

        public void UpdateMovement()
        {
            _customPhysicsModel.UpdateVelocity(_movementConfig.DampenValue);

            _movementView.Move(_customPhysicsModel.Velocity);
        }

        public void Kick(Vector3 inputValue)
        {
            _customPhysicsModel.AddForce(inputValue * _movementConfig.KickPower);
        }

        private void OnWallContacted(Vector3 contactNormalPosition)
        {
            var compressPower = _customPhysicsModel.Velocity.magnitude;

            _compressionView.Compress(contactNormalPosition.normalized,
                compressPower);
            _colorView.SetCompressColor(compressPower);
            _effectView.InvokeWallContactEffect(compressPower);

            _customPhysicsModel.Reflect(contactNormalPosition);
        }
    }
}