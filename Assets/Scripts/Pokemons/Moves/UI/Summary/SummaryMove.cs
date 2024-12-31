using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMove : MonoBehaviour, IMoveBind
    {
        [SerializeField] private TextMeshProUGUI moveName;
        [SerializeField] private TextMeshProUGUI powerPoint;
        [SerializeField] private Image moveSprite;

        public Move Move { get; private set; } // Hold a reference to the move

        public void Bind(Move move)
        {
            Move = move;

            moveName.text = move.Data.MoveName;
            powerPoint.text = $"{move.PowerPointRemaining}/{move.Data.PowerPoint}";
            moveSprite.sprite = move.Data.Type.Sprite;
            moveSprite.enabled = true;
        }
    }
}
