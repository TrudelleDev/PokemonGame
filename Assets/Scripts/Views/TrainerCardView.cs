using System;
using PokemonGame.Characters;
using TMPro;
using UnityEngine;

namespace PokemonGame.Views
{

    public class TrainerCardView : View
    {
        [SerializeField] private TextMeshProUGUI playerID;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerMoney;
        [SerializeField] private TextMeshProUGUI timePlayed;
        [Space]
        [SerializeField] private Character player;

        public override void Initialize() { }

        private void Awake()
        {
            playerID.text = $"IDNo. {player.ID}";
            playerName.text = player.CharacterName;
            playerMoney.text = $"${player.Money}";
        }

        private void Update()
        {
            // Format time to Hours/minutes
           // TimeSpan time = TimeSpan.FromSeconds(player.TimePlayed);
          //  var timeConverted = string.Format("{0,1:00}:{1,2:00}", time.Hours, time.Minutes);

            // Display time as Hours/minutes
            //timePlayed.text = timeConverted;
        }
    }
}
