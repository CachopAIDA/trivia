using System;
using System.IO;
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
				GameRunner.Main(null);
			}

			Assert.Equal(
					File.ReadAllText(Path.Combine(folder, "trivialOutput.0.txt")),
					File.ReadAllText(Path.Combine(folder, "trivialOutput.1.txt"))
			);
		}
	}
}