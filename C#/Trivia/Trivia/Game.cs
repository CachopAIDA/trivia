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

        public void AddPlayer(string name)
        {
            Player player = new Player(name);
            players.Add(player);

            Console.WriteLine($"{name} was added");
            Console.WriteLine($"They are player number {players.Count}");
        }

        public void Roll(int roll)
        {
            Console.WriteLine($"{CurrentPlayer.Name} is the current player");
            Console.WriteLine($"They have rolled a {roll}");

            if (CurrentPlayer.InPenaltyBox)
            {
                isGettingOutOfPenaltyBox = roll % 2 != 0;
                if (!isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine($"{CurrentPlayer.Name} is not getting out of the penalty box");
                    return;
                }
                Console.WriteLine($"{CurrentPlayer.Name} is getting out of the penalty box");
            }

            AskNextQuestion(roll);
        }

        private void AskNextQuestion(int roll)
        {
            CurrentPlayer.Places += roll;
            if (CurrentPlayer.Places > 11) CurrentPlayer.Places -= 12;

            Console.WriteLine($"{CurrentPlayer.Name}'s new location is {CurrentPlayer.Places}");
            Console.WriteLine($"The category is {CurrentPlayer.CurrentCategory()}");
            questions.AskNextQuestion(CurrentPlayer.CurrentCategory());
        }

        private Player CurrentPlayer => players[currentPlayerIndex];

        public void CorrectAnswer()
        {
            bool canAnswer = !CurrentPlayer.InPenaltyBox || isGettingOutOfPenaltyBox;
            if (canAnswer)
            {
                Console.WriteLine("Answer was correct!!!!");
                CurrentPlayer.AddPoints();
                Console.WriteLine(
                    $"{CurrentPlayer.Name} now has {CurrentPlayer.Points} Gold Coins.");
            }

            SetCurrentPlayer();
        }

        public void WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine($"{CurrentPlayer.Name} was sent to the penalty box");
            CurrentPlayer.SendToPenaltyBox();

            SetCurrentPlayer();
        }

        private void SetCurrentPlayer()
        {
            if (CurrentPlayer.HasWon) return;
            currentPlayerIndex++;
            if (currentPlayerIndex == players.Count) currentPlayerIndex = 0;
        }

        public bool HasNoWinner ()
        {
            return !CurrentPlayer.HasWon;
        }
    }
}