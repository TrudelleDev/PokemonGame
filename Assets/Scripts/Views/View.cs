using System;
using MonsterTamer.Audio;
using MonsterTamer.Characters.Config;
using MonsterTamer.Shared.UI.Core;
using MonsterTamer.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Views
{
    /// <summary>
    /// Base class for all UI views.
    /// Handles show, hide, freeze/unfreeze, and key-based close logic with optional transitions.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        [Title("Base View Settings")]

        [SerializeField]
        private AudioClip closeSound;

        [SerializeField]
        private MenuController menuController;

        [SerializeField, Required, Tooltip("Transition used when opening this view.")]
        private TransitionType openTransition = TransitionType.None;

        [SerializeField, Required, Tooltip("Transition used when closing this view.")]
        private TransitionType closeTransition = TransitionType.None;

        private bool isFrozen;

        /// <summary>
        /// Raised when the view is closed via key input.
        /// </summary>
        public event Action ReturnKeyPressed;

        /// <summary>
        /// Transition used when opening this view.
        /// </summary>
        public TransitionType OpenTransition => openTransition;

        /// <summary>
        /// Transition used when closing this view.
        /// </summary>
        public TransitionType CloseTransition => closeTransition;

        /// <summary>
        /// Preload assets or prepare data before showing the view.
        /// </summary>
        public virtual void Preload() { }

        /// <summary>
        /// Show the view.
        /// </summary>
        public virtual void Show() => gameObject.SetActive(true);

        /// <summary>
        /// Hide the view.
        /// </summary>
        public virtual void Hide()
        {
            AudioManager.Instance.PlayUISFX(closeSound);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Freeze the view, disabling interaction.
        /// By default, disables any <see cref="MenuController"/> on the GameObject.
        /// </summary>
        public virtual void Freeze()
        {
            isFrozen = true;

            if (menuController != null)
            {
                menuController.enabled = false;
            }
        }

        /// <summary>
        /// Unfreeze the view, resuming interaction.
        /// By default, enables any <see cref="MenuController"/> on the GameObject.
        /// </summary>
        public virtual void Unfreeze()
        {
            isFrozen = false;

            if (menuController != null)
            {
                menuController.enabled = true;
            }
        }

        protected void ResetMenuController()
        {
            if (menuController != null)
            {
                menuController.ResetController();
            }
        }

        /// <summary>
        /// Handles input and closes the view when the cancel key is pressed.
        /// </summary>
        protected virtual void Update()
        {
            if (isFrozen || ViewManager.Instance == null || ViewManager.Instance.IsTransitioning)
                return;

            if (Input.GetKeyDown(KeyBinds.Cancel))
            {
                ReturnKeyPressed?.Invoke();
            }
        }
    }
}
