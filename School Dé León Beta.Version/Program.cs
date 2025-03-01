using Microsoft.Data.SqlClient;
using System.Data;

namespace School_Dé_León_Beta.Version
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till School Dè Léon Applikationen!" +
                "\nBeta Version 2.5");
            Thread.Sleep(5000);
            Console.Clear();
            var SchoolManager = new SchoolMethods();

            bool run = true;
            while (run)
            {
                Console.WriteLine("School Dè Léon Beta.V.2.5");
                Console.WriteLine("Välkommen till Huvudmenyn" +
                "\nVälj ett av följande alternativ...(Tryck in respektive siffra)");
                Console.WriteLine("------------------------------------------------------");

                Console.WriteLine("1. Visa alla elever");
                Console.WriteLine("2. Hämta elev info");
                Console.WriteLine("3. Ny perosnal");
                Console.WriteLine("4. Visa all personal");
                Console.WriteLine("5. Visa Löner (Tidigare `$$$`)");
                Console.WriteLine("6. Kurser/Ämnen");
                Console.WriteLine("7. Sätt betyg på elev");
                Console.WriteLine("8. Visa Betyg");
                Console.WriteLine("0. Avsluta program");
                Console.WriteLine("------------------------------------------------------");

                try
                {
                    int menyChoice = Convert.ToInt32(Console.ReadLine());
                    switch (menyChoice)
                    {
                        case 1:
                            SchoolManager.ShowAllStudents();
                            break;

                        case 2:
                            Console.Write("Ange studentens ID: ");
                            if (int.TryParse(Console.ReadLine(), out int studentId))
                            {
                                SchoolManager.GetStudentInfo(studentId);
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Felaktigt ID, försök igen.");
                                Console.ReadKey();
                            }
                            break;

                        case 3:
                            SchoolManager.AddNewEmploye();
                            break;

                        case 4:
                            SchoolManager.ShowAllEmployes();
                            break;

                        case 5:
                            Console.WriteLine("Vill du se: " +
                                "\n1. Genomsnittlig lön per avdelning?" +
                                "\n2. Total lön per avdelning?" +
                                "\n0. Tillbaka till huvudmeny" +
                                "\nVälj ett av följande alternativ...(Tryck in respektive siffra)");
                            Console.WriteLine("------------------------------------------------------");

                            if (int.TryParse(Console.ReadLine(), out int salaryChoice))
                            {
                                switch (salaryChoice)
                                {
                                    case 1:
                                        SchoolManager.ShowAverageSalaryPerDepartment();
                                        break;
                                    case 2:
                                        SchoolManager.ShowDepartmentSalaries();
                                        break;
                                    case 0:
                                        break;
                                    default:
                                        Console.WriteLine("Ogiltigt val, återgår till huvudmenyn.");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ogiltig inmatning, återgår till huvudmenyn.");
                            }
                            Console.ReadKey();
                            break;

                        case 6:
                            SchoolManager.ShowAllCourses();
                            break;

                        case 7:
                            Console.WriteLine("Skriva in elev id för den elev du vill ändra:");
                            int givenstudentId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Vilket ämne vill du sätta eller ändra betyg i?");
                            int givenSubjectId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Vilket betyg vill du sätta:\n1-5");
                            int givenGrade = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Skriv in ditt lärare id");
                            int TeacherId = Convert.ToInt32(Console.ReadLine());

                            SchoolManager.SetGradeForStudent(givenstudentId, givenSubjectId, givenGrade, TeacherId);

                            break;
                        case 8:
                            Console.Write("Ange studentens ID: ");
                            if (int.TryParse(Console.ReadLine(), out int studentId1))
                            {
                                SchoolManager.ShowStudentGrades(studentId1);
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Felaktigt ID, försök igen.");
                                Console.ReadKey();
                            }
                            break;
                        case 0:
                            Console.WriteLine("STänger ner programmet" +
                                "\nLämmna en review Google Review");
                            Console.ReadKey();
                            run = false;
                            break;

                        default:
                            Console.WriteLine("Något blev fel i menyval");
                            break;

                    }
                    Console.Clear();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine($"Något blev fel i {Ex.Message}");
                    Console.ReadKey();
                }
            }

        }
    }
}
