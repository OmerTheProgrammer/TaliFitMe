using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Trainer: Person
    {
        private double paymet_per_hour;
        private string certificate;     
        private bool experience;
        private string description;

        public double Paymet_per_hour { get => paymet_per_hour; set => paymet_per_hour = value; }
        public string Certificate { get => certificate; set => certificate = value; }
        public bool Experience { get => experience; set => experience = value; }
        public string Description { get => description; set => description = value; }
    }
}
