using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.MenuControllers
{
    public class ScrollMenuController : MenuController
    {
        private const float KeyPressDelay = 0.5f;

        [SerializeField] private int scrollRange = 5;

        private RectTransform rectTransform;
        private VerticalLayoutGroup verticalLayoutGroup;
        private GameObject contentItem;

        private float verticalLayoutSpacing;
        private float contentItemHeight;

        private int lastButtonDownIndex;
        private float timer;
        private bool isKeyHoldDelay;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            verticalLayoutSpacing = verticalLayoutGroup.spacing;

            // Get the first content item
            contentItem = transform.GetChild(0).gameObject;
            // Get the height of first content item, they all have the same size
            contentItemHeight = contentItem.GetComponent<RectTransform>().rect.height;
        }


        protected override void Update()
        {
            CheckKeyPressLongEnough();

            // If the user is holding down the control key long enough,its will enable fast scrolling
            if (isKeyHoldDelay)
            {
                if (Input.GetKey(Keybind.DownKey) && currentButtonIndex < transform.childCount - 1)
                {
                    ScrollDownLogic();
                }
                if (Input.GetKey(Keybind.UpKey) && currentButtonIndex > 0)
                {
                    ScrollUpLogic();
                }
            }
            else
            {
                if (Input.GetKeyDown(Keybind.DownKey) && currentButtonIndex < transform.childCount - 1)
                {
                    ScrollDownLogic();
                }
                if (Input.GetKeyDown(Keybind.UpKey) && currentButtonIndex > 0)
                {
                    ScrollUpLogic();
                }
            }

            UpdateSelection();
        }

        private void CheckKeyPressLongEnough()
        {
            // User hold key down
            if (Input.GetKey(Keybind.DownKey) || Input.GetKey(Keybind.UpKey))
            {
                timer += Time.deltaTime;

                if (timer > KeyPressDelay)
                {
                    isKeyHoldDelay = true;
                    timer = 0;
                }
            }

            // User release key
            else if (Input.GetKeyUp(Keybind.DownKey) || Input.GetKeyUp(Keybind.UpKey))
            {
                // Reset when key is released
                timer = 0;
                isKeyHoldDelay = false;
            }
        }

        private void ScrollDownLogic()
        {
            currentButtonIndex++;

            // Enable the scrolling 
            if (currentButtonIndex > scrollRange && currentButtonIndex <= transform.childCount - scrollRange)
            {
                rectTransform.anchoredPosition += (contentItemHeight + verticalLayoutSpacing) * Vector2.up;
                lastButtonDownIndex = currentButtonIndex;
            }
        }

        private void ScrollUpLogic()
        {
            currentButtonIndex--;

            // Enable the scrolling
            if (currentButtonIndex < lastButtonDownIndex && currentButtonIndex >= scrollRange)
            {
                rectTransform.anchoredPosition += (contentItemHeight + verticalLayoutSpacing) * Vector2.down;
            }
        }
    }
}
