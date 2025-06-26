using PokemonGame.MenuControllers;
using PokemonGame.Pokemons.UI.Summary;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    /// <summary>
    /// Controls the move-related UI panel in the Pokémon summary screen.
    /// Manages move selection, binding Pokémon data to UI elements, 
    /// and toggling header visibility during panel activation.
    /// </summary>
    public class SummaryMovePanel : MonoBehaviour
    {
        [Title("Pokemon")]
        [SerializeField] private SummaryHeader header;
        [SerializeField] private SummaryIdentityPanel indentityPanel;

        [Title("Move")]
        [SerializeField] private SummaryMoveDescriptionUI moveDescription;
        [SerializeField] private SummaryMoveListUI moveManager;
        [SerializeField] private VerticalMenuController moveController;

        private void Awake()
        {
            moveController.OnSelect += HandleMoveSelect;
        }

        private void OnDestroy()
        {
            moveController.OnSelect -= HandleMoveSelect;
        }

        private void OnEnable()
        {
            header.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            header.gameObject.SetActive(true);
            moveController.ResetToFirstElement();
        }

        /// <summary>
        /// Binds the given Pokémon's data to the move list and identity panels.
        /// </summary>
        /// <param name="pokemon">The Pokémon to bind.</param>
        public void Bind(Pokemon pokemon)
        {
            moveManager.Bind(pokemon);
            indentityPanel.Bind(pokemon);
        }

        private void HandleMoveSelect(MenuButton button)
        {
            SummaryMoveSlotUI summaryMove = button.GetComponent<SummaryMoveSlotUI>();

            if (summaryMove?.Move != null)
            {
                moveDescription.Bind(summaryMove.Move);
            }
            else
            {
                moveDescription.Unbind();
            }
        }
    }
}
