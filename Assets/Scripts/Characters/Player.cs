using PokemonGame.Encyclopedia;
using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Characters
{
    public class Player : Character
    {
        private const float RAYCAST_DISTANCE = 1f;
        private const float OFFSET_Y = 0.5f;

        private Party party;
        private Pokedex pokedex;
        private CharacterController controller;
        private RaycastHit2D[] hit;

        private SpriteRenderer spriteRenderer;

        public float TimePlayed { get; set; }


        private void Awake()
        {
            party = GetComponent<Party>();
            pokedex = GetComponent<Pokedex>();
            controller = GetComponent<CharacterController>();
        }

        protected override void Start()
        {
            base.Start();

            // For testing.
            foreach (Pokemon pokemon in party.Pokemons)
            {
                pokemon.OwnerName = characterName;
                pokedex.AddData(new PokedexEntry(true, pokemon.Data));
            }
        }

        private void Update()
        {
            TimePlayed += Time.deltaTime;

            Interaction();
         
        }

        public void Interaction()
        {
            Vector3 rayCastOrigin = new Vector3(transform.position.x, transform.position.y + OFFSET_Y, transform.position.z);

            hit = Physics2D.RaycastAll(rayCastOrigin, new Vector2(controller.FacingDirection.x, controller.FacingDirection.y), RAYCAST_DISTANCE);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i])
                {
                    if (Input.GetKeyDown(KeyBind.Accept))
                    {
                        hit[i].collider.gameObject.GetComponent<IInteract>()?.OnInteract();
                    }

                    //hit.collider.GetComponent<IInteract>().Interact(transform);

                    //hit.collider.GetComponent<ITrigger>()?.Trigger();
                }
            }
           
        }
    }
}
