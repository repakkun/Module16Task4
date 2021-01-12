using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Module16Task4
{ 
    [Serializable]
    class BirthDate 
    {
        public DateTime DateOfBirth { get; set; }
        DateTime dateOfBirth;
        public BirthDate()
        {
            DateTime DateOfBirth = dateOfBirth;
        }
    }
    class Program
    {
       
        public static void CheckFolder()
        {
            string path = @"c:\users\1\Desktop\Student";

            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("Такая папка уже существует");
                }

                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("папка создана в {0}.", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }     
        static void Main(string[] args)
        {
            CheckFolder();
            try
            {
                Console.WriteLine("Введите текущее расположение бинарного файла"); //@"c:\users\1\Desktop\Students.dat"
                string path = Console.ReadLine();
                string filePath = @"c:\users\1\Desktop\Student\Students.dat";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);                  
                }
                File.Move(path, filePath);
                Console.WriteLine($"{path} перемещен в {filePath}");
                if (File.Exists(path))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                }
                string Name;
                string Group;               
                BirthDate DateOfBirth = new BirthDate();
                Console.WriteLine("Объект создан");
                //long DateOfBirth = dateTime1.ToBinary();
                BinaryFormatter formatter = new BinaryFormatter();
                
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, DateOfBirth);
                    Console.WriteLine("Объект сериализован");
                }

                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                     Name = reader.ReadString();
                     Group = reader.ReadString();
                    // DateOfBirth = reader.ReadInt64();  
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        var newBirthDate = (BirthDate)formatter.Deserialize(fs);
                        Console.WriteLine("Объект десериализован");               
                    }                                            
                }
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(str);
                    }
                }            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }
    }
}
