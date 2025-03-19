using Microsoft.Data.SqlClient;
using School_Dé_León_Beta.Version.Models;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace School_Dé_León_Beta.Version
{
    public class SchoolMethods
    {
        //Scaffold-DbContext “Data Source = localhost; Database=SchoolDèLéonApplikationen;Integrated Security = True; Trust Server Certificate=true;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        string connectionString = @"Data Source = localhost;Initial Catalog = SchoolDèLéonApplikationen;Integrated Security=True; Trust Server Certificate=True";

        public void ShowAllStudents()
        {
            using (var context = new SchoolDèLéonApplikationenContext())    // Creating a context to interact with the database
            {
                var students = (from s in context.Students  // Query question that asks to fetch all students by/which class they are in
                                join c in context.Classes on s.ClassId equals c.ClassId
                                select new
                                {
                                    s.StudentId,
                                    s.FirstName,
                                    s.LastName,
                                    ClassName = c.ClassName
                                }).ToList();
                Console.WriteLine("\n--- Lista över alla elever ---\n");

                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.StudentId} | Namn: {student.FirstName} {student.LastName} | Klass: {student.ClassName}");
                }
            }
            Console.ReadKey();
        }

        public void GetStudentInfo(int studentId)
        {
            try
            {   // Opennig a connection whit database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetStudentInfo", conn))
                    {   // Here we use a stored procedure to get the student info by studentId, check database for the CD
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("\n--- Elevinformation ---\n");
                                Console.WriteLine($"ID: {reader["StudentID"]} | Namn: {reader["FirstName"]} {reader["LastName"]} | PersonNummer: {reader["SSNUM"]} | Klass: {reader["ClassName"]}");
                                Console.ReadKey();
                            }
                            else
                            {   // If no student match whit ID
                                Console.WriteLine("Ingen elev hittades med det angivna ID:t.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)    // Error message
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
        public void AddNewEmploye()
        {   // Ask the user to about new employee
            Console.Write("Ange förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Ange efternamn: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("\nVälj roll:");  // A dictionary with the availabe rolls/positions at this marveoulos school
            Dictionary<int, string> roles = new Dictionary<int, string>
            {
            {1, "Lärare"},
            {2, "Rektor"},
            {3, "Administratör"},
            {4, "Skolsköterska"},
            {5, "Vaktmästare"},
            {6, "Bibliotikarie"},
            {7, "Kurator"}
            };

            foreach (var role in roles) 
            { 
                Console.WriteLine($"{role.Key}. {role.Value}");
            }

            Console.Write("Ange roll (nummer): ");
            int rollId = Convert.ToInt32(Console.ReadLine());

            Dictionary<int, string> departments = new Dictionary<int, string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DepartmentID, DepartmentName FROM Department";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())   // adds alla departments in the dictionary
                    {
                        departments.Add(reader.GetInt32(0), reader.GetString(1));
                    }
                }

                Console.WriteLine("\nVälj avdelning:");
                foreach (var dept in departments) 
                { 
                    Console.WriteLine($"{dept.Key}. {dept.Value}");
                }

                Console.Write("Ange avdelning (nummer): ");
                int departmentId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Ange startdatum (YYYY-MM-DD): ");
                string hireDate = Console.ReadLine();
                // Puts all the info in the database and adds our new employee
                string insertQuery = "INSERT INTO Employe (EmployeFName, EmployeLName, RollID, DepartmentID, HireDate) " +
                                     "VALUES (@firstName, @lastName, @rollId, @departmentId, @hireDate)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@firstName", firstName);
                    insertCmd.Parameters.AddWithValue("@lastName", lastName);
                    insertCmd.Parameters.AddWithValue("@rollId", rollId);
                    insertCmd.Parameters.AddWithValue("@departmentId", departmentId);
                    insertCmd.Parameters.AddWithValue("@hireDate", hireDate);
                    insertCmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("\nPersonal tillagd!");
            Console.ReadKey();
        }
        public void ShowAllEmployes()   // Vissa antal per avdelning
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @" SELECT d.DepartmentName AS Avdelning, COUNT(e.EmployeID) AS AntalAnställda
                FROM Department d
                LEFT JOIN Employe e ON e.DepartmentID = d.DepartmentID
                GROUP BY 
                d.DepartmentName;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n--- Antal anställda per avdelning ---\n");
                        
                    while (reader.Read())
                    {
                        Console.WriteLine($"Avdelning: {reader["Avdelning"]} | Antal anställda: {reader["AntalAnställda"]}");
                    }
                }
                Console.ReadKey();
            }
        }

        public void ShowAllCourses()    // Added (Where) to show all avaiable courses
        {
            using (var context = new SchoolDèLéonApplikationenContext())
            {
                var courses = context.Subjects.Where(s => s.IsActive).ToList();    
                Console.WriteLine("\n--- Lista över alla kurser ---\n");

                foreach (var course in courses)
                {
                   
                    Console.WriteLine($"ID: {course.SubjectId} | Kurs: {course.SubjectName} | Tillgänglig: {course.IsActive} |");
                }
            }
            Console.ReadKey();
        }

        public void SetGradeForStudent(int givenstudentId, int givenSubjectId, int givenGrade, int teacherId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {   //trying to update a grade from a student with the info from user
                    string updateQuery = "UPDATE Grade SET GradeValue = @grade, EmployeID = " +
                        "@EmployeId, DateTime = GETDATE()" +
                        " WHERE StudentID = @studentId AND SubjectID = @subjectId";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@studentId", givenstudentId);
                        cmd.Parameters.AddWithValue("@SubjectId", givenSubjectId);
                        cmd.Parameters.AddWithValue("@grade", givenGrade);
                        cmd.Parameters.AddWithValue("@EmployeId", teacherId);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    Console.WriteLine("Betyg satt!");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Något gick fel, betyg sparades inte. Fel: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        public void ShowStudentGrades(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {   
                conn.Open();    // Asks to get all the grades for 1 student
                string query = @"SELECT s.SubjectName AS Course, 
                        g.GradeValue AS Grade, 
                        CONCAT(e.EmployeFName, ' ', e.EmployeLName) AS Teacher, 
                        g.DateTime AS DateSet 
                 FROM Grade g
                 JOIN Subject s ON g.SubjectId = s.SubjectID
                 JOIN Employe e ON g.EmployeId = e.EmployeID
                 WHERE g.StudentId = @studentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n--- Betyg för elev ---\n");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Kurs: {reader["Course"]} | Betyg: {reader["Grade"]} | Lärare: {reader["Teacher"]} | Datum: {reader["DateSet"]}");
                        }
                    }
                }
            }
        }
        public void ShowDepartmentSalaries()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();    // Asks to get all the money that is paid out to all departments every month in based on salary
                string query = @"SELECT d.DepartmentName, COALESCE(SUM(r.Salary), 0) AS TotalSalary 
                    FROM Department d
                    LEFT JOIN Employe e ON e.DepartmentID = d.DepartmentID
                    LEFT JOIN Roll r ON e.RollID = r.RollID
                    GROUP BY d.DepartmentName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n--- Löner per avdelning ---\n");
                    bool hasRows = false;

                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine($"Avdelning: {reader["DepartmentName"]} | Total lön: {reader["TotalSalary"]} SEK");
                    }

                    if (!hasRows)   // If a department does not have personal
                    {
                        Console.WriteLine("Ingen personal hittades i databasen.");
                    }

                    Console.ReadKey();
                }
            }
        }
        public void ShowAverageSalaryPerDepartment()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();    // asks to the avarage for each department... (salary for the moment is based on roll not on the person... so some departments are not that fun)
                    string query = @"SELECT D.DepartmentName, AVG(R.Salary) AS AvgSalary FROM Employe E
                    JOIN 
                    Roll R ON E.RollID = R.RollID
                    JOIN 
                    Department D ON E.DepartmentID = D.DepartmentID
                    GROUP BY 
                    D.DepartmentName;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n--- Medellön per avdelning ---\n");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Avdelning: {reader["DepartmentName"]} | Medellön: {reader["AvgSalary"]} SEK");
                        }
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
