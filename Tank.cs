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
        public PictureBox Sprite { get; }

        public Tank()
        {
            Sprite = new PictureBox
            {
                Image = Properties.Resources.tank1,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(300, 300)
            };

            Health = 100;
            MoveSpeed = 20;
            Direction = "Up";

            Element = ElementType.Fire;
            ShootEffect = new PictureBox
            {
                Image = Properties.Resources.fire1,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(Sprite.Left, Sprite.Top - Sprite.Height)
            };
        }
    }
}
