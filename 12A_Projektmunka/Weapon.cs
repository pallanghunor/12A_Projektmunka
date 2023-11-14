using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12A_Projektmunka
{
    class Weapon
    {
        private string _name;
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public string WeaponType { get; set; }
        public string FileName { get; set; }
        public int Cost { get; set; }
        public int Ammo { get; set; }
        public int Damage { get; set; }
        public int FireRate { get; set; }
        public int Penetration { get; set; }
        public int Difficulty { get; set; }

        public Weapon(string row)
        {
            string[] data = row.Split(';');
            Name = data[0];
            WeaponType = data[1];
            FileName = data[2];
            Cost = int.Parse(data[3]);
            Ammo = int.Parse(data[4]);
            Damage = int.Parse(data[5]);
            FireRate = int.Parse(data[6]);
            Penetration = int.Parse(data[7]);
            Difficulty = int.Parse(data[8]);
        }

        public Weapon()
        {
            
        }
    }
}
