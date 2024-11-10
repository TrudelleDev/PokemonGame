using System;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonClone.Menu
{
    public class MenuOption : MonoBehaviour
    {
        private Image selectionImage;

        public event Action OnClick;

        private void Awake()
        {
            selectionImage = GetComponentInChildren<Image>();
        }

        public void Click()
        {
            selectionImage.enabled = true;
            OnClick?.Invoke();
        }

        public void Select()
        {
            selectionImage.enabled = true;
        }

        public void UnSelect()
        {
            selectionImage.enabled = false;
        }
    }
}
