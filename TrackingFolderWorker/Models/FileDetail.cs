using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFolderWorker.Models
{
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
