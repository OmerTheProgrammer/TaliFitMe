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
            command.CommandText = $"SELECT Person.*, Trainer.paymet_per_hour, Trainer.certificate," +
                $" Trainer.experience, Trainer.description FROM  (Person INNER JOIN  Trainer ON Person.id = Trainer.id)";
            TrainerList groupList = new TrainerList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Trainer t = entity as Trainer;
            t.Paymet_per_hour= double.Parse(reader["paymet_per_hour"].ToString());
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
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Trainer p = entity as Trainer;
            if (p != null)
            {
                string sqlStr = $"UPDATE Trainer SET Paymet_per_hour=@paymet_per_hour," +
                    $"Certificate=@certificate,Experience=@experience,Description=@description " +
                    $" WHERE ID=@id";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@paymet_per_hour", p.Paymet_per_hour));
                command.Parameters.Add(new OleDbParameter("@certificate", p.Certificate));
                command.Parameters.Add(new OleDbParameter("@experience", p.Experience));
                command.Parameters.Add(new OleDbParameter("@description", p.Description));
                command.Parameters.Add(new OleDbParameter("@id", p.Id));
            }
        }
      
        public override void Update(BaseEntity entity)
        {
            Trainer t = entity as Trainer;
            if (t != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
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
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id", t.Id));
                command.Parameters.Add(new OleDbParameter("@paymet_per_hour", t.Paymet_per_hour));
                command.Parameters.Add(new OleDbParameter("@certificate", t.Certificate));
                command.Parameters.Add(new OleDbParameter("@experience", t.Experience));
                command.Parameters.Add(new OleDbParameter("@ description", t.Description));
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
                string sqlStr = "DELETE FROM Trainer where id=@pid";

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

