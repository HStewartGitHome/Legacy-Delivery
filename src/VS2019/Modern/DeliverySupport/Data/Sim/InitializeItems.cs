using DeliverySupport.Models;
using System.Collections.Generic;

namespace DeliverySupport.Data.Sim
{
    public static class InitializeItems
    {
        public static int NextID { get; set; }
        public static List<IItemModel> CreateDefaultItems()
        {
            List<IItemModel> Items = new List<IItemModel>();

            IItemModel Item = null;
            int Id = 1;

            // The following is created form XMLDataTest to with seeditems.txt file created
            // source that is copy and included in this code.

            // <<<<< Start of Included Source >>>>>>

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1001;
            Item.Description = "Pita Chips, Garlic Herb, 9 oz";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "GarlicHerbPitaChips.png";
            Item.CategoryNum = 13;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1023;
            Item.Description = "Old Fashion Rolled Oats";
            Item.Amount = 5.58M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "RolledOats.png";
            Item.CategoryNum = 12;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1022;
            Item.Description = "Food for Life Bread";
            Item.Amount = 4.98M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "FoodForLifeBread.png";
            Item.CategoryNum = 8;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1021;
            Item.Description = "Organic Pink Lady Apple, One Medium";
            Item.Amount = 1.45M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "PinkLadyApple.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1008;
            Item.Description = "365 Everyday Value, Organic Brown Rice Whole Grain";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "BrownRice.png";
            Item.CategoryNum = 7;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1016;
            Item.Description = "Cascadian Farm Organic Mirepoix (Onions, Celery, C";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Mirepoix.png";
            Item.CategoryNum = 6;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1015;
            Item.Description = "Natural Vitality Calm, Magnesium Citrate Supplemen";
            Item.Amount = 23.61M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "VitalityCalm.png";
            Item.CategoryNum = 15;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1014;
            Item.Description = "Natural Vitality Calm Sleep Magnesium Anti Stress,";
            Item.Amount = 32.49M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "VitalityCalmSleep.png";
            Item.CategoryNum = 15;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1013;
            Item.Description = "Dole Bananas, 2 lb Bag";
            Item.Amount = 0.98M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "DoleBananas.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1012;
            Item.Description = "Organic Riced Cauliflower, 12 oz, (Frozen)";
            Item.Amount = 1.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "RiceCauliflower.png";
            Item.CategoryNum = 7;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1011;
            Item.Description = "365 Everyday Value, Organic Baby Carrots, 1 lb";
            Item.Amount = 1.69M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "BabyCarrots.png";
            Item.CategoryNum = 3;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1024;
            Item.Description = "Whole Strawberries, Frozen, 10Oz";
            Item.Amount = 2.47M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "WholeStrawberries.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1010;
            Item.Description = "Organic White Quinoa, 12 oz, (Frozen)";
            Item.Amount = 3.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "WhiteQuinoa.png";
            Item.CategoryNum = 6;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1007;
            Item.Description = "Chopped Kale, 16 oz";
            Item.Amount = 3.49M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ChoppedKale.png";
            Item.CategoryNum = 3;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1020;
            Item.Description = "GT's Kombucha - Grape Chia 16oz";
            Item.Amount = 3.11M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "GrapeChia.png";
            Item.CategoryNum = 14;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1006;
            Item.Description = "365 Granny Smith Apple Slices, Freeze Dried, 1 oz";
            Item.Amount = 3.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "GrannySmithAppleSlices.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1005;
            Item.Description = "Organic Brocoli, 16 oz (Frozen)";
            Item.Amount = 1.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "FrozenBrocoliFlorets.png";
            Item.CategoryNum = 6;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1004;
            Item.Description = "Organic Blue Curled Kale, 16 oz (Frozen)";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "BlueCurledKale.png";
            Item.CategoryNum = 6;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1003;
            Item.Description = "Organic Wild Blueberries, 10 oz (Frozen)";
            Item.Amount = 3.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "WildBlueberries.png";
            Item.CategoryNum = 5;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1002;
            Item.Description = "365 Everyday Value, Pita Chips, Sea Salt, 9 oz";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "SeaSaltPitaChips.png";
            Item.CategoryNum = 13;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1017;
            Item.Description = "Organic White Rice Thai Jasmine, 20 oz, (Frozen)";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "whiterice.png";
            Item.CategoryNum = 7;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1018;
            Item.Description = "GT's Kombucha, Gingerberry 16 Fl Oz";
            Item.Amount = 2.97M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Gingerberry.png";
            Item.CategoryNum = 14;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1019;
            Item.Description = "Finish All in 1 Gelpacs Orange, 32ct, Dishwasher D";
            Item.Amount = 5.49M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "FinishAll.png";
            Item.CategoryNum = 16;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1009;
            Item.Description = "Broccoli Florets, 12 oz";
            Item.Amount = 2.49M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "BroccoliFlorets.png";
            Item.CategoryNum = 3;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1025;
            Item.Description = "Chopped Spinach";
            Item.Amount = 2.27M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ChoppedSpinach.png";
            Item.CategoryNum = 3;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1026;
            Item.Description = "Almonds, 16 Oz";
            Item.Amount = 6.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Almonds.png";
            Item.CategoryNum = 9;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1027;
            Item.Description = "Sweetened Banana Chips, 8 Oz";
            Item.Amount = 2.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "BananaChips.png";
            Item.CategoryNum = 13;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1028;
            Item.Description = "Seedless Raisins, 8 oz";
            Item.Amount = 3.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Raisins.png";
            Item.CategoryNum = 13;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1040;
            Item.Description = "Organic Almond Butter";
            Item.Amount = 10.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "AlmondButter.png";
            Item.CategoryNum = 11;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1041;
            Item.Description = "Theo Dark Chocolate";
            Item.Amount = 3.97M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "TheoDarkChocolate.png";
            Item.CategoryNum = 10;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1030;
            Item.Description = "Organic Lemons, 2lb";
            Item.Amount = 4.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Lemons.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1031;
            Item.Description = "Yellow Onion, One Large";
            Item.Amount = 0.56M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Onion.png";
            Item.CategoryNum = 3;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1032;
            Item.Description = "Organic Pear, One Large";
            Item.Amount = 1M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Pear.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1033;
            Item.Description = "Broccoli, One Head";
            Item.Amount = 3.11M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Broccoli.png";
            Item.CategoryNum = 3;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1034;
            Item.Description = "Red Seedless Grapes, 2lb";
            Item.Amount = 5.38M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "Grapes.png";
            Item.CategoryNum = 2;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1035;
            Item.Description = "Orville Redenbacher's Naturals Simply Salted Popco";
            Item.Amount = 3.23M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "NaturalLightPopcorn.png";
            Item.CategoryNum = 13;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1036;
            Item.Description = "Orville Redenbacher's Ultimate Butter Popcorn, 6-C";
            Item.Amount = 3.27M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "UltimateButterPopcorn.png";
            Item.CategoryNum = 13;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1100;
            Item.Description = "ISALean Pro French Vanilla, Canister";
            Item.Amount = 49.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ISALeanProFrenchVan.png";
            Item.CategoryNum = 17;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1101;
            Item.Description = "BĒA Citrus Sparkling Energy Drink, 12 Pack";
            Item.Amount = 59.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ISABeaCitrus.png";
            Item.CategoryNum = 17;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1102;
            Item.Description = "ISAGenix Tri Protein Chocolate, Canister";
            Item.Amount = 59.99M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ISATriChocolate.png";
            Item.CategoryNum = 17;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1200;
            Item.Description = "Clinical ClearGel Deodorant";
            Item.Amount = 9.97M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ClearGel.png";
            Item.CategoryNum = 16;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1201;
            Item.Description = "Shaving Gel for Men";
            Item.Amount = 14.50M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "ShavingGel.png";
            Item.CategoryNum = 16;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1202;
            Item.Description = "Jason Tee Tree Shampoo";
            Item.Amount = 9.98M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "JasonShampoo.png";
            Item.CategoryNum = 16;
            Items.Add(Item);

            Item = new ItemModel();
            Item.Id = Id++;
            Item.ItemNum = 1203;
            Item.Description = "Nutiva MCT Oil";
            Item.Amount = 17.30M;
            Item.DefaultQuantity = 1;
            Item.ImageFileName = "NutivaMCTOil.png";
            Item.CategoryNum = 15;
            Items.Add(Item);



            // <<<<< Start of Included Source >>>>>>

            NextID = Id;
            return Items;
        }
    }
}
