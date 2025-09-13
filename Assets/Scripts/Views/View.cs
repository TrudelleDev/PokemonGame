using PokemonGame.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views. Provides standard show/hide logic.
    /// Transition handling is managed externally by <see cref="ViewManager"/>.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        [Title("Transition")]
        [SerializeField, Required]
        [Tooltip("Transition to use when navigating away from this view.")]
        private TransitionType transitionType;

        /// <summary>
        /// The transition type assigned to this view.
        /// </summary>
        public TransitionType TransitionType => transitionType;

        /// <summary>
        /// Called once before the view is first shown.
        /// Override this to bind button events or initialize UI state.
        /// </summary>
        public virtual void Preload() { }

        /// <summary>
        /// Shows the view by enabling its GameObject.
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
    }
}
