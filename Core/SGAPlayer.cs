using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Idglibrary;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Shaders;
using Terraria.Utilities;
using SGAmod.Buffs.Debuffs;

namespace SGAmod
{
	public partial class SGAPlayer : ModPlayer
	{

		// Apocalyptical related
		public double[] apocalypticalChance = { 0, 0, 0, 0 };
		public float apocalypticalStrength = 1f;
		public float lifestealentropy = 0f;

		public bool dragonFriend = false;

		public List<int> ExpertisePointsFromBosses;
		public List<string> ExpertisePointsFromBossesModded;
		public List<int> ExpertisePointsFromBossesPoints;
		public long ExpertiseCollected = 0;
		public long ExpertiseCollectedTotal = 0;

		// Buffs
		public bool acidBurn = false;

		// Accessories
		public byte cobwebRepellent = 0;

		// World
		public bool Drakenshopunlock = false;

		public override void Initialize()
		{
			ExpertisePointsFromBosses = new();
			ExpertisePointsFromBossesModded = new();
			ExpertisePointsFromBossesPoints = new();
		}

		public override void ResetEffects()
		{
			Player.breathMax = 200;
			acidBurn = false;

			cobwebRepellent = 0;
			Drakenshopunlock = false;
		}

		public override void CopyClientState(ModPlayer targetCopy)
		{
			SGAPlayer sgaplayer = targetCopy as SGAPlayer;
			sgaplayer.ExpertiseCollected = ExpertiseCollected;
			sgaplayer.ExpertiseCollectedTotal = ExpertiseCollectedTotal;
			sgaplayer.dragonFriend = dragonFriend;
			sgaplayer.Drakenshopunlock = Drakenshopunlock;
		}

		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			bool mismatch = false;
			SGAPlayer sgaplayer = clientPlayer as SGAPlayer;

			if (sgaplayer.ExpertiseCollected != ExpertiseCollected || 
				sgaplayer.ExpertiseCollectedTotal != ExpertiseCollectedTotal || 
				sgaplayer.dragonFriend != dragonFriend ||
				sgaplayer.Drakenshopunlock != Drakenshopunlock
				)
			{
				mismatch = true;
			}

			if (mismatch)
			{
				SendClientChangesPacket();
			}
		}


		private void SendClientChangesPacket()
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{

			}
		}

		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			return new[] {
				new Item(ModContent.ItemType<Items.Consumables.GrabBags.StartingBag>())
			};
		}

		public override void SaveData(TagCompound tag)
		{
			tag["ZZZExpertiseCollectedZZZ"] = ExpertiseCollected;
			tag["ZZZExpertiseCollectedTotalZZZ"] = ExpertiseCollectedTotal;
			tag["dragonFriend"] = dragonFriend;
			tag["Drakenshopunlock"] = Drakenshopunlock;

			SaveExpertise(ref tag);
		}

		public override void LoadData(TagCompound tag)
		{
			dragonFriend = tag.ContainsKey("dragonFriend");
			Drakenshopunlock = tag.ContainsKey("Drakenshopunlock");

			LoadExpertise(tag);
		}

		public override void UpdateBadLifeRegen()
		{
			if (acidBurn)
			{
				Player.lifeRegen -= 15 + (int)(Player.statDefense * 0.90f);
				Player.statDefense -= 5;
			}
		}
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (acidBurn)
			{
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.AcidDust>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1f);
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					drawInfo.DustCache.Add(dust);
				}
				r *= 0.1f;
				g *= 0.7f;
				b *= 0.1f;
				fullBright = true;
			}
		}
	}
}