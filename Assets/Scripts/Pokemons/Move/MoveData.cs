using UnityEngine;

namespace PokemonGame.Pokemons.Move
{
    [CreateAssetMenu(fileName = "NewMoveData", menuName = "ScriptableObjects/Move Data")]
    public class MoveData : ScriptableObject
    {
        [SerializeField] private string moveName;
        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        [SerializeField] private int powerPoint;
        [Space]
        [SerializeField, TextArea(5, 10)] private string description;
        [SerializeField] private Type type;
        [SerializeField] private MoveCategory category;

        public string MoveName => moveName;
        public int Power => power;
        public int Accuracy => accuracy;
        public int PowerPoint => powerPoint;
        public string Description => description;
        public Type Type => type;
        public MoveCategory Category => category;
    }
}
