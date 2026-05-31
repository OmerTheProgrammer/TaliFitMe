using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class PersonDB : BaseDB
    {
        public PersonList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Person";
            PersonList groupList = new PersonList(base.Select());
            return groupList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Person p = entity as Person;
            p.First_name = reader["first_name"].ToString();
            p.Last_name = reader["last_name"].ToString();
            p.Telephone = reader["telephone"].ToString();
            p.Num_id = reader["num_id"].ToString();
            p.Email = reader["email"].ToString();
            p.Born_date = DateTime.Parse(reader["born_date"].ToString());
            p.Photo = reader["photo"].ToString();
            p.User_name = reader["user_name"].ToString();
            p.Pass = reader["pass"].ToString();
            p.Id_gender = GenderDB.SelectById((int)reader["id_gender"]);
            //string imagePath = Path()+"Images\\"+reader["photoPath"].ToString();
            //string base64String= ImageToBase64Converter.ImageToBase64(imagePath);
            //p.Photo = base64String;
            string fileName = reader["photo"]?.ToString() ?? "";
            if (!string.IsNullOrEmpty(fileName))
            {
                string base64Result = ImageToBase64Converter.ImageFromResourceToBase64(fileName);

                if (!string.IsNullOrEmpty(base64Result))
                {
                    p.Photo = base64Result;
                }
                else
                {
                    p.Photo = "Missing resource: " + fileName;
                }
            }
            base.CreateModel(entity);
            return p;
        }


        public string SelectPersonPhotoById(int id)
        {
            PersonList pList= SelectAll();
            Person p = pList.Find(item => item.Id == id);
            string photo=p.Photo;
            return photo;
        }
        public override BaseEntity NewEntity()
        {
            return new Person();
        }
        static private PersonList list = new PersonList();
        public static Person SelectById(int id)
        {
            PersonDB db = new PersonDB();
            list = db.SelectAll();

            Person g = list.Find(item => item.Id == id);
            return g;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person p = entity as Person;
            if (p != null)
            {
                string sqlStr = "DELETE FROM Person where id=@pid";

                command.CommandText = sqlStr;

                command.Parameters.Add(new OleDbParameter("@pid", p.Id));

            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person p = entity as Person;
            if (p != null)
            {
                string sqlStr = $"INSERT INTO Person( First_name, Last_name " +
                    $", Telephone , Num_id , Email, Born_date , Photo , User_name ," +
                    $" Pass ,Id_gender) VALUES (@first_name, @last_name , @telephone , " +
                    $"@num_id , @email, @born_date , @photo , @user_name , @pass ,@id_gender)";
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name));
                cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name));
                cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone));
                cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id));
                cmd.Parameters.Add(new OleDbParameter("@email", p.Email));
                cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));
                cmd.Parameters.Add(new OleDbParameter("@photo", p.Photo));
                cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name));
                cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass));
                cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender.Id));
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person p = entity as Person;
            if (p != null)
            {
                string sqlStr = $"UPDATE Person SET First_name=@first_name, Last_name=@last_name " +
                    $", Telephone=@telephone , Num_id=@num_id , Email=@email, Born_date=@born_date " +
                    $", Photo=@photo , User_name=@user_name , Pass=@pass , Id_gender=@id_gender WHERE ID=@id";
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name));
                cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name));
                cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone));
                cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id));
                cmd.Parameters.Add(new OleDbParameter("@email", p.Email));
                cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));
                cmd.Parameters.Add(new OleDbParameter("@photo", p.Photo));
                cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name));
                cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass));
                cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender.Id));
                cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
            }
        }
    }
}
