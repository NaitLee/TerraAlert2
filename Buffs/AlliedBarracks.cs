using Terraria;
using Terraria.ModLoader;

namespace TerraAlert2.Buffs
{
    class AlliedBarracks : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 3;
        }
    }
}
