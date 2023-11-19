using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class TeacherManagementSystem
{
    private const string TEACHERS_FILE = "teachers.json";

    static List<Teacher> LoadTeachers()
    {
        if (File.Exists(TEACHERS_FILE))
        {
            string json = File.ReadAllText(TEACHERS_FILE);
            return JsonConvert.DeserializeObject<List<Teacher>>(json);
        }
        return new List<Teacher>();
    }

    static void SaveTeachers(List<Teacher> teachers)
    {
        string json = JsonConvert.SerializeObject(teachers, Formatting.Indented);
        File.WriteAllText(TEACHERS_FILE, json);
    }

    static void ShowAllTeachers(List<Teacher> teachers)
    {
        Console.WriteLine("\nAll Teachers:");
        foreach (var teacher in teachers)
        {
            PrintTeacherInfo(teacher);
        }
    }

    static void PrintTeacherInfo(Teacher teacher)
    {
        Console.WriteLine($"Name: {teacher.Name}");
        Console.WriteLine($"Age: {teacher.Age}");
        Console.WriteLine($"Date of Birth: {teacher.DOB}");
        Console.WriteLine($"Number of Classes: {teacher.NumClasses}");
        Console.WriteLine("----------------------------");
    }

    static void AddTeacher(List<Teacher> teachers)
    {
        Console.Write("Enter teacher's full name: ");
        string name = Console.ReadLine();
        Console.Write("Enter teacher's age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Enter teacher's date of birth (YYYY-MM-DD): ");
        string dob = Console.ReadLine();
        Console.Write("Enter number of classes: ");
        int numClasses = int.Parse(Console.ReadLine());

        Teacher newTeacher = new Teacher
        {
            Name = name,
            Age = age,
            DOB = dob,
            NumClasses = numClasses
        };

        teachers.Add(newTeacher);
        SaveTeachers(teachers);
        Console.WriteLine("Teacher added successfully.");
    }

    static void FilterTeachersByAge(List<Teacher> teachers)
    {
        Console.Write("Enter the age criteria: ");
        int ageCriteria = int.Parse(Console.ReadLine());
        var filteredTeachers = teachers.Where(teacher => teacher.Age == ageCriteria).ToList();
        ShowAllTeachers(filteredTeachers);
    }

    static void FilterTeachersByClasses(List<Teacher> teachers)
    {
        Console.Write("Enter the number of classes criteria: ");
        int classesCriteria = int.Parse(Console.ReadLine());
        var filteredTeachers = teachers.Where(teacher => teacher.NumClasses == classesCriteria).ToList();
        ShowAllTeachers(filteredTeachers);
    }

    static void SearchTeacher(List<Teacher> teachers)
    {
        Console.Write("Enter the full name to search: ");
        string searchName = Console.ReadLine();
        var foundTeachers = teachers.Where(teacher => teacher.Name.ToLower().Contains(searchName.ToLower())).ToList();
        ShowAllTeachers(foundTeachers);
    }

    static void UpdateTeacher(List<Teacher> teachers)
    {
        Console.Write("Enter the full name of the teacher to update: ");
        string searchName = Console.ReadLine();
        var teacherToUpdate = teachers.FirstOrDefault(teacher => teacher.Name.ToLower().Contains(searchName.ToLower()));

        if (teacherToUpdate != null)
        {
            PrintTeacherInfo(teacherToUpdate);
            Console.Write("Enter the field to update (name/age/dob/num_classes): ");
            string updateField = Console.ReadLine();
            Console.Write($"Enter the new value for {updateField}: ");
            string newValue = Console.ReadLine();

            switch (updateField.ToLower())
            {
                case "name":
                    teacherToUpdate.Name = newValue;
                    break;
                case "age":
                    teacherToUpdate.Age = int.Parse(newValue);
                    break;
                case "dob":
                    teacherToUpdate.DOB = newValue;
                    break;
                case "num_classes":
                    teacherToUpdate.NumClasses = int.Parse(newValue);
                    break;
                default:
                    Console.WriteLine("Invalid field.");
                    break;
            }

            SaveTeachers(teachers);
            Console.WriteLine("Teacher updated successfully.");
        }
        else
        {
            Console.WriteLine("Teacher not found.");
        }
    }

    static void DeleteTeacher(List<Teacher> teachers)
    {
        Console.Write("Enter the full name of the teacher to delete: ");
        string searchName = Console.ReadLine();
        var teacherToDelete = teachers.FirstOrDefault(teacher => teacher.Name.ToLower().Contains(searchName.ToLower()));

        if (teacherToDelete != null)
        {
            teachers.Remove(teacherToDelete);
            SaveTeachers(teachers);
            Console.WriteLine("Teacher deleted successfully.");
        }
        else
        {
            Console.WriteLine("Teacher not found.");
        }
    }

    static void CalculateAverageClasses(List<Teacher> teachers)
    {
        int totalClasses = teachers.Sum(teacher => teacher.NumClasses);
        if (teachers.Count > 0)
        {
            double averageClasses = (double)totalClasses / teachers.Count;
            Console.WriteLine($"Average Number of Classes: {averageClasses}");
        }
        else
        {
            Console.WriteLine("No teachers available to calculate average classes.");
        }
    }

    static void Main()
    {
        List<Teacher> teachers = LoadTeachers();

        while (true)
        {
            Console.WriteLine("\nTeacher Management System:");
            Console.WriteLine("1. Show all teachers");
            Console.WriteLine("2. Add a teacher");
            Console.WriteLine("3. Filter teachers by age");
            Console.WriteLine("4. Filter teachers by classes");
            Console.WriteLine("5. Search for a teacher");
            Console.WriteLine("6. Update a teacher's record");
            Console.WriteLine("7. Delete a teacher");
            Console.WriteLine("8. Calculate average number of classes (Bonus)");
            Console.WriteLine("9. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowAllTeachers(teachers);
                    break;
                case 2:
                    AddTeacher(teachers);
                    break;
                case 3:
                    FilterTeachersByAge(teachers);
                    break;
                case 4:
                    FilterTeachersByClasses(teachers);
                    break;
                case 5:
                    SearchTeacher(teachers);
                    break;
                case 6:
                    UpdateTeacher(teachers);
                    break;
                case 7:
                    DeleteTeacher(teachers);
                    break;
                case 8:
                    CalculateAverageClasses(teachers);
                    break;
                case 9:
                    Console.WriteLine("Exiting the Teacher Management System. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}

class Teacher
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string DOB { get; set; }
    public int NumClasses { get; set; }
}
