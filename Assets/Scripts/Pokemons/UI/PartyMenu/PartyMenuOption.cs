using System;
using PokemonGame.Characters.Inputs;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class PartyMenuOption : MonoBehaviour
    {
        [SerializeField] private MenuButton summaryButton;
        [SerializeField] private MenuButton cancelButton;

        private VerticalMenuController optionMenuController;

        public event Action OnCancel;

        private void Awake()
        {
            summaryButton.OnClick += OnSummaryClick;
            cancelButton.OnClick += OnCancelClick;

            optionMenuController = GetComponentInChildren<VerticalMenuController>();
        }

        private void OnDestroy()
        {
            summaryButton.OnClick -= OnSummaryClick;
            cancelButton.OnClick -= OnCancelClick;

        }

        private void OnSummaryClick()
        {
            ViewManager.Instance.Show<SummaryView>();
        }

        private void OnCancelClick()
        {
            OnCancel?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyBinds.Cancel))
            {
                OnCancelClick();
            }
        }
    }
}
