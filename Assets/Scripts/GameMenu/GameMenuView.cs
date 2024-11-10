using PokemonClone.Menu;
using UnityEngine;

public class GameMenuView : View
{
    [SerializeField] private MenuOption pokedex;
    [SerializeField] private MenuOption party;
    [SerializeField] private MenuOption inventory;
    [SerializeField] private MenuOption trainerCard;
    [SerializeField] private MenuOption save;
    [SerializeField] private MenuOption option;
    [SerializeField] private MenuOption exit;

    public void Awake()
    {
        pokedex.OnClick += () => ViewManager.Instance.Show<PokedexView>();

        exit.OnClick += () => ViewManager.Instance.ShowLast();
    }
}
