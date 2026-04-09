using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Gender:BaseEntity
    {
      private string gender_name = "Female";

        public string Gender_name { get => gender_name; set => gender_name = value; }
    }
}

