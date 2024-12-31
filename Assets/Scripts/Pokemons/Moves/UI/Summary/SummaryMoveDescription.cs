using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMoveDescription : MonoBehaviour, IMoveBind
    {
        [SerializeField] private TextMeshProUGUI movePower;
        [SerializeField] private TextMeshProUGUI moveAccuracy;
        [SerializeField] private TextMeshProUGUI moveEffect;

        public void Bind(Move move)
        {
            movePower.text = $"{move.Data.Power}";
            moveAccuracy.text = $"{move.Data.Accuracy}";
            moveEffect.text = $"{move.Data.Effect}";
        }

        public void Clear()
        {
            movePower.text = "";
            moveAccuracy.text = "";
            moveEffect.text = "";
        }
    }
}
