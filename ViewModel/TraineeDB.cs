using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class TraineeDB : PersonDB
    {
        public TraineeList SelectAll()
        {
            command.CommandText = $"SELECT  Person.id, Person.first_name, Person.last_name, " +
                $"Person.telephone, Person.born_date, Person.photo, Person.user_name, Person.pass, " +
                $"Person.id_gender, Person.email, Person.num_id, Trainee.health_Declaration, "+
                         $" Trainee.joining_date, Trainee.id_Sub "+
                         $" FROM(Person INNER JOIN "+
                         $" Trainee ON Person.id = Trainee.id)";
            TraineeList groupList = new TraineeList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Trainee te = entity as Trainee;
            var XX = reader["health_Declaration"].ToString();
            te.Health_Declaration = bool.Parse(reader["health_Declaration"].ToString());
            te.Joining_date = DateTime.Parse(reader["joining_date"].ToString());
            te.Id_Sub = SubscriptionDB.SelectById((int)reader["id_Sub"]);
            base.CreateModel(entity);
            return te;
        }
        public override BaseEntity NewEntity()
        {
            return new Trainee();
        }
        static private TraineeList list = new TraineeList();

        public static Trainee SelectById(int id)
        {
            TraineeDB db = new TraineeDB();
            list = db.SelectAll();

            Trainee te = list.Find(item => item.Id == id);
            return te;
        }
     
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainee p = entity as Trainee;
            if (p != null)
            {
                // שימוש ב-cmd שהתקבל כפרמטר וניקוי שאריות של טבלאות אחרות
                //cmd.Parameters.Clear();

                string sqlStr = $"UPDATE Trainee SET Health_Declaration=@health_Declaration, " +
                                $"Joining_Date=@joining_Date, Id_Sub=@id_Sub " +
                                $"WHERE ID=@id";

                cmd.CommandText = sqlStr;

                // הוספת הפרמטרים ל-cmd לפי הסדר המדויק בשאילתה
                cmd.Parameters.Add(new OleDbParameter("@health_Declaration", p.Health_Declaration));
                cmd.Parameters.Add(new OleDbParameter("@joining_date", p.Joining_date));
                cmd.Parameters.Add(new OleDbParameter("@id_Sub", p.Id_Sub.Id));
                cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Trainee t = entity as Trainee;
            if (t != null)
            {
                // הפיכת הסדר: קודם מעדכנים את האבא Person (שם יושבת התמונה) ורק אז את הבן Trainee
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
            }
        }


        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainee t = entity as Trainee;
            if (t != null)
            {
                string sqlStr = $"INSERT INTO Trainee(id, health_Declaration , joining_date , id_Sub ) " +
                    $"VALUES (@id, @health_Declaration, @joining_date , @id_Sub)";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id", t.Id));//כי יורש
                command.Parameters.Add(new OleDbParameter("@health_Declaration",false));
                command.Parameters.Add(new OleDbParameter("@joining_date", t.Joining_date.Date));
                if(t.Id_Sub != null)
                {
                    command.Parameters.Add(new OleDbParameter("@id_Sub", t.Id_Sub.Id));
                }
                else
                {
                    command.Parameters.Add(new OleDbParameter("@id_Sub", 1));
                }

            }
        }
        public override void Insert(BaseEntity entity)
        {
            Trainee t = entity as Trainee;
            if (t != null)
            {
                inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
            }
        }
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainee te = entity as Trainee;
            if (te != null)
            {
                string sqlStr = "DELETE FROM Trainee where id=@pid";

                command.CommandText = sqlStr;

                command.Parameters.Add(new OleDbParameter("@pid", te.Id));

            }
        }

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
            }
        }

    }
}
