using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MonsterTamer.Transitions
{
    /// <summary>
    /// Base class for UI transitions (fade, mask, etc.).
    /// Provides coroutine and async versions of fade methods.
    /// </summary>
    public abstract class Transition : MonoBehaviour
    {
        public static event Action OnFadeOutComplete;

        // Abstract implementations must be provided by subclasses.
        protected abstract void FadeInInternal(Action onComplete);
        protected abstract void FadeOutInternal(Action onComplete);

        public void FadeIn(Action onComplete = null) => FadeInInternal(onComplete);
        public void FadeOut(Action onComplete = null)
        {
            // Wrap the internal call so we trigger the event when done.
            FadeOutInternal(() =>
            {
                OnFadeOutComplete?.Invoke();
                onComplete?.Invoke();
            });
        }


        // Coroutine helpers
        public IEnumerator FadeInCoroutine()
        {
            bool done = false;
            FadeIn(() => done = true);
            yield return new WaitUntil(() => done);
        }

        public IEnumerator FadeOutCoroutine()
        {
            bool done = false;
            FadeOut(() => done = true);
            yield return new WaitUntil(() => done);
        }

        // Async helpers
        public async Task FadeInAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            FadeIn(() => tcs.SetResult(true));
            await tcs.Task;
        }

        public async Task FadeOutAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            FadeOut(() => tcs.SetResult(true));
            await tcs.Task;
        }
    }
}
