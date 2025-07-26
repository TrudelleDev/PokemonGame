using System.Collections;
using PokemonGame.Transitions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views. Provides standard show/hide logic
    /// and handles transitions to other views using a TransitionHandler.
    /// </summary>
    [RequireComponent(typeof(TransitionHandler))]
    [RequireComponent(typeof(CloseView))]
    public abstract class View : MonoBehaviour
    {
        [Title("Transition")]
        [SerializeField, Required]
        [Tooltip("Handles transition logic between views.")]
        private TransitionHandler transitionHandler;

        /// <summary>
        /// Called once before the view is first shown.
        /// Override this to bind button events or initialize UI state.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Enables the view GameObject.
        /// </summary>
        public void Show() => gameObject.SetActive(true);

        /// <summary>
        /// Disables the view GameObject.
        /// </summary>
        public void Hide() => gameObject.SetActive(false);

        /// <summary>
        /// Handles transition to another view. If no target is provided, just hides this view.
        /// </summary>
        /// <param name="targetView">The view to transition to.</param>
        public virtual IEnumerator HandleTransition(View targetView)
        {
            if (targetView == null)
            {
                Hide();
                yield break;
            }

            yield return transitionHandler.PlayTransition(this, targetView);
        }
    }
}
