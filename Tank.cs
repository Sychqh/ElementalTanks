using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ElementalTanks
{
    public enum ElementType
    {
        Fire,
        Water,
        Earth,
        Wind,
        Lightning,
        Cold
    }

    class Tank
    {
        public ElementType Element { get; }
        public PictureBox ShootEffect { get; }
        public int Health { get; set; }
        public int MoveSpeed { get; private set; }
        public string Direction { get; set; }

        public Point Location { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Image Sprite { get; set; }

        public Tank()
        {
            Health = 100;
            MoveSpeed = 20;
            Direction = "Up";
            X = 300;
            Y = 300;
            Sprite = Properties.Resources.tank1;
           
            
            Element = ElementType.Fire;
            //ShootEffect = new PictureBox
            //{
            //    Image = Properties.Resources.fire1,
            //    SizeMode = PictureBoxSizeMode.AutoSize,
            //    Location = new Point(Sprite.Left, Sprite.Top - Sprite.Height)
            //};

        }
    }
}
