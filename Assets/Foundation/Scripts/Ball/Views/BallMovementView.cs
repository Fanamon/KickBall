using UnityEngine;

namespace Foundation.Scripts.Ball.Views
{
    public class BallMovementView : MonoBehaviour
    {
        [SerializeField] private Transform _modelTransform;

        private Transform _transform;

        public void Initialize()
        {
            _transform = transform;
        }

        public void Move(Vector3 velocity)
        {
            Vector3 crossVector = Vector3.Cross(Vector3.up, velocity.normalized);

            _transform.position += velocity * Time.fixedDeltaTime;
            _modelTransform.RotateAround(_modelTransform.position, crossVector, velocity.magnitude);
        }
    }
}