using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace ElementalTanks
{
    public partial class Game
    {
        public readonly Player Player;
        public readonly List<IEntity> Entities;
        public readonly List<IEntity> Deleted;

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

        public static readonly Dictionary<Point, string> DirectionForMovement = new Dictionary<Point, string>
        {
            [new Point(0, -1)] = "Up",
            [new Point(0, 1)] = "Down",
            [new Point(-1, 0)] = "Left",
            [new Point(1, 0)] = "Right"
        };

        public Game(Form form, Dictionary<IEntity, Image> sourceImages)
        {
            this.sourceImages = sourceImages;
            this.form = form;

            Entities = new List<IEntity>
            {
                new Player(300, 300, ElementType.Fire),
            };
            Player = Entities[0] as Player;
            Entities.Add(new Enemy(100, 100, ElementType.Fire, Entities, 0));
            Entities.Add(new Enemy(200, 100, ElementType.Water, Entities, 0));
            Entities.Add(new Enemy(300, 100, ElementType.Earth, Entities, 0));
            Entities.Add(new Enemy(400, 100, ElementType.Wind, Entities, 0));
            Entities.Add(new Enemy(500, 100, ElementType.Lightning, Entities, 0));
            Entities.Add(new Enemy(500, 200, ElementType.Cold, Entities, 0));
            Entities.Add(new Obstacle(100, 500, ElementType.Fire));
            Entities.Add(new Obstacle(200, 500, ElementType.Water));
            Entities.Add(new Obstacle(300, 500, ElementType.Earth));
            Entities.Add(new Obstacle(400, 500, ElementType.Wind));
            Entities.Add(new Obstacle(500, 500, ElementType.Lightning));
            Entities.Add(new Obstacle(100, 400, ElementType.Cold));

            Deleted = new List<IEntity>();

        }

        public void Update()
        {
            //foreach(var entity in Entities.Where(e => e is Enemy))
            //{
            //    var enemy = entity as Enemy;
            //    enemy.Move(enemy.FindNextDirection());
            //}
            foreach (var entity in Entities)
                entity.Update();

            foreach (var tank in Entities.Where(e => e is ITank))
                if (!IsEntityInBounds(form, tank))
                    (tank as ITank).MoveBack();

            foreach (var e1 in Entities)
            {
                foreach (var e2 in Entities.Where(e => e != e1))
                {
                    if (AreCollided(e1, e2))
                    {
                        Player.MoveBack();
                    }
                }
            }

            if (Deleted != null)
            {
                foreach (var delete in Deleted)
                    Entities.Remove(delete);
              
                Deleted.Clear();
            }

            //foreach (var bullet in entities.Where(ent => ent is Bullet))
            //{
            //    foreach (var enemy in entities.Where(ent => ent is Enemy))
            //    {
            //        //if (IsTankHit(bullet as Bullet, enemy as Enemy))
            //            deleted.Add(enemy);
            //    }
            //}
        }

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
        //private void Shoot()
        //{
        //    var bullet = new Bullet(player, player.Element, 1, player.GunPosition, player.Direction);
        //    bullets.Add(bullet);
        //}
    }
}
