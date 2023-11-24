using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Data.Entity
{
    internal class Question
    {
        public Guid QuestionId { get; set; }

        public Guid IdTest { get; set; }
        public string QuestionTest { get; set; } = null!;
        public int NumberQuestion { get; set; }
        public string Answer1 { get; set; } = null!;
        public string Answer2 { get; set; } = null!;
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? Answer5 { get; set; }

        public bool AnswerBool1 { get; set; } = false;    
        public bool AnswerBool2 { get; set; } = false;
        public bool AnswerBool3 { get; set; } = false;
        public bool AnswerBool4 { get; set; } = false;
        public bool AnswerBool5 { get; set; } = false;

       
    }
}
