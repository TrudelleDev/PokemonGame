using UnityEngine;

namespace PokemonGame.Views
{
    public abstract class View : MonoBehaviour
    {
        public abstract void Initialize();

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
