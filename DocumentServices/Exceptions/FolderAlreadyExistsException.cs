using System;

namespace DocumentServices.Exceptions
{
    [Serializable]
    public class FolderAlreadyExistsException : Exception
    {
        public FolderAlreadyExistsException(string folderName)
            : base(string.Format("{0} has already exists.", folderName))
        {

        }
    }
}