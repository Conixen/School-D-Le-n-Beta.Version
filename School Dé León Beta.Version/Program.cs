using Microsoft.Data.SqlClient;
using System.Data;

namespace School_Dé_León_Beta.Version
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till School Dè Léon Applikationen!" +
                "\nBeta Version 2.3");
            Thread.Sleep(5000);
            Console.Clear();
            var SchoolManager = new SchoolMethods();

            bool run = true;
            while (run) 
            {
                Console.WriteLine("School Dè Léon Beta.V.2.3");
                Console.WriteLine("Välkommen till Huvudmenyn" +
                "\nVälj ett av följande alternativ...(Tryck in respektive siffra)");
                Console.WriteLine("------------------------------------------------------");

                Console.WriteLine("1. Visa alla elever");
                Console.WriteLine("2. Hämta elev info");
                Console.WriteLine("3. Ny perosnal");
                Console.WriteLine("4. Visa all personal");//namn, beffatning, hur länge de jobbat, 
                Console.WriteLine("5. $$$");
                Console.WriteLine("6. Kurser/Ämnen");
                Console.WriteLine("7. Sätt betyg på elev");
                Console.WriteLine("8. Visa Betyg");
                Console.WriteLine("0. Avsluta program");

                int menyChoice = Convert.ToInt32(Console.ReadLine());
                switch (menyChoice) 
                {
                    case 1:
                        SchoolManager.ShowAllStudents(); // Fin ito
                        break;

                    case 2:
                        Console.Write("Ange studentens ID: ");  // Fin ito
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
                        SchoolManager.ShowAllEmployes();    // Fin ito
                        break;

                    case 5:
                        //Console.WriteLine("Vill du se lönerna per avdelning?" +
                        //    "\nEller medellönen per avdelning?" +
                        //    "");
                        //SchoolManager.ShowDepartmentSalaries();
                        SchoolManager.ShowAverageSalaryPerDepartment();
                        break;

                    case 6:
                        //SchoolManager.ShowAllCourses();
                        break;

                    case 7:
                        //SchoolManager.SetGradeForStudent();
                        break;
                    case 8:
                        //SchoolManager.ShowStudentGrades();
                        break;
                    case 0:
                        Console.WriteLine("STänger ner programmet" +
                            "\nLämmna en review Google Review");
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Något blev fel i menyval");
                        break;

                }
                Console.Clear();
            }

        }
        //public void ShowAllStudents()
        //{
        //    using (var context = new SchoolContext())
        //    {
        //        var students = context.Students.ToList();
        //        Console.WriteLine("\n--- Lista över alla elever ---\n");

        //        foreach (var student in students)
        //        {
        //            Console.WriteLine($"ID: {student.Id} | Namn: {student.FirstName} {student.LastName} | Klass: {student.ClassName}");
        //        }
        //    }
        //}


        //public void GetStudentInfo(int studentId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand("GetStudentInfo", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@StudentId", studentId);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    Console.WriteLine($"ID: {reader["Id"]} | Namn: {reader["FirstName"]} {reader["LastName"]} | Klass: {reader["ClassName"]}");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Ingen elev hittades.");
        //                }
        //            }
        //        }
        //    }
        //}

        //public void AddNewEmploye()
        //{
        //    Console.Write("Ange namn: ");
        //    string name = Console.ReadLine();
        //    Console.Write("Ange befattning: ");
        //    string position = Console.ReadLine();
        //    Console.Write("Ange startdatum (YYYY-MM-DD): ");
        //    string startDate = Console.ReadLine();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "INSERT INTO Employees (Name, Position, StartDate) VALUES (@name, @position, @startDate)";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@name", name);
        //            cmd.Parameters.AddWithValue("@position", position);
        //            cmd.Parameters.AddWithValue("@startDate", startDate);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    Console.WriteLine("Personal tillagd!");
        //}

        //public void ShowAllEmployes()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT Name, Position, DATEDIFF(YEAR, StartDate, GETDATE()) AS YearsWorked FROM Employees";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            Console.WriteLine("\n--- Personal Översikt ---\n");
        //            while (reader.Read())
        //            {
        //                Console.WriteLine($"Namn: {reader["Name"]} | Befattning: {reader["Position"]} | År på skolan: {reader["YearsWorked"]}");
        //            }
        //        }
        //    }
        //}

        //public void ShowAllCourses()
        //{
        //    using (var context = new SchoolContext())
        //    {
        //        var courses = context.Courses.ToList();
        //        Console.WriteLine("\n--- Lista över alla kurser ---\n");

        //        foreach (var course in courses)
        //        {
        //            Console.WriteLine($"ID: {course.Id} | Kurs: {course.Name}");
        //        }
        //    }
        //}

        //public void SetGradeForStudent(int studentId, int courseId, string grade, int teacherId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlTransaction transaction = conn.BeginTransaction();

        //        try
        //        {
        //            string query = "INSERT INTO Grades (StudentId, CourseId, Grade, TeacherId, DateSet) VALUES (@studentId, @courseId, @grade, @teacherId, GETDATE())";
        //            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        //            {
        //                cmd.Parameters.AddWithValue("@studentId", studentId);
        //                cmd.Parameters.AddWithValue("@courseId", courseId);
        //                cmd.Parameters.AddWithValue("@grade", grade);
        //                cmd.Parameters.AddWithValue("@teacherId", teacherId);
        //                cmd.ExecuteNonQuery();
        //            }
        //            transaction.Commit();
        //            Console.WriteLine("Betyg satt!");
        //        }
        //        catch
        //        {
        //            transaction.Rollback();
        //            Console.WriteLine("Något gick fel, betyg sparades inte.");
        //        }
        //    }
        //}

        //public void ShowStudentGrades(int studentId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = @"SELECT c.Name AS Course, g.Grade, t.Name AS Teacher, g.DateSet 
        //                 FROM Grades g
        //                 JOIN Courses c ON g.CourseId = c.Id
        //                 JOIN Teachers t ON g.TeacherId = t.Id
        //                 WHERE g.StudentId = @studentId";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@studentId", studentId);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                Console.WriteLine("\n--- Betyg för elev ---\n");
        //                while (reader.Read())
        //                {
        //                    Console.WriteLine($"Kurs: {reader["Course"]} | Betyg: {reader["Grade"]} | Lärare: {reader["Teacher"]} | Datum: {reader["DateSet"]}");
        //                }
        //            }
        //        }
        //    }
        //}
        ////public void ShowTeachersPerDepartment()
        ////{
        ////    using (var context = new SchoolContext())
        ////    {
        ////        var departments = context.Teachers
        ////            .GroupBy(t => t.Department)
        ////            .Select(group => new { Department = group.Key, Count = group.Count() })
        ////            .ToList();

        ////        Console.WriteLine("\n--- Lärare per avdelning ---\n");
        ////        foreach (var dep in departments)
        ////        {
        ////            Console.WriteLine($"Avdelning: {dep.Department} | Antal lärare: {dep.Count}");
        ////        }
        ////    }
        ////}
        //public void ShowDepartmentSalaries()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT Department, SUM(Salary) AS TotalSalary FROM Employees GROUP BY Department";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            Console.WriteLine("\n--- Löner per avdelning ---\n");
        //            while (reader.Read())
        //            {
        //                Console.WriteLine($"Avdelning: {reader["Department"]} | Total lön: {reader["TotalSalary"]} SEK");
        //            }
        //        }
        //    }
        //}
        //public void ShowAverageSalaryPerDepartment()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT Department, AVG(Salary) AS AvgSalary FROM Employees GROUP BY Department";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            Console.WriteLine("\n--- Medellön per avdelning ---\n");
        //            while (reader.Read())
        //            {
        //                Console.WriteLine($"Avdelning: {reader["Department"]} | Medellön: {reader["AvgSalary"]} SEK");
        //            }
        //        }
        //    }
        //}



    }
}
