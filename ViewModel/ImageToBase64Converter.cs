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
        public static string ImageFromResourceToBase64(string Photos)
        {
            try
            {
                var assembly = typeof(ImageToBase64Converter).Assembly;

                string resourcePath = $"ViewModel.Photos.{Photos}";

                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream == null)
                    {
                        Console.WriteLine($"[שגיאה] ה-Resource לא נמצא בנתיב: {resourcePath}");
                        Console.WriteLine("הנה הרשימה האמיתית של ה-Resources בתוך ViewModel:");
                        foreach (string name in assembly.GetManifestResourceNames())
                        {
                            Console.WriteLine("-> " + name);
                        }
                        return "";
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
