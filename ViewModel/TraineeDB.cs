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
            command.CommandText = $"SELECT  Person.* , Trainee.health_Declaration, "+
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
                // 1. ניקוי פרמטרים קודמים כדי שסדר הפרמטרים של ה-Trainee יהיה ראשון ונקי
                cmd.Parameters.Clear();

                // 2. שאילתת ה-UPDATE
                string sqlStr = "UPDATE Trainee SET " +
                                "Health_Declaration = @health_Declaration, " +
                                "Joining_Date = @joining_Date, " +
                                "Id_Sub = @id_Sub " +
                                "WHERE ID = @id";

                cmd.CommandText = sqlStr;

                // טיפול בהצהרת בריאות
                object healthToSave = p.Health_Declaration;
                if (healthToSave == null)
                {
                    healthToSave = "";
                }

                // טיפול בתאריך הצטרפות
                DateTime dateToSave = p.Joining_date;
                if (dateToSave == DateTime.MinValue)
                {
                    dateToSave = DateTime.Today;
                }

                // טיפול במנוי - כאן הוספנו בדיקה: אם המנוי הוא 0 או null, נשמור null ב-Access
                // (בתנאי שבממסד הנתונים השדה id_Sub מאפשר ערכים ריקים)
                object subIdToSave = DBNull.Value;
                if (p.Id_Sub != null && p.Id_Sub.Id != 0)
                {
                    subIdToSave = p.Id_Sub.Id;
                }

                // 3. הוספת הפרמטרים ל-cmd לפי הסדר המדויק בשאילתה
                cmd.Parameters.Add(new OleDbParameter("@health_Declaration", healthToSave));
                cmd.Parameters.Add(new OleDbParameter("@joining_Date", dateToSave));
                cmd.Parameters.Add(new OleDbParameter("@id_Sub", subIdToSave));

                // הפרמטר של ה-WHERE חייב להיות האחרון ברשימה!
                cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Trainee t = entity as Trainee;
            if (t != null)
            {
                // הוספת הפעולה המעודכנת לרשימת השינויים
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));

                // קריאה למחלקת הבסיס (למשל אם יש שדות בטבלת Person שצריך לעדכן)
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
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
