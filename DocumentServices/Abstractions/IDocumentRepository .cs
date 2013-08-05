using System.Collections.Generic;
using System.IO;
using DocumentServices.Models;
using File = DocumentServices.Models.File;

namespace DocumentServices.Abstractions
{
    public interface IDocumentRepository
    {
        IList<Folder> LoadRootFolders();
        IList<Folder> GetFoldersByParentId(int id);
        IList<File> GetFilesInFolder(int id);
        Folder GetFolderById(int id);
        Folder InsertNewFolder(string folder, int parentFolderId, int ownerId);
        Folder GetFolderByName(string foldername, int parentFolderId);
        File GetFileByNameAndFolder(string fileName, int folderId);
        void CreateNewVersionOfFile(File file, Stream inputStream);
        void CreateNewFile(File file, Stream inputStream);
        File GetFileById(int id);
        IList<FileVersion> GetFileVersionsByFileId(int id);
        IList<Comment> GetCommentsByFileId(int id);
        FileVersion GetFileVersionById(int versionId);
        User GetUserById(int ownerId);
        Comment SaveComment(Comment newComment);
        IList<Comment> GetCommentsByVersionId(int versionId);
    }
}
