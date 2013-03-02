using EnvDTE;
using Microsoft.AspNet.Scaffolding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScaffolder.ModelScaffolder.UI
{
    public class ModelScaffolderViewModel
    {
        private List<CodeType> _codeTypesCollection;
        private readonly CodeGenerationServices _codeGenerationServices;

        public ModelScaffolderViewModel(CodeGenerationServices services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }

            _codeGenerationServices = services;

            PropertiesCollection = new Dictionary<string, string>();
        }

        public CodeType BaseClassType { get; set; }

        public Dictionary<string, string> PropertiesCollection { get; set; }

        public string ModelName { get; set; }

        public List<CodeType> ModelTypeCollection
        {
            get
            {
                if (_codeTypesCollection == null)
                {
                    _codeTypesCollection = new List<CodeType>();
                    foreach (CodeType codeType in _codeGenerationServices.ModelTypeLocator.GetAllTypes())
                    {
                        _codeTypesCollection.Add(codeType);
                    }
                }
                return _codeTypesCollection;
            }
        }
    }
}
