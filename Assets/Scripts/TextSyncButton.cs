using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A Button that automatically tints its label text
/// according to the button's ColorBlock.
/// </summary>
public class TextSyncButton : Button
{
    [SerializeField] private TextMeshProUGUI targetText;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (targetText == null)
            targetText = GetComponentInChildren<TextMeshProUGUI>();

        if (targetText == null) return;

        Color targetColor = colors.normalColor;

        switch (state)
        {
            case SelectionState.Normal:
                targetColor = colors.normalColor;
                break;
            case SelectionState.Highlighted:
                targetColor = colors.highlightedColor;
                break;
            case SelectionState.Pressed:
                targetColor = colors.pressedColor;
                break;
            case SelectionState.Disabled:
                targetColor = colors.disabledColor;
                break;
        }

        targetText.color = targetColor;
    }
}
