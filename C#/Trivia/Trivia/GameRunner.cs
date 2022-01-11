using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool notAWinner;

        public static void Main(string[] args)
        {
            PlayGame(new Random());
        }

        public static void PlayGame(Random rand)
        {
            var game = new Game();

            game.AddPlayer("Chet");
            game.AddPlayer("Pat");
            game.AddPlayer("Sue");

            do
            {
                game.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                    notAWinner = game.WrongAnswer();
                else
                    notAWinner = game.WasCorrectlyAnswered();
            } while (notAWinner);
        }
    }
}