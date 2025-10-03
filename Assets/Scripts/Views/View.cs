using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views. Provides standard show, hide, and close logic.
    /// If <see cref="allowKeyClose"/> is disabled, the view cannot be closed with input.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        [Title("Settings")]
        [SerializeField]
        [Tooltip("Enable to allow this view to be closed by key input.")]
        private bool allowKeyClose = false;

        [SerializeField, Required]
        [Tooltip("If true, this view will be layered on top of others rather than replacing them.")]
        private bool isOverlay;

        [Title("Audio")]
        [SerializeField]
        private AudioClip closeSound;

        [Title("Transition")]
        [SerializeField, Required]
        [Tooltip("Transition to use when navigating away from this view.")]
        private TransitionType transitionType;
        
        public bool IsOpen { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this view is an overlay (appears on top of others).
        /// </summary>
        public bool IsOverlay => isOverlay;

        /// <summary>
        /// Gets the type of transition used when showing or closing this view.
        /// </summary>
        public TransitionType TransitionType => transitionType;

        /// <summary>
        /// Called before the view is shown, used for preloading assets or data.
        /// </summary>
        public virtual void Preload() { }

        /// <summary>
        /// Called when the view is frozen (e.g., during a pause or modal state).
        /// Override to stop animations or input.
        /// </summary>
        public virtual void Freeze() { }

        /// <summary>
        /// Called when the view is unfrozen and resumes activity.
        /// </summary>
        public virtual void Unfreeze() { }

        /// <summary>
        /// Shows the view by enabling its GameObject.
        /// </summary>
        public virtual void Show()
        {
            IsOpen = true;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view if it is not marked as closed.
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
            if (ViewManager.Instance != null && ViewManager.Instance.IsTransitioning)
            {
                return;
            }

            if (!allowKeyClose)
            {
                return;
            }

            if (Input.GetKeyDown(KeyBinds.Cancel))
            {
                ViewManager.Instance.CloseCurrentView();

                if (closeSound != null) 
                {
                    AudioManager.Instance.PlaySFX(closeSound);
                }            
            }
        }
    }
}
