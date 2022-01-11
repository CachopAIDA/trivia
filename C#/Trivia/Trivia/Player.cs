namespace Trivia
{
    public class Player
    {
        public string name;
        public bool InPenaltyBox { get; set; }
        public int points { get; set; }
        public int places { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.places = 0;
            this.points = 0;
            this.InPenaltyBox = false;
        }

        public void AddPoints()
        {
            this.points++;
        }

        public string CurrentCategory()
        {
            switch (this.places)
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
    }
}