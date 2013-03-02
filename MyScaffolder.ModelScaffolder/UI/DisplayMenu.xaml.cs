using Microsoft.VisualStudio.PlatformUI;
using MyScaffolder.ModelScaffolder.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Linq;

namespace MyScaffolder.RepositoryScaffolder.UI
{
    /// <summary>
    /// Interaction logic for DisplayMenu.xaml
    /// </summary>
    public partial class DisplayMenu : Window
    {
        public DisplayMenu(ModelScaffolderViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        public ModelScaffolderViewModel ViewModel { get; set; }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CollectionViewSource modelTypesViewSource = (CollectionViewSource)this.Resources["modelTypesCollectionViewSource"];
            modelTypesViewSource.Source = ViewModel.ModelTypeCollection;

            ModelBox.SelectedIndex = -1;
        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (ViewModel.PropertiesCollection.Count == 0)
            {
                ViewModel.PropertiesCollection.Add(Property1.Text, Property1Type.Text);
                ViewModel.PropertiesCollection.Add(Property2.Text, Property2Type.Text);

                ViewModel.ModelName = ModelNameBox.Text;

                if (!String.IsNullOrEmpty(ModelBox.Text))
                {
                    ViewModel.BaseClassType = ViewModel.ModelTypeCollection.Where(x => x.Name.Equals(ModelBox.Text)).FirstOrDefault();
                }
            }

            DialogResult = true;
        }
    }

}
