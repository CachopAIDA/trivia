using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class Questions
    {
        private readonly string[] categories = new []{ "Pop", "Rock", "Sports", "Science" };
        private readonly Dictionary<string, Question> questions = new Dictionary<string, Question>();

        public Questions()
        {
            foreach (var category in categories)
            {
                this.questions.Add(category, new Question(category));
            }
        }

        public void AskNextQuestion(string category)
        {
            if (questions.TryGetValue(category, out var question))
                question.NextQuestion();
        }
    }
}
