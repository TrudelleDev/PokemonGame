using System.Collections;
using PokemonGame.Transitions;
using PokemonGame.Transitions.Controllers;
using PokemonGame.Transitions.Enums;
using PokemonGame.Transitions.Extensions;
using PokemonGame.Transitions.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Views
{
    /// <summary>
    /// Base class for all UI views. Provides standard show/hide logic
    /// and handles transitions between views.
    /// </summary>
    [RequireComponent(typeof(CloseView))]
    public abstract class View : MonoBehaviour
    {
        [Title("Transition")]
        [SerializeField, Required]
        [Tooltip("The type of transition to use when switching from this view to another.")]
        private TransitionType transitionType;

        private ITransition transition;

        private void Awake()
        {
            transition = TransitionResolver.Resolve(transitionType);
        }

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
        /// Transitions to another view. 
        /// If transition is <c>null</c> (e.g., <see cref="TransitionType.None"/>), instantly swaps.
        /// </summary>
        public virtual IEnumerator HandleTransition(View targetView)
        {
            yield return transition.RunViewTransition(this, targetView);
        }
    }
}
