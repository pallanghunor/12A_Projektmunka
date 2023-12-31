﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12A_Projektmunka
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPorpertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Weapon> _weapons;
        public ObservableCollection<Weapon> weapons
        {
            get { return _weapons; }
            set { _weapons = value; OnPorpertyChanged("weapons"); }
        }

        private HashSet<string> _types;
        public HashSet<string> types
        {
            get { return _types; }
            set { _types = value; OnPorpertyChanged("types"); }
        }

        private HashSet<string> _typesSelection;
        public HashSet<string> typesSelection
        {
            get { return _typesSelection; }
            set { _typesSelection = value; OnPorpertyChanged("typesSelection"); }
        }

        private ObservableCollection<Weapon> _filteredWeapons;
        public ObservableCollection<Weapon> filteredWeapons
        {
            get { return _filteredWeapons; }
            set { _filteredWeapons = value; OnPorpertyChanged("filteredWeapons"); }
        }

        private Weapon _selectedWeapon = new Weapon();
        public Weapon selectedWeapon
        {
            get { return _selectedWeapon; }
            set { _selectedWeapon = value; OnPorpertyChanged("selectedWeapon");}
        }

        private Weapon _tempWeapon;
        public Weapon tempWeapon
        {
            get { return _tempWeapon; }
            set { _tempWeapon = value; OnPorpertyChanged("tempWeapon"); }
        }

        public Model()
        {
            ReadFile();
        }

        public void FilterWeapons(string weaponType)
        {
            filteredWeapons = new ObservableCollection<Weapon>();
            foreach (var weapon in weapons)
            {
                if (weapon.WeaponType == weaponType || weaponType == "Show All" || weaponType == "")
                {
                    filteredWeapons.Add(weapon);
                }
            }
        }

        private void ReadFile()
        {

            weapons = new ObservableCollection<Weapon>();
            types = new HashSet<string>();
            types.Add("Show All");
            typesSelection = new HashSet<string>();
            StreamReader sr = new StreamReader("weapons.txt");
            sr.ReadLine();
            while(!sr.EndOfStream)
            {
                string row = sr.ReadLine();
                weapons.Add(new Weapon(row));
                types.Add(row.Split(';')[1]);
                typesSelection.Add(row.Split(';')[1]);
            }
            sr.Close();
        }
    }
}
