using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;

namespace TerraAlert2.Tiles
{
    internal class AlliedBarracksOnWallTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = false;
            // TileID.Sets.FramesOnKillWall[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.Width = 9;
            TileObjectData.newTile.Height = 8;
            TileObjectData.newTile.Origin = new Point16(4, 7);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("TerraAlert2_AlliedBarracks");
            name.AddTranslation(GameCulture.English, "TerraAlert2: AlliedBarracks");
            name.AddTranslation(GameCulture.Chinese, "泰拉警戒2：盟军兵营");
            AddMapEntry(new Color(255, 0, 0), name);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            foreach (Player player in Main.player)
            {
                if (player.active) player.GetModPlayer<TerraAlert2Player>().nearBarracks = false;
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/uselbuil").WithVolume(1.0f).WithPitchVariance(0.0f));
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
    internal class AlliedBarracksOnWallItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 144;
            item.height = 128;
            item.createTile = ModContent.TileType<AlliedBarracksOnWallTile>();
            item.rare = ItemRarityID.Pink;
            item.value = 200000;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/uplace").WithVolume(1.0f).WithPitchVariance(0.0f);
            item.consumable = true;
            item.placeStyle = 0;
            item.tileBoost = 40;
        }
    }
}
