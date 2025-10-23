using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views. Provides standard show, hide, and close logic.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        private const string Group = "View Settings";

        [BoxGroup(Group), SerializeField]
        [Tooltip("Enable to allow this view to be closed by key input (e.g., Cancel button).")]
        private bool allowKeyClose = false;

        [BoxGroup(Group), SerializeField, Required]
        [Tooltip("If true, this view will be layered on top of others rather than replacing them.")]
        private bool isOverlay;

        [BoxGroup(Group), SerializeField]
        [Tooltip("Sound effect played when this view is closed manually.")]
        private AudioClip closeSound;

        [BoxGroup(Group), SerializeField, Required]
        [Tooltip("Transition type to use when navigating away from this view.")]
        private TransitionType transitionType;

        /// <summary>
        /// Gets whether this view is currently open and visible.
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// Gets whether this view appears as an overlay on top of others.
        /// </summary>
        public bool IsOverlay => isOverlay;

        /// <summary>
        /// Gets the transition type used when showing or closing this view.
        /// </summary>
        public TransitionType TransitionType => transitionType;

        /// <summary>
        /// Called before the view is shown. Used for preloading assets or preparing data.
        /// </summary>
        public virtual void Preload() { }

        /// <summary>
        /// Called when the view is frozen (e.g., when a modal or pause menu is open).
        /// Override to disable animations or input.
        /// </summary>
        public virtual void Freeze() { }

        /// <summary>
        /// Called when the view is unfrozen and resumes normal activity.
        /// </summary>
        public virtual void Unfreeze() { }

        /// <summary>
        /// Displays the view by enabling its GameObject.
        /// </summary>
        public virtual void Show()
        {
            IsOpen = true;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view by disabling its GameObject.
        /// </summary>
        public virtual void Hide()
        {
            IsOpen = false;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Checks for close input (if allowed) and triggers view closure.
        /// </summary>
        protected virtual void Update()
        {
            if (!allowKeyClose)
            {
                return;
            }

            if (ViewManager.Instance == null || ViewManager.Instance.IsTransitioning)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBinds.Cancel))
            {
                if (closeSound != null)
                {
                    AudioManager.Instance.PlaySFX(closeSound);
                }

                ViewManager.Instance.CloseCurrentView();
            }
        }
    }
}
