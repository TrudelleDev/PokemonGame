using MonsterTamer.Characters.Config;
using MonsterTamer.Shared.UI.Core;
using MonsterTamer.Views;
using UnityEngine;

namespace MonsterTamer.Shared.UI.Navigation
{
    /// <summary>
    /// Handles vertical menu navigation (up/down) for keyboard/controller input.
    /// Automatically skips non-interactable buttons.
    /// </summary>
    internal sealed class VerticalMenuController : MenuController
    {
        private void Update()
        {
            if (ViewManager.Instance != null && ViewManager.Instance.IsTransitioning) return;

            if (Input.GetKeyDown(KeyBinds.Down))
            {
                SelectNext();
            }
            else if (Input.GetKeyDown(KeyBinds.Up))
            {
                SelectPrevious();
            }
            else if (Input.GetKeyDown(KeyBinds.Interact))
            {
                HandleConfirmation();
            }
        }

        /// <summary>
        /// Applies visual selection by highlighting the current button
        /// and unhighlighting the previously selected button.
        /// </summary>
        /// <param name="previousIndex">Previously selected button index.</param>
        /// <param name="currentIndex">Currently selected button index.</param>
        protected override void ApplySelection(int previousIndex, int currentIndex)
        {
            if (previousIndex >= 0 && previousIndex < ItemCount)
            {
                buttons[previousIndex].SetSelected(false);
            }

            if (currentIndex >= 0 && currentIndex < ItemCount)
            {
                buttons[currentIndex].SetSelected(true);
            }
        }

        /// <summary>
        /// Selects the next interactable button below the current selection, if any.
        /// Skips buttons that are not interactable.
        /// </summary>
        private void SelectNext()
        {
            for (int i = currentIndex + 1; i < ItemCount; i++)
            {
                if (buttons[i].IsInteractable)
                {
                    HandleSelection(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Selects the previous interactable button above the current selection, if any.
        /// Skips buttons that are not interactable.
        /// </summary>
        private void SelectPrevious()
        {
            for (int i = currentIndex - 1; i >= 0; i--)
            {
                if (buttons[i].IsInteractable)
                {
                    HandleSelection(i);
                    return;
                }
            }
        }
    }
}
