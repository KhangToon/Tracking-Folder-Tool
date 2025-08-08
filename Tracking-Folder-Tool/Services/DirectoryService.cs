using System.IO;

namespace Tracking_Folder_Tool.Services
{
    public class DirectoryService
    {
        public List<string> GetDirectories(string parentPath)
        {
            try
            {
                if (string.IsNullOrEmpty(parentPath) || !Directory.Exists(parentPath))
                {
                    parentPath = Directory.GetCurrentDirectory();
                }
                return Directory.GetDirectories(parentPath).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
