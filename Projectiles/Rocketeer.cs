using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace TerraAlert2.Projectiles
{
    class Rocketeer : ModProjectile
    {
        private int shootCD = 0;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 12;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targeting
        }
        public override void SetDefaults()
        {
            // projectile.CloneDefaults(ProjectileID.UFOMinion);
            aiType = ProjectileID.UFOMinion;
            projectile.netImportant = true;
            projectile.width = 94;
            projectile.height = 120;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1 / 8;
            // projectile.penetrate = -1;
            // projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.dead && player.GetModPlayer<TerraAlert2Player>().nearBarracks)
            {
                projectile.timeLeft = 60;
            }
            projectile.frame++;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 12)
            {
                projectile.frameCounter = 0;
                projectile.frame = 0;
            }
            projectile.direction = player.direction == 1 ? -1 : 1;
            projectile.spriteDirection = projectile.direction;
            projectile.velocity = (player.Center - projectile.Center + new Vector2((projectile.direction == 1 ? 32f : -32f) * (projectile.identity % 8), -32f * (projectile.identity / 8) * Main.rand.NextFloat(0.8f, 1.2f))) / 5f;
            if (shootCD > 0)
            {
                shootCD--;
                return;
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                Projectile.NewProjectile(projectile.Center, move * 3, ProjectileID.Bullet, 16, 0.1f, projectile.owner);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/irocatta"));
            }
            shootCD = Main.rand.Next(50, 60);
            // base.AI();
        }
    }
}
