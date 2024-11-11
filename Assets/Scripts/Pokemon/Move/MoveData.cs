using PokemonClone;
using PokemonGame;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveData", menuName = "ScriptableObjects/Move Data")]
public class MoveData : ScriptableObject
{
    [field: SerializeField] public string MoveName { get; private set; }

    [field: SerializeField] public int Power { get; private set; }

    [field: SerializeField] public int Accuracy { get; private set; }

    [field: SerializeField] public int MaxPowerPoint { get; private set; }

    [field: SerializeField, TextArea(5, 10), Space] public string Description { get; private set; }

    [field: SerializeField] public Type Type { get; private set; }

    [field: SerializeField] public MoveCategory Category { get; private set; }
}
