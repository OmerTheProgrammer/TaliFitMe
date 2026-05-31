using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Trainee: Person
    {
        private bool health_Declaration = false;
        private DateTime joining_date = DateTime.Now;
        private Subscription id_Sub = null;
       


        public DateTime Joining_date { get => joining_date; set => joining_date = value; }
        public Subscription Id_Sub { get => id_Sub; set => id_Sub = value; }
        public bool Health_Declaration { get => health_Declaration; set => health_Declaration = value; }
    }
}
