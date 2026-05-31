using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class List_of_Exc_workouts: BaseEntity
    {
        private Kinds_of_workouts id_kindOf_workouts = null;
        private DateTime workout_date ;
        private Trainer id_trainer = null;

        public Kinds_of_workouts Id_kindOf_workouts { get => id_kindOf_workouts; set => id_kindOf_workouts = value; }
        public DateTime Workout_date { get => workout_date; set => workout_date = value; }
        public Trainer Id_trainer { get => id_trainer; set => id_trainer = value; }
    }
}
