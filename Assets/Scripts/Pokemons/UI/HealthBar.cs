using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour, IPokemonBind, IComponentInitialize
    {
        [SerializeField] private Sprite exelentHealthSprite;
        [SerializeField] private Sprite goodHealthSprite;
        [SerializeField] private Sprite badHealthSprite;
        [Space]
        [SerializeField] private Image fillImage;

        private Slider slider;

        public void Initialize()
        {
            slider = GetComponent<Slider>();
        }

        public void Bind(Pokemon pokemon)
        {
            slider.minValue = 0;
            slider.maxValue = pokemon.CoreStat.HealthPoint;
            slider.value = slider.maxValue;

            pokemon.OnHealthChange += OnPokemonHealthChange;

        }

        // TODO: Change this for enum healthstate and do calculation in pokemon class
        private void OnPokemonHealthChange(float health)
        {
            float halfHealth = slider.maxValue * 0.5f;
            float oneQuarterHealth = slider.maxValue * 0.25f;

            slider.value = health;

            // Greater than 50% health
            if (health > halfHealth)
            {
                fillImage.sprite = exelentHealthSprite;
            }
            // Greater than 25% health and lower than 50% health
            else if (health > oneQuarterHealth && health < halfHealth)
            {
                fillImage.sprite = goodHealthSprite;
            }
            // Lower than 25% health
            else
            {
                fillImage.sprite = badHealthSprite;
            }
        }
    }
}
