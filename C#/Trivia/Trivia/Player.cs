namespace Trivia
{
    public class Player
    {
        public readonly string Name;
        public bool InPenaltyBox { get; set; }
        public int Points { get; private set; }
        public int Places { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.Places = 0;
            this.Points = 0;
            this.InPenaltyBox = false;
        }

        public void AddPoints()
        {
            this.Points++;
        }

        public bool HasWon => Points == 6;

        public string CurrentCategory()
        {
            switch (this.Places)
            {
                case 0:
                case 4:
                case 8:
                    return "Pop";
                case 1:
                case 5:
                case 9:
                    return "Science";
                case 2:
                case 6:
                case 10:
                    return "Sports";
                default:
                    return "Rock";
            }
        }

        public void SendToPenaltyBox()
        {
            this.InPenaltyBox = true;
        }
    }
}