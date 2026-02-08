using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterTamer.Type
{
    /// <summary>
    /// Stores lists of target types for each type effectiveness category.
    /// </summary>
    [Serializable]
    public struct TypeEffectivenessGroups
    {
        [SerializeField, Tooltip("Types this type is super effective against.")]
        private List<TypeDefinition> superEffectiveAgainst;

        [SerializeField, Tooltip("Types this type is not very effective against.")]
        private List<TypeDefinition> notEffectiveAgainst;

        [SerializeField, Tooltip("Types this type has no effect against.")]
        private List<TypeDefinition> immuneAgainst;

        /// <summary>
        /// Returns the effectiveness of this type against a given target type.
        /// </summary>
        /// <param name="targetType">The target Pokémon's type.</param>
        /// <returns>The <see cref="TypeEffectiveness"/> representing damage effectiveness.</returns>
        public readonly TypeEffectiveness GetEffectiveness(TypeDefinition targetType)
        {
            if (superEffectiveAgainst.Contains(targetType))
            {
                return TypeEffectiveness.SuperEffective;
            }
            if (notEffectiveAgainst.Contains(targetType))
            {
                return TypeEffectiveness.NotVeryEffective;
            }
            if (immuneAgainst.Contains(targetType))
            {
                return TypeEffectiveness.Immune;
            }

            return TypeEffectiveness.Normal;
        }
    }
}
