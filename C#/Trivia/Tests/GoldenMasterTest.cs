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
    }
}