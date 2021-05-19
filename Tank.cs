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
        public int X { get; set; }
        public int Y { get; set; }
        public Image SourceImage { get; set; }
        public Image Sprite { get; set; }

        public readonly Dictionary<string, RotateFlipType> Rotations;
        public readonly Dictionary<string, (int, int)> MovementForDirection;

        public Tank(Image sprite, int x, int y, ElementType element, int moveSpeed)
        {
            Health = 100;
            MoveSpeed = moveSpeed;
            Direction = "Up";
            X = x;
            Y = y;
            Sprite = sprite;
           
            Element = element;

            Rotations = new Dictionary<string, RotateFlipType>
            {
                ["Up"] = RotateFlipType.RotateNoneFlipNone,
                ["Down"] = RotateFlipType.RotateNoneFlipY,
                ["Left"] = RotateFlipType.Rotate90FlipX,
                ["Right"] = RotateFlipType.Rotate90FlipNone
            };

            MovementForDirection = new Dictionary<string, (int, int)>
            {
                ["Up"] = (0, -MoveSpeed),
                ["Down"] = (0, MoveSpeed),
                ["Left"] = (-MoveSpeed, 0),
                ["Right"] = (MoveSpeed, 0)
            };
        }

        public bool IsInBounds(Form form)
        {
            return Direction switch
            {
                "Left" => X > 1,
                "Up" => Y > 1,
                "Right" => X + Sprite.Width < form.ClientSize.Width - 1,
                "Down" => Y + Sprite.Height < form.ClientSize.Height,
                _ => true,
            };
        }
    }
}
