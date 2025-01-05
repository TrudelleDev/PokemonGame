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
        [Space]
        [SerializeField] private VerticalMenuController summaryMoveController;
        [SerializeField] private HorizontalMenuController summaryPageController;
        [Space]
        [SerializeField] private MenuButton cancelButton;
        [Space]
        [SerializeField] private Party party;

        public bool IsMoveDescriptionOpen { get; private set; }

        private void Awake()
        {
            summaryMoveController.OnSelect += OnMenuControllerSelect;
            cancelButton.OnClick += () => CloseMoveSelection();
        }

        private void Start()
        {
            Pokemon pokemon = party.SelectedPokemon;

            summaryMoveController.enabled = false;

            DisableSummaryMoves();
            BindSumaryMoves(pokemon.Moves);        
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
            IsMoveDescriptionOpen = true;
            pokemonDisplay.gameObject.SetActive(true);
            pokemonDescription.gameObject.SetActive(false);
            moveDescription.gameObject.SetActive(false);       
            summaryMoveController.ResetController();
        }

        private void DisableSummaryMoves()
        {
            for (int i = 0; i < summaryMoveController.transform.childCount - 1; i++)
            {
                if (summaryMoveController.transform.GetChild(i).GetComponent<SummaryMove>() != null)
                {
                    summaryMoveController.transform.GetChild(i).GetComponent<MenuButton>().Interactable = false;
                }
            }
        }

        private void BindSumaryMoves(Move[] moves)
        {
            for (int i = 0; i < moves.Length; i++)
            {
                if (summaryMoveController.transform.GetChild(i).GetComponent<SummaryMove>() != null)
                {
                    // Enable the move slot
                    summaryMoveController.transform.GetChild(i).GetComponent<MenuButton>().Interactable = true;
                    // Bind the move data to the slot
                    summaryMoveController.transform.GetChild(i).GetComponent<SummaryMove>().Bind(moves[i]);
                }
            }
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
