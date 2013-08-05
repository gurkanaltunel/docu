using Moq;
using Web.Abstractions;
using Web.Controllers;
using Web.Models;

namespace Web.Tests.Entities
{
    public class TestableHomeController : HomeController
    {
        public readonly Mock<IProfileRepository> ProfileRepo;
        public readonly Mock<ISessionHelper> SessionHelper;
        public readonly Mock<IDocumentService> DocumentService;

        TestableHomeController(Mock<IProfileRepository> profileRepo, Mock<ISessionHelper> sessionHelper, Mock<IDocumentService> documentService)
            : base(profileRepo.Object, sessionHelper.Object, documentService.Object)
        {
            ProfileRepo = profileRepo;
            SessionHelper = sessionHelper;
            DocumentService = documentService;
        }

        public static TestableHomeController Create(UserContext user = null)
        {
            var sessionHelper = new Mock<ISessionHelper>();
            if (user != null)
            {
                sessionHelper.Setup(helper => helper.CurrentUser).Returns(user);
            }
            var profileRepo = new Mock<IProfileRepository>();

            var documentService = new Mock<IDocumentService>();

            return new TestableHomeController(profileRepo, sessionHelper, documentService);
        }
    }
}
