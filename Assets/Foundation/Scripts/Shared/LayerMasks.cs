using UnityEngine;

namespace Foundation.Scripts.Shared
{
    public static class LayerMasks
    {
        private const string Ball = nameof(Ball);
        private const string Floor = nameof(Floor);
        private const string Wall = nameof(Wall);

        public static LayerMask BallLayer => LayerMask.GetMask(Ball);
        public static LayerMask FloorLayer => LayerMask.GetMask(Floor);
        public static LayerMask WallLayer => LayerMask.GetMask(Wall);
    }
}