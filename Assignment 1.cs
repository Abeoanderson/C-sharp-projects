using System;
using System.Collections.Generic;
using System.Linq;
public abstract class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public string GetFullName(){
        return FirstName + " " + LastName;
    }
}
public interface IEnrollable
{
    void EnrollInCourse(Course course); 
}
public class Student : Person, IEnrollable
{
    public List<Course> EnrolledCourses { get; private set; } = new List<Course>();

    public void EnrollInCourse(Course course)
    {
        EnrolledCourses.Add(course);
    }
}
public class Course
{
    public string CourseName { get; set; }
    public int Credits { get; set; }
}
public class CourseManagementSystem
{
    private List<Course> courses = new List<Course>();
    private List<Student> students = new List<Student>();

    public void AddCourse(Course course)
    {
        courses.Add(course);
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public IEnumerable<Student> GetStudentsEnrolledInCourse(string courseName)
    {
        return students.Where(s => s.EnrolledCourses.Any(c => c.CourseName == courseName));
    }

    public double GetAverageCreditsEnrolledByStudents()
    {
        return students.Average(s => s.EnrolledCourses.Sum(c => c.Credits));
    }
}

class Program
{
    static void Main()
    {
        // Create courses
        Course math101 = new Course { CourseName = "Math 101", Credits = 3 };
        Course cs101 = new Course { CourseName = "CS 101", Credits = 4 };
        Course history101 = new Course { CourseName = "History 101", Credits = 3 };
        // Create students
        Student student1 = new Student
        {
            FirstName = "John",
            LastName = "Doe",
            Age =
        20
        };
        Student student2 = new Student
        {
            FirstName = "Jane",
            LastName = "Smith",
            Age =
        22
        };
        Student student3 = new Student
        {
            FirstName = "Alice",
            LastName = "Brown",
            Age =
        21
        };
        // Enroll students in courses
        student1.EnrollInCourse(math101);
        student1.EnrollInCourse(cs101);
        student2.EnrollInCourse(cs101);
        student2.EnrollInCourse(history101);
        student3.EnrollInCourse(math101);
        student3.EnrollInCourse(history101);
        // Create the course management system and add data
        CourseManagementSystem cms = new CourseManagementSystem();
        cms.AddCourse(math101);
        cms.AddCourse(cs101);
        cms.AddCourse(history101);
        cms.AddStudent(student1);
        cms.AddStudent(student2);
        cms.AddStudent(student3);
        // Use LINQ to get students enrolled in "CS 101"
        var studentsInCS101 = cms.GetStudentsEnrolledInCourse("CS 101");
        Console.WriteLine("Students enrolled in CS 101:");
        foreach (var student in studentsInCS101)
        {
            Console.WriteLine(student.GetFullName());
        }
        // Use LINQ to get the average credits per student
        var averageCredits = cms.GetAverageCreditsEnrolledByStudents();
        Console.WriteLine($"\nAverage credits per student: {averageCredits:F2}");
    }
}