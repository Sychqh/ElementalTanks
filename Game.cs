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

        public bool isGoing;
        private int score;

        public static readonly Dictionary<string, RotateFlipType> SpriteRotations = new Dictionary<string, RotateFlipType>
        {
            ["Up"] = RotateFlipType.RotateNoneFlipNone,
            ["Down"] = RotateFlipType.RotateNoneFlipY,
            ["Left"] = RotateFlipType.Rotate90FlipX,
            ["Right"] = RotateFlipType.Rotate90FlipNone
        };

        public Game()
        {
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

            if (isGoing)
            {
                player.X += Tank.MovementForDirection[player.Direction].Item1 * player.MoveSpeed;
                player.Y += Tank.MovementForDirection[player.Direction].Item2 * player.MoveSpeed;
            }

            //foreach (var bullet in entities.Where(ent => ent is Bullet))
            //{
            //    foreach (var enemy in entities.Where(ent => ent is Enemy))
            //    {
            //        //if (IsTankHit(bullet as Bullet, enemy as Enemy))
            //            deleted.Add(enemy);
            //    }
            //}

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

        //public bool IsTankHit(Bullet bullet, Tank tank)
        //{
        //    return (bullet.X < tank.X + tank.Sprite.Width) &&
        //    (tank.X < (bullet.X + bullet.Sprite.Width)) &&
        //    (bullet.Y < tank.Y + tank.Sprite.Height) &&
        //    (tank.Y < bullet.Y + bullet.Sprite.Height);
        //}
    }
}
