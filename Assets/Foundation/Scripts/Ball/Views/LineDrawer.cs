using UnityEngine;

namespace Foundation.Scripts.Ball.Views
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineDrawer : MonoBehaviour
    {
        private const int StartPositionIndex = 0;
        private const int EndPositionIndex = 1;

        private const float LinePositionYOffset = 0.1f;

        private Transform _transform;
        private LineRenderer _lineRenderer;

        public void Initialize()
        {
            _transform = transform;
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public bool TryDrawLine(Vector3 currentInput, bool isCurrentInputSystemNull, 
            bool isCurrentInputMagnitudeGreaterMinValue)
        {
            if (isCurrentInputSystemNull == false &&
                isCurrentInputMagnitudeGreaterMinValue)
            {
                SetLine(_transform.position + currentInput);

                if (gameObject.activeSelf == false)
                {
                    SetLine(_transform.position);
                    gameObject.SetActive(true);
                }
            }
            else if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }

            return gameObject.activeSelf;
        }

        private void SetLine(Vector3 endPosition)
        {
            Vector3 startPosition = new Vector3(_transform.position.x, 
                _transform.position.y + LinePositionYOffset, _transform.position.z);
            Vector3 newPosition = new Vector3(endPosition.x, startPosition.y, endPosition.z);

            _lineRenderer.SetPosition(StartPositionIndex, startPosition);
            _lineRenderer.SetPosition(EndPositionIndex, newPosition);
        }
    }
}