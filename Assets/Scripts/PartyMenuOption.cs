using System;
using PokemonGame.MenuControllers;
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
            summaryButton.OnClick += () => ViewManager.Instance.Show<SummaryView>();
            cancelButton.OnClick += () => HandleCancel();

            optionMenuController = GetComponentInChildren<VerticalMenuController>();
        }

        private void HandleCancel()
        {
            optionMenuController.ResetMenuController();
            OnCancel?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(Keybind.CancelKey))
            {
                HandleCancel();
            }
        }
    }
}
