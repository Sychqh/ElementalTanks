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
        private List<Point> pathToPlayer;
        private int currentIndex = 1;

        public Enemy(int x, int y, ElementType element, Player player)
        {
            this.player = player;
            X = x;
            Y = y;
            Element = element;
            Health = 100;
            MoveSpeed = 20;
            Direction = "Up";
            pathToPlayer = FindPathToPlayer();
        }

        public Point GunPosition(Image sprite)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            X = pathToPlayer[currentIndex - 1].X;
            Y = pathToPlayer[currentIndex - 1].Y;
            if (currentIndex < pathToPlayer.Count - 1)
            {
                currentIndex++;

            }
            else 
            {
                currentIndex = 1;
                if (pathToPlayer[currentIndex - 1] != new Point(X, Y))
                    pathToPlayer = FindPathToPlayer();
            }
        }

        public void FindNextDirection(List<Point> path)
        {

        }

        public void Move(string direction)
        {
            Direction = direction;

            switch (Direction)
            {
                case "Up":
                    UpMovement = MoveSpeed;
                    DownMovement = LeftMovement = RightMovement = 0;
                    break;
                case "Down":
                    DownMovement = MoveSpeed;
                    UpMovement = LeftMovement = RightMovement = 0;
                    break;
                case "Left":
                    LeftMovement = MoveSpeed;
                    UpMovement = DownMovement = RightMovement = 0;
                    break;
                case "Right":
                    RightMovement = MoveSpeed;
                    UpMovement = DownMovement = LeftMovement = 0;
                    break;
            }
        }
        public void MoveBack()
        {
            
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
                        new Point(current.Value.X + 100, current.Value.Y),
                        new Point(current.Value.X - 100, current.Value.Y),
                        new Point(current.Value.X, current.Value.Y + 100),
                        new Point(current.Value.X, current.Value.Y - 100)
                }.Where(n => !visited.Contains(n));

                foreach (var neighbour in neighbours)
                {
                    var next = new SinglyLinkedList<Point>(neighbour, current);
                    visited.Add(neighbour);
                    queue.Enqueue(next);

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
