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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
        public void SelectTable(object sender, RoutedEventArgs e)
        {
            RenderTable();
        }
        private void RenderTable()
        {
            var item = (ComboBoxItem)Table.SelectedItem;
            var fields = Helper.GetFields(item.Content.ToString());
            dataGrid.Columns.Clear();
            foreach ( var field in fields )
            {
                DataGridBoundColumn col = new DataGridTextColumn();
                switch (field.DataType)
                {
                    case Objects.DataType.String:
                        col = new DataGridTextColumn();
                        col.Header = field.Text;
                        break;
                    case Objects.DataType.Number:
                        col = new DataGridTextColumn();
                        col.Header = field.Text;
                        break;
                    case Objects.DataType.DateTime:
                        col = new DataGridTextColumn();
                        col.Header = field.Text;
                        break;
                }

                dataGrid.Columns.Add(col);
            }
            dataGrid.Columns.Add(new DataGridTemplateColumn 
            {
            Header = "Edit"
            });
            dataGrid.Columns.Add(new DataGridTemplateColumn
            {
                Header = "Delete"
            });
        }
    }
}
