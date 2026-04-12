using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Workouts_of_trainers : BaseEntity
    {
        private Trainer id_trainer=null;
        private Kinds_of_workouts id_kind_of_workouts=null;
        public Trainer Id_trainer { get => id_trainer; set => id_trainer = value; }
        public Kinds_of_workouts Id_kind_of_workouts { get => id_kind_of_workouts; set => id_kind_of_workouts = value; }
    }
}
