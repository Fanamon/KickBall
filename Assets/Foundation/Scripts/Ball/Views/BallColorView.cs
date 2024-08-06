using System.Collections;
using UnityEngine;

namespace Foundation.Scripts.Ball.Views
{
    public class BallColorView : MonoBehaviour
    {
        [SerializeField] private Renderer _ballRenderer;

        [SerializeField, Min(0f)] private float _changeColorSpeed;

        [SerializeField] private Color _maxCompressColor;

        private Material _ballMaterial;

        private float _maxKickPower;

        private Color _defaultMaterialColor;
        private Coroutine _colorChanger;

        public void Initialize(float maxKickPower)
        {
            _maxKickPower = maxKickPower;

            _ballMaterial = _ballRenderer.material;
            _defaultMaterialColor = _ballMaterial.color;
        }

        public void SetCompressColor(float compressPower)
        {
            if (_colorChanger != null)
            {
                StopCoroutine(_colorChanger);
            }

            Color targetColor = Color.Lerp(_defaultMaterialColor, _maxCompressColor,
                compressPower / _maxKickPower);

            _colorChanger = StartCoroutine(ChangeColor(compressPower, targetColor));
        }

        private IEnumerator ChangeColor(float compressPower, Color targetColor)
        {
            float currentCompressPower = 0f;
            float changeColorSpeed = _changeColorSpeed * compressPower;

            _ballMaterial.color = targetColor;

            while (currentCompressPower < compressPower)
            {
                currentCompressPower += Time.deltaTime * changeColorSpeed;

                yield return null;
            }

            while (currentCompressPower > 0f)
            {
                currentCompressPower -= Time.deltaTime * changeColorSpeed;

                _ballMaterial.color = Color.Lerp(_defaultMaterialColor, targetColor,
                    currentCompressPower / compressPower);

                yield return null;
            }

            _colorChanger = null;
        }
    }
}