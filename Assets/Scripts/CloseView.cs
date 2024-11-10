using PokemonClone.Config;
using UnityEngine;

public class CloseView : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(Configuration.CancelKey))
        {
            ViewManager.Instance.ShowLast();
        }
    }
}
