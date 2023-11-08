using System;
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

        public Model()
        {
            ReadFile();
        }

        private void ReadFile()
        {

            weapons = new ObservableCollection<Weapon>();
            types = new HashSet<string>();
            StreamReader sr = new StreamReader("weapons.txt");
            sr.ReadLine();
            while(!sr.EndOfStream)
            {
                string row = sr.ReadLine();
                weapons.Add(new Weapon(row));
                types.Add(row.Split(';')[1]);
            }
            sr.Close();
        }
    }
}
