using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Trivia;
using Xunit;

namespace Tests
{
	public class GoldenMasterTest
	{
		[Fact]
		public void Verify_output_is_random()
		{
			const string folder = @"../../../Output";
			Directory.CreateDirectory(folder);

			for (var i = 0; i <= 1; i++)
			{
				var file = $"trivialOutput.{i}.txt";
				using var fileStream = new FileStream(Path.Combine(folder, file), FileMode.Create);
				using var outputStream = new StreamWriter(fileStream);
				Console.SetOut(outputStream);
				GameRunner.PlayGame(new Random(1));
			}

			Assert.Equal(
					File.ReadAllText(Path.Combine(folder, "trivialOutput.0.txt")),
					File.ReadAllText(Path.Combine(folder, "trivialOutput.1.txt"))
			);
		}

        [Fact]
        public void Generate_golden_master_files()
        {
            const string folder = @"../../../OutputGM";
            Directory.CreateDirectory(folder);

            for (var i = 0; i <= 1000; i++)
            {
                var file = $"trivialOutput.{i}.txt";
                using var fileStream = new FileStream(Path.Combine(folder, file), FileMode.Create);
                using var outputStream = new StreamWriter(fileStream);
                Console.SetOut(outputStream);
                GameRunner.PlayGame(new Random(i));
            }
        }

        [Fact]
        public void Verify_file_content()
        {
            const string folder = @"../../../OutputGM";
            for (var i = 0; i <= 1000; i++)
            {
                var file = $"trivialOutput.{i}.txt";

                var expectedContent = new StringBuilder();
                Console.SetOut(new StringWriter(expectedContent));

                GameRunner.PlayGame(new Random(i));
                Assert.Equal(expectedContent.ToString(), File.ReadAllText(Path.Combine(folder, file)));
            }
        }
	}
}