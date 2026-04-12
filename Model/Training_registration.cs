using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Training_registration : BaseEntity
    {
        private Trainee id_trainee = null;
        private List_of_Exc_workouts id_excWorkouts = null;

        public Trainee Id_trainee { get => id_trainee; set => id_trainee = value; }
        public List_of_Exc_workouts Id_excWorkouts { get => id_excWorkouts; set => id_excWorkouts = value; }
    }
}
