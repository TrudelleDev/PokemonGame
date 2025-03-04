namespace PokemonGame.Characters
{
    public class FootController
    {
        private const int LeftFoot = 1;
        private const int RightFoot = 2;

        public int CurrentFoot { get; private set; } = RightFoot;

        public void ResetFoot()
        {
            CurrentFoot = RightFoot;
        }

        public void ChangeFoot()
        {
            if (CurrentFoot == LeftFoot)
            {
                CurrentFoot = RightFoot;
            }
            else if (CurrentFoot == RightFoot)
            {
                CurrentFoot = LeftFoot;
            }
        }
    }
}
