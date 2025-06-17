using TMPro;

namespace PokemonGame.Shared
{
    /// <summary>
    /// Helper methods for safely setting TextMeshProUGUI text values.
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// Sets the text component to the specified string or empty if null.
        /// Does nothing if the text component is null.
        /// </summary>
        /// <param name="text">The TextMeshProUGUI component to update.</param>
        /// <param name="value">The string value to set.</param>
        /// <param name="format">Optional format string (ignored for strings).</param>
        public static void SetText(TextMeshProUGUI text, string value, string format = null)
        {
            if (text == null) 
                return;

            text.text = value ?? string.Empty;
        }

        /// <summary>
        /// Sets the text component to the specified integer value, optionally formatted.
        /// Does nothing if the text component is null.
        /// </summary>
        /// <param name="text">The TextMeshProUGUI component to update.</param>
        /// <param name="value">The integer value to set.</param>
        /// <param name="format">Optional numeric format string.</param>
        public static void SetText(TextMeshProUGUI text, int value, string format = null)
        {
            if (text == null) 
                return;

            text.text = format == null ? value.ToString() : value.ToString(format);
        }

        /// <summary>
        /// Sets the text component to a default "N/A" value.
        /// </summary>
        /// <param name="textComponent">The TextMeshProUGUI component to update.</param>
        public static void SetTextNA(TextMeshProUGUI textComponent)
        {
            SetText(textComponent, "N/A");
        }
    }
}