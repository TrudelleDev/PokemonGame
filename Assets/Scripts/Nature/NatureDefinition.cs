using MonsterTamer.Nature.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Nature
{
    /// <summary>
    /// Define a Pokemon nature that modifies stats and has a display name.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Nature/Nature Definition")]
    public class NatureDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Name shown in the UI.")]
        private string displayName;

        [SerializeField]
        [Tooltip("Modifiers applied by this nature: one stat is increased and another is decreased.")]
        private NatureStatsModifier modifiers;

        public string DisplayName => displayName;
        public NatureStatsModifier Modifiers => modifiers;
    }
}
