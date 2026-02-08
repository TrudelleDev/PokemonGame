using MonsterTamer.Shared.UI.Core;
using MonsterTamer.Shared.UI.Navigation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Move.UI
{
    /// <summary>
    /// Controls the move detail section in the summary screen.
    /// Listens for move selection and updates the detail display accordingly.
    /// </summary>
    internal sealed class MoveDetailUIController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the selected move's power, accuracy, and effect.")]
        private MoveDetailUI moveDetail;

        [SerializeField, Required]
        [Tooltip("Vertical menu controller that emits selection events.")]
        private VerticalMenuController controller;

        private void Awake()
        {
            controller.Selected += OnControllerSelect;
        }

        private void OnDestroy()
        {
            controller.Selected -= OnControllerSelect;
        }

        private void OnControllerSelect(MenuButton button)
        {
            MoveSlotUI summaryMove = button.GetComponent<MoveSlotUI>();

            if (summaryMove.Move != null)
            {
                moveDetail.Bind(summaryMove.Move);
            }
            else
            {
                moveDetail.Unbind();
            }
        }
    }
}
