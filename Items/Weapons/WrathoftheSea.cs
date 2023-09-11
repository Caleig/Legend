using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using LegendMod.misc;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace LegendMod.Items.Weapons
{
    public class WrathoftheSea:ModItem
    {
        bool useammo = true;
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 118;
            Item.height = 18;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = 5;
            Item.knockBack = 1;
            Item.value = 250000;
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 60f;

            

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 3; // The humber of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(ItemID.ChainGun, 1);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.ShroomiteBar, 5);
            recipe.AddIngredient(ModContent.ItemType<SharpTooth>(), 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(3) != 1)
            {
                useammo = false;
            }
            else
            {
                useammo = true;
            }
            return useammo;
        }
    }
}