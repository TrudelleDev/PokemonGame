using UnityEngine;

namespace MonsterTamer.Nature
{
    /// <summary>
    /// Holds a list of all available Pokemon natures and provides utility methods.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Nature/Nature Database")]
    public class NatureDatabase : ScriptableObject
    {
        [SerializeField, Tooltip("All available nature definitions.")]
        private NatureDefinition[] natures;

        public NatureDefinition GetRandomNature()
        {
            return natures[UnityEngine.Random.Range(0, natures.Length)];
        }
    }
}
