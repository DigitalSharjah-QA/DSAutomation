using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace SD.Datasources.Attachments
{

        
              public static class AttachmentsPaths
        {
            public static string GetPath(string fileName)
            {
                return Path.GetFullPath(Path.Combine("Datasources", "Attachments", fileName));
            }
        }
    
    
}
