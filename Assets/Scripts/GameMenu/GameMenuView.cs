using PokemonClone.Menu;
using UnityEngine;

public class GameMenuView : View
{
    [SerializeField] private SelectableUIElement pokedex;
    [SerializeField] private SelectableUIElement party;
    [SerializeField] private SelectableUIElement inventory;
    [SerializeField] private SelectableUIElement trainerCard;
    [SerializeField] private SelectableUIElement save;
    [SerializeField] private SelectableUIElement option;
    [SerializeField] private SelectableUIElement exit;

    public void Awake()
    {
        pokedex.OnClick += () => ViewManager.Instance.Show<PokedexView>();

        exit.OnClick += () => ViewManager.Instance.ShowLast();
    }

    public override void Initialize()
    {
       // throw new System.NotImplementedException();
    }
}
