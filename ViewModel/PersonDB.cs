//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Model;
//using System.Data.OleDb;

//namespace ViewModel
//{
//    public class PersonDB : BaseDB
//    {
//        public PersonList SelectAll()
//        {
//            command.CommandText = $"SELECT * FROM Person";
//            PersonList groupList = new PersonList(base.Select());
//            return groupList;
//        }
//        protected override BaseEntity CreateModel(BaseEntity entity)
//        {
//            Person p = entity as Person;
//            p.First_name = reader["first_name"].ToString();
//            p.Last_name = reader["last_name"].ToString();
//            p.Telephone = reader["telephone"].ToString();
//            p.Num_id = reader["num_id"].ToString();
//            p.Email = reader["email"].ToString();
//            p.Born_date = DateTime.Parse(reader["born_date"].ToString());
//            p.Photo = reader["photo"].ToString();
//            p.User_name = reader["user_name"].ToString();
//            p.Pass = reader["pass"].ToString();
//            p.Id_gender = GenderDB.SelectById((int)reader["id_gender"]);
//            //string imagePath = Path()+"Images\\"+reader["photoPath"].ToString();
//            //string base64String= ImageToBase64Converter.ImageToBase64(imagePath);
//            //p.Photo = base64String;
//            string fileName = reader["photo"]?.ToString() ?? "";
//            if (!string.IsNullOrEmpty(fileName))
//            {
//                string base64Result = ImageToBase64Converter.ImageFromResourceToBase64(fileName);

//                if (!string.IsNullOrEmpty(base64Result))
//                {
//                    p.Photo = base64Result;
//                }
//                else
//                {
//                    p.Photo = "Missing resource: " + fileName;
//                }
//            }
//            base.CreateModel(entity);
//            return p;
//        }


//        public string SelectPersonPhotoById(int id)
//        {
//            PersonList pList= SelectAll();
//            Person p = pList.Find(item => item.Id == id);
//            string photo=p.Photo;
//            return photo;
//        }
//        public override BaseEntity NewEntity()
//        {
//            return new Person();
//        }
//        static private PersonList list = new PersonList();
//        public static Person SelectById(int id)
//        {
//            PersonDB db = new PersonDB();
//            list = db.SelectAll();

//            Person g = list.Find(item => item.Id == id);
//            return g;
//        }

//        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
//        {
//            Person p = entity as Person;
//            if (p != null)
//            {
//                string sqlStr = "DELETE FROM Person where id=@pid";

//                command.CommandText = sqlStr;

//                command.Parameters.Add(new OleDbParameter("@pid", p.Id));

//            }
//        }

//        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
//        {
//            Person p = entity as Person;
//            if (p != null)
//            {
//                string sqlStr = $"INSERT INTO Person( First_name, Last_name " +
//                    $", Telephone , Num_id , Email, Born_date , Photo , User_name ," +
//                    $" Pass ,Id_gender) VALUES (@first_name, @last_name , @telephone , " +
//                    $"@num_id , @email, @born_date , @photo , @user_name , @pass ,@id_gender)";
//                cmd.CommandText = sqlStr;
//                cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name));
//                cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name));
//                cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone));
//                cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id));
//                cmd.Parameters.Add(new OleDbParameter("@email", p.Email));
//                cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));
//                cmd.Parameters.Add(new OleDbParameter("@photo", p.Photo));
//                cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name));
//                cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass));
//                cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender.Id));
//            }
//        }

//        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
//        {
//            Person p = entity as Person;
//            if (p != null)
//            {
//                string sqlStr = $"UPDATE Person SET First_name=@first_name, Last_name=@last_name " +
//                    $", Telephone=@telephone , Num_id=@num_id , Email=@email, Born_date=@born_date " +
//                    $", Photo=@photo , User_name=@user_name , Pass=@pass , Id_gender=@id_gender WHERE ID=@id";
//                cmd.CommandText = sqlStr;
//                cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name));
//                cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name));
//                cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone));
//                cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id));
//                cmd.Parameters.Add(new OleDbParameter("@email", p.Email));
//                cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));
//                cmd.Parameters.Add(new OleDbParameter("@photo", p.Photo));
//                cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name));
//                cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass));
//                cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender.Id));
//                cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
//            }
//        }
//    }
//}
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
            p.User_name = reader["user_name"].ToString();
            p.Pass = reader["pass"].ToString();
            p.Id_gender = GenderDB.SelectById((int)reader["id_gender"]);

            if (reader["photoPath"] == DBNull.Value)
            {
                p.Photo = reader["photo"]?.ToString() ?? "";
                p.PhotoPath = null;
            }
            else
            {
                p.PhotoPath = Path() + "\\Photos\\" + reader["photoPath"].ToString();
                string fileName = p.PhotoPath;
                if (!string.IsNullOrEmpty(fileName))
                {
                    string base64Result = ImageToBase64Converter.ImageToBase64(fileName);

                    if (!string.IsNullOrEmpty(base64Result))
                    {
                        p.Photo = base64Result;
                    }
                    else
                    {
                        p.Photo = "Missing resource: " + fileName;
                    }
                }
            }

            base.CreateModel(entity);
            return p;
        }

        //public string SelectPersonPhotoById(int id)
        //{
        //    PersonList pList = SelectAll();
        //    Person p = pList.Find(item => item.Id == id);
        //    string photo = p.Photo;
        //    return photo;
        //}
        public string SelectPersonPhotoById(int id)
        {
            // שליפת רשימה טרייה
            PersonList pList = SelectAll();

            // הגנה: אם הרשימה ריקה או לא קיימת, נחזיר מחרוזת ריקה ולא נקרוס
            if (pList == null || pList.Count == 0)
            {
                return string.Empty;
            }

            // חיפוש האדם ברשימה
            Person p = pList.Find(item => item.Id == id);

            // הגנה קריטית: בודקים אם האדם נמצא לפני שניגשים למאפיין התמונה שלו!
            if (p != null && !string.IsNullOrEmpty(p.Photo))
            {
                return p.Photo;
            }

            // אם האדם לא נמצא, מחזירים מחרוזת ריקה במקום לקרוס
            return string.Empty;
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

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();   // ניקוי חובה למניעת כפילויות וערבוב פרמטרים

                cmd.Parameters.Add(new OleDbParameter("@pid", p.Id));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person p = entity as Person;
            if (p != null)
            {
                // התיקון: הוספת פסיק בין user_name ל-[pass], ועטיפת מילים שמורות בסוגריים מרובעים
                string sqlStr = $"INSERT INTO Person(first_name, last_name, telephone, num_id, email, born_date, photoPath, [photo], user_name, [pass], id_gender) " +
                                $"VALUES (@first_name, @last_name, @telephone, @num_id, @email, @born_date, @PhotoPath, @Photo, @user_name, @pass, @id_gender)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // הוספת הפרמטרים בסדר מדויק התואם לשאילתה
                cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@email", p.Email ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));

                // טיפול בטוח בתמונות (מניעת קריסות סדר ב-OleDb)
                cmd.Parameters.Add(new OleDbParameter("@PhotoPath", !string.IsNullOrEmpty(p.PhotoPath) ? p.PhotoPath : (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@Photo", !string.IsNullOrEmpty(p.Photo) ? p.Photo : (object)DBNull.Value));

                cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender != null ? p.Id_gender.Id : (object)DBNull.Value));
            }
        }



        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person p = entity as Person;
            if (p != null)
            {
                // 1. Structured SQL Command String
                string sqlStr = "UPDATE Person SET " +
                                "first_name = @first_name, " +
                                "last_name = @last_name, " +
                                "telephone = @telephone, " +
                                "num_id = @num_id, " +
                                "email = @email, " +
                                "born_date = @born_date, " +
                                "photoPath = @PhotoPath, " +
                                "[photo] = @Photo, " +
                                "user_name = @user_name, " +
                                "[pass] = @pass, " +
                                "id_gender = @id_gender " +
                                "WHERE id = @id";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear(); // Clear parameters to prevent mixing or doubling

                // 2. Add Parameters in the EXACT Positional Sequence of the SQL String above
                cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@email", p.Email ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));

                // Safely extract the clean file name for photoPath if it contains an absolute local path
                string cleanPhotoPath = p.PhotoPath;
                if (!string.IsNullOrEmpty(cleanPhotoPath) && cleanPhotoPath.Contains("\\Photos\\"))
                {
                    cleanPhotoPath = System.IO.Path.GetFileName(cleanPhotoPath);
                }

                cmd.Parameters.Add(new OleDbParameter("@PhotoPath", !string.IsNullOrEmpty(cleanPhotoPath) ? cleanPhotoPath : (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@Photo", !string.IsNullOrEmpty(p.Photo) ? p.Photo : (object)DBNull.Value));

                cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass ?? (object)DBNull.Value));
                cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender != null ? p.Id_gender.Id : (object)DBNull.Value));

                // 3. The WHERE clause condition variable MUST always be last
                cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
            }
        }

































        //protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    Person p = entity as Person;
        //    if (p != null)
        //    {
        //        // התיקון: עטיפת [photo] ו-[pass] בסוגריים מרובעים
        //        string sqlStr = $"UPDATE Person SET first_name=@first_name, last_name=@last_name, " +
        //                        $"telephone=@telephone, num_id=@num_id, email=@email, born_date=@born_date, " +
        //                        $"photoPath=@PhotoPath, [photo]=@Photo, user_name=@user_name, [pass]=@pass, id_gender=@id_gender WHERE id=@id";

        //        cmd.CommandText = sqlStr;
        //        cmd.Parameters.Clear();

        //        cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@email", p.Email ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));

        //        cmd.Parameters.Add(new OleDbParameter("@PhotoPath", !string.IsNullOrEmpty(p.PhotoPath) ? p.PhotoPath : (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@Photo", !string.IsNullOrEmpty(p.Photo) ? p.Photo : (object)DBNull.Value));

        //        cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender != null ? p.Id_gender.Id : (object)DBNull.Value));

        //        // ה-ID תמיד אחרון ב-UPDATE בגלל ה-WHERE
        //        cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
        //    }
        //}






























        //protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    Person p = entity as Person;
        //    if (p != null)
        //    {
        //        string sqlStr = $"INSERT INTO Person( first_name, last_name " +
        //            $", telephone , num_id , email, born_date , photoPath , photo , user_name ," +
        //            $" pass ,id_gender) VALUES (@first_name, @last_name , @telephone , " +
        //            $"@num_id , @email, @born_date , @PhotoPath , @Photo , @user_name , @pass ,@id_gender)";

        //        cmd.CommandText = sqlStr;
        //        cmd.Parameters.Clear();   // ניקוי חובה

        //        cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name));
        //        cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name));
        //        cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone));
        //        cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id));
        //        cmd.Parameters.Add(new OleDbParameter("@email", p.Email));
        //        cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));

        //        if (!string.IsNullOrEmpty(p.PhotoPath))
        //        {
        //            cmd.Parameters.Add(new OleDbParameter("@PhotoPath", p.PhotoPath));
        //            cmd.Parameters.Add(new OleDbParameter("@Photo", DBNull.Value));
        //        }
        //        else
        //        {
        //            cmd.Parameters.Add(new OleDbParameter("@PhotoPath", DBNull.Value));
        //            cmd.Parameters.Add(new OleDbParameter("@Photo", string.IsNullOrEmpty(p.Photo) ? (object)DBNull.Value : p.Photo));
        //        }
        //        cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name));
        //        cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass));
        //        cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender != null ? p.Id_gender.Id : (object)DBNull.Value));
        //    }
        //}


        //protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    Person p = entity as Person;
        //    if (p != null)
        //    {
        //        string sqlStr = $"UPDATE Person SET first_name=@first_name, last_name=@last_name " +
        //            $", telephone=@telephone , num_id=@num_id , email=@email, born_date=@born_date " +
        //            $", photoPath=@PhotoPath, photo=@Photo, user_name=@user_name , pass=@pass , id_gender=@id_gender WHERE id=@id";

        //        cmd.CommandText = sqlStr;
        //        cmd.Parameters.Clear();

        //        // הוספת הפרמטרים בסדר מדויק משמאל לימין לפי השאילתה!
        //        cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@email", p.Email ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));

        //        // כאן היה התיקון - במקום תנאי if/else שמשבש את הסדר, שולחים תמיד ערך (אפילו אם הוא ריק)
        //        cmd.Parameters.Add(new OleDbParameter("@PhotoPath", p.PhotoPath ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@Photo", p.Photo ?? (object)DBNull.Value));

        //        cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass ?? (object)DBNull.Value));
        //        cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender != null ? p.Id_gender.Id : (object)DBNull.Value));

        //        // ה-ID מופיע אחרון ב-WHERE
        //        cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
        //    }
        //}

        //protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        //{
        //    Person p = entity as Person;
        //    if (p != null)
        //    {
        //        string sqlStr = $"UPDATE Person SET first_name=@first_name, last_name=@last_name " +
        //            $", telephone=@telephone , num_id=@num_id , email=@email, born_date=@born_date " +
        //            $", photoPath=@PhotoPath, photo=@Photo, user_name=@user_name , pass=@pass , id_gender=@id_gender WHERE id=@id";

        //        cmd.CommandText = sqlStr;
        //        cmd.Parameters.Clear();   // התיקון הקריטי: מנקה את ה-cmd מהפרמטרים של Trainer לפני הוספת פרמטרי ה-Person

        //        // הוספה לפי הסדר המדויק של השאילתה משמאל לימין!
        //        cmd.Parameters.Add(new OleDbParameter("@first_name", p.First_name));
        //        cmd.Parameters.Add(new OleDbParameter("@last_name", p.Last_name));
        //        cmd.Parameters.Add(new OleDbParameter("@telephone", p.Telephone));
        //        cmd.Parameters.Add(new OleDbParameter("@num_id", p.Num_id));
        //        cmd.Parameters.Add(new OleDbParameter("@email", p.Email));
        //        cmd.Parameters.Add(new OleDbParameter("@born_date", p.Born_date));


        //        if(p.PhotoPath != null)
        //        {
        //            cmd.Parameters.Add(new OleDbParameter("@PhotoPath", p.PhotoPath));
        //            cmd.Parameters.Add(new OleDbParameter("@Photo", DBNull.Value));
        //        }
        //        else {
        //            cmd.Parameters.Add(new OleDbParameter("@PhotoPath", DBNull.Value));
        //            cmd.Parameters.Add(new OleDbParameter("@Photo", p.Photo));
        //        }

        //        cmd.Parameters.Add(new OleDbParameter("@user_name", p.User_name));
        //        cmd.Parameters.Add(new OleDbParameter("@pass", p.Pass));
        //        cmd.Parameters.Add(new OleDbParameter("@id_gender", p.Id_gender != null ? p.Id_gender.Id : (object)DBNull.Value));

        //        // ה-ID מופיע אחרון ב-WHERE
        //        cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
        //    }
        //}

    }
}