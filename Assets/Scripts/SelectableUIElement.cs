using System;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonClone.Menu
{
    public class SelectableUIElement : MonoBehaviour
    {
        [SerializeField] private Image selectorImage;

        public event Action OnClick;

        public void Click()
        {
            selectorImage.enabled = true;
            OnClick?.Invoke();
        }

        public void Select()
        {
            selectorImage.enabled = true;
        }

        public void UnSelect()
        {
            selectorImage.enabled = false;
        }
    }
}
