using MonsterTamer.Move;
using MonsterTamer.Move.Enums;
using MonsterTamer.Pokemon;
using MonsterTamer.Type;
using UnityEngine;

namespace MonsterTamer.Battle
{
    /// <summary>
    /// Centralized system that calculates the damage dealt by a Monster move,
    /// factoring in type effectiveness, STAB, attack/defense stats, level, and random variation.
    /// </summary>
    public static class DamageCalculator
    {
        private const float STABMultiplier = 1.5f; // The multiplier applied when a move matches the user's type (STAB).
        private const float RandomDamageMin = 0.85f; // The minimum random factor applied to damage.
        private const float RandomDamageMax = 1f; // The maximum random factor applied to damage.
        private const float LevelMultiplier = 2f; // The level multiplier used in the base damage formula.
        private const float LevelDamageDivider = 250f; // The divider used in the base damage formula.
        private const float BaseDamageBonus = 2f; // The constant added at the end of the base damage calculation

        /// <summary>
        /// Calculates the final damage a move deals from a user Pokémon to a target Pokémon.
        /// Considers physical/special category, stats, type effectiveness, STAB, and random factor.
        /// Automatically consumes one PP from the move.
        /// </summary>
        /// <param name="user">The Pokémon using the move.</param>
        /// <param name="target">The Pokémon receiving the move.</param>
        /// <param name="move">The move being used.</param>
        /// <returns>The final damage as an integer (minimum 1).</returns>
        internal static int CalculateDamage(PokemonInstance user, PokemonInstance target, MoveInstance move)
        {
            bool isPhysical = move.Definition.Classification.Category == MoveCategory.Physical;

            int attack = isPhysical
                ? user.Stats.StatStage.Modified.Attack
                : user.Stats.StatStage.Modified.SpecialAttack;

            int defense = isPhysical
                ? target.Stats.StatStage.Modified.Defense
                : target.Stats.StatStage.Modified.SpecialDefense;

            float baseDamage =
                (((LevelMultiplier * user.Experience.Level + 10f) / LevelDamageDivider)
                * (attack / (float)defense)
                * move.Definition.MoveInfo.Power
                + BaseDamageBonus);

            float typeModifier = CalculateTypeModifier(move, target);
            float stabModifier = CalculateSTAB(user, move);
            float randomModifier = Random.Range(RandomDamageMin, RandomDamageMax);

            float finalDamage = baseDamage * typeModifier * stabModifier * randomModifier;

            return Mathf.Max(1, Mathf.RoundToInt(finalDamage));
        }

        /// <summary>
        /// Calculates the type effectiveness multiplier of a move against a target Monster.
        /// </summary>
        /// <param name="move">The move being used.</param>
        /// <param name="target">The Monster receiving the move.</param>
        /// <returns>A multiplier representing effectiveness (e.g., 0.5, 1, 2).</returns>
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

        /// <summary>
        /// Calculates the STAB (Same-Type Attack Bonus) multiplier for a move.
        /// Returns 1.5 if the move matches the user's type, otherwise 1.
        /// </summary>
        /// <param name="user">The Monster using the move.</param>
        /// <param name="move">The move being used.</param>
        /// <returns>STAB multiplier.</returns>
        private static float CalculateSTAB(PokemonInstance user, MoveInstance move)
        {
            var moveType = move.Definition.Classification.TypeDefinition;
            var types = user.Definition.Types;

            return (types.FirstType == moveType || types.SecondType == moveType) ? STABMultiplier : 1f;
        }
    }
}
