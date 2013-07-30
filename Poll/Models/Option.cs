using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poll.Models
{
    public class Option : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private int _Count;
        [JsonIgnore] // We're using JsonIgnore because we don't want the framework to create a column for that in the database, as it is computed through the notifications
        public int Count
        {
            get { return _Count; }
            set
            {
                if (value == _Count)
                {
                    return;
                }
                _Count = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
