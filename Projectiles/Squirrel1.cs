using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    internal class Squirrel1 : ModProjectile
    {
        public int Counter = 1;

        public override string Texture => "FargowiltasSouls/Items/Weapons/TophatSquirrel";

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 19;
            projectile.height = 19;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.thrown = true;
            projectile.scale = 1f;
            projectile.timeLeft = 150;
        }

        public override void AI()
        {
            projectile.rotation += 0.2f;

            if (Counter >= 75) projectile.scale += .1f;

            Counter++;
        }

        public override void Kill(int timeLeft)
        {
            const int proj2 = 88; //laser rifle

            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 5f, proj2, (int) (projectile.damage * 0.5f), 2 /*kb*/, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, 0f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -5f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, 0f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, 4f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, -4f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, -4f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, 4f, proj2, (int) (projectile.damage * 0.5f), 2, Main.myPlayer);

            for (int i = 0; i < 40; i++)
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-1000, 1000), projectile.Center.Y - 1000, 0f, 0f + Main.rand.Next(4, 10), proj2,
                    (int) (projectile.damage * 0.5f), 0f, Main.myPlayer);
        }
    }
}