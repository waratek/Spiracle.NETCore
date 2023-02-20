using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class LibraryLoadModel
    {
        private List<string> loadMethodsList = new List<string>(new string[]
        {
            "NativeLibrary::Load",
            "NativeLibrary::TryLoad",
            "System.Reflection.Assembly::LoadFile",
            "System.Reflection.Assembly::LoadFrom"
        });

        public List<string> LoadMethods
        {
            get => loadMethodsList;
        }

        public string SelectedLibraryLoadMethod { get; set; } = "NativeLibrary::Load" ;

        public string LibraryName { get; set; } = "Newtonsoft.Json.dll";
        public string OperationResult { get; set; }

    }
}
