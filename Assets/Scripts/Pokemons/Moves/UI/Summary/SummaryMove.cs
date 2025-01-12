using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMove : MonoBehaviour, IMoveBind
    {
        [SerializeField] private TextMeshProUGUI moveName;
        [SerializeField] private TextMeshProUGUI movePowerPoint;
        [SerializeField] private Image moveSprite;

        public Move MoveReference { get; private set; }

        public void Bind(Move move)
        {
            MoveReference = move;

            moveName.text = move.Data.MoveName;
            movePowerPoint.text = $"{move.PowerPointRemaining}/{move.Data.PowerPoint}";
            moveSprite.sprite = move.Data.Type.Sprite;
            moveSprite.enabled = true;
        }

        public void Clear()
        {
            moveName.text = "-";
            movePowerPoint.text = "--";
            moveSprite.enabled = false;
        }
    }
}
