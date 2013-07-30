using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poll.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public string UserId { get; set; }
    }
}
