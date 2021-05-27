using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ElementalTanks
{
    enum BulletType
    {
        Spray,
        Projectile
    }

    public class Bullet : IEntity
    {
        public ElementType Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public ITank Sender;

        public Bullet(ITank sender, ElementType element, int spriteNumber, Point location, string direction)
        {
            Sender = sender;
            Element = element;
            X = location.X;
            Y = location.Y;
            Direction = direction;
        }

        public void Update()
        {
            //X = Sender.GunPosition.X;
            //Y = Sender.GunPosition.Y;
            Direction = Sender.Direction;
            //Sprite = SourceImage;
            //Sprite.RotateFlip(Tank.Rotations[Direction]);
        }
    }
}
