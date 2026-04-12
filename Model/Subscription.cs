using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Subscription : BaseEntity
    {
        private string name_of_sub = "";
        private double price = 0;

        public string Name_of_sub { get => name_of_sub; set => name_of_sub = value; }
        public double Price { get => price; set => price = value; }
    }
}
