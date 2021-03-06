using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.Utilities;

namespace TerraAlert2.Items
{
    class ItemRocketeer : ModItem
    {
        private static readonly int maxToSummon = 16;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 48;
            item.rare = ItemRarityID.LightRed;
            item.summon = true;
            item.damage = 16;
            item.knockBack = 0.1f;
            item.useTime = 30;
            item.useAnimation = 30;
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.IronBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
