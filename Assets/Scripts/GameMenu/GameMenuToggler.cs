using PokemonClone.Config;
using UnityEngine;

public class GameMenuToggler : MonoBehaviour
{
    [SerializeField] private GameMenuView gameMenu;

    private void Update()
    {
        if (Input.GetKeyDown(Configuration.StartKey))
        {
            if (!gameMenu.gameObject.activeInHierarchy && ViewManager.Instance.IsHistoryEmpty())
            {
                ViewManager.Instance.Show<GameMenuView>();
            }
            else
            {
                ViewManager.Instance.ShowLast();
            }
        }
    }
}
