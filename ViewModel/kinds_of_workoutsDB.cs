using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;


namespace ViewModel
{
    public class Kinds_of_workoutsDB : BaseDB
    {
        public Kinds_of_workoutsList SelectAll()
        {
            command.CommandText = $"SELECT Kinds_of_workouts.* FROM Kinds_of_workouts";
            Kinds_of_workoutsList pList = new Kinds_of_workoutsList(base.Select());
            return pList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Kinds_of_workouts kw = entity as Kinds_of_workouts;
            kw.Name_of_workout = reader["name_of_workout"].ToString();
            kw.Max_amount_of_people = int.Parse(reader["max_amount_of_people"].ToString());
           
            base.CreateModel(entity);
            return kw;
        }
        public override BaseEntity NewEntity()
        {
            return new Kinds_of_workouts();
        }
        static private Kinds_of_workoutsList list = new Kinds_of_workoutsList();
        public static Kinds_of_workouts SelectById(int id)
        {
            Kinds_of_workoutsDB db = new Kinds_of_workoutsDB();
            list = db.SelectAll();

            Kinds_of_workouts g = list.Find(item => item.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {

            Kinds_of_workouts k = entity as Kinds_of_workouts;
            if (k != null)
            {
                string sqlStr = "DELETE FROM Kinds_of_workouts where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", k.Id));

            }
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Kinds_of_workouts k = entity as Kinds_of_workouts;
            if (k != null)
            {
                string sqlStr = $"INSERT INTO Kinds_of_workouts( Name_of_workout , Max_amount_of_people)" +
                    $" VALUES (@name_of_workout, @max_amount_of_people)";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@name_of_workout", k.Name_of_workout));
                command.Parameters.Add(new OleDbParameter("@max_amount_of_people", k.Max_amount_of_people));
            }
        }
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            
            Kinds_of_workouts k = entity as Kinds_of_workouts;
            if (k != null)
            {
                string sqlStr = $"UPDATE Kinds_of_workouts " +
                    $"SET Name_of_workout=@name_of_workout,Max_amount_of_people=@max_amount_of_people" +
                    $" WHERE ID=@id";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@name_of_workout", k.Name_of_workout));
                command.Parameters.Add(new OleDbParameter("@max_amount_of_workout", k.Max_amount_of_people));
                command.Parameters.Add(new OleDbParameter("@id", k.Id));
            }
        }
    }
 }
