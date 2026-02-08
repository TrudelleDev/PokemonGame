namespace MonsterTamer.Battle.Models
{
    /// <summary>
    /// Centralized collection of player-facing battle dialogue messages.
    /// </summary>
    internal static class BattleMessages
    {
        // Battle intros
        public const string TrainerIntro = "{0} \nwould like to battle!";
        public const string WildIntro = "Wild {0} appeared!";

        // Player choice
        public const string ChooseAction = "What will {0} do?";

        // Escape
        public const string EscapeSuccess = "Got away safely!";
        public const string EscapeFail = "Can't escape!";
        public const string EscapeTrainer = "You cannot escape a Trainer battle!";

        // Battle flow
        public const string TrainerWantsToBattle = "{0} wants to battle!";
        public const string TrainerSentOut = "{0} sent out {1}!";
        public const string PlayerSendMonster = "Go {0}!";
        public const string MonsterReturnParty = "{0} that's enough!\nCome back!";
        public const string UseMove = "{0} used {1}!";
        public const string FaintedMessage = "{0}\nfainted!";

        // Defeat / blackout
        public const string BlackoutMessage = "All your monsters have fainted!\nYou blacked out...";
        public const string CheckpointRelocationMessage = "You were rushed to\nthe nearest checkpoint.";

        // Progression
        public const string GainExperience = "{0} gained\n{1} Exp. Points.";
        public const string LevelUp = "{0} grew to Level {1}!";

        // Validation
        public const string MonsterHasNoEnergy = "{0} has no energy left to fight!";
        public const string MonsterAlreadyInBattle = "That Monster is already in battle!";
        
    }
}
