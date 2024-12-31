using System;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Image targetGraphic;
        [SerializeField] private bool interactable = true;

        public event Action OnClick;

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
            }
        }

        public void Click()
        {
            targetGraphic.enabled = true;
            OnClick?.Invoke();
        }

        public void Select()
        {
            targetGraphic.enabled = true;
        }

        public void UnSelect()
        {
            targetGraphic.enabled = false;
        }
    }
}
