using PokemonGame.Encyclopedia;
using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private string trainerName;

        private Party party;
        private Pokedex pokedex;

        public string TrainerName => trainerName;

        public int Money { get; set; } = 0;
        public string ID { get; set; }
        public float TimePlayed { get; set; }


        private void Awake()
        {
            party = GetComponent<Party>();
            pokedex = GetComponent<Pokedex>();
        }

        private void Start()
        {
            foreach (Pokemon pokemon in party.Pokemons)
            {
                pokemon.OwnerName = trainerName;
                pokedex.AddData(new PokedexEntry(true, pokemon.Data));
            }

            IDGenerator generator = new IDGenerator(10000, 99999);
            ID = generator.GetID();     
        }

        private void Update()
        {
            TimePlayed += Time.deltaTime;
        }
    }
}
