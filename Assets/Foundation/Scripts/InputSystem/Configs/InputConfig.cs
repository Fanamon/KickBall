using UnityEngine;

namespace Foundation.Scripts.InputSystem.Configs
{
    [CreateAssetMenu(fileName = "InputConfigs", menuName = "Foundation/InputConfigs", 
        order = 0)]
    public class InputConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)] public float KeyboardInputSpeed { get; private set; }
    }
}