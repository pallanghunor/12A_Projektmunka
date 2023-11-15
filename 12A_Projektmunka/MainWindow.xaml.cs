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
        AboutUs w = new AboutUs();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = model;
            model.FilterWeapons("");
        }

        private void selectedWeaponActive(bool active)
        {
            selectedNameTbx.IsEnabled = 
                selectedWeaponTypeCbx.IsEnabled = 
                selectedcostTbx.IsEnabled = 
                selectedammoTbx.IsEnabled = 
                selecteddamageTbx.IsEnabled = 
                selecteddamageSldr.IsEnabled = 
                selectedfrateSldr.IsEnabled = 
                selectedpenTbx.IsEnabled = 
                selectedpenSldr.IsEnabled = active;
            if (active)
            {
                selectedNameTbx.Focus();
                selectWeaponZone.IsEnabled = false;
                modifyBtn.Visibility = Visibility.Collapsed;
                delBtn.Visibility = Visibility.Collapsed;
                saveBtn.Visibility = Visibility.Visible;
                cancelBtn.Visibility = Visibility.Visible;
            }
            else
            {
                selectWeaponZone.IsEnabled = true;
                saveBtn.Visibility = Visibility.Collapsed;
                cancelBtn.Visibility = Visibility.Collapsed;
                modifyBtn.Visibility = Visibility.Visible;
                delBtn.Visibility = Visibility.Visible;
            }
        }

        private void weaponTypeCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.FilterWeapons(weaponTypeCbx.SelectedItem.ToString());
        }

        private void weaponLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWeaponZone.Visibility = Visibility.Visible;
            model.selectedWeapon = (Weapon)weaponLbx.SelectedItem;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponZone.Visibility = Visibility.Visible;
            model.selectedWeapon = new Weapon();
            selectedWeaponActive(true);
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w.Close();
            
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            w.Show();
        }

        private void modifyBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponActive(true);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            model.selectedWeapon = model.tempWeapon(model.selectedWeapon);
            selectedWeaponActive(false);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponActive(false);
        }

        private void selectedWeaponZoneBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponZone.Visibility = Visibility.Hidden;
            selectedWeaponActive(false);
        }

        
    }
}
