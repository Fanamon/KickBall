using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Foundation.Scripts.WalletSystem.Views
{
    public class WalletView : MonoBehaviour
    {
        private const float CoinsIncreaseScaleMultiplier = 1.5f;
        private const float CoinsScalingTime = 0.035f;
        private const int CoinsIncreaseStep = 1;

        [SerializeField] private RectTransform _coinsText;
        [SerializeField] private TMP_Text _coinsCountText;

        [SerializeField, Min(1f)] private float _coinsIncreaseSpeed;

        private Vector3 _defaultScale;
        private Vector3 _targetScale;

        private Coroutine _coinsTextChanger;
        private Coroutine _coinsScaleChanger;

        public void Initialize()
        {
            _defaultScale = _coinsText.localScale;
            _targetScale = _defaultScale * CoinsIncreaseScaleMultiplier;
        }

        public void UpdateCoinsCount(int coinsCount)
        {
            Int32.TryParse(_coinsCountText.text, out int currentCoins);

            if (_coinsTextChanger != null)
            {
                StopCoroutine(_coinsTextChanger);
            }

            _coinsTextChanger = StartCoroutine(ChangeCoinsText(currentCoins, coinsCount));
        }

        private IEnumerator ChangeCoinsText(int currentCoinsCount, int targetCoinsCount)
        {
            float stepCount = 0f;

            while (currentCoinsCount < targetCoinsCount)
            {
                stepCount += Time.deltaTime * _coinsIncreaseSpeed;

                if (stepCount >= CoinsIncreaseStep)
                {
                    stepCount = 0f;
                    currentCoinsCount += CoinsIncreaseStep;

                    if (_coinsScaleChanger != null)
                    {
                        StopCoroutine(_coinsScaleChanger);
                        _coinsCountText.text = currentCoinsCount.ToString();
                    }

                    _coinsScaleChanger = StartCoroutine(ChangeTextScale(currentCoinsCount));
                }

                yield return null;
            }

            _coinsTextChanger = null;
        }

        private IEnumerator ChangeTextScale(int currentCoins)
        {
            float scalingTimer = 0f;
            Vector3 currentScale = _coinsText.localScale;

            while (scalingTimer <= CoinsScalingTime)
            {
                scalingTimer += Time.deltaTime;

                _coinsText.localScale = Vector3.Lerp(currentScale, _targetScale, 
                    scalingTimer / CoinsScalingTime);

                yield return null;
            }

            scalingTimer = 0f;
            currentScale = _coinsText.localScale;
            _coinsCountText.text = currentCoins.ToString();

            while (scalingTimer <= CoinsScalingTime)
            {
                scalingTimer += Time.deltaTime;

                _coinsText.localScale = Vector3.Lerp(currentScale, _defaultScale,
                    scalingTimer / CoinsScalingTime);

                yield return null;
            }

            _coinsText.localScale = _defaultScale;
            _coinsScaleChanger = null;
        }
    }
}