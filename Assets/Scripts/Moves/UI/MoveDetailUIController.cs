using PokemonGame.MenuControllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Moves.UI
{
    /// <summary>
    /// Controls the move detail section in the summary screen.
    /// Listens for move selection and updates the detail display accordingly.
    /// </summary>
    public class MoveDetailUIController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the selected move's power, accuracy, and effect.")]
        private MoveDetailUI moveDetail;

        [SerializeField, Required]
        [Tooltip("Vertical menu controller that emits selection events.")]
        private VerticalMenuController controller;

        private void Awake()
        {
            controller.OnSelect += OnControllerSelect;
        }

        private void OnDestroy()
        {
            controller.OnSelect -= OnControllerSelect;
        }

        private void OnDisable()
        {
            controller.ResetToFirstElement();
        }

        private void OnControllerSelect(Button button)
        {
            MoveSlotUI summaryMove = button.GetComponent<MoveSlotUI>();

            if (summaryMove?.Move != null)
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
