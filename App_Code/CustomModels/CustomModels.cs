using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomModels
/// </summary>
public class CustomModels
{
    public partial class FileItem
    {
        public string fileName { get; set; }
        public decimal fileSize { get; set; }
        public string fileType { get; set; }
        public string fileBase64 { get; set; }
        public string id { get; set; }
    }
}