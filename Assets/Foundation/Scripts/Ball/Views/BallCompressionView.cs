using System.Collections;
using UnityEngine;

namespace Foundation.Scripts.Ball.Views
{
    public class BallCompressionView : MonoBehaviour
    {
        [SerializeField] private Transform _modelTransform;
        [SerializeField] private Transform _modelScaler;

        [SerializeField, Range(0.4f, 0.7f)] private float _maxCompressValue;
        [SerializeField, Min(0f)] private float _compressSpeed;

        private float _maxKickPower;

        private Vector3 _defaultScale;
        private Vector3 _maxCompressScale;

        private Coroutine _scaleChanger;

        public void Initialize(float maxKickPower)
        {
            _maxKickPower = maxKickPower;

            _defaultScale = _modelScaler.localScale;
            _maxCompressScale = new Vector3(_defaultScale.x,
                _defaultScale.y + (1 - _maxCompressValue), _maxCompressValue);
        }

        public void Compress(Vector3 comressDirection, float compressPower)
        {
            if (_scaleChanger != null)
            {
                StopCoroutine(_scaleChanger);
            }

            Quaternion currentModelRotation = _modelTransform.rotation;
            Vector3 targetScale = Vector3.Lerp(_modelScaler.localScale, _maxCompressScale,
                compressPower / _maxKickPower);

            _modelScaler.forward = comressDirection;
            _modelTransform.rotation = currentModelRotation;

            _scaleChanger = StartCoroutine(ChangeScaleAndColor(compressPower, targetScale));
        }

        private IEnumerator ChangeScaleAndColor(float compressPower, Vector3 targetScale)
        {
            float currentCompressPower = 0f;
            float compressSpeed = _compressSpeed * compressPower;

            while (currentCompressPower < compressPower)
            {
                currentCompressPower += Time.deltaTime * compressSpeed;

                _modelScaler.localScale = Vector3.Lerp(_defaultScale, targetScale,
                                currentCompressPower / compressPower);

                yield return null;
            }

            while (currentCompressPower > 0f)
            {
                currentCompressPower -= Time.deltaTime * compressSpeed;

                _modelScaler.localScale = Vector3.Lerp(_defaultScale, targetScale,
                                currentCompressPower / compressPower);

                yield return null;
            }

            _scaleChanger = null;
        }
    }
}