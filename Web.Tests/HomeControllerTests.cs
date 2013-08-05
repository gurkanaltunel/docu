// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Moq;
using Web.Abstractions;
using Web.Models;
using Web.Tests.Entities;
using Xunit;

namespace Web.Tests
{
    public class HomeControllerTests
    {
        private readonly UserContext _currentUser = new UserContext { LoginTime = DateTime.Now, Id = 1 };
        private readonly IList<Profile> _profiles = new List<Profile>
                                               {
                                                   new Profile{Id=0, Name = "Test"},
                                                   new Profile{Id=1, Name = "Test"},
                                                   new Profile{Id=2, Name = "Test"},
                                               };

        [Fact]
        public void Add_File_Test()
        {
            var controller = TestableHomeController.Create(_currentUser);
            var result = controller.AddFile();

            controller.ProfileRepo.Setup(repository => repository.GetProfileForUser(It.IsAny<int>())).Returns(_profiles);
            var addfileModel = result.Model as AddFileModel;
            Assert.NotNull(addfileModel);

            Assert.True(addfileModel.Profiles != null && addfileModel.Profiles.Count > 0);
        }
    }
}