using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    class Model
    {
        public Player CurrentPlayer;
        public List<Enemy> OnScreenEnemies;
        int currentGameLevel;
        static int EnemyCoolDown = 0;

        public Model()
        {
            CurrentPlayer = new Player();
            OnScreenEnemies = new List<Enemy>();
            currentGameLevel = 5;
            CurrentPlayer.Initialize();
        }

        public void UpdateColision()
        {
            foreach (var enemy in OnScreenEnemies)
            {
                foreach (var proj in CurrentPlayer.playerProjectiles)
                {
                    if (proj.IsActive && proj.CollidesWith(enemy))
                    {
                        enemy.GetDamaged(proj.projDamage);
                        proj.IsActive = false;
                    }
                }
            }

            foreach (var enemy in OnScreenEnemies)
            {
                if (enemy.CollidesWith(CurrentPlayer))
                {
                    CurrentPlayer.DamagePlayer(enemy.enemyDamage);
                    enemy.enemyHealth = 0;
                }
            }
        }

        public void UpdateEnemies()
        {
            EnemyCoolDown += currentGameLevel;
            if ( EnemyCoolDown >= 500 )
            {
                Enemy newEnemy = new Enemy();
                newEnemy.Initialize();
                OnScreenEnemies.Add(newEnemy);
                Game.Instance.GameView.AddEnemyAnimation();
            }
            EnemyCoolDown %= 500;
            foreach (Enemy item in OnScreenEnemies)
            {
                item.UpdatePozition();
            }
            OnScreenEnemies.RemoveAll((item) => (item.enemyPosition.X < -100 || !item.IsActive));
        }

        public void IncrementLevel()
        {
            currentGameLevel++;
        }

        public void PlayerMoveUp()
        {
            CurrentPlayer.MovePlayerUp();
        }

        public void PlayerMoveDown()
        {
            CurrentPlayer.MovePlayerDown();
        }

        public void PlayerMoveLeft()
        {
            CurrentPlayer.MovePlayerLeft();
        }

        public void PlayerMoveRight()
        {
            CurrentPlayer.MovePlayerRight();
        }

        public void PlayerDamage(int damageValue)
        {
            CurrentPlayer.DamagePlayer(damageValue);
        }
    }
}