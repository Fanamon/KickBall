using UnityEngine;

namespace Foundation.Scripts.Movement.Models
{
    public class CustomPhysicsModel
    {
        public Vector3 Velocity { get; private set; }

        public void UpdateVelocity(float dampenValue)
        {
            Velocity *= Mathf.Pow(dampenValue, Time.fixedDeltaTime);
        }

        public void AddForce(Vector3 forceValue)
        {
            Velocity += forceValue;
        }

        public void Reflect(Vector3 contactNormalPosition)
        {
            Velocity = Vector3.Reflect(Velocity, contactNormalPosition);
        }
    }
}