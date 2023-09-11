using LegendMod.Buffs;
using LegendMod.Common.AuxiliaryMeans;
using LegendMod.Common.DrawTools;
using LegendMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace LegendMod.Items.Weapons
{
    public class Impasse : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Melee;
            Item.width = 1;
            Item.height = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Terraria.Item.buyPrice(0, 54, 67, 0);
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<DarkRedCut>();
            Item.shootSpeed = 7f;
        }

        public override string Texture => "LegendMod/Asset/Items/Impasse2";

        public override ModItem NewInstance(Item entity)
        {
            return base.NewInstance(entity);
        }

        Player HoldPlayer = null;
        Vector2 ToMus = Vector2.Zero;
        public override void HoldItem(Player player)
        {
            HoldPlayer = player;

            ToMus = Main.MouseWorld - player.Center;

            //这个为内部使用的方向向量，可以省略使用ToMus替代
            Vector2 ToMous = Main.MouseWorld - player.Center;

            //纠正玩家的方向，让玩家朝向跟随武器
            if (ToMous.X > 0)
            {
                player.ChangeDir(1);
            }
            else
            {
                player.ChangeDir(-1);
            }

            player.AddBuff(ModContent.BuffType<ImpasseBuff>(), 1);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D mainValue = DrawUtils.GetT2DValue("LegendMod/Asset/Items/Impasse");

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            if (HoldPlayer != null)
            {
                if (HoldPlayer.HeldItem.type == Item.type)
                {
                    SpriteEffects spriteEffects = HoldPlayer.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    float Rot = (float)(Math.Abs(HoldPlayer.velocity.X) > 0 && HoldPlayer.velocity.Y == 0 ? MathHelper.ToRadians(Math.Abs((float)Math.Sin(Main.fpsCount * 0.2f) * 10f)) : 0);
                    if (PlayerInput.Triggers.Current.MouseLeft)
                    {
                        mainValue = DrawUtils.GetT2DValue("LegendMod/Asset/Items/Impasse3");
                        Rot = 0;
                    }

                    Main.EntitySpriteDraw(
                    mainValue,
                    DrawUtils.WDEpos(HoldPlayer.Center),
                    DrawUtils.GetRec(mainValue),
                    drawColor,
                    Rot,
                    DrawUtils.GetOrig(mainValue),
                    0.75f,
                    spriteEffects,
                    0
                    );
                }
            }

            
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, null, Main.UIScaleMatrix);

            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}
