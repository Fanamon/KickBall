using UnityEngine;

namespace Foundation.Scripts.ControlSystem.Configs
{
    [CreateAssetMenu(fileName = "ControllerConfig", menuName = "Foundation/ControllerConfig", 
        order = 1)]
    public class ControllerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)] public float MinInputValue { get; private set; }
        [field: SerializeField, Min(0f)] public float MaxInputValue { get; private set; }

        private void OnValidate()
        {
            MaxInputValue = Mathf.Max(MaxInputValue, MinInputValue);
        }
    }
}