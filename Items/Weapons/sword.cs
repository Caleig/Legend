using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using System;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace LegendMod.Items.Weapons
{
    public class sword : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            // DisplayName.SetDefault("残念");
            /* Tooltip.SetDefault("以切玉断金，如削土木矣\n"+
                                    "右键向鼠标方向冲刺，冲刺过程中附带伤害"); */
        }
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.width = 0;
            Item.height = 0;
            Item.noMelee = true;
            Item.useTime = 50;
            Item.shootSpeed = 15f;
            Item.useAnimation = 50;
            Item.useStyle = 1;
            Item.reuseDelay = 0;
            Item.noUseGraphic = true;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.swords>();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Katana, 1)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();                           
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<vertex>();
                 Item.useStyle = ItemUseStyleID.HiddenAnimation;
            }
            else
            {
                Item.useStyle = 1;
                Item.shoot = ModContent.ProjectileType<Projectiles.swords>();
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}