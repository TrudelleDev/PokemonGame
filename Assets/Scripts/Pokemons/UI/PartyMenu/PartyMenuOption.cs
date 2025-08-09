using System;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame
{
    public class PartyMenuOption : MonoBehaviour
    {
        [SerializeField] private Button summaryButton;
        [SerializeField] private Button cancelButton;

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
