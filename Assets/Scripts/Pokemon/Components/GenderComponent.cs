using PokemonGame.Pokemon.Enums;

namespace PokemonGame.Pokemon.Components
{
    public struct GenderComponent
    {
        public PokemonGender Gender { get; private set; }

        public GenderComponent(float maleRatio)
        {
            float roll = UnityEngine.Random.Range(0f, 100f);
            Gender = roll < maleRatio ? PokemonGender.Male : PokemonGender.Female;
        }
    }
}
