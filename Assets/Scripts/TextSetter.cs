using TMPro;
using UnityEngine;

namespace PokemonGame
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField ]private TextMeshProUGUI textMesh;

        public void SetText(string text)
        {
            if (textMesh != null)
                textMesh.text = text;
            else
                Debug.LogWarning("TextSetter: No TextMeshProUGUI reference set.", this);
        }
    }
}
