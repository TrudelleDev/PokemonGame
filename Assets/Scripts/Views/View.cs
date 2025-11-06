using System;
using PokemonGame.Audio;
using PokemonGame.Characters.Inputs;
using PokemonGame.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views. 
    /// Provides standard show, hide, and close logic with independent open/close transitions.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        private const string Group = "View Settings";

        [BoxGroup(Group), SerializeField]
        [Tooltip("Enable to allow this view to be closed by key input (e.g., Cancel button).")]
        private bool allowKeyClose = false;

        [BoxGroup(Group), SerializeField]
        [Tooltip("Sound effect played when this view is closed manually.")]
        private AudioClip closeSound;

        [Title("Transitions")]
        [BoxGroup(Group), SerializeField, Required]
        [Tooltip("Transition used when opening this view.")]
        private TransitionType openTransition = TransitionType.None;

        [BoxGroup(Group), SerializeField, Required]
        [Tooltip("Transition used when closing this view.")]
        private TransitionType closeTransition = TransitionType.None;

        private bool isFrozen;

        /// <summary>
        /// Gets the transition used when this view opens.
        /// </summary>
        public TransitionType OpenTransition => openTransition;

        /// <summary>
        /// Gets the transition used when this view closes.
        /// </summary>
        public TransitionType CloseTransition => closeTransition;


        /// <summary>
        /// Called before the view is shown. Used for preloading assets or preparing data.
        /// </summary>
        public virtual void Preload() { }


        /// <summary>
        /// Displays the view by enabling its GameObject.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view by disabling its GameObject.
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Called when the view is frozen (e.g., when a modal or pause menu is open).
        /// Override to disable animations or input.
        /// </summary>
        public virtual void Freeze() 
        {
            isFrozen = true;
        }

        /// <summary>
        /// Called when the view is unfrozen and resumes normal activity.
        /// </summary>
        public virtual void Unfreeze() 
        {
            isFrozen = false; 
        }

        /// <summary>
        /// Checks for close input (if allowed) and triggers view closure.
        /// </summary>
        protected virtual void Update()
        {
            if (isFrozen)
            {
                return;
            }

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

                ViewManager.Instance.CloseTopView();
                Close();
            }
        }

        public virtual void Close()
        {

        }
    }
}
