using System.Collections.Generic;
using System.IO;
using DocumentServices.Models;
using File = DocumentServices.Models.File;

namespace DocumentServices.Abstractions
{
    public interface IDocumentService
    {
        FolderInformation GetFolderInformation(int id);
        FolderInformation GetFolderInformation();
        IList<Folder> GetFolderTree(int id = 0);
        void CreateDocument(Stream inputStream, int profileName, string fileName);
        string GetFolderPathById(int id);
        Folder CreateFolder(string folder, int parentFolderId);
        File GetFileById(int id);
        IList<FileVersion> GetFileVersionsAndCommentsByFileId(int id);
        FileVersion GetFileVersionById(int versionId);

        void AddComment(int versionId, string comment);
        IList<Comment> GetCommentsByVersionId(int versionId);
    }
}
