using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reflection;

namespace ViewModel
{
    public class ImageToBase64Converter
    {
        public static string ImageToBase64(string imagePath)
        {
            try
            {
                // Read the image file into a byte array
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Convert the byte array to a base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting image to base64: " + ex.Message);
                return null;
            }
        }
    }
}
