using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFolderWorker.Requests
{
    public class CsvColumnHeaderResponse
    {
        public Guid Id { get; set; }
        public string GExSerial { get; set; } = string.Empty;
        public string HeaderName { get; set; } = string.Empty;
    }
}
