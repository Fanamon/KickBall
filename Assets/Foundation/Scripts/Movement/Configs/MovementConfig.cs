using UnityEngine;

namespace Foundation.Scripts.Movement.Configs
{
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "Foundation/MovementConfig", 
        order = 2)]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)] public float KickPower { get; private set; } 
        [field: SerializeField, Range(0f, 0.999999f)] public float DampenValue { get; private set; }
    }
}