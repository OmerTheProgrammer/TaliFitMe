using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class TrainerDB : PersonDB
    {
        public TrainerList SelectAll()
        {
            command.CommandText = "SELECT Trainer.paymet_per_hour," +
                " Trainer.certificate, Trainer.experience, Trainer.description," +
                " Person.* FROM (Trainer INNER JOIN Person ON " +
                "Trainer.id = Person.id)";
            TrainerList groupList = new TrainerList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Trainer t = entity as Trainer;
            t.Paymet_per_hour = double.Parse(reader["paymet_per_hour"].ToString());
            t.Certificate = reader["certificate"].ToString();
            t.Experience = true;
            t.Description = reader["description"].ToString();
            base.CreateModel(entity);
            return t;
        }
        public override BaseEntity NewEntity()
        {
            return new Trainer();
        }
        static private TrainerList list = new TrainerList();
        public static Trainer SelectById(int id)
        {
            TrainerDB db = new TrainerDB();
            list = db.SelectAll();

            Trainer g = list.Find(item => item.Id == id);
            return g;
        }


        //protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    Trainee p = entity as Trainee;
        //    if (p != null)
        //    {
        //        // 1. עדכון ה-CommandText של ה-cmd הקיים (בלי לנקות פרמטרים גלובליים שקשורים לטרנזקציה)
        //        cmd.CommandText = "UPDATE Trainee SET " +
        //                        "Health_Declaration = @health_Declaration, " +
        //                        "Joining_Date = @joining_Date, " +
        //                        "Id_Sub = @id_Sub " +
        //                        "WHERE ID = @id";

        //        // 2. טיפול בטוח בהצהרת בריאות
        //        object healthToSave = p.Health_Declaration;
        //        if (healthToSave == null)
        //        {
        //            healthToSave = "";
        //        }

        //        // 3. טיפול בטוח בתאריך הצטרפות
        //        DateTime dateToSave = p.Joining_date;
        //        if (dateToSave == DateTime.MinValue)
        //        {
        //            dateToSave = DateTime.Today;
        //        }

        //        // 4. טיפול בטוח במנוי 
        //        object subIdToSave = DBNull.Value;
        //        if (p.Id_Sub != null && p.Id_Sub.Id != 0)
        //        {
        //            subIdToSave = p.Id_Sub.Id;
        //        }

        //        // 5. הוספת הפרמטרים בסדר המדויק
        //        cmd.Parameters.Add(new OleDbParameter("@health_Declaration", healthToSave));
        //        cmd.Parameters.Add(new OleDbParameter("@joining_Date", dateToSave));
        //        cmd.Parameters.Add(new OleDbParameter("@id_Sub", subIdToSave));
        //        cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
        //    }
        //}

        //public override void Update(BaseEntity entity)
        //{
        //    Trainer t = entity as Trainer;
        //    if (t != null)
        //    {
        //        // הוספת הפעולה המעודכנת לרשימת השינויים
        //        updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));

        //        // קריאה למחלקת הבסיס (למשל אם יש שדות בטבלת Person שצריך לעדכן)
        //        updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
        //    }
        //}



        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainer t = entity as Trainer;
            if (t != null)
            {
                // 1. Define the SQL string to update the Trainer-specific table
                // Adjust the table name (e.g., [Trainer] or Trainer) and column names to match your database exactly.
                cmd.CommandText = "UPDATE Trainer SET " +
                                  "Certificate = @certificate, " +
                                  "Experience = @experience, " +
                                  "Description = @description, " +
                                  "Paymet_per_hour = @paymentPerHour " +
                                  "WHERE ID = @id";

                cmd.Parameters.Clear(); // Clear parameters for this specific query scope

                // 2. Map the Trainer properties to the query parameters in exact order
                cmd.Parameters.Add(new OleDbParameter("@certificate", t.Certificate ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@experience", t.Experience));
                cmd.Parameters.Add(new OleDbParameter("@description", t.Description ?? (object)DBNull.Value)); // <-- Fixed the typo here
                cmd.Parameters.Add(new OleDbParameter("@paymentPerHour", t.Paymet_per_hour));

                // 3. The ID parameter must be last because it's used in the WHERE clause
                cmd.Parameters.Add(new OleDbParameter("@id", t.Id));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Trainer t = entity as Trainer;
            if (t != null)
            {
                // FIRST: Queue the Trainer specific fields update using our new method
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));

                // SECOND: Queue the Person base fields update (First Name, Last Name, Email, etc.)
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
            }
        }

      





        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainer t = entity as Trainer;
            if (t != null)
            {
                string sqlStr = $"INSERT INTO Trainer( Id, Paymet_per_hour ,Certificate, Experience , Description ) " +
                    $"VALUES (@id, @paymet_per_hour, @certificate , @Experience ,@description)";
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new OleDbParameter("@id", t.Id));
                cmd.Parameters.Add(new OleDbParameter("@paymet_per_hour", t.Paymet_per_hour));
                cmd.Parameters.Add(new OleDbParameter("@certificate", t.Certificate));
                cmd.Parameters.Add(new OleDbParameter("@experience", t.Experience));
                cmd.Parameters.Add(new OleDbParameter("@ description", t.Description));
            }
        }
        public override void Insert(BaseEntity entity)
        {
            Trainer tr = entity as Trainer;
            if (tr != null)
            {
                inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
            }
        }
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainer te = entity as Trainer;
            if (te != null)
            {
                string sqlStr = "DELETE FROM Trainer WHERE id = @pid";

                // שינוי מ-command ל-cmd (משתמשים במשתנה שמתקבל בבנאי של הפונקציה)
                cmd.CommandText = sqlStr;

                // הוספת הפרמטר ל-cmd שקיבלנו
                cmd.Parameters.Add(new OleDbParameter("@pid", te.Id));
            }
        }

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            // שימי לב: שיניתי גם ל-&& שזה תקין יותר ב-C#
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                // משאירים רק את השורה הזו שקוראת לפונקציה המעודכנת שלך!
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
            }
        }
    }
}

