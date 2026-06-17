using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person:BaseEntity
    {
        private string first_name="";
        private string last_name="";
        private DateTime born_date;
        private string telephone="";
        private string email="";
        private string num_id="";
        private string photo="";
        private string? photoPath = null;
        private string pass="";
        private string user_name="";
        private Gender id_gender = null;

        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public DateTime Born_date { get => born_date; set => born_date = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Num_id { get => num_id; set => num_id = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Pass { get => pass; set => pass = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public Gender Id_gender { get => id_gender; set => id_gender = value; }
        public string? PhotoPath { get => photoPath; set => photoPath = value; }
    }
}
