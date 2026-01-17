using System.Collections.Generic;
using PokemonGame.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI
{
    /// <summary>
    /// Manages and binds Pokémon party data to UI slots.
    /// Automatically fills and updates the slot visuals from the current party.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyMenuSlotManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Reference to the current player party.")]
        private Character player;

        private List<PartyMenuSlot> slots;

        private void Start()
        {
            BindSlots();
        }

        /// <summary>
        /// Re-binds all slots to the current party data.
        /// Call this after swapping Pokémon to refresh the UI.
        /// </summary>
        internal void RefreshSlots()
        {
            BindSlots();
        }

        /// <summary>
        /// Binds each slot to a corresponding Pokémon in the party.
        /// Extra slots are cleared.
        /// </summary>
        private void BindSlots()
        {
            if (slots == null)
            {
                slots = new List<PartyMenuSlot>(GetComponentsInChildren<PartyMenuSlot>());
            }

            UnbindSlots();

            for (int i = 0; i < slots.Count; i++)
            {
                if (i < player.Party.Members.Count)
                {
                    slots[i].Bind(player.Party.Members[i]);
                    slots[i].SetSlotIndex(i);
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
