using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;

namespace TerraAlert2.Tiles
{
    internal class AlliedBarracksOnWallTile : BaseBuildingTile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NewTile(9, 8, new Point16(4, 7), 16, new int[] { 16, 16, 16, 16, 16, 16, 16, 16 });
            CreateMapEntry("TerraAlert2_SovietBarracks", new System.Collections.Generic.Dictionary<GameCulture, string> {
                { GameCulture.English, "TerraAlert2: AlliedBarracks" },
                { GameCulture.Chinese, "泰拉警戒2：盟军兵营" }
            }, new Color(255, 0, 0));
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            base.KillMultiTile(i, j, frameX, frameY);
            Item.NewItem(i * 16, j * 16, 72, 64, ModContent.ItemType<AlliedBarracksOnWallItem>());
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            TerraAlert2Player modPlayer = Main.LocalPlayer.GetModPlayer<TerraAlert2Player>();
            int buffType = ModContent.BuffType<Buffs.AlliedBarracks>();
            if (closer)
            {
                modPlayer.nearBarracks = true;
                Main.LocalPlayer.AddBuff(buffType, 60);
            }
            else
            {
                modPlayer.nearBarracks = false;
                // if (Main.LocalPlayer.HasBuff(buffType)) Main.LocalPlayer.DelBuff(buffType);  // Crashes the game
            }
        }
    }
    internal class AlliedBarracksOnWallItem : BaseBuildingItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.width = 144;
            item.height = 128;
            item.createTile = ModContent.TileType<AlliedBarracksOnWallTile>();
            item.rare = ItemRarityID.Pink;
            item.value = 200000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 16);
            recipe.AddIngredient(ItemID.StoneBlock, 18);
            recipe.AddIngredient(ItemID.IronBar, 24);
            // recipe.AddIngredient(ItemID.BlueBanner, 1);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
