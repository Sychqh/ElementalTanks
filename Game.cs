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
        public readonly Player Player;
        public readonly List<IEntity> Entities;
        public readonly List<IEntity> Deleted;
        public IEntity[,] Map;
        public readonly int MapWidth;
        public readonly int MapHeight;

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

        public Game()
        {
            MapWidth = 800;
            MapHeight = 600;
            Map = new IEntity[MapWidth, MapHeight];

            Entities = new List<IEntity>
            {
                new Player(300, 300, new Water()),
            };
            Player = Entities[0] as Player;
            GenerateLevel();
            Deleted = new List<IEntity>();

        }

        public void Update()
        {
            UpdateMap();
            foreach (var entity in Entities)
                entity.Update(Map);

            foreach (var entity in Entities)
            {
                if (!IsEntityInBounds(entity))
                    if (entity is Bullet)
                        Deleted.Add(entity);
            }

            foreach (var bullet in Entities.Where(ent => ent is Bullet))
            {
                foreach (var tank in Entities.Where(ent => ent is ITank))
                {
                    if (AreCollided(bullet, tank))
                        (tank as ITank).TakeDamage(bullet as Bullet);
                }
            }

            foreach (var tank in Entities.Where(ent => ent is ITank))
            {
                if ((tank as ITank).Health < 0)
                    Deleted.Add(tank);

            }

            if (Deleted != null)
            {
                foreach (var delete in Deleted)
                    Entities.Remove(delete);
              
                Deleted.Clear();
            }

        }

        public void UpdateMap()
        {
            Map = new IEntity[MapWidth, MapHeight];
            foreach (var entity in Entities.Where(ent => !(ent is Bullet)))
            {
                for (var i = entity.X; i < entity.X + entity.Width; i++)
                    for (var j = entity.Y; j < entity.Y + entity.Height; j++)
                        Map[i, j] = entity;
            }
        }

        public bool AreCollided(IEntity first, IEntity second)
        {
            return (first.X < second.X + second.Width) 
                && (second.X < first.X + first.Width) 
                && (first.Y < second.Y + second.Height) 
                && (second.Y < first.Y + first.Height);
        }

        public bool IsEntityInBounds(IEntity entity)
        {
            return entity.Direction switch
            {
                "Left" => entity.X > 0,
                "Up" => entity.Y > 0,
                "Right" => entity.X + entity.Width < MapWidth,
                "Down" => entity.Y + entity.Height < MapHeight,
                _ => true,
            };
        }

        public void GenerateLevel()
        {
            var rnd = new Random(2);
            var enemyAmount = rnd.Next(1, 4);
            
            for (var i = 0; i < enemyAmount; i++)
            {
                Entities.Add(new Enemy(rnd.Next(MapWidth - 100), rnd.Next(MapHeight - 100), ChooseElement(rnd.Next(8)), Player, 1));
            }

            var obstacleAmount = rnd.Next(1, 8);
            for (var i = 0; i < obstacleAmount; i++)
            {
                Entities.Add(new Obstacle(rnd.Next(MapWidth - 100), rnd.Next(MapHeight - 100), ChooseElement(rnd.Next(8))));
            }
        }

        public void GenerateLevel1()
        {
            //Entities.Add(new Obstacle(0, 90, new Fire()));
            Entities.Add(new Enemy(90, 90, new Fire(), Player, 1));
        }

        public IElement ChooseElement(int num)
        {
            return num switch
            {
                0 => new None(),
                1 => new Fire(),
                2 => new Water(),
                3 => new Earth(),
                4 => new Wind(),
                5 => new Lightning(),
                6 => new Cold(),
                _ => new None(),
            };
        }
    }
}
