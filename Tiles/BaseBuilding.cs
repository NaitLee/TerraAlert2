using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;
using System.Collections.Generic;

namespace TerraAlert2.Tiles
{
    internal abstract class BaseBuildingTile : ModTile
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
        }
        public void NewTile(int width, int height, Point16 origin, int cWidth, int[] cHeight)
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.Width = width;
            TileObjectData.newTile.Height = height;
            TileObjectData.newTile.Origin = origin;
            TileObjectData.newTile.CoordinateWidth = cWidth;
            TileObjectData.newTile.CoordinateHeights = cHeight;
            TileObjectData.addTile(Type);
        }
        public void CreateMapEntry(string setDefault, Dictionary<GameCulture, string> translations, Color color)
        {
            ModTranslation name = CreateMapEntryName();
            name.SetDefault(setDefault);
            foreach (KeyValuePair<GameCulture, string> pair in translations)
            {
                name.AddTranslation(pair.Key, pair.Value);
            }
            AddMapEntry(color, name);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            foreach (Player player in Main.player)
            {
                if (player.active) player.GetModPlayer<TerraAlert2Player>().nearBarracks = false;
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/uselbuil").WithVolume(1.0f).WithPitchVariance(0.0f));
        }
    }
    internal abstract class BaseBuildingItem : ModItem
    {
        public override void SetDefaults()
        {
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
