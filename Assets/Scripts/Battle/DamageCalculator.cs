using PokemonGame.Move;
using PokemonGame.Move.Enums;
using PokemonGame.Pokemon;
using PokemonGame.Type;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Centralized system that performs full Pokémon damage calculations,
    /// including type effectiveness, STAB, and random factor.
    /// </summary>
    public static class DamageCalculator
    {
        // -----------------------
        // Damage formula constants
        // -----------------------
        private const float STAB_MULTIPLIER = 1.5f;
        private const float RANDOM_MIN = 0.85f;
        private const float RANDOM_MAX = 1f;
        private const float BASE_LEVEL_MULTIPLIER = 2f;
        private const float BASE_DIVIDER = 250f;
        private const float BASE_ADD = 2f;

        /// <summary>
        /// Calculates the final damage a move deals from a user Pokémon to a target Pokémon.
        /// Considers:
        /// - Physical/Special move category
        /// - Attacker's and defender's modified stats
        /// - Type effectiveness
        /// - STAB (Same-Type Attack Bonus)
        /// - Random damage factor
        /// </summary>
        /// <param name="user">The Pokémon using the move.</param>
        /// <param name="target">The Pokémon receiving the move.</param>
        /// <param name="move">The move being used.</param>
        /// <returns>The final damage as an integer (minimum 1).</returns>
        public static int CalculateDamage(PokemonInstance user, PokemonInstance target, MoveInstance move)
        {
            bool isPhysical = move.Definition.Classification.CategoryDefinition.Category == MoveCategory.Physical;

            int attack = isPhysical
                ? user.Stats.StatStage.Modified.Attack
                : user.Stats.StatStage.Modified.SpecialAttack;

            int defense = isPhysical
                ? target.Stats.StatStage.Modified.Defense
                : target.Stats.StatStage.Modified.SpecialDefense;

            float baseDamage =
                (((BASE_LEVEL_MULTIPLIER * user.Experience.Level + 10f) / BASE_DIVIDER)
                * (attack / (float)defense)
                * move.Definition.MoveInfo.Power
                + BASE_ADD);

            float typeModifier = CalculateTypeModifier(move, target);
            float stabModifier = CalculateSTAB(user, move);
            float randomModifier = Random.Range(RANDOM_MIN, RANDOM_MAX);

            float finalDamage = baseDamage * typeModifier * stabModifier * randomModifier;

            return Mathf.Max(1, Mathf.RoundToInt(finalDamage));
        }

        // -----------------------
        // Internal helper methods
        // -----------------------

        private static float CalculateTypeModifier(MoveInstance move, PokemonInstance target)
        {
            float multiplier = 1f;
            var moveType = move.Definition.Classification.TypeDefinition;
            var types = target.Definition.Types;

            multiplier *= moveType.EffectivenessGroups.GetEffectiveness(types.FirstType).ToMultiplier();

            if (types.SecondType != null)
            {
                multiplier *= moveType.EffectivenessGroups.GetEffectiveness(types.SecondType).ToMultiplier();
            }

            return multiplier;
        }

        private static float CalculateSTAB(PokemonInstance user, MoveInstance move)
        {
            var moveType = move.Definition.Classification.TypeDefinition;
            var types = user.Definition.Types;

            if (types.FirstType == moveType || types.SecondType == moveType)
            {
                return STAB_MULTIPLIER;
            }

            return 1f;
        }
    }
}
