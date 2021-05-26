using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ElementalTanks
{
    public class Tank : IEntity
    {
        public ElementType Element { get; set; }
        public int Health { get; set; }
        public int MoveSpeed { get; private set; }

        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        private readonly int spriteNumber;
        private Image SourceImage => (Image)Properties.Resources.ResourceManager.GetObject("tank" + Element.ToString() + spriteNumber.ToString(), Properties.Resources.Culture);
        public Image Sprite { get; set; }

        public static readonly Dictionary<string, RotateFlipType> Rotations = new Dictionary<string, RotateFlipType>
        {
             ["Up"] = RotateFlipType.RotateNoneFlipNone,
             ["Down"] = RotateFlipType.RotateNoneFlipY,
             ["Left"] = RotateFlipType.Rotate90FlipX,
             ["Right"] = RotateFlipType.Rotate90FlipNone
        };

        public static readonly Dictionary<string, (int, int)> MovementForDirection = new Dictionary<string, (int, int)> 
        {
            ["Up"] = (0, -1),
            ["Down"] = (0, 1),
            ["Left"] = (-1, 0),
            ["Right"] = (1, 0)
        };

        public Point GunPosition 
        { 
            get => Direction switch
            {
                "Up" => new Point(X, Y - Sprite.Height),
                "Down" => new Point(X, Y + Sprite.Height),
                "Left" => new Point(X - Sprite.Width, Y),
                "Right" => new Point(X + Sprite.Width, Y),
                _ => Point.Empty
            };
        }
        
        public Tank(int spriteNumber, int x, int y, ElementType element, int moveSpeed)
        {
            Element = element;
            Health = 100;
            this.spriteNumber = spriteNumber;
            MoveSpeed = moveSpeed;
            Direction = "Up";
            X = x;
            Y = y;
            Sprite = SourceImage;
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

        public void Rotate()
        {
            Sprite = SourceImage;
            Sprite.RotateFlip(Rotations[Direction]);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
