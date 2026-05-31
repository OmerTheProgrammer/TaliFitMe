using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class List_of_Exc_workoutsDB : BaseDB
    {
        public List_of_Exc_workoutsList SelectAll()
        {
            command.CommandText = $"SELECT  * FROM  List_of_Exc_workouts";
            List_of_Exc_workoutsList groupList = new List_of_Exc_workoutsList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            List_of_Exc_workouts lw = entity as List_of_Exc_workouts;
            lw.Workout_date = DateTime.Parse(reader["workout_date"].ToString());
            int x= (int)reader["id_trainer"];
            lw.Id_trainer = TrainerDB.SelectById((int)reader["id_trainer"]);
            lw.Id_kindOf_workouts = Kinds_of_workoutsDB.SelectById((int)reader["id_kindOf_workouts"]);
            base.CreateModel(entity);
            return lw;
        }
        public override BaseEntity NewEntity()
        {
            return new List_of_Exc_workouts();
        }
        static private List_of_Exc_workoutsList list = new List_of_Exc_workoutsList();

        public static List_of_Exc_workouts SelectById(int id)
        {
            List_of_Exc_workoutsDB db = new List_of_Exc_workoutsDB();
            list = db.SelectAll();

            List_of_Exc_workouts g = list.Find(item => item.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            List_of_Exc_workouts l = entity as List_of_Exc_workouts;
            if (l != null)
            {
                string sqlStr = "DELETE FROM List_of_Exc_workouts where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", l.Id));
            }
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            List_of_Exc_workouts lw = entity as List_of_Exc_workouts;
            if (lw != null)
            {
                string sqlStr = $"INSERT INTO List_of_Exc_workouts ( Id_kindOf_workouts, Workout_date , Id_trainer) VALUES " +
                    $"(@id_kindOf_workouts , @workout_date , @id_trainer)";
                command.CommandText = sqlStr;
                 command.Parameters.Add(new OleDbParameter("@id_kindOf_workouts", lw.Id_kindOf_workouts.Id));
                OleDbParameter oleDbParameter = new OleDbParameter("@workout_date", lw.Workout_date);
                oleDbParameter.Value = lw.Workout_date;
                command.Parameters.Add(oleDbParameter);
                command.Parameters.Add(new OleDbParameter("@id_trainer", lw.Id_trainer.Id));
            }
        }
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            List_of_Exc_workouts l = entity as List_of_Exc_workouts;
            if (l != null)
            {
                string sqlStr = $"UPDATE List_of_Exc_workouts " +
                    $"SET Id_kindOf_workouts=@id_kindOf_workouts,Workout_date=@workout_date," +" Id_trainer=@id_trainer" +
                    $" WHERE ID=@id";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id_kindOf_workouts", l.Id_kindOf_workouts.Id));
                OleDbParameter oleDbParameter = new OleDbParameter("@workout_date", l.Workout_date);
                oleDbParameter.Value = l.Workout_date;
                command.Parameters.Add(oleDbParameter);

                var x = l;
                command.Parameters.Add(new OleDbParameter("@id_trainer", l.Id_trainer.Id));
                command.Parameters.Add(new OleDbParameter("@id", l.Id));
            }
        }
    }
}
