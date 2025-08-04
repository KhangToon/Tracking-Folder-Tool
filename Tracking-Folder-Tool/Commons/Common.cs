using static Tracking_Folder_Tool.Pages.Index;

namespace Tracking_Folder_Tool.Commons
{
    public static class Common
    {
        public static List<FileDetail> FileLists = [];

        public static string TargetPath = @"D:\KHANG-DEV-DOC\FolderToTracking";
        public class FileDetail
        {
            public string Name { get; set; } = "";
            public string FullPath { get; set; } = "";
            public string Extension { get; set; } = "";
            public long Size { get; set; }
            public string ActionType { get; set; } = "";
            public DateTime Created { get; set; }
            public DateTime Changed { get; set; }
        }
    }

}
