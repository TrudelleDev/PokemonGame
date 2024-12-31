using UnityEngine;

namespace PokemonGame.Pokemons.Moves
{
    [CreateAssetMenu(fileName = "NewMoveData", menuName = "ScriptableObjects/Move Data")]
    public class MoveData : ScriptableObject
    {
        [SerializeField] private string moveName;
        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        [SerializeField] private int powerPoint;
        [Space]
        [SerializeField, TextArea(5, 10)] private string effect;
        [SerializeField] private TypeData type;
        [SerializeField] private MoveCategory category;

        public string MoveName => moveName;
        public int Power => power;
        public int Accuracy => accuracy;
        public int PowerPoint => powerPoint;
        public string Effect => effect;
        public TypeData Type => type;
        public MoveCategory Category => category;
    }
}
