using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Image targetGraphic;
        [Space]
        [SerializeField] private MenuButtonType menuButtonType;
        [Space]
        [SerializeField] private bool interactable = true;
        [Space]
        [ShowIf("menuButtonType", MenuButtonType.SpriteSwap)]
        [SerializeField] private Sprite disableSprite;
        [ShowIf("menuButtonType", MenuButtonType.SpriteSwap)]
        [SerializeField] private Sprite selectedSprite;
        [ShowIf("menuButtonType", MenuButtonType.SpriteSwap)]
        [SerializeField] private Sprite nomralSprite;

        public event Action OnClick;

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;

                if (interactable)
                {
                    UnSelect();
                }
                else
                {
                    if (menuButtonType == MenuButtonType.SpriteSwap)
                    {
                        targetGraphic.sprite = disableSprite;
                    }
                }
            }
        }

        public void Click()
        {
            if (menuButtonType == MenuButtonType.SpriteSwap)
            {
                targetGraphic.sprite = selectedSprite;
            }

            targetGraphic.enabled = true;
            OnClick?.Invoke();
        }

        public void Select()
        {
            if (menuButtonType == MenuButtonType.SpriteSwap)
            {
                targetGraphic.sprite = selectedSprite;
            }

            targetGraphic.enabled = true;


        }

        public void UnSelect()
        {
            if (menuButtonType == MenuButtonType.SpriteSwap)
            {
                targetGraphic.enabled = true;
                targetGraphic.sprite = nomralSprite;
            }
            else
            {
                targetGraphic.enabled = false;
            }
        }
    }
}
