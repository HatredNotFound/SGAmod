using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SGAmod.Tiles.Bars
{
    public class VibraniumCrystalTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 200;
            TileID.Sets.Ore[Type] = true;
            MinPick = 250;
            HitSound = SoundID.Item50;
            DustType = -1;
            ItemDrop = ModContent.ItemType<Items.Materials.Bars.VibraniumCrystal>();
            MineResist = 2.50f;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Vibranium Crystal");
            AddMapEntry(new Color(255, 60, 60), name);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            int typr = 182;
            for (int xi = 0; xi < 20; xi += 1)
            {
                int dust = Dust.NewDust(new Vector2(i, j) * 16, 16, 16, typr);
                Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
                Vector2 normvel = Main.rand.NextVector2Circular(6f, 6f);
                Color Rainbow = Color.White;
                Main.dust[dust].color = Rainbow;
                Main.dust[dust].scale = 0.75f;
                Main.dust[dust].velocity = ((randomcircle / 1f) + (-normvel));
                Main.dust[dust].noGravity = true;
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.75f;
            g = 0.25f;
            b = 0.25f;
        }
    }

    public class PrismalTile : ModTile
    {
        public override void SetStaticDefaults()
        {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 200;
            TileID.Sets.Ore[Type] = true;
            MinPick = 120;
            HitSound = SoundID.Tink;
            ItemDrop = ModContent.ItemType<Items.Materials.Bars.PrismalOre>();
            MineResist = 1.25f;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Prismal Ore");
            AddMapEntry(new Color(70, 0, 40), name);
        }
      
        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)  
        {
            r = 0.5f;
            g = 0.5f;
            b = 0.5f;
        }
    }
}