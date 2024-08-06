using Foundation.Scripts.Environment.Views;
using System;
using UnityEngine;

namespace Foundation.Scripts.Ball.Views
{
    public class BallView : MonoBehaviour
    {
        public event Action<Vector3> WallContacted;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out WallView _))
            {
                WallContacted?.Invoke(collision.contacts[0].normal);
            }
        }
    }
}