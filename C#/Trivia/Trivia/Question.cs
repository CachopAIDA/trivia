using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Question
    {
        private string category;
        private readonly LinkedList<string> items;

        public Question(string category)
        {
            this.category = category;
            items = new LinkedList<string>();
            for (var i = 0; i < 50; i++) items.AddLast($"{category} Question {i}");
        }

        public void NextQuestion()
        {
            Console.WriteLine(items.First());
            items.RemoveFirst();
        }
    }
}