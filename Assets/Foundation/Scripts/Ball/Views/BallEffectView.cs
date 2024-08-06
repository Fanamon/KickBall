using UnityEngine;

namespace Foundation.Scripts.Ball.Views
{
    public class BallEffectView : MonoBehaviour
    {
        private const int SpeedLinesEffectBurstIndex = 0;
        private const float SpeedLinesEffectBurstTime = 0f;
        private const short SpeedLinesEffectBurstCountOffset = 5;

        [SerializeField] private ParticleSystem _compressEffect;
        [SerializeField] private ParticleSystem _speedLinesEffect;

        [SerializeField, Min(0f)] private int _minSpeedLinesCount;
        [SerializeField, Min(0f)] private int _maxSpeedLinesCount;

        private float _maxKickPower;

        public void Initialize(float maxKickPower)
        {
            _maxKickPower = maxKickPower;
        }

        public void InvokeWallContactEffect(float compressPower)
        {
            short targetLinesCount = (short)Mathf.Lerp(_minSpeedLinesCount, _maxSpeedLinesCount,
                compressPower / _maxKickPower);

            _speedLinesEffect.emission.SetBurst(SpeedLinesEffectBurstIndex,
                new ParticleSystem.Burst(SpeedLinesEffectBurstTime, targetLinesCount,
                (short)(targetLinesCount + SpeedLinesEffectBurstCountOffset)));
            _compressEffect.Play();
        }

        private void OnValidate()
        {
            _maxSpeedLinesCount = Mathf.Max(_maxSpeedLinesCount, _minSpeedLinesCount);
        }
    }
}