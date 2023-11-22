// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
using (var context = new MyContext())
{
    // Create and save a new Students
    Console.WriteLine("Adding new students");

    var student = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        gpa=3.4,
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students!.Add(student);

    var student1 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        gpa = 3.1,
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        Console.WriteLine($" ID:{stdnt.ID}, Name: {stdnt.FirstMidName},gpa :{stdnt.gpa}" );
    }


    // Update a student's information using SQL query
    Console.WriteLine("Updating student information using SQL query...");

    string updateSql = "UPDATE Students SET  gpa = 2.9 WHERE FirstMidName = 'Atyia'";
    context.Database.ExecuteSqlCommand(updateSql);

    // Display updated student information
    Console.WriteLine("Retrieve all Students after updating:");
    var updatedStudents = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    foreach (var stdnt in updatedStudents)
    {
        Console.WriteLine($"ID: {stdnt.ID}, Name: {stdnt.FirstMidName}, Phone: {stdnt.gpa},");
    }
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course ?Course { get; set; }
    public virtual Student ?Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string ?LastName { get; set; }
    public string ?FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string ?gpa { get; set; }

    public virtual ICollection<Enrollment> ?Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string ?Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment> ?Enrollments { get; set; }
}

public class MyContext : DbContext
{
    public virtual DbSet<Course> ?Courses { get; set; }
    public virtual DbSet<Enrollment> ?Enrollments { get; set; }
    public virtual DbSet<Student> ?Students { get; set; }
}


