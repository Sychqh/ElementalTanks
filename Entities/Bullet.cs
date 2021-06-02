using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ElementalTanks
{
    public class Bullet : IEntity
    {
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int MoveSpeed { get; set; }
        public ITank Sender;

        public Bullet(ITank sender)
        {
            Sender = sender;
            Element = sender.Element;
            X = sender.GunPosition.X;
            Y = sender.GunPosition.Y;
            Direction = sender.Direction;
            Width = Height = 64;
            MoveSpeed = 10;
        }

        public void Update()
        {
            if (Sender.Element.Type == BulletType.Spray)
            {
                Direction = Sender.Direction;
                X = Sender.GunPosition.X;
                Y = Sender.GunPosition.Y;
            }
            else
            {
                X += Game.MovementForDirection[Direction].X * MoveSpeed;
                Y += Game.MovementForDirection[Direction].Y * MoveSpeed;
            }
        }

        public void Move(string direction)
        {
            
        }
    }
}
