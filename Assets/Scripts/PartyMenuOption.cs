using System;
using PokemonGame.MenuControllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    public class PartyMenuOption : MonoBehaviour
    {
        [SerializeField, Required] private Button summaryButton;
        [SerializeField, Required] private Button cancelButton;

        private VerticalMenuController optionMenuController;

        public event Action OnCancel;

        private void Awake()
        {
            summaryButton.onClick.AddListener(OnSummaryClick);
            cancelButton.onClick.AddListener(OnCancelClick);

            optionMenuController = GetComponentInChildren<VerticalMenuController>();
        }

        private void OnSummaryClick()
        {
            ViewManager.Instance.Show<SummaryView>();
        }

        private void OnCancelClick()
        {
            optionMenuController.ResetToFirstElement();
            OnCancel?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyBind.Cancel))
            {
                OnCancelClick();
            }
        }
    }
}
