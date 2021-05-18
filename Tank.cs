using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ElementalTanks
{
    public enum Element
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
        public Element Element { get; }
        public int Health { get; private set; }
        public int MoveSpeed { get; private set; }
        public string Direction { get; private set; }
        public PictureBox Sprite { get; private set; }
        public Image spr { get; }
        public Tank()
        {
            Sprite = new PictureBox
            {
                Image = Properties.Resources.tanks_52,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(516, 130)
            };
            //Sprite.
            //Sprite.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
    }
}
