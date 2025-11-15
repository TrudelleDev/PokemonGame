using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Experience
{
    public class ExperienceUI : MonoBehaviour
    {
        [SerializeField]
        private ExperienceBar experienceBar;

        [SerializeField]
        private TextMeshProUGUI totalExperiencePointText;
        [SerializeField]
        private TextMeshProUGUI nextLevelExperiencePointText;

        public void Bind(Pokemon pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            pokemon.OnExperienceChange += Pokemon_OnExperienceChange;

            experienceBar.Bind(pokemon);
            totalExperiencePointText.text = pokemon.CurrentExp.ToString();
            nextLevelExperiencePointText.text = (pokemon.GetExpForNextLevel()- pokemon.CurrentExp).ToString();

        }

        private void Pokemon_OnExperienceChange(int arg1, int arg2)
        {

        }

        public void Unbind()
        {
            experienceBar.Unbind();
            totalExperiencePointText.text = string.Empty;
            nextLevelExperiencePointText.text = string.Empty;
        }
    }
}
