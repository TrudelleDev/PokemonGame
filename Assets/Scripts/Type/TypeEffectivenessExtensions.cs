namespace MonsterTamer.Type
{
    /// <summary>
    /// Provides extension methods for the <see cref=TypeEffectiveness"/> enum,
    /// including conversion to damage multiplier and display text.
    /// </summary>
    public static class TypeEffectivenessExtensions
    {
        /// <summary>
        /// Converts a <see cref="TypeEffectiveness"/> value to its corresponding damage multiplier.
        /// </summary>
        /// <param name="effectiveness">The type effectiveness.</param>
        /// <returns>A float representing the damage multiplier.</returns>
        public static float ToMultiplier(this TypeEffectiveness effectiveness)
        {
            return effectiveness switch
            {
                TypeEffectiveness.SuperEffective    => 2f,
                TypeEffectiveness.NotVeryEffective  => 0.5f,
                TypeEffectiveness.Immune            => 0f,
                _                                   => 1f
            };
        }

        /// <summary>
        /// Returns a human-readable string describing the type effectiveness.
        /// </summary>
        /// <param name="effectiveness">The type effectiveness.</param>
        /// <returns>A string suitable for battle dialogue.</returns>
        public static string ToText(this TypeEffectiveness effectiveness)
        {
            return effectiveness switch
            {
                TypeEffectiveness.SuperEffective    => "It's Super Effective!",
                TypeEffectiveness.NotVeryEffective  => "It's Not Very Effective!",
                TypeEffectiveness.Immune            => "No Effect!",
                _                                   => ""
            };
        }
    }
}
