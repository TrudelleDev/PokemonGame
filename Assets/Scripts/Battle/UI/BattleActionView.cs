using System;
using PokemonGame.Menu;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.UI
{
    /// <summary>
    /// View that displays the main battle action options
    /// (Fight, Bag, Pokémon, Run).
    /// Raises intent events when the player selects an action,
    /// allowing the battle state machine to react accordingly.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleActionView : View
    {
        [SerializeField, Required, Tooltip("Button to initiate the move selection screen.")]
        private MenuButton fightButton;

        [SerializeField, Required, Tooltip("Button to open the inventory/item selection screen.")]
        private MenuButton bagButton;

        [SerializeField, Required, Tooltip("Button to open the party/switch Pokémon screen.")]
        private MenuButton partyButton;

        [SerializeField, Required, Tooltip("Button to attempt escaping the battle.")]
        private MenuButton runButton;

        internal event Action FightSelected;
        internal event Action BagSelected;
        internal event Action PartySelected;
        internal event Action RunSelected;

        private void OnEnable()
        {
            fightButton.OnSubmitted += OnFightSelected;
            bagButton.OnSubmitted += OnBagSelected;
            partyButton.OnSubmitted += OnPartySelected;
            runButton.OnSubmitted += OnRunSelected;
        }

        private void OnDisable()
        {
            fightButton.OnSubmitted -= OnFightSelected;
            bagButton.OnSubmitted -= OnBagSelected;
            partyButton.OnSubmitted -= OnPartySelected;
            runButton.OnSubmitted -= OnRunSelected;
        }

        private void OnFightSelected() => FightSelected?.Invoke();
        private void OnBagSelected() => BagSelected?.Invoke();
        private void OnPartySelected() => PartySelected?.Invoke();
        private void OnRunSelected() => RunSelected?.Invoke();
    }
}