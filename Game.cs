using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace ElementalTanks
{
    public class Game
    {
        public readonly Player player;
        public readonly List<IEntity> entities;
        public readonly List<IEntity> deleted;

        private readonly Dictionary<IEntity, Image> sourceImages;
        private readonly Form form;
        private int score;

        public static readonly Dictionary<string, RotateFlipType> SpriteRotations = new Dictionary<string, RotateFlipType>
        {
            ["Up"] = RotateFlipType.RotateNoneFlipNone,
            ["Down"] = RotateFlipType.RotateNoneFlipY,
            ["Left"] = RotateFlipType.Rotate90FlipX,
            ["Right"] = RotateFlipType.Rotate90FlipNone
        };

        public static readonly Dictionary<string, Point> MovementForDirection = new Dictionary<string, Point>
        {
            ["Up"] = new Point(0, -1),
            ["Down"] = new Point(0, 1),
            ["Left"] = new Point(-1, 0),
            ["Right"] = new Point(1, 0)
        };

        public Game(Form form, Dictionary<IEntity, Image> sourceImages)
        {
            this.sourceImages = sourceImages;
            this.form = form;

            entities = new List<IEntity>
            {
                new Player(300, 300, ElementType.Fire),
                new Enemy(100, 100, ElementType.Water),
                new Enemy(600, 300, ElementType.Fire),
                new Enemy(100, 500, ElementType.Water),
            };
            deleted = new List<IEntity>();
            player = entities[0] as Player;

        }

        public void MainTimerEvent(object sender, EventArgs e)
        {
            foreach (var entity in entities)
                entity.Update();

            //foreach (var bullet in entities.Where(ent => ent is Bullet))
            //{
            //    foreach (var enemy in entities.Where(ent => ent is Enemy))
            //    {
            //        //if (IsTankHit(bullet as Bullet, enemy as Enemy))
            //            deleted.Add(enemy);
            //    }
            //}

            foreach (var e1 in entities)
            {
                foreach (var e2 in entities.Where(e => e != e1))
                {
                    if (AreCollided(e1, e2))
                        player.MoveBack();
                    if (!IsEntityInBounds(form, e1))
                        player.MoveBack();
                }
            }

            if (deleted != null)
            {
                foreach (var delete in deleted)
                    entities.Remove(delete);
                deleted.Clear();
            }
        }
        //private void Shoot()
        //{
        //    var bullet = new Bullet(player, player.Element, 1, player.GunPosition, player.Direction);
        //    bullets.Add(bullet);
        //}

        public bool AreCollided(IEntity first, IEntity second)
        {
            return (first.X < second.X + sourceImages[second].Width) &&
            (second.X < (first.X + sourceImages[first].Width)) &&
            (first.Y < second.Y + sourceImages[second].Height) &&
            (second.Y < first.Y + sourceImages[first].Height);
        }

        public bool IsEntityInBounds(Form form, IEntity entity)
        {
            return entity.Direction switch
            {
                "Left" => entity.X > 1,
                "Up" => entity.Y > 1,
                "Right" => entity.X + sourceImages[entity].Width < form.ClientSize.Width - 1,
                "Down" => entity.Y + sourceImages[entity].Height < form.ClientSize.Height,
                _ => true,
            };
        } 
    }
}
