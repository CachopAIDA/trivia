using System;
using System.Collections.Generic;

namespace Trivia
{
    public class Game
    {
        private readonly bool[] inPenaltyBox = new bool[6];

        private readonly int[] places = new int[6];
        private readonly List<string> players = new List<string>();

        private readonly int[] points = new int[6];

        private readonly Question popQuestions = new Question("Pop");
        private readonly Question rockQuestions = new Question("Rock");
        private readonly Question scienceQuestions = new Question("Science");
        private readonly Question sportsQuestions = new Question("Sports");

        private int currentPlayer;
        private bool isGettingOutOfPenaltyBox;

        public bool Add(string playerName)
        {
            players.Add(playerName);
            places[HowManyPlayers()] = 0;
            points[HowManyPlayers()] = 0;
            inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        private int HowManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    HandleInPenaltyBoxEvenRoll(roll);
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
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

            Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
            HandleNotInPenaltyBox(roll);
        }

        private void HandleNotInPenaltyBox(int roll)
        {
            places[currentPlayer] += roll;
            if (places[currentPlayer] > 11) places[currentPlayer] -= 12;

            Console.WriteLine(players[currentPlayer]
                              + "'s new location is "
                              + places[currentPlayer]);
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
            switch (places[currentPlayer])
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
            if (inPenaltyBox[currentPlayer])
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
            points[currentPlayer]++;
            Console.WriteLine(players[currentPlayer]
                              + " now has "
                              + points[currentPlayer]
                              + " Gold Coins.");

            var winner = DidPlayerWin();
            SetCurrentPlayer();
            return winner;
        }

        private void SetCurrentPlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            SetCurrentPlayer();
            return true;
        }


        private bool DidPlayerWin()
        {
            return points[currentPlayer] != 6;
        }
    }
}