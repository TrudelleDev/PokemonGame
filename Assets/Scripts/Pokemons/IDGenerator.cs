using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    public static class IDGenerator
    {
        private static readonly List<int> avaliableIDs = new();

        private static readonly int minRangeID = 1000;
        private static readonly int maxRangeID = 9999;

        static IDGenerator()
        {
            for (int i = minRangeID; i < maxRangeID; i++)
            {
                avaliableIDs.Add(i);
            }
        }

        public static string GetID()
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
