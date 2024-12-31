using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AbilityEffectText : MonoBehaviour, IAbilityBind
    {
        private TextMeshProUGUI textMesh;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        public void Bind(Ability ability)
        {
            textMesh.text = ability.Data.Effect;
        }
    }
}
