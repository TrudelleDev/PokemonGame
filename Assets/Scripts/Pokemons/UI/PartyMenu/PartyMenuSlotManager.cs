using System.Collections.Generic;
using PokemonGame.MenuControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    /// <summary>
    ///  Manages and binds Pokémon party data to UI slots.
    /// </summary>
    public class PartyMenuSlotManager : MonoBehaviour
    {
        [SerializeField, Required] private Party party;
        [SerializeField, Required] private VerticalMenuController verticalMenuController;

        [ShowInInspector, ReadOnly]
        private List<PartyMenuSlot> slots;

        private void Awake()
        {
            // Automatically find all PartyMenuSlot components in children
            slots = new List<PartyMenuSlot>(GetComponentsInChildren<PartyMenuSlot>());
        }

        private void Start()
        {
            if (party == null || slots == null || slots.Count == 0)
                return;

            BindSlots();

            // Make sure the first PartyMenuSlot is selected
            verticalMenuController.Initialize();
        }

        private void BindSlots()
        {
            UnbindSlots(); // Unbind all first

            for (int i = 0; i < slots.Count; i++)
            {
                if (i < party.Pokemons.Count)
                    slots[i].Bind(party.Pokemons[i]);

                else
                    slots[i].Unbind(); // In case some slots don't get used
            }
        }

        private void UnbindSlots()
        {
            foreach (PartyMenuSlot slot in slots)
                slot.Unbind();
        }
    }
}
