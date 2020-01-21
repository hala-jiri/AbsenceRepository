using System;
using Xunit;
using Moq;
using AbsenceWebApp;
using AbsenceWebApp.Models;
using AbsenceWebApp.Controllers;
using System.Collections.Generic;
using AbsenceWebApp.Areas.Employee.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace XUnitTestProjectAbsenceWebApp
{
    public class UnitTestClass
    {
        private string user1Id = Guid.NewGuid().ToString();
        [Fact]
        public void Chief_Absence_controller_getListOfAbsences()
        {
            // arrange
            var mockRepository = new Mock<IAbsenceBusinessLayer>();
            mockRepository.Setup(repo => repo.GetListOfAbsences()).Returns(Get5AbsencesOfUserInMonthFromNow());
            var controller = new AbsenceController(mockRepository.Object);

            //TODO: missing context with Identity as a loged in user


            // act
            var result = controller.Index();

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Absence>>(viewResult.ViewData.Model);
            Assert.Equal(5, model.Count());

        }

        [Fact]
        public void Employee_Absence_controller_createCoverAbsence()
        {
            //TODO: missing context with Identity as a loged in user, tests are empty till will be solve user issue
            // arrange
            // act
            // assert
        }
        [Fact]
        public void Admin_User_controller_lockHimself()
        {
            //TODO: missing context with Identity as a loged in user, tests are empty till will be solve user issue
            // arrange
            // act
            // assert
        }

        private IEnumerable<Absence> GetOneDayAbsenceInFuture()
        {
            //string userId = Guid.NewGuid().ToString();

            return new List<Absence>()
            {
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now.AddDays(7), DatetimeTo = DateTime.Now.AddDays(8) },
            };
        }
        private IEnumerable<Absence> GetOneMonthAbsenceFromNow()
        {
            //string userId = Guid.NewGuid().ToString();

            return new List<Absence>()
            {
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now, DatetimeTo = DateTime.Now.AddDays(30) },
            };
        }
        private IEnumerable<Absence> Get5AbsencesOfUserInMonthFromNow()
        {
            string userId = Guid.NewGuid().ToString();

            return new List<Absence>()
            {
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now.AddDays(1), DatetimeTo = DateTime.Now.AddDays(5) },
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now.AddDays(6), DatetimeTo = DateTime.Now.AddDays(10) },
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now.AddDays(11), DatetimeTo = DateTime.Now.AddDays(15) },
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now.AddDays(16), DatetimeTo = DateTime.Now.AddDays(20) },
                new Absence(){ Id = 1, UserID = user1Id, DatetimeOfCreated = DateTime.Now, DatetimeFrom = DateTime.Now.AddDays(21), DatetimeTo = DateTime.Now.AddDays(25) },
            };
        }
        private IEnumerable<ApplicationUser> GetTestUsers()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser(){ Id = Guid.NewGuid().ToString(), Name = "TestUser1", Email = "TestUser1@gmail.com", UserName = "TestUser1@gmail.com"},
                new ApplicationUser(){ Id = Guid.NewGuid().ToString(), Name = "TestUser2", Email = "TestUser2@gmail.com", UserName = "TestUser2@gmail.com"},
                new ApplicationUser(){ Id = Guid.NewGuid().ToString(), Name = "TestUser3", Email = "TestUser3@gmail.com", UserName = "TestUser3@gmail.com"}
            };
        }
    }
}
