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

namespace _12A_Projektmunka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = model;
            model.FilterWeapons("");
        }

        private void weaponTypeCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.FilterWeapons(weaponTypeCbx.SelectedItem.ToString());
        }

        private void weaponLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.selectedWeapon = (Weapon)weaponLbx.SelectedItem;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            model.selectedWeapon = new Weapon();
        }
    }
}
