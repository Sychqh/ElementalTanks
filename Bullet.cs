using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ElementalTanks
{
    class Bullet
    {
        public Tank Sender;
        public string Direction { get; set; }
        private readonly int spriteNumber;
        public ElementType Element { get; }
        private Image SourceImage => (Image)Properties.Resources.ResourceManager.GetObject("bullet" + Element.ToString() + spriteNumber.ToString(), Properties.Resources.Culture);
        public Image Sprite { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Bullet(Tank sender, ElementType element, int spriteNumber, Point location, string direction)
        {
            Sender = sender;
            Element = element;
            this.spriteNumber = spriteNumber;
            Sprite = SourceImage;
            X = location.X;
            Y = location.Y;
            Direction = direction;
        }

        public void Rotate()
        {
            Sprite = SourceImage;
            Sprite.RotateFlip(Tank.Rotations[Direction]);
        }

        public void Update()
        {
            X = Sender.GunPosition.X;
            Y = Sender.GunPosition.Y;
            Direction = Sender.Direction;
            Sprite = SourceImage;
            Sprite.RotateFlip(Tank.Rotations[Direction]);
        }
    }
}
