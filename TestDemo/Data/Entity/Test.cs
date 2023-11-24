using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Data.Entity
{
    public class Test
    {
        public Guid Id { get; set; }
        public String NameTest { get; set; } = null!; // назва тесту
        public DateTime CreatedAt { get; set; } //  час створення
        public DateTime? UpdatedAt { get; set; } //  дати редактування
        public int QuestionCount { get; set; } // кількість питань

    }
}
