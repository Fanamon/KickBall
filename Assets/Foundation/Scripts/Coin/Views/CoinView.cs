using Foundation.Scripts.Ball.Views;
using System;
using System.Collections;
using UnityEngine;

namespace Foundation.Scripts.Coin.Views
{
    [RequireComponent(typeof(Collider))]
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private Transform _modelTransform;
        [SerializeField] private Transform _coinModelTransform;
        [SerializeField] private ParticleSystem _explosionEffect;

        [SerializeField, Min(0f)] private float _rotationAngle;
        [SerializeField, Min(0)] private int _value;
        [SerializeField, Min(0f)] private float _reduceSizeTime;
        [SerializeField, Min(1f)] private float _reduceSizeSpeed;

        private Collider _collider;

        private readonly Vector3 _targetSize = new Vector3(0.1f, 0.1f, 0.1f);

        public event Action<int> Taken;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Update()
        {
            _coinModelTransform.Rotate(Vector3.up, _rotationAngle * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallView _))
            {
                Taken?.Invoke(_value);
                _collider.enabled = false;

                StartCoroutine(BlastCoin());
            }
        }

        public int Take()
        {
            _collider.enabled = false;

            StartCoroutine(BlastCoin());

            return _value;
        }

        private IEnumerator BlastCoin()
        {
            float reduceSizeCounter = 0f;
            Vector3 defaultScale = _modelTransform.localScale;

            while (reduceSizeCounter <= _reduceSizeTime)
            {
                reduceSizeCounter += Time.deltaTime * _reduceSizeSpeed;

                _modelTransform.localScale = Vector3.Lerp(defaultScale, _targetSize, 
                    reduceSizeCounter / _reduceSizeTime);

                yield return null;
            }

            _modelTransform.gameObject.SetActive(false);
            _explosionEffect.Play();
        }
    }
}