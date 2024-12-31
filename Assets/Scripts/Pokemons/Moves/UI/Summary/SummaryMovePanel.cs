using PokemonGame.Pokemons.UI.Summary;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMovePanel : MonoBehaviour
    {
        [SerializeField] private SummaryMoveManager moveManager;
        [SerializeField] private SummaryMoveDescription moveDescription;
        [SerializeField] private SummaryPokemonDescription pokemonDescription;
        [SerializeField] private SummaryPokemonDisplay pokemonDisplay;
        [Space]
        [SerializeField] private VerticalMenuController verticalMenuController;
        [SerializeField] private HorizontalMenuController horizontalMenuController;
        [Space]
        [SerializeField] private MenuButton cancelButton;
        [Space]
        [SerializeField] private Party party;

        public bool IsMoveDescriptionOpen { get; private set; }

        private void Awake()
        {
            verticalMenuController.OnSelect += OnMenuControllerSelect;
            cancelButton.OnClick += () => CloseMoveSelection();
        }

        private void Start()
        {
            Pokemon pokemon = party.SelectedPokemon;

            verticalMenuController.enabled = false;
            moveManager.Bind(pokemon);
        }


        private void Update()
        {
            if (Input.GetKeyDown(Keybind.AcceptKey))
            {
                OpenMoveSelection();
            }
            if (Input.GetKeyDown(Keybind.CancelKey))
            {
                CloseMoveSelection();
            }
        }

        private void OpenMoveSelection()
        {
            verticalMenuController.enabled = true;
            horizontalMenuController.enabled = false;
            IsMoveDescriptionOpen = false;

            pokemonDisplay.gameObject.SetActive(false);
            pokemonDescription.gameObject.SetActive(true);
            moveDescription.gameObject.SetActive(true);
        }

        private void CloseMoveSelection()
        {
            horizontalMenuController.enabled = true;
            IsMoveDescriptionOpen = true;

            pokemonDisplay.gameObject.SetActive(true);
            pokemonDescription.gameObject.SetActive(false);
            moveDescription.gameObject.SetActive(false);
            
            verticalMenuController.ResetController();
        }

        private void OnMenuControllerSelect(GameObject gameObject)
        {
            if (gameObject.GetComponent<SummaryMove>() != null)
            {
                Move move = gameObject.GetComponent<SummaryMove>().Move;

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
