using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingEF.Data;
using TestingEF.Domain;

namespace TestingEF.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddVariousTypes();
            RetrieveInsertAndDeleteStudent(2);
            //GetStudentsAndGrades();
            Console.WriteLine("Press any key.....");
            Console.ReadKey();
        }

        private static void RetrieveInsertAndDeleteStudent(int id)
        {
            List<Student> numberList = new List<Student>();
            using (SchoolDBContext _context = new SchoolDBContext())
            {
                try
                {
                    var student = _context.Students
                              .Where(s => s.Id == id)
                              .Include(s => s.Grades)
                              .First();
                    numberList.Add(student);
                }
                catch(InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }
            using (SchoolDBContext _context = new SchoolDBContext())
            {
                _context.ConnectionNum = DB.Offline;
                _context.Database.EnsureCreated();
                _context.Students.Add(numberList[0]);
                _context.SaveChanges();
            }
            using (SchoolDBContext _context = new SchoolDBContext())
            {
                var student = _context.Students.Find(id);
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        private static void GetStudentsAndGrades()
        {
            using (SchoolDBContext _context = new SchoolDBContext())
            {
                var students = _context.Students.Select(s => new { s.Id, s.Name, s.Grades }).ToList();

                var json = JsonConvert.SerializeObject(students);
            }
            
        }

        private static void AddVariousTypes()
        {
            int i = 1;
            using (SchoolDBContext _context = new SchoolDBContext())
            {
                _context.Database.EnsureCreated();
                while (i < 11)
                {
                    _context.AddRange(
                        new Student { Id = i, Name = $"Test{i}" },
                        new Grade { GradeID = i, GradeName = $"A{i}", StudentId = i });
                    i++;
                }
                _context.SaveChanges();
            }
        }
    }
}
