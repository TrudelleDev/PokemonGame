using PokemonGame.Characters;
using PokemonGame.Items.Storage;
using UnityEngine;

namespace PokemonGame.Items
{
    public class ItemGiver : MonoBehaviour, IInteract
    {
        [SerializeField] private Item item;

        private bool hasBeenGiven;

        public void OnInteract(Character character)
        {
            if (!hasBeenGiven)
            {
                character.GetComponent<Bag>().Add(item);
                hasBeenGiven = true;
            }
        }
    }
}
