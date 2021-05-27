using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ElementalTanks
{
    class Enemy : ITank
    {
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public ElementType Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int UpMovement { get; set; }
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }

        private readonly Player player;
        private readonly List<IEntity> entities;
        private List<Point> pathToPlayer;
        private int currentIndex = 1;

        public Enemy(int x, int y, ElementType element, List<IEntity> entities)
        {
            player = entities[0] as Player;
            this.entities = entities;
            X = x;
            Y = y;
            Element = element;
            Health = 100;
            MoveSpeed = 5;
            Direction = "Up";
            pathToPlayer = FindPathToPlayer();
        }

        public Point GunPosition(Image sprite)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            //Move(FindNextDirection());
            if (X == player.X && Y == player.Y)
                UpMovement = DownMovement = LeftMovement = RightMovement = 0;

            X += RightMovement - LeftMovement;
            Y += DownMovement - UpMovement;


            //X = pathToPlayer[currentIndex - 1].X;
            //Y = pathToPlayer[currentIndex - 1].Y;
            //if (currentIndex < pathToPlayer.Count - 1)
            //{
            //    currentIndex++;
            //
            //}
            //else 
            //{
            //    currentIndex = 1;
            //    if (pathToPlayer[currentIndex - 1] != new Point(X, Y))
            //        pathToPlayer = FindPathToPlayer();
            //}
        }

        public string FindNextDirection()
        {
            //pathToPlayer = FindPathToPlayer();
            
            if (currentIndex < pathToPlayer.Count - 1)
            {
                if (X == pathToPlayer[currentIndex].X && Y == pathToPlayer[currentIndex].Y)
                    currentIndex++;
                var newX = Math.Sign(pathToPlayer[currentIndex].X - pathToPlayer[currentIndex - 1].X);
                var newY = Math.Sign(pathToPlayer[currentIndex].Y - pathToPlayer[currentIndex - 1].Y);
                return Game.DirectionForMovement[new Point(newX, newY)];
            }
            else
                UpMovement = DownMovement = LeftMovement = RightMovement = 0;
            return Direction;
        }

        public void Move(string direction)
        {
            Direction = direction;

            switch (Direction)
            {
                case "Up":
                    UpMovement = MoveSpeed;
                    DownMovement = LeftMovement = RightMovement = 0;
                    //Y -= MoveSpeed;
                    break;
                case "Down":
                    DownMovement = MoveSpeed;
                    UpMovement = LeftMovement = RightMovement = 0;
                    //Y += MoveSpeed;
                    break;
                case "Left":
                    LeftMovement = MoveSpeed;
                    UpMovement = DownMovement = RightMovement = 0;
                    //X -= MoveSpeed;
                    break;
                case "Right":
                    RightMovement = MoveSpeed;
                    UpMovement = DownMovement = LeftMovement = 0;
                    //X += MoveSpeed;
                    break;
            }
        }
        public void MoveBack()
        {
            X -= MoveSpeed;
            Y -= MoveSpeed;
            //X -= RightMovement - LeftMovement;
            //Y -= DownMovement - UpMovement;
        }

        private List<Point> FindPathToPlayer()
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>();
            var prev = new List<Point>();

            queue.Enqueue(new SinglyLinkedList<Point>(new Point(X, Y)));
            visited.Add(new Point(X, Y));

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                var neighbours = new[]
                {
                        new Point(current.Value.X + 10, current.Value.Y),
                        new Point(current.Value.X - 10, current.Value.Y),
                        new Point(current.Value.X, current.Value.Y + 10),
                        new Point(current.Value.X, current.Value.Y - 10)
                }.Where(n => !visited.Contains(n));

                foreach (var neighbour in neighbours)
                {
                    var next = new SinglyLinkedList<Point>(neighbour, current);
                    visited.Add(neighbour);
                    queue.Enqueue(next);

                    if (entities.Where(e => !(e is Player)).Any(en => neighbour.Equals(new Point(en.X, en.Y))))
                        continue;
                    if (neighbour.Equals(new Point(player.X, player.Y)))
                    {
                        var finalPath = next.ToList();
                        finalPath.Reverse();
                        return finalPath;
                    }
                }
            }
            return null;
        }

    }
}
