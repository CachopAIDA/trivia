using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Question
    {
        private LinkedList<string> items;
        private string category;

        public Question(string category)
        {
            this.category = category;
            this.items = new LinkedList<string>();
            for (var i = 0; i < 50; i++)
            {
                this.items.AddLast($"{category} Question {i}");
            }
        }

        public void NextQuestion()
        {
            Console.WriteLine(this.items.First());
            this.items.RemoveFirst();
        }
    }
}