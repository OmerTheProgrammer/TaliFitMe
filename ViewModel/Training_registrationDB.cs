using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class Training_registrationDB : BaseDB
    {
        public Training_registrationList SelectAll()
        {
            command.CommandText = $"SELECT  id, id_trainee, id_excWorkouts FROM Training_registration";
            Training_registrationList groupList = new Training_registrationList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Training_registration tr = entity as Training_registration;

            var z = reader[1].ToString();
            tr.Id_trainee = TraineeDB.SelectById(int.Parse(reader[1].ToString()));
            tr.Id_excWorkouts = List_of_Exc_workoutsDB.SelectById(int.Parse(reader[2].ToString()));
            base.CreateModel(entity);
            return tr;
        }
        public override BaseEntity NewEntity()
        {
            return new Training_registration();
        }
        static private Training_registrationList list = new Training_registrationList();

        public static Training_registration SelectById(int id)
        {
            Training_registrationDB db = new Training_registrationDB();
            list = db.SelectAll();

            Training_registration g = list.Find(item => item.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Training_registration t = entity as Training_registration;
            if (t != null)
            {
                string sqlStr = "DELETE FROM Training_registration where id=@pid";

                command.CommandText = sqlStr;

                command.Parameters.Add(new OleDbParameter("@pid", t.Id));

            }
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Training_registration tr = entity as Training_registration;
            if (tr != null)
            {
                string sqlStr = $"INSERT INTO Training_registration ( Id_trainee, Id_excWorkouts)" +
                    $" VALUES (@id_trainee, @id_excWorkouts)";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id_trainee", tr.Id_trainee.Id));
                command.Parameters.Add(new OleDbParameter("@id_excWorkouts", tr.Id_excWorkouts.Id));

            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Training_registration t = entity as Training_registration;
            if (t != null)
            {
                string sqlStr = $"UPDATE Training_registration " +
                    $"SET Id_trainee=@id_trainee , Id_excWorkouts=@id_excWorkouts" +
                    $" WHERE ID=@id";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id_trainee", t.Id_trainee.Id));
                command.Parameters.Add(new OleDbParameter("@id_excWorkouts", t.Id_excWorkouts.Id));
                command.Parameters.Add(new OleDbParameter("@id", t.Id));
            }
        }
    }
}

