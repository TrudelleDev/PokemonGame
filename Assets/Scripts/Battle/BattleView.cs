using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleView : View
    {
        private void OnEnable()
        {
            StartCoroutine(ShowDialogueAfterTransition());
        }

        private IEnumerator ShowDialogueAfterTransition()
        {
            // Wait until transition has finished (approximate or hook into event)
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            ViewManager.Instance.Show<DialogueBoxView>();
        }
    }
}
