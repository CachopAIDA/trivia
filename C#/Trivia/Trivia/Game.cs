using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Trivia
{
    public class Game
    {
        //private readonly bool[] inPenaltyBox = new bool[6];

        //private readonly int[] places = new int[6];
        private readonly List<Player> players = new List<Player>();

        //private readonly int[] points = new int[6];

        private readonly Question popQuestions = new Question("Pop");
        private readonly Question rockQuestions = new Question("Rock");
        private readonly Question scienceQuestions = new Question("Science");
        private readonly Question sportsQuestions = new Question("Sports");

        private int currentPlayerIndex;
        private bool isGettingOutOfPenaltyBox;

        public bool Add(string playerName)
        {
            Player player = new Player(playerName);
            players.Add(player);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayerIndex].name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (players[currentPlayerIndex].inPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    HandleInPenaltyBoxEvenRoll(roll);
                }
                else
                {
                    Console.WriteLine(players[currentPlayerIndex].name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                HandleNotInPenaltyBox(roll);
            }
        }

        private void HandleInPenaltyBoxEvenRoll(int roll)
        {
            isGettingOutOfPenaltyBox = true;

            Console.WriteLine(players[currentPlayerIndex].name + " is getting out of the penalty box");
            HandleNotInPenaltyBox(roll);
        }

        private void HandleNotInPenaltyBox(int roll)
        {
            players[currentPlayerIndex].places += roll;
            if (players[currentPlayerIndex].places > 11) players[currentPlayerIndex].places -= 12;

            Console.WriteLine(players[currentPlayerIndex].name
                              + "'s new location is "
                              + players[currentPlayerIndex].places);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
                popQuestions.NextQuestion();

            if (CurrentCategory() == "Science")
                scienceQuestions.NextQuestion();

            if (CurrentCategory() == "Sports")
                sportsQuestions.NextQuestion();

            if (CurrentCategory() == "Rock")
                rockQuestions.NextQuestion();
        }

        private string CurrentCategory()
        {
            switch (players[currentPlayerIndex].places)
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

        public bool WasCorrectlyAnswered()
        {
            if (players[currentPlayerIndex].inPenaltyBox)
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
            players[currentPlayerIndex].AddPoints();
            Console.WriteLine(players[currentPlayerIndex].name
                              + " now has "
                              + players[currentPlayerIndex].points
                              + " Gold Coins.");

            var winner = DidPlayerWin();
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
            Console.WriteLine(players[currentPlayerIndex].name + " was sent to the penalty box");
            players[currentPlayerIndex].inPenaltyBox = true;

            SetCurrentPlayer();
            return true;
        }


        private bool DidPlayerWin()
        {
            return players[currentPlayerIndex].points != 6;
        }
    }
}