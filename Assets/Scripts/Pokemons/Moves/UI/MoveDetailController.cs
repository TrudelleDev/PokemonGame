using PokemonGame.MenuControllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// Controls the move detail section in the summary screen.
    /// Listens for move selection and updates the detail display accordingly.
    /// </summary>
    public class MoveDetailController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the selected move's power, accuracy, and effect.")]
        private MoveDetailUI moveDetail;

        [SerializeField, Required] 
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

        /// <summary>
        /// Handles move selection from the vertical menu.
        /// Binds the selected move to the detail display or clears it.
        /// </summary>
        /// <param name="button">The selected menu button.</param>
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
