using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.Utilities;

namespace TerraAlert2.Projectiles
{
    class Conscript : ModProjectile
    {
        private int shootCD = 0;
        private int jumpCD = 0;
        private bool moving = false;
        private bool attacking = false;
        private bool leftToFar = false;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 13;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targeting
        }
        public override void SetDefaults()
        {
            // projectile.CloneDefaults(ProjectileID.UFOMinion);
            // aiType = ProjectileID.UFOMinion;
            projectile.netImportant = true;
            projectile.width = 24;
            projectile.height = 31;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 0;
            // projectile.penetrate = -1;
            // projectile.timeLeft = 18000;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.dead && player.GetModPlayer<TerraAlert2Player>().nearBarracks)
            {
                projectile.timeLeft = 60;
            }
            if (moving)
            {
                if (projectile.frameCounter >= 1 && projectile.frameCounter <= 6)
                {
                    projectile.frameCounter++;
                    projectile.frame++;
                }
                else
                {
                    projectile.frameCounter = 1;
                    projectile.frame = 1;
                }
            }
            else if (attacking)
            {
                if (projectile.frameCounter >= 8 && projectile.frameCounter <= 12)
                {
                    projectile.frameCounter++;
                    projectile.frame++;
                }
                else
                {
                    projectile.frameCounter = 8;
                    projectile.frame = 8;
                }
            }
            else
            {
                projectile.frameCounter = 0;
                projectile.frame = 0;
            }
            Vector2 move = Vector2.Zero;
            float distance = 800f;
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
            projectile.direction = player.direction == 1 ? -1 : 1;
            projectile.spriteDirection = projectile.direction;
            Vector2 positionToGo = (player.Center - projectile.Center) + new Vector2((projectile.direction == 1 ? -16f : 16f) * projectile.identity, 0f);

            moving = projectile.velocity.X > 0.01f;
            attacking = target;
            leftToFar = positionToGo.LengthSquared() > 90000f;

            projectile.tileCollide = !leftToFar;
            if (leftToFar)
            {
                projectile.Center = player.Center;
            }
            else
            {
                projectile.velocity.X = positionToGo.X / 24f;
                if (projectile.velocity.Y < 16f) projectile.velocity.Y += 0.18f;
            }

            if (positionToGo.LengthSquared() > 3200f && jumpCD <= 0)
            {
                projectile.velocity.Y -= 6f;
                jumpCD = 90;
            }
            jumpCD--;

            if (shootCD > 0)
            {
                shootCD--;
                return;
            }
            if (attacking)
            {
                WeightedRandom<string> attackSounds = new WeightedRandom<string>();
                attackSounds.Add("iconatta");
                attackSounds.Add("iconattb");
                attackSounds.Add("iconattc");
                attackSounds.Add("iconattd");
                attackSounds.Add("iconatte");
                Projectile.NewProjectile(projectile.Center, move, ProjectileID.Bullet, 8, 0.1f, projectile.owner);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/" + attackSounds.Get()));
            }
            shootCD = Main.rand.Next(50, 60);
            // base.AI();
        }
    }
}
