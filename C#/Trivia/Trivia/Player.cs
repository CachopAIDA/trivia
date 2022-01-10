namespace Trivia
{
    public class Player
    {
        public string name;
        public bool inPenaltyBox { get; set; }
        public int points { get; set; }
        public int places { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.places = 0;
            this.points = 0;
            this.inPenaltyBox = false;
        }

        public void AddPoints()
        {
            this.points++;
        }

    }
}