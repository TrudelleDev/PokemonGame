namespace PokemonGame.Pokemons
{
    public interface IComponentInitialize
    {
        /// <summary>
        /// Cache the component manually to prevent null reference exeption.
        /// </summary>
        public void Initialize(); 
    }
}
