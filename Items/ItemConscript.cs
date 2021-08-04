using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.Utilities;

namespace TerraAlert2.Items
{
    class ItemConscript : ModItem
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
            item.shoot = ModContent.ProjectileType<Projectiles.Conscript>();
            // item.buffType = ModContent.BuffType<Buffs.Rocketeer>();
        }
        public override bool CanUseItem(Player player)
        {
            bool canUse = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Conscript>()] <= maxToSummon && player.GetModPlayer<TerraAlert2Player>().nearBarracks;
            if (canUse)
            {
                WeightedRandom<string> sounds = new WeightedRandom<string>();
                sounds.Add("iconata");
                sounds.Add("iconatb");
                sounds.Add("iconatc");
                sounds.Add("iconatd");
                sounds.Add("iconmoa");
                sounds.Add("iconmob");
                sounds.Add("iconmoc");
                sounds.Add("iconmod");
                sounds.Add("iconsea");
                sounds.Add("iconseb");
                sounds.Add("iconsec");
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/" + sounds.Get()).WithVolume(0.9f).WithPitchVariance(0.0f));
            }
            return canUse;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 24);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
