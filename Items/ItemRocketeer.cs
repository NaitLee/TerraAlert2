using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.Utilities;

namespace TerraAlert2.Items
{
    class ItemRocketeer : ModItem
    {
        private static readonly int maxToSummon = 24;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 48;
            item.rare = ItemRarityID.LightRed;
            item.summon = true;
            item.damage = 16;
            item.useTime = 17;
            item.useAnimation = 17;
            item.reuseDelay = 0;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.value = 50000;
            item.shoot = ModContent.ProjectileType<Projectiles.Rocketeer>();
            // item.buffType = ModContent.BuffType<Buffs.Rocketeer>();
        }
        public override bool CanUseItem(Player player)
        {
            bool canUse = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Rocketeer>()] <= maxToSummon && player.GetModPlayer<TerraAlert2Player>().nearBarracks;
            if (canUse)
            {
                WeightedRandom<string> sounds = new WeightedRandom<string>();
                sounds.Add("irocata");
                sounds.Add("irocatb");
                sounds.Add("irocatc");
                sounds.Add("irocatd");
                sounds.Add("irocate");
                sounds.Add("irocmoa");
                sounds.Add("irocmob");
                sounds.Add("irocmoc");
                sounds.Add("irocmod");
                sounds.Add("irocmoe");
                sounds.Add("irocmof");
                sounds.Add("irocsea");
                sounds.Add("irocseb");
                sounds.Add("irocsec");
                sounds.Add("irocsed");
                sounds.Add("irocsee");
                sounds.Add("irocsef");
                sounds.Add("irocseg");
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/" + sounds.Get()).WithVolume(0.9f).WithPitchVariance(0.0f));
            }
            return canUse;
        }
    }
}
