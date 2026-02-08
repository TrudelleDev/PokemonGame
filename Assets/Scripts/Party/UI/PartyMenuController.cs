using MonsterTamer.Party.UI.PartyOptions;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Party.UI
{
    /// <summary>
    /// Handles party menu flow and navigation.
    /// Listens to <see cref="PartyMenuPresenter"/> intent events
    /// and opens or closes related views (options menu, party menu).
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PartyMenuController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Presenter that raises intent events for party menu actions.")]
        private PartyMenuPresenter partyMenuPresenter;

        private void OnEnable()
        {
            partyMenuPresenter.OptionsRequested += HandleOptionsRequested;
            partyMenuPresenter.ReturnRequested += HandleReturnRequested;
        }

        private void OnDisable()
        {
            partyMenuPresenter.OptionsRequested -= HandleOptionsRequested;
            partyMenuPresenter.ReturnRequested -= HandleReturnRequested;
        }

        private void HandleOptionsRequested()
        {
            ViewManager.Instance.Show<PartyMenuOptionsView>();
        }

        private void HandleReturnRequested()
        {
            ViewManager.Instance.Close<PartyMenuView>();
        }
    }
}
