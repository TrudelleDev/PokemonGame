using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Pokemon.UI
{
    internal class ExperienceUI : MonoBehaviour
    {
        [SerializeField, Required]
        private TextMeshProUGUI totalExperiencePointText;
        [SerializeField, Required]
        private TextMeshProUGUI nextLevelExperiencePointText;
        [SerializeField, Required]
        private ExperienceBar experienceBar;

        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            pokemon.Experience.OnExperienceChange += Pokemon_OnExperienceChange;


            totalExperiencePointText.text = pokemon.Experience.CurrentExp.ToString();
            nextLevelExperiencePointText.text = (pokemon.Experience.GetExpForNextLevel() - pokemon.Experience.CurrentExp).ToString();
            experienceBar.Bind(pokemon);

        }

        private void Pokemon_OnExperienceChange(int arg1, int arg2)
        {

        }

        public void Unbind()
        {
            totalExperiencePointText.text = string.Empty;
            nextLevelExperiencePointText.text = string.Empty;
            experienceBar.Unbind();
        }
    }
}
