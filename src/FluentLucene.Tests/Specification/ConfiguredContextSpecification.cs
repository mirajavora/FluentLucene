using System;
using System.IO;
using System.Reflection;
using FluentLucene.Configuration;
using FluentLucene.Tests.Mapping;

namespace FluentLucene.Tests.Specification
{
    public class ConfiguredContextSpecification : ContextSpecification
    {
        protected override void Context_BeforeAllSpecs()
        {
            if (LuceneMapper.IsConfigured) return;

            LuceneMapper.AddFromAssemblyOf<FluentMappingSpec>();
            LuceneMapper.Configure();
        }

        protected string ContentDirectory
        {
            get
            {
                var dir = GetAssemblyDirectory();
                if (dir.Parent.Name == "Debug" || dir.Parent.Name == "Release")
                    return Path.Combine(Path.GetDirectoryName(dir.Parent.Parent.FullName), "Content");
                else
                    return Path.Combine(Path.GetDirectoryName(dir.FullName), "Content");
            }
        }

        protected string AssemblyDirectory
        {
            get { return GetAssemblyDirectory().Parent.FullName; }
        }

        private DirectoryInfo GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return new DirectoryInfo(path);
        }

        protected string TempDirectory
        {
            get
            {
                var dir = Path.Combine(ContentDirectory, "Temp");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                return dir;
            }
        }

        protected override void CleanUpContext()
        {
            base.CleanUpContext();

            //clear the temp directory
            GC.Collect();
            var dir = new DirectoryInfo(TempDirectory);
            if (dir.Exists) dir.Delete(true);
        }
    }
}