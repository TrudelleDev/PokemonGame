using System;
using UnityEngine;

namespace PokemonGame.Utilities
{
    /// <summary>
    /// Represents rectangular padding values for UI layout elements.
    /// Defines space between the content and the edges of a container.
    /// </summary>
    [Serializable]
    public struct RectPadding
    {
        [SerializeField, Min(0)]
        [Tooltip("Distance from the left edge of the container.")]
        private float left;

        [SerializeField, Min(0)]
        [Tooltip("Distance from the right edge of the container.")]
        private float right;

        [SerializeField, Min(0)]
        [Tooltip("Distance from the top edge of the container.")]
        private float top;

        [SerializeField, Min(0)]
        [Tooltip("Distance from the bottom edge of the container.")]
        private float bottom;

        /// <summary>
        /// Gets the padding on the left side.
        /// </summary>
        public readonly float Left => left;

        /// <summary>
        /// Gets the padding on the right side.
        /// </summary>
        public readonly float Right => right;

        /// <summary>
        /// Gets the padding on the top side.
        /// </summary>
        public readonly float Top => top;

        /// <summary>
        /// Gets the padding on the bottom side.
        /// </summary>
        public readonly float Bottom => bottom;

        /// <summary>
        /// Applies this padding to a specified <see cref="RectTransform"/>.
        /// This adjusts <see cref="RectTransform.offsetMin"/> and <see cref="RectTransform.offsetMax"/>
        /// based on the defined left, right, top, and bottom padding values.
        /// </summary>
        /// <param name="rect">The RectTransform to modify.</param>
        public void ApplyTo(RectTransform rect)
        {
            if (rect == null)
            {
                return;
            }

            // offsetMin = lower-left corner (x = left, y = bottom)
            // offsetMax = upper-right corner (x = -right, y = -top)
            rect.offsetMin = new Vector2(left, bottom);
            rect.offsetMax = new Vector2(-right, -top);
        }
    }
}
