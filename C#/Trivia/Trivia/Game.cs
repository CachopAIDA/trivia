using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> players = new List<Player>();

        private readonly Questions questions = new Questions();

        private int currentPlayerIndex;
        private bool isGettingOutOfPenaltyBox;

        public bool AddPlayer(string name)
        {
            Player player = new Player(name);
            players.Add(player);

            Console.WriteLine($"{name} was added");
            Console.WriteLine($"They are player number {players.Count}");
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine($"{CurrentPlayer.name} is the current player");
            Console.WriteLine($"They have rolled a {roll}");

            if (CurrentPlayer.InPenaltyBox)
            {
                isGettingOutOfPenaltyBox = roll % 2 != 0;
                if (!isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine($"{CurrentPlayer.name} is not getting out of the penalty box");
                    return;
                }
                Console.WriteLine($"{CurrentPlayer.name} is getting out of the penalty box");
            }

            AskNextQuestion(roll);
        }

        private void AskNextQuestion(int roll)
        {
            CurrentPlayer.places += roll;
            if (CurrentPlayer.places > 11) CurrentPlayer.places -= 12;

            Console.WriteLine($"{CurrentPlayer.name}'s new location is {CurrentPlayer.places}");
            Console.WriteLine($"The category is {CurrentPlayer.CurrentCategory()}");
            questions.AskNextQuestion(CurrentPlayer.CurrentCategory());
        }

        private Player CurrentPlayer
        {
            get { return players[currentPlayerIndex]; }
        }

        public bool WasCorrectlyAnswered()
        {
            if (CurrentPlayer.InPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox) return HandleCurrentAnswer();

                SetCurrentPlayer();
                return true;
            }

            return HandleCurrentAnswer();
        }

        private bool HandleCurrentAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
            CurrentPlayer.AddPoints();
            Console.WriteLine(
                $"{CurrentPlayer.name} now has {CurrentPlayer.points} Gold Coins.");

            var winner = CurrentPlayer.points != 6;
            SetCurrentPlayer();
            return winner;
        }

        private void SetCurrentPlayer()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine($"{CurrentPlayer.name} was sent to the penalty box");
            CurrentPlayer.InPenaltyBox = true;

            SetCurrentPlayer();
            return true;
        }
    }
}