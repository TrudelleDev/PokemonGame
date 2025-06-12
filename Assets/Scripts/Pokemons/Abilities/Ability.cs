namespace PokemonGame.Pokemons.Abilities
{
    public class Ability
    {
        public AbilityData Data { get; private set; }

        public Ability(AbilityData data)
        {
            Data = data;
        }
    }
}
