using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class ManagerDB : PersonDB
    {
        public ManagerList SelectAll()
        {
            command.CommandText = $"SELECT Person.first_name, Person.last_name, Person.telephone, Person.num_id, Person.born_date, Person.email, Person.id_gender, Person.pass, Person.user_name, Person.photo, Manager.id\r\nFROM            (Manager INNER JOIN\r\n                         Person ON Manager.id = Person.id)";
            ManagerList groupList = new ManagerList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Manager mg = entity as Manager;
            base.CreateModel(entity);
            return mg;
        }
        public override BaseEntity NewEntity()
        {
            return new Manager();
        }
        
        static private ManagerList list = new ManagerList();

        public static Manager SelectById(int id)
        {
            ManagerDB db = new ManagerDB();
            list = db.SelectAll();

            Manager g = list.Find(item => item.Id == id);
            return g;
        }
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Manager p = entity as Manager;
            if (p != null)
            {
                string sqlStr = $"INSERT INTO Manager( ) VALUES ()";
                command.CommandText = sqlStr;

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
            Manager p = entity as Manager;
            if (p != null)
            {
                string sqlStr = "DELETE FROM Manager where id=@pid";

                command.CommandText = sqlStr;

                command.Parameters.Add(new OleDbParameter("@pid", p.Id));

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
