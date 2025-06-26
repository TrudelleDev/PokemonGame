using PokemonGame.Shared.Interfaces;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    /// <summary>
    /// Displays the detailed properties of a Pokémon move, including power, accuracy,
    /// and effect description, in the summary screen UI.
    /// </summary>
    public class SummaryMoveDescriptionUI : MonoBehaviour, IMoveBind, IUnbind
    {
        [SerializeField] private MoveDescriptionUI moveDescription;

        /// <summary>
        /// Binds a move to the UI and updates the power, accuracy, and effect fields.
        /// </summary>
        /// <param name="move">The move to display.</param>
        public void Bind(Move move)
        {
            if (move?.Data == null)
            {
                Unbind();
                return;
            }

            MoveData data = move.Data;

            moveDescription.PowerText.text = data.Power.ToString();
            moveDescription.AccuracyText.text = data.Accuracy.ToString();
            moveDescription.EffectText.text = data.Effect;
        }

        /// <summary>
        /// Clears all displayed text, unbinding any current move.
        /// </summary>
        public void Unbind()
        {
            moveDescription.PowerText.text = string.Empty;
            moveDescription.AccuracyText.text = string.Empty;
            moveDescription.EffectText.text = string.Empty;
        }
    }
}
