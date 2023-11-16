using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Path = System.IO.Path;

namespace _12A_Projektmunka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model();
        AboutUs w;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = model;
            model.FilterWeapons("");
        }

        private void refreshLbx()
        {
            model.FilterWeapons(weaponTypeCbx.SelectedItem.ToString());
        }

        private bool checkError()
        {
            if (String.IsNullOrWhiteSpace(selectedNameTbx.Text))
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Name field must contain a charachter!");
                errorMessageBox.ShowDialog();
                return false;
            } else if(selectedWeaponTypeCbx.SelectedItem == null)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Combobox field is empty!");
                errorMessageBox.ShowDialog();
                return false;
            } else if (!int.TryParse(selectedammoTbx.Text, out int ammo) || ammo <= 0)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Incorrect ammo value!");
                errorMessageBox.ShowDialog();
                return false;
            } else if(!int.TryParse(selectedcostTbx.Text, out int cost) || cost <= 0)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Incorrect cost value!");
                errorMessageBox.ShowDialog();
                return false;
            } else if(!int.TryParse(selecteddamageTbx.Text, out int damage) || damage <= 0 || damage > 100)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Incorrect damage value!");
                errorMessageBox.ShowDialog();
                return false;
            } else if(!int.TryParse(selectedpenTbx.Text, out int pen) || pen <= 0 || pen > 100)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Incorrect penetration value!");
                errorMessageBox.ShowDialog();
                return false;
            } else if(selectedfrateSldr.Value == 0)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Incorrect fire rate value!");
                errorMessageBox.ShowDialog();
                return false;
            } else if (model.tempWeapon.FileName == null)
            {
                ErrorMessageBox errorMessageBox = new ErrorMessageBox("Weapon image is not selected!");
                errorMessageBox.ShowDialog();
                return false;
            }
            return true;
        }

        private void resetWeaponStats()
        {
            model.tempWeapon.Name = model.selectedWeapon.Name;
            model.tempWeapon.WeaponType = model.selectedWeapon.WeaponType;
            model.tempWeapon.Cost = model.selectedWeapon.Cost;
            model.tempWeapon.FileName = model.selectedWeapon.FileName;
            model.tempWeapon.Ammo = model.selectedWeapon.Ammo;
            model.tempWeapon.Damage = model.selectedWeapon.Damage;
            model.tempWeapon.Difficulty = model.selectedWeapon.Difficulty;
            model.tempWeapon.FireRate = model.selectedWeapon.FireRate;
            model.tempWeapon.Penetration = model.selectedWeapon.Penetration;
            model.tempWeapon.filePath = model.selectedWeapon.filePath;
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
            refreshLbx();
        }

        private void weaponLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                model.selectedWeapon = (Weapon)weaponLbx.SelectedItem;
            if (model.selectedWeapon != null)
            {
                selectedWeaponZone.Visibility = Visibility.Visible;
                model.tempWeapon = new Weapon
                {
                    Name = model.selectedWeapon.Name,
                    WeaponType = model.selectedWeapon.WeaponType,
                    FileName = model.selectedWeapon.FileName,
                    Cost = model.selectedWeapon.Cost,
                    Ammo = model.selectedWeapon.Ammo,
                    Damage = model.selectedWeapon.Damage,
                    Difficulty = model.selectedWeapon.Difficulty,
                    FireRate = model.selectedWeapon.FireRate,
                    Penetration = model.selectedWeapon.Penetration,
                    filePath = model.selectedWeapon.filePath
                };
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponZone.Visibility = Visibility.Visible;
            weaponLbx.SelectedItem = null;
            model.selectedWeapon = null;
            model.tempWeapon = new Weapon();
            selectedWeaponActive(true);
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("weapons.txt");
            sw.WriteLine("Name;Type;File name;Cost;Ammo;Damage;Fire rate;Penetration;Difficulty");
            foreach (var weapon in model.weapons)
            {
                sw.WriteLine($"{weapon.Name};{weapon.WeaponType};{weapon.FileName};{weapon.Cost};{weapon.Ammo};{weapon.Damage};{weapon.Difficulty};{weapon.FireRate};{weapon.Penetration}");
            }
            sw.Close();
            this.Close();
            if(w != null)
            {
                w.Close();
            }
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            w = new AboutUs();
            w.Show();
        }

        private void modifyBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponActive(true);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkError())
            {
                selectedWeaponActive(false);
                if (model.selectedWeapon != null) //Meglévő fegyver modosítása
                {
                    for (int i = 0; i < model.weapons.Count; i++)
                    {
                        if (model.weapons[i] == model.selectedWeapon)
                        {
                            model.weapons[i] = model.tempWeapon;
                            refreshLbx();
                            break;
                        }
                    }
                }
                else // új fegyver hozzáadása
                {
                    model.weapons.Add(model.tempWeapon);
                }
                refreshLbx();
                weaponLbx.SelectedItem = model.selectedWeapon;
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponActive(false);
            if(model.selectedWeapon  == null)
            {
                selectedWeaponZone.Visibility = Visibility.Hidden;
            }
            else
            {
                resetWeaponStats();
            }
        }

        private void selectedWeaponZoneBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponZone.Visibility = Visibility.Hidden;
            selectedWeaponActive(false);
            weaponLbx.SelectedItem = null;
        }

        private void selectWeaponZoneBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedWeaponZone.Visibility = Visibility.Hidden;
            selectWeaponZone.Visibility = Visibility.Hidden;
            selectedWeaponActive(false);
            weaponTypeCbx.SelectedIndex = 0;
        }

        private void showWeaponsBtn_Click(object sender, RoutedEventArgs e)
        {
            selectWeaponZone.Visibility= Visibility.Visible;
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteMessageBox deleteMessageBox = new DeleteMessageBox();
            bool? result = deleteMessageBox.ShowDialog();
            if (result == true)
            {
                model.weapons.Remove(model.selectedWeapon);
                selectedWeaponZone.Visibility = Visibility.Hidden;
                refreshLbx();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                string executableDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

                string projectDirectory = Directory.GetParent(Directory.GetParent(executableDirectory).FullName).FullName;

                string targetFolder = Path.Combine(projectDirectory, "img");

                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                string fileName = Path.GetFileName(selectedFilePath);

                string targetFilePath = Path.Combine(targetFolder, fileName);

                File.Copy(selectedFilePath, targetFilePath, true);

                Debug.WriteLine($"Copied file to: {targetFilePath}");

                model.tempWeapon.FileName = fileName;
            }
        }
    }
}
