using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDo_Presentation.Views
{
    /// <summary>
    /// Interaction logic for NewListView.xaml
    /// </summary>
    public partial class NewListView : Window
    {
        public NewListView()
        {
            InitializeComponent();
        }

        private void createListButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<IListService>().CreateList(Session.UserId, nameBox.Text);
            this.Close();
        }
    }
}
