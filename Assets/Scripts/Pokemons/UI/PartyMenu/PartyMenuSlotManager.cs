using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    /// <summary>
    /// Manages and binds Pok�mon party data to UI slots.
    /// Automatically fills and updates the slot visuals from the current party.
    /// </summary>
    public class PartyMenuSlotManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the current player party.")]
        private Party party;

        private List<PartyMenuSlot> slots;

        private void Awake()
        {
            // Cache all PartyMenuSlot components in children
            slots = new List<PartyMenuSlot>(GetComponentsInChildren<PartyMenuSlot>());

        }

        private void Start()
        {
            BindSlots();
        }

        /// <summary>
        /// Binds each slot to a corresponding Pok�mon in the party.
        /// Extra slots are cleared.
        /// </summary>
        private void BindSlots()
        {
            UnbindSlots();

            for (int i = 0; i < slots.Count; i++)
            {
                if (i < party.Pokemons.Count)
                {
                    slots[i].Bind(party.Pokemons[i]);
                }
                else
                {
                    slots[i].Unbind();
                }
            }
        }

        /// <summary>
        /// Clears all slot bindings.
        /// </summary>
        private void UnbindSlots()
        {
            foreach (PartyMenuSlot slot in slots)
            {
                slot.Unbind();
            }
        }
    }
}
