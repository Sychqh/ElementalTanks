using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace ElementalTanks
{
    class Enemy : ITank
    {
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int UpMovement { get; set; }
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private readonly Player player;
        private readonly List<IEntity> entities;
        private List<Point> pathToPlayer;
        private int currentIndex = 1;

        public Enemy(int x, int y, IElement element, List<IEntity> entities, int moveSpeed)
        {
            this.entities = entities;
            player = entities[0] as Player;
            X = x;
            Y = y;
            Width = Height = 77;
            Element = element;
            Health = 100;
            MoveSpeed = moveSpeed;
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
            //if (X == player.X && Y == player.Y)
            //    UpMovement = DownMovement = LeftMovement = RightMovement = 0;
            Move(Direction);
            X += RightMovement - LeftMovement;
            Y += DownMovement - UpMovement;
            //if (X > player.X)
            //{
            //    Direction = "Left";
            //    X -= MoveSpeed;
            //}
            //if (X < player.X)
            //{
            //    Direction = "Right";
            //    X += MoveSpeed;
            //}
            //if (Y > player.Y)
            //{
            //    Direction = "Up";
            //    Y -= MoveSpeed;
            //}
            //if (Y < player.Y)
            //{
            //    Direction = "Down";
            //    Y += MoveSpeed;
            //}
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
            if (currentIndex == pathToPlayer.Count - 1)
            {
                currentIndex = 1;
                pathToPlayer = FindPathToPlayer();
                //var func = new Func<List<Point>>(FindPathToPlayer);
                //var result = func.BeginInvoke(null, null);
                //pathToPlayer = func.EndInvoke(result);
                //var thread = new Thread(() => FindPathToPlayer());
                //thread.Start();
            }
            else 
            {
                if (X == pathToPlayer[currentIndex].X && Y == pathToPlayer[currentIndex].Y)
                    currentIndex++;
                var newX = Math.Sign(pathToPlayer[currentIndex].X - pathToPlayer[currentIndex - 1].X);
                var newY = Math.Sign(pathToPlayer[currentIndex].Y - pathToPlayer[currentIndex - 1].Y);
                return Game.DirectionForMovement[new Point(newX, newY)];
            }

            return "None";
        }

        public void Move(string direction)
        {
            //Direction = direction;
            if (X > player.X)
            {
                Direction = "Left";
                LeftMovement = MoveSpeed;
                UpMovement = DownMovement = RightMovement = 0;
            }
            if (X < player.X)
            {
                Direction = "Right";
                RightMovement = MoveSpeed;
                UpMovement = DownMovement = LeftMovement = 0;
            }
            if (Y > player.Y)
            {
                Direction = "Up";
                UpMovement = MoveSpeed;
                DownMovement = LeftMovement = RightMovement = 0;
            }
            if (Y < player.Y)
            {
                Direction = "Down";
                DownMovement = MoveSpeed;
                UpMovement = LeftMovement = RightMovement = 0;
            }
            //switch (direction)
            //{
            //    case "Up":
            //        UpMovement = MoveSpeed;
            //        DownMovement = LeftMovement = RightMovement = 0;
            //        Direction = direction;
            //        //Y -= MoveSpeed;
            //        break;
            //    case "Down":
            //        DownMovement = MoveSpeed;
            //        UpMovement = LeftMovement = RightMovement = 0;
            //        Direction = direction;
            //        //Y += MoveSpeed;
            //        break;
            //    case "Left":
            //        LeftMovement = MoveSpeed;
            //        UpMovement = DownMovement = RightMovement = 0;
            //        Direction = direction;
            //            //X -= MoveSpeed;
            //        break;
            //    case "Right":
            //        RightMovement = MoveSpeed;
            //        UpMovement = DownMovement = LeftMovement = 0;
            //        Direction = direction;
            //        //X += MoveSpeed;
            //        break;
            //    case "None":
            //        UpMovement = DownMovement = LeftMovement = RightMovement = 0;
            //        break;
            //}
        }
        public void MoveBack()
        {
            //X -= MoveSpeed;
            //Y -= MoveSpeed;
            X -= RightMovement - LeftMovement;
            Y -= DownMovement - UpMovement;
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
                    new Point(current.Value.X + 50, current.Value.Y),
                    new Point(current.Value.X - 50, current.Value.Y),
                    new Point(current.Value.X, current.Value.Y + 50),
                    new Point(current.Value.X, current.Value.Y - 50)
                }.Where(n => !visited.Contains(n));//.Where(n => n.X > 0 && n.Y > 0 && n.X < 800 && n.Y < 600);

                foreach (var neighbour in neighbours)
                {
                    var next = new SinglyLinkedList<Point>(neighbour, current);
                    visited.Add(neighbour);
                    queue.Enqueue(next);

                    //if (entities.Where(e => !(e is Player || e is Enemy)).Any(en => neighbour.Equals(new Point(en.X, en.Y))))
                    //    continue;
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
