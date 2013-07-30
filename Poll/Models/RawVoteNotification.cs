using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poll.Models
{
    /// <summary>
    /// This class is used to parse the notification into an object
    /// </summary>
    public class RawVoteNotification
    {
        public int OptionId { get; set; }
        public int Count { get; set; }
    }
}
