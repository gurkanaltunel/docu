using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DocumentServices.Abstractions;
using DocumentServices.Exceptions;
using DocumentServices.Models;
using DocumentServices.Repository;
using ServiceStack.OrmLite;
using File = DocumentServices.Models.File;

namespace DocumentServices
{
    public class DefaultDocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ISessionHelper _sessionHelper;

        public DefaultDocumentService(IDocumentRepository documentRepository, ISessionHelper sessionHelper)
        {
            _documentRepository = documentRepository;
            _sessionHelper = sessionHelper;
        }

        public FolderInformation GetFolderInformation(int id)
        {
            var folders = _documentRepository.GetFoldersByParentId(id);
            var files = _documentRepository.GetFilesInFolder(id);
            var folder = _documentRepository.GetFolderById(id);
            return new FolderInformation
                       {
                           FolderId = id,
                           ParentFolder = folder.ParentFolder == null ? 0 : folder.ParentFolder.Value,
                           FolderName = folder.Name,
                           Folders = folders,
                           Files = files
                       };
        }

        public FolderInformation GetFolderInformation()
        {
            var folder = _documentRepository.LoadRootFolders();
            return new FolderInformation
                       {
                           Folders = folder,
                           FolderName = "$",
                           Files = new List<File>()
                       };
        }

        public IList<Folder> GetFolderTree(int id = 0)
        {
            return id == 0 ? _documentRepository.LoadRootFolders() : _documentRepository.GetFoldersByParentId(id);
        }

        public void CreateDocument(Stream inputStream, int profileName, string fileName)
        {
            File file = _documentRepository.GetFileByNameAndFolder(fileName, _sessionHelper.CurrentFolder.FolderId);
            if (file != null)
            {
                _documentRepository.CreateNewVersionOfFile(file, inputStream);
            }
            else
            {
                _documentRepository.CreateNewFile(new File
                                                      {
                                                          FolderId = _sessionHelper.CurrentFolder.FolderId,
                                                          FileName = fileName,
                                                          CreateDate = DateTime.Now,
                                                          Owner = _sessionHelper.CurrentUser.Id
                                                      }, inputStream);
            }

        }

        public string GetFolderPathById(int id)
        {
            List<Folder> folders;
            using (var db = DbHelper.CreateConnection())
            {
                folders = db.Select<Folder>();
            }
            return GetFolderPath(folders, id);
        }

        public Folder CreateFolder(string foldername, int parentFolderId)
        {
            var folder = _documentRepository.GetFolderByName(foldername, parentFolderId);
            if (folder == null)
            {
                return _documentRepository.InsertNewFolder(foldername, parentFolderId, _sessionHelper.CurrentUser.Id);
            }
            throw new FolderAlreadyExistsException(foldername);
        }

        public File GetFileById(int id)
        {
            var file = _documentRepository.GetFileById(id);

            if (file == null)
            {
                throw new FileNotFoundException(string.Format("File with the id '{0}' not found.", id));
            }
            return file;
        }

        public IList<FileVersion> GetFileVersionsAndCommentsByFileId(int id)
        {
            var comments = _documentRepository.GetCommentsByFileId(id).OrderBy(comment => comment.FileVersionId);
            var fileVersions = _documentRepository.GetFileVersionsByFileId(id);
            foreach (var fileVersion in fileVersions)
            {
                fileVersion.Comments = comments.Where(comment => comment.FileVersionId == fileVersion.Id).ToList();

            }
            foreach (var comment in comments)
            {
                comment.OwnerUser = _documentRepository.GetUserById(comment.OwnerId);
            }
            return fileVersions;
        }

        public FileVersion GetFileVersionById(int versionId)
        {
            var version = _documentRepository.GetFileVersionById(versionId);
            if (version == null)
            {
                throw new FileNotFoundException(string.Format("File with the version id '{0}'.", versionId));
            }
            return version;
        }

        public void AddComment(int versionId, string comment)
        {
            var newComment = new Comment
                                  {
                                      FileVersionId = versionId,
                                      OwnerId = _sessionHelper.CurrentUser.Id,
                                      FileId = _sessionHelper.CurrentFile.Id,
                                      Text = comment,
                                      CommentDate = DateTime.Now
                                  };
            _documentRepository.SaveComment(newComment);
        }

        public IList<Comment> GetCommentsByVersionId(int versionId)
        {
            var comments = _documentRepository.GetCommentsByVersionId(versionId);
            foreach (var comment in comments)
            {
                comment.OwnerUser = _documentRepository.GetUserById(comment.OwnerId);
            }
            return comments;
        }

        private static string GetFolderPath(IEnumerable<Folder> folders, int id, string currentPath = "")
        {
            var enumerable = folders as Folder[] ?? folders.ToArray();
            var folder = enumerable.FirstOrDefault(folder1 => folder1.Id == id);
            if (folder == null)
            {
                return currentPath;
            }
            currentPath = string.Format("{0}/{1}", folder.Name, currentPath);
            return folder.ParentFolder == null
                       ? currentPath
                       : GetFolderPath(enumerable, folder.ParentFolder.Value, currentPath);
        }
    }
}