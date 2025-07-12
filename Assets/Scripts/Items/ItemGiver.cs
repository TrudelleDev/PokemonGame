using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Systems.Inventory;
using UnityEngine;

namespace PokemonGame.Items
{
    public class ItemGiver : MonoBehaviour, IInteract
    {
        [SerializeField] private Item item;

        private bool hasBeenGiven;

        public void Interact(Character character)
        {
            if (!hasBeenGiven)
            {
                character.GetComponent<InventoryManager>().Add(item);
                hasBeenGiven = true;
            }
        }
    }
}
