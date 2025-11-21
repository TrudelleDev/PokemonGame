using PokemonGame.Pokemon.Enums;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemon.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PokemonGenderSymbol : MonoBehaviour
    {
        private const string MaleSymbol = "£";
        private const string FemaleSymbol = "¬";

        private TextMeshProUGUI genderText;

        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon == null)
            {
                Unbind();
                return;
            }

            EnsureText();

            string symbol = GetGenderSprite(pokemon.Gender);
            genderText.text = symbol;
        }

        public void Unbind()
        {
            EnsureText();
            genderText.text = string.Empty;
        }

        private void EnsureText()
        {
            if (genderText == null)
            {
                genderText = GetComponent<TextMeshProUGUI>();
            }
        }

        private string GetGenderSprite(PokemonGender gender)
        {
            return gender switch
            {
                PokemonGender.Male => MaleSymbol,
                PokemonGender.Female => FemaleSymbol,
                _ => null
            };
        }
    }
}
