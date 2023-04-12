using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class ReleaseFile
    {
        public uint Size;

        public uint Downloads;

        public bool Secured;

        public string Id;

        public string Name;

        public string Url;

        public string Extension;

        public string Checksum;

        public string ReleaseId;

        public string CreatedAt;

        public string UpdatedAt;
    }

    public class Release
    {
        public uint Totalfiles;

        public bool IsPrivate;

        public bool Published;

        public string Id;

        public string CreatedAt;

        public string UpdatedAt;

        public string Name;

        public string Channel;

        public string Version;

        public string Notes;

        public string PublishedAt;

        public string ProductId;

        public List<string> Platforms;

        public List<ReleaseFile> Files;
    }
}
