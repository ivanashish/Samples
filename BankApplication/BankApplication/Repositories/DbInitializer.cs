using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            ////// Look for any students.
            ////if (context.Projects.Any())
            ////{
            ////    return;   // DB has been seeded
            ////}

            ////var projects = new Project[]
            ////{
            ////    new Project { Name = "Project1", Description = "This is project1", StartDate = new DateTime(2018,06,17), EndDate = new DateTime(2018,07,16) },
            ////    new Project { Name = "Project2", Description = "This is project2", StartDate = new DateTime(2018,06,17), EndDate = new DateTime(2018,08,16) },
            ////    new Project { Name = "Project3", Description = "This is project3", StartDate = new DateTime(2018,06,17), EndDate = new DateTime(2018,09,16) },
            ////    new Project { Name = "Other", Description = "This is a project for misceleneous activities such as Time Away, Holiday etc", StartDate = new DateTime(2018,06,17), EndDate = DateTime.MaxValue },
            ////};

            ////foreach (var project in projects)
            ////{
            ////    context.Projects.Add(project);

            ////    var tasks = new DBModels.ProjectTask[]
            ////    {
            ////        new DBModels.ProjectTask { ProjectId = project.Id, Name = project.Name + "- Task1", Description = "This is task1", StartDate = new DateTime(2018,06,17), EndDate = new DateTime(2018,07,16) },
            ////        new DBModels.ProjectTask { ProjectId = project.Id, Name = project.Name + "- Task2", Description = "This is task2", StartDate = new DateTime(2018,06,17), EndDate = new DateTime(2018,08,16) },
            ////        new DBModels.ProjectTask { ProjectId = project.Id, Name = project.Name + "- Task3", Description = "This is task3", StartDate = new DateTime(2018,06,17), EndDate = new DateTime(2018,09,16) }
            ////    };

            ////    foreach (var task in tasks)
            ////    {
            ////        context.Tasks.Add(task);
            ////    }
            ////}

            ////context.SaveChanges();

            ////if (!context.Employees.Any())
            ////{
            ////    var employees = new Employee[]
            ////    {
            ////        new Employee { Name = "Ivan", UserId = "FAREAST\v-paiva" }
            ////    };

            ////    foreach (var employee in employees)
            ////    {
            ////        context.Employees.Add(employee);
            ////    }
            ////}

            ////context.SaveChanges();

            ////if (!context.ProjectEmployees.Any())
            ////{
            ////    context.ProjectEmployees.Add(new ProjectEmployee { ProjectId = projects[0].Id, EmployeeId = context.Employees.First(x => x.Name == "Ivan").Id });
            ////}

            ////context.SaveChanges();
        }
    }
}
