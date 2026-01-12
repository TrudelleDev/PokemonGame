using UnityEngine;

namespace PokemonGame.Utilities
{
    internal static class VisionGizmoDrawer
    {
        public static void DrawBoxCast(
            Vector2 origin,
            Vector2 direction,
            float distance,
            Vector2 boxSize,
            Color color)
        {
            Gizmos.color = color;

            Vector2 startCenter = origin;
            Vector2 endCenter = origin + direction * distance;

            DrawBox(startCenter, boxSize);
            DrawBox(endCenter, boxSize);

            Vector2 half = boxSize * 0.5f;

            Gizmos.DrawLine(startCenter + new Vector2(half.x, half.y), endCenter + new Vector2(half.x, half.y));
            Gizmos.DrawLine(startCenter + new Vector2(-half.x, half.y), endCenter + new Vector2(-half.x, half.y));
            Gizmos.DrawLine(startCenter + new Vector2(half.x, -half.y), endCenter + new Vector2(half.x, -half.y));
            Gizmos.DrawLine(startCenter + new Vector2(-half.x, -half.y), endCenter + new Vector2(-half.x, -half.y));
        }

        private static void DrawBox(Vector2 center, Vector2 size)
        {
            Vector2 half = size * 0.5f;

            Vector3 tl = center + new Vector2(-half.x, half.y);
            Vector3 tr = center + new Vector2(half.x, half.y);
            Vector3 br = center + new Vector2(half.x, -half.y);
            Vector3 bl = center + new Vector2(-half.x, -half.y);

            Gizmos.DrawLine(tl, tr);
            Gizmos.DrawLine(tr, br);
            Gizmos.DrawLine(br, bl);
            Gizmos.DrawLine(bl, tl);
        }
    }
}
