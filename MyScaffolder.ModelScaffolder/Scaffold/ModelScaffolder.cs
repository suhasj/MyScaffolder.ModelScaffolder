using EnvDTE;
using Microsoft.AspNet.Scaffolding.Core;
using MyScaffolder.ModelScaffolder.UI;
using MyScaffolder.RepositoryScaffolder.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MyScaffolder.ModelScaffolder.Scaffold
{
    [CodeGeneratorMetadata(DisplayName = "Add new Model class from Base class", SupportsGui = true)]
    class ModelScaffolder : CodeGeneratorBase
    {
        public void GenerateCode(string modelName,
                                 CodeType baseClassType,
                                 Dictionary<string, string> propertiesCollection,
                                 bool overwriteViews = true)
        {
            Project project = Project;

            string modelNamespace = baseClassType == null ? project.Name + ".Models" : baseClassType.Namespace.FullName;

            List<string> properties = propertiesCollection.Keys.ToList<string>();

            List<string> propertyTypes = propertiesCollection.Values.ToList<string>();

            string outputPath = Path.Combine("Models", "MyModel");

            AddProjectItemViaTemplate(outputPath,
                    templateName: "MyModel",
                    templateModel: new Hashtable() 
                    {
                        {"ModelName" , modelName},
                        {"Namespace" , modelNamespace},
                        {"BaseClassTypeName", baseClassType == null ? "" : baseClassType.Name},
                        {"PropertiesCollection",properties},
                        {"PropertiesTypeCollection",propertyTypes}
                    }, overwrite: false);
        }

        public void ShowUIAndGenerateCode()
        {
            ModelScaffolderViewModel viewModel = new ModelScaffolderViewModel(Services);
            DisplayMenu window = new DisplayMenu(viewModel);
            bool? isOk = window.ShowDialog();

            if (isOk.Value)
            {
                Cursor currentCursor = Mouse.OverrideCursor;
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;

                    if (String.IsNullOrEmpty(viewModel.ModelName))
                    {
                        throw new ArgumentNullException("model name cannot be null");
                    }

                    GenerateCode(viewModel.ModelName,
                    viewModel.BaseClassType,
                        viewModel.PropertiesCollection);
                }
                finally
                {
                    Mouse.OverrideCursor = currentCursor;
                }
            }
        }
    }
}
