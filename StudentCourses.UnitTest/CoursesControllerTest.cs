using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using StudentCourses.Data;
using StudentCourses.Data.Repository;
using StudentCourses.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace StudentCourses.UnitTest
{
    public class CoursesControllerTest
    {
        private  CoursesController coursesController;
         
        public CoursesControllerTest()
        {
      
        }
        [Fact]
        public void CoursesControllerTest_Post()
        {
            var course = new Course { CourseId = 1, Credits = 2, Name = "Math" };

            var substitute = Substitute.For<IGenericRepository<Course>>();
            var courseAdded = new List<string>();
         
            substitute.Insert(course);
            substitute.Received().Insert(Arg.Is<Course>(p => p.Name == "Math" && p.CourseId == 1 && p.Credits == 2));
            coursesController = new CoursesController(substitute);            

            var result = coursesController.PostCourse(course).Result;
            Assert.IsType<CreatedAtActionResult> (result.Result);
            var value = ((CreatedAtActionResult)result.Result).Value as Course;
            Assert.Equal("Math", value.Name);
            Assert.Equal(1, value.CourseId);
            Assert.Equal(2, value.Credits);
        }
    }
}
