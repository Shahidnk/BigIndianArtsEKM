using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class FileToUpload
    {
        public int FId { get; set; }
        public string FileName { get; set; }
        public byte[] ByteArray { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
    }
}
