using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    public class IDGenerator
    {
        private readonly List<int> avaliableIDs = new();

        public IDGenerator(int minRange, int maxRange)
        {
            for (int i = minRange; i < maxRange; i++)
            {
                avaliableIDs.Add(i);
            }
        }

        public string GetID()
        {
            if (avaliableIDs.Count > 0)
            {
                int randomIndex = Random.Range(0, avaliableIDs.Count);
                int randomID = avaliableIDs[randomIndex];
                avaliableIDs.RemoveAt(randomIndex);
                Debug.Log("New ID: " + randomID);
                return randomID.ToString();
            }

            Debug.Log("No ID are available");
            return "ERROR";
        }
    }
}
