using System;
using System.IO;
using System.Text;
using ApprovalTests;
using Trivia;
using Xunit;

namespace Tests
{
    public class GoldenMasterTest
    {
        [Fact]
        public void output_for_adding_3_players_to_the_game()
        {
            var expectedResult =
                "Chet was added\r\nThey are player number 1\r\nPat was added\r\nThey are player number 2\r\nSue was added\r\nThey are player number 3\r\n";
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            var game = new Game();

            game.Add("Chet");
            game.Add("Pat");
            game.Add("Sue");

            Assert.Equal(expectedResult, fakeoutput.ToString());
        }

        [Fact]
        public void output_for_adding_3_players_to_the_game_and_every_player_has_a_turn()
        {
            var expectedResult =
                @"Chet was added
They are player number 1
Pat was added
They are player number 2
Sue was added
They are player number 3
Chet is the current player
They have rolled a 5
Chet's new location is 5
The category is Science
Science Question 0
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 6
The category is Sports
Sports Question 0
Question was incorrectly answered
Pat was sent to the penalty box
Sue is the current player
They have rolled a 1
Sue's new location is 1
The category is Science
Science Question 1
Answer was corrent!!!!
Sue now has 1 Gold Coins.
";
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            var game = new Game();

            game.Add("Chet");
            game.Add("Pat");
            game.Add("Sue");
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WrongAnswer();
            game.Roll(1);
            game.WasCorrectlyAnswered();

            Assert.Equal(expectedResult, fakeoutput.ToString());
        }

        [Fact]
        public void output_for_adding_2_players_to_the_game_and_player2_wins()
        {
            var expectedResult =
                @"Chet was added
They are player number 1
Pat was added
They are player number 2
Chet is the current player
They have rolled a 5
Chet's new location is 5
The category is Science
Science Question 0
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 6
The category is Sports
Sports Question 0
Answer was corrent!!!!
Pat now has 1 Gold Coins.
Chet is the current player
They have rolled a 5
Chet is getting out of the penalty box
Chet's new location is 10
The category is Sports
Sports Question 1
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 0
The category is Pop
Pop Question 0
Answer was corrent!!!!
Pat now has 2 Gold Coins.
Chet is the current player
They have rolled a 5
Chet is getting out of the penalty box
Chet's new location is 3
The category is Rock
Rock Question 0
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 6
The category is Sports
Sports Question 2
Answer was corrent!!!!
Pat now has 3 Gold Coins.
Chet is the current player
They have rolled a 5
Chet is getting out of the penalty box
Chet's new location is 8
The category is Pop
Pop Question 1
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 0
The category is Pop
Pop Question 2
Answer was corrent!!!!
Pat now has 4 Gold Coins.
Chet is the current player
They have rolled a 5
Chet is getting out of the penalty box
Chet's new location is 1
The category is Science
Science Question 1
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 6
The category is Sports
Sports Question 3
Answer was corrent!!!!
Pat now has 5 Gold Coins.
Chet is the current player
They have rolled a 5
Chet is getting out of the penalty box
Chet's new location is 6
The category is Sports
Sports Question 4
Question was incorrectly answered
Chet was sent to the penalty box
Pat is the current player
They have rolled a 6
Pat's new location is 0
The category is Pop
Pop Question 3
Answer was corrent!!!!
Pat now has 6 Gold Coins.
";
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            var game = new Game();

            game.Add("Chet");
            game.Add("Pat");
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(5);
            game.WrongAnswer();
            game.Roll(6);
            game.WasCorrectlyAnswered();

            Assert.Equal(expectedResult, fakeoutput.ToString());
        }

        [Fact]
        public void output_for_0_players_and_try_play_throws_ArgumentOutOfRangeException()
        {
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            var game = new Game();

            Assert.Throws<ArgumentOutOfRangeException>(() => game.Roll(5));
        }
    }
}