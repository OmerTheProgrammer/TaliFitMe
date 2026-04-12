using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Kinds_of_workouts : BaseEntity
    {
        private string name_of_workout = "";
        private int max_amount_of_people = 0;

        public string Name_of_workout { get => name_of_workout; set => name_of_workout = value; }
        public int Max_amount_of_people { get => max_amount_of_people; set => max_amount_of_people = value; }
    }
}
