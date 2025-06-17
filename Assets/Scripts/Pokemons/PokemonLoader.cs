using PokemonGame.Pokemons.Data;
using UnityEngine;

public static class PokemonLoader
{
    private const string ResourcePath = "Pokemons/";

    public static PokemonData Load(string pokemonID)
    {
        if (string.IsNullOrEmpty(pokemonID))
        {
            Debug.LogError("Cannot load Pokémon: ID is null or empty.");
            return null;
        }

        PokemonData pokemon = Resources.Load<PokemonData>(ResourcePath + pokemonID);

        if (pokemon == null)
        {
            Debug.LogError($"Pokemon '{pokemonID}' not found in Resources/{ResourcePath}");
        }

        return pokemon;
    }

    public static PokemonData[] LoadAll()
    {
        return Resources.LoadAll<PokemonData>(ResourcePath);
    }
}