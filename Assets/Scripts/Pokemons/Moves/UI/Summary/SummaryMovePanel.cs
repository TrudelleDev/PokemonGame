using PokemonGame.MenuControllers;
using PokemonGame.Pokemons.UI.Summary;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMovePanel : MonoBehaviour
    {
        [SerializeField] private SummaryMoveDescription moveDescription;
        [SerializeField] private SummaryPokemonDescription pokemonDescription;
        [SerializeField] private SummaryPokemonDisplay pokemonDisplay;
        [SerializeField] private SummaryMoveManager moveManager;
        [Space]
        [SerializeField] private VerticalMenuController summaryMoveController;
        [SerializeField] private HorizontalPanelController summaryPageController;
        [Space]
        [SerializeField] private MenuButton cancelButton;

        public bool IsMoveDescriptionOpen { get; private set; } = true;

        private void Awake()
        {
            summaryMoveController.Select += OnMenuControllerSelect;
            cancelButton.OnClick += () => CloseMoveSelection();
            summaryMoveController.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Accept))
            {
                OpenMoveSelection();
            }
            if (Input.GetKeyDown(KeyBind.Cancel))
            {
                CloseMoveSelection();
            }
        }

        public void Bind(Pokemon pokemon)
        {
            moveManager.Bind(pokemon);
            pokemonDescription.Bind(pokemon);
            summaryMoveController.ResetMenuItemList();
        }

        private void OpenMoveSelection()
        {
            summaryMoveController.enabled = true;
            summaryPageController.enabled = false;
            IsMoveDescriptionOpen = false;
            pokemonDisplay.gameObject.SetActive(false);
            pokemonDescription.gameObject.SetActive(true);
            moveDescription.gameObject.SetActive(true);
        }

        private void CloseMoveSelection()
        {
            summaryPageController.enabled = true;
            summaryMoveController.enabled = false;
            IsMoveDescriptionOpen = true;
            pokemonDisplay.gameObject.SetActive(true);
            pokemonDescription.gameObject.SetActive(false);
            moveDescription.gameObject.SetActive(false);       
            summaryMoveController.ResetMenuController();
        }

        private void OnMenuControllerSelect(MenuButton menuButton)
        {
            // Display the infromation of the move when selecting a move 
            if (menuButton.GetComponent<SummaryMove>() != null)
            {
                Move move = menuButton.GetComponent<SummaryMove>().MoveReference;

                if (move != null)
                {
                    moveDescription.Bind(move);
                }
                else
                {
                    moveDescription.Clear();
                }
            }
        }
    }
}
