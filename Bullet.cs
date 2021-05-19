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
        private readonly int spriteNumber;
        public ElementType Element { get; }
        private Image SourceImage => (Image)Properties.Resources.ResourceManager.GetObject("bullet" + Element.ToString() + spriteNumber.ToString(), Properties.Resources.Culture);
        public Image Sprite { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Bullet(ElementType element, int spriteNumber, int x, int y)
        {
            Element = element;
            this.spriteNumber = spriteNumber;
            Sprite = SourceImage;
            X = x;
            Y = y;
        }
    }
}
