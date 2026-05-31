using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class Workouts_of_trainersDB : BaseDB
    {
        public Workouts_of_trainersList SelectAll()
        {
            command.CommandText = $"SELECT  id, id_trainer, id_kind_of_workouts FROM  Workouts_of_trainers";
           Workouts_of_trainersList groupList = new Workouts_of_trainersList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
           Workouts_of_trainers w = entity as Workouts_of_trainers;
           w.Id_trainer = TrainerDB.SelectById((int)reader["id_trainer"]);
           w.Id_kind_of_workouts= Kinds_of_workoutsDB.SelectById((int)reader["id_kind_of_workouts"]);
            base.CreateModel(entity);
            return w;
        }
        public override BaseEntity NewEntity()
        {
            return new Workouts_of_trainers();
        }
        static private Workouts_of_trainersList list = new Workouts_of_trainersList();
        public static Workouts_of_trainers SelectById(int id)
        {
           Workouts_of_trainersDB db = new Workouts_of_trainersDB();
            list = db.SelectAll();

           Workouts_of_trainers g = list.Find(item => item.Id == id);
            return g;
        }
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Workouts_of_trainers w = entity as Workouts_of_trainers;
            if (w != null)
            {
                string sqlStr = "DELETE FROM Workouts_of_trainers where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", w.Id));
            }
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Workouts_of_trainers p = entity as Workouts_of_trainers;
            if (p != null)
            {
                string sqlStr = $"INSERT INTO  Workouts_of_trainers ( Id_trainer, Id_kind_of_workouts) VALUES" +
                    $" (@id_trainer, @id_kind_of_workouts)";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id_trainer", p.Id_trainer.Id));
                command.Parameters.Add(new OleDbParameter("@id_kind_of_workouts", p.Id_kind_of_workouts.Id));
            }
        }
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {

            Workouts_of_trainers wt = entity as Workouts_of_trainers;
            if (wt != null)
            {
                string sqlStr = $"UPDATE Workouts_of_trainers SET Id_trainer=@id_trainer , " +
                    $"Id_kind_of_workouts=@id_kind_of_workouts" +
                    $" WHERE ID=@id";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id_trainer", wt.Id_trainer.Id));
                command.Parameters.Add(new OleDbParameter("@id_kind_of_workouts", wt.Id_kind_of_workouts.Id));
                command.Parameters.Add(new OleDbParameter("@id", wt.Id));
            }
        }
    }
}
