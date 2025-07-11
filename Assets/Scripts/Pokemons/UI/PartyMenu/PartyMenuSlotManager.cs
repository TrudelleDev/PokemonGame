using System.Collections.Generic;
using PokemonGame.MenuControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    /// <summary>
    /// Manages and binds Pokémon party data to UI slots.
    /// Automatically fills and updates the slot visuals from the current party.
    /// </summary>
    public class PartyMenuSlotManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the current player party.")]
        private Party party;

        [SerializeField, Required]
        [Tooltip("Controls vertical input and UI navigation for the party menu.")]
        private VerticalMenuController verticalMenuController;

        [Title("Slot Instances (Auto-collected)")]
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

            // Initialize vertical menu selection on first available slot
            verticalMenuController?.Initialize();
        }

        /// <summary>
        /// Binds each slot to a corresponding Pokémon in the party.
        /// Extra slots are unbound.
        /// </summary>
        private void BindSlots()
        {
            UnbindSlots();

            for (int i = 0; i < slots.Count; i++)
            {
                if (i < party.Pokemons.Count)
                    slots[i].Bind(party.Pokemons[i]);
                else
                    slots[i].Unbind(); // Clear unused slots
            }
        }

        /// <summary>
        /// Clears all slot data and unbinds existing Pokémon references.
        /// </summary>
        private void UnbindSlots()
        {
            foreach (PartyMenuSlot slot in slots)
                slot.Unbind();
        }
    }
}
