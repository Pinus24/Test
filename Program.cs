using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TestProject;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml;

namespace TestProject
{
    class Program
    {

        public static List<string> Departments = new List<string>() { "Отдел программирования",
                                                                      "Отдел тестирования",
                                                                      "Отдел маркетинга",
                                                                      "Отдел снабжения",
                                                                      "Отдел персонала",
                                                                      "Административный отдел",
                                                                      "Финансовый отдел",
                                                                      "Коммерческий отдел",
                                                                      "Секретариат",
                                                                      "Бухгалтерия", };

        public static ConsoleKey response;

        public static List<Сandidate> Candidates = new List<Сandidate>();

        static void Main(string[] args)
        {
            var newCandidate = new Сandidate();

            TrySerializeCollection(Environment.GetEnvironmentVariable("LocalAppData"), false);

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("************ СТОУН-XXI ************");
            Console.WriteLine("****** Пройдите краткий опрос *****");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("-");

            Console.Write("Введите Имя: ");
            newCandidate.Name = Console.ReadLine().ToString();

            Console.WriteLine("-");

            Console.Write("Введите Фамилию: ");
            newCandidate.SurName = Console.ReadLine().ToString();

            Console.WriteLine("-");

            Console.Write("Желаете пройти тестовое задание? [y/n] ");
            
            response = Console.ReadKey(false).Key;

            Console.WriteLine("-");

            if (response == ConsoleKey.Y)
            {
                TestCase(newCandidate);
            }
            else
                Console.WriteLine("-");
            
            Console.ReadLine();

        }
        
        public static void TestCase(Сandidate candidate) //метод опросник
        {

            Console.WriteLine("Какой из наших отделов вы рассматриваете для работы? [1-10]");
            var id = 1;
            int age;

            Console.WriteLine();
            
            foreach (string dep in Departments)
            {
                Console.WriteLine($"{id}.{dep}");
                id++;
            }

            Console.WriteLine();
            
            try
            {
                age = Convert.ToInt32(Console.ReadLine());
                if (0 < age && age <= 10)
                {
                    candidate.RecommendedDepartment = Departments[age - 1];

                    Console.WriteLine($"Отлично!");
                    Console.Write($"Укажите почту для отправки вам тестового задания: ");

                    candidate.Email = Console.ReadLine();
                    Console.WriteLine();
                }
                else
                    throw new Exception("Неверные данные");
            }
            catch
            {
                Console.WriteLine($"Введён неверный номер отдела.");
                Console.Write($"Попробовать снова? [y/n] ");
                
                response = Console.ReadKey(false).Key;

                if (response == ConsoleKey.Y)
                {
                    Console.WriteLine("-");
                    TestCase(candidate);
                }
                else
                    Console.WriteLine("-");
            }

            Console.WriteLine($"Отлично!");
            Console.Write($"После проходения тестового испытания готовы пройти Собеседование? [y/n] ");
            Console.WriteLine();
            Console.WriteLine("-");

            response = Console.ReadKey(false).Key;

            if (response == ConsoleKey.Y)
            {
                candidate.Interview = true;
            }
            else
                Console.WriteLine("-");
            Candidates.Add(candidate);
            TrySerializeCollection(Environment.GetEnvironmentVariable("LocalAppData"));
        }

        public static bool TrySerializeCollection(string path,bool serialize = true)
        {
            try
            {
                var xmlPath = Path.Combine(path);
                var xmlFile = Path.Combine(xmlPath, "Сandidates.xml");                // Места сохранения и всё подобное
                var xml = new XmlSerializer(typeof(List<Сandidate>), new[] { typeof(Сandidate) });

                if (serialize)
                {
                    if (!File.Exists(xmlPath))
                        Directory.CreateDirectory(xmlPath);
                    using (FileStream file = File.Create(xmlFile))                  // Правильное IDisposable использование объектов (неявное освобождение ресурсов)
                        xml.Serialize(file, Candidates);
                }
                else // deserialize
                {
                    if (File.Exists(xmlFile))
                    {
                        var reader = XmlReader.Create(xmlFile);
                        if (reader != null)
                            Candidates = xml.Deserialize(reader) as List<Сandidate>  // Приведение возвращаемого обьекта к коллекции
                                ?? new List<Сandidate>();
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
