using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DocumentServices.Abstractions;
using DocumentServices.Exceptions;
using DocumentServices.Models;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller, IController
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ISessionHelper _sessionHelper;
        private readonly IDocumentService _documentService;

        public HomeController(IProfileRepository profileRepository,
                                ISessionHelper sessionHelper,
                                IDocumentService documentService)
        {
            _profileRepository = profileRepository;
            _sessionHelper = sessionHelper;
            _documentService = documentService;
            _sessionHelper.CreateLogin(new UserContext { Id = 1 });
        }

        public ActionResult Index()
        {
            var currentFolder = _documentService.GetFolderInformation();
            var folders = _documentService.GetFolderTree();

            _sessionHelper.CurrentFolder = currentFolder;
            return View(new MainViewModel
            {
                Folders = folders,
                CurrentFolder = currentFolder
            });
        }

        [HttpPost]
        public ActionResult AddFile(int profileName, string returnUrl, HttpPostedFileBase file)
        {
            _documentService.CreateDocument(file.InputStream, profileName, file.FileName);
            return Redirect(returnUrl);
            //Json(new { folderId = _sessionHelper.CurrentFolder.FolderId });
        }

        [HttpGet]
        public ViewResult AddFile()
        {
            var addFileModel = new AddFileModel
            {
                Profiles = _profileRepository.GetProfileForUser(_sessionHelper.CurrentUser.Id)
            };
            return View(addFileModel);
        }

        [HttpPost]
        public ActionResult ChangeFolder(int id)
        {
            var model = id == 0 ? _documentService.GetFolderInformation() : _documentService.GetFolderInformation(id);
            _sessionHelper.CurrentFolder = model;
            model.CurrentPath = _documentService.GetFolderPathById(id);
            return View("Folder", model);
        }

        public ActionResult CreateFolder()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateFolder(string folder)
        {
            try
            {
                _documentService.CreateFolder(folder, _sessionHelper.CurrentFolder.FolderId);
                return Json(new { ok = true, folderId = _sessionHelper.CurrentFolder.FolderId });
            }
            catch (FolderAlreadyExistsException ex)
            {
                return Json(new { ok = false, ex.Message });
            }

        }

        public ActionResult OpenFile(int id)
        {
            var model = new OpenFileViewModel
                            {
                                File = _documentService.GetFileById(id),
                                FileVersions = _documentService.GetFileVersionsAndCommentsByFileId(id)
                            };
            _sessionHelper.CurrentFile = model.File;
            return View(model);
        }

        public ActionResult GetFile(int versionId)
        {
            var version = _documentService.GetFileVersionById(versionId);
            var file = _documentService.GetFileById(version.FileId);
            return File(version.File, "application/force-download", file.FileName);
        }

        [HttpPost]
        public ActionResult AddComment(AddCommentModel model)
        {
            _documentService.AddComment(model.Id, model.Value);
            return Json(new {ok = true});
        }

        public ActionResult GetComments(int id)
        {
            IList<Comment> comments = _documentService.GetCommentsByVersionId(id);
            return View(comments);
        }
    }
}