﻿using FargowiltasSouls.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using FargowiltasSouls.ModCompatibilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FargowiltasSouls
{
    internal class Fargowiltas : Mod
    {
        internal static ModHotKey FreezeKey;
        internal static ModHotKey GoldKey;
        internal static ModHotKey SmokeBombKey;
        internal static ModHotKey BetsyDashKey;

        internal static List<int> DebuffIDs;

        internal static Fargowiltas Instance;

        internal bool LoadedNewSprites;

        public UserInterface CustomResources;

        internal static readonly Dictionary<int, int> ModProjDict = new Dictionary<int, int>();

        internal bool FargowiltasLoaded;

        public Fargowiltas()
        {
            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load()
        {
            Instance = this;

            if (Language.ActiveCulture == GameCulture.Chinese)
            {
                FreezeKey = RegisterHotKey("冻结时间", "P");
                GoldKey = RegisterHotKey("金身", "O");
                SmokeBombKey = RegisterHotKey("Throw Smoke Bomb", "I");
                BetsyDashKey = RegisterHotKey("Betsy Dash", "C");
            }
            else
            {
                FreezeKey = RegisterHotKey("Freeze Time", "P");
                GoldKey = RegisterHotKey("Turn Gold", "O");
                SmokeBombKey = RegisterHotKey("Throw Smoke Bomb", "I");
                BetsyDashKey = RegisterHotKey("Fireball Dash", "C");
            }
            
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SteelRed"), ItemType("MutantMusicBox"), TileType("MutantMusicBoxSheet"));

            #region Toggles
            #region enchants
            

            ModTranslation text = CreateTranslation("WoodHeader");
            text.SetDefault("[i:" + Instance.ItemType("TimberForce") + "] Force of Timber");
            AddTranslation(text);
            text = CreateTranslation("LifeHeader");
            text.SetDefault("[i:" + Instance.ItemType("LifeForce") + "] Force of Life");
            AddTranslation(text);
            text = CreateTranslation("NatureHeader");
            text.SetDefault("[i:" + Instance.ItemType("NatureForce") + "] Force of Nature");
            AddTranslation(text);
            text = CreateTranslation("ShadowHeader");
            text.SetDefault("[i:" + Instance.ItemType("ShadowForce") + "] Shadow Force");
            AddTranslation(text);
            text = CreateTranslation("SpiritHeader");
            text.SetDefault("[i:" + Instance.ItemType("SpiritForce") + "] Force of Spirit");
            AddTranslation(text);
            text = CreateTranslation("CosmoHeader");
            text.SetDefault("[i:" + Instance.ItemType("CosmoForce") + "] Force of Cosmos");
            AddTranslation(text);
            ModTranslation borealtrans = CreateTranslation("BorealConfig");
            borealtrans.SetDefault("[i:" + Instance.ItemType("BorealWoodEnchant") + "][c/8B7464: Boreal Snowballs]");
            AddTranslation(borealtrans);
            text = CreateTranslation("MahoganyConfig");
            text.SetDefault("[i:" + Instance.ItemType("RichMahoganyEnchant") + "][c/b56c64: Mahogany Hook Speed]");
            AddTranslation(text);
            text = CreateTranslation("EbonConfig");
            text.SetDefault("[i:" + Instance.ItemType("EbonwoodEnchant") + "][c/645a8d: Ebonwood Shadowflame]");
            AddTranslation(text);
            text = CreateTranslation("ShadeConfig");
            text.SetDefault("[i:" + Instance.ItemType("ShadewoodEnchant") + "][c/586876: Blood Geyser On Hit]");
            AddTranslation(text);
            text = CreateTranslation("PalmConfig");
            text.SetDefault("[i:" + Instance.ItemType("PalmWoodEnchant") + "][c/b78d56: Palmwood Sentry]");
            AddTranslation(text);
            text = CreateTranslation("PearlConfig");
            text.SetDefault("[i:" + Instance.ItemType("PearlwoodEnchant") + "][c/ad9a5f: Pearlwood Rain]");
            AddTranslation(text);
            text = CreateTranslation("EarthHeader");
            text.SetDefault("[i:" + Instance.ItemType("EarthForce") + "] Force of Earth");
            AddTranslation(text);
            text = CreateTranslation("AdamantiteConfig");
            text.SetDefault("[i:" + Instance.ItemType("AdamantiteEnchant") + "][c/dd557d: Adamantite Projectile Splitting]");
            AddTranslation(text);
            text = CreateTranslation("CobaltConfig");
            text.SetDefault("[i:" + Instance.ItemType("CobaltEnchant") + "][c/3da4c4: Cobalt Shards]");
            AddTranslation(text);
            text = CreateTranslation("MythrilConfig");
            text.SetDefault("[i:" + Instance.ItemType("MythrilEnchant") + "][c/9dd290: Mythril Weapon Speed]");
            AddTranslation(text);
            text = CreateTranslation("OrichalcumConfig");
            text.SetDefault("[i:" + Instance.ItemType("OrichalcumEnchant") + "][c/eb3291: Orichalcum Fireballs]");
            AddTranslation(text);
            text = CreateTranslation("PalladiumConfig");
            text.SetDefault("[i:" + Instance.ItemType("PalladiumEnchant") + "][c/f5ac28: Palladium Healing]");
            AddTranslation(text);
            text = CreateTranslation("TitaniumConfig");
            text.SetDefault("[i:" + Instance.ItemType("TitaniumEnchant") + "][c/828c88: Titanium Shadow Dodge]");
            AddTranslation(text);
            text = CreateTranslation("TerraHeader");
            text.SetDefault("[i:" + Instance.ItemType("TerraForce") + "] Terra Force");
            AddTranslation(text);
            text = CreateTranslation("CopperConfig");
            text.SetDefault("[i:" + Instance.ItemType("CopperEnchant") + "][c/d56617: Copper Lightning]");
            AddTranslation(text);
            text = CreateTranslation("IronMConfig");
            text.SetDefault("[i:" + Instance.ItemType("IronEnchant") + "][c/988e83: Iron Magnet]");
            AddTranslation(text);
            text = CreateTranslation("IronSConfig");
            text.SetDefault("[i:" + Instance.ItemType("IronEnchant") + "][c/988e83: Iron Shield]");
            AddTranslation(text);
            text = CreateTranslation("CthulhuShield");
            text.SetDefault("[i:" + Instance.ItemType("IronEnchant") + "][c/988e83: Shield of Cthulhu]");
            AddTranslation(text);
            text = CreateTranslation("TinConfig");
            text.SetDefault("[i:" + Instance.ItemType("TinEnchant") + "][c/a28b4e: Tin Crits]");
            AddTranslation(text);
            text = CreateTranslation("TungstenConfig");
            text.SetDefault("[i:" + Instance.ItemType("TungstenEnchant") + "][c/b0d2b2: Tungsten Effect]");
            AddTranslation(text);
            text = CreateTranslation("WillHeader");
            text.SetDefault("[i:" + Instance.ItemType("WillForce") + "] Force of Will");
            AddTranslation(text);
            text = CreateTranslation("GladiatorConfig");
            text.SetDefault("[i:" + Instance.ItemType("GladiatorEnchant") + "][c/9c924e: Gladiator Rain]");
            AddTranslation(text);
            text = CreateTranslation("GoldConfig");
            text.SetDefault("[i:" + Instance.ItemType("GoldEnchant") + "][c/e7b21c: Gold Lucky Coin]");
            AddTranslation(text);
            text = CreateTranslation("RedRidingConfig");
            text.SetDefault("[i:" + Instance.ItemType("RedRidingEnchant") + "][c/c01b3c: Red Riding Super Bleed]");
            AddTranslation(text);
            text = CreateTranslation("ValhallaConfig");
            text.SetDefault("[i:" + Instance.ItemType("ValhallaKnightEnchant") + "][c/93651e: Valhalla Knockback]");
            AddTranslation(text);

            string[] EnchConfig = {
            //force of life
            "BeetleConfig",
            "CactusConfig",
            "PumpkinConfig",
            "SpiderConfig",
            "TurtleConfig",
            //force of nature
            "ChlorophyteConfig",
            "CrimsonConfig",
            "FrostConfig",
            "JungleConfig",
            "MoltenConfig",
            "ShroomiteConfig",
            //shadow force
            "DarkArtConfig",
            "NecroConfig",
            "ShadowConfig",
            "ShinobiConfig",
            "ShinobiTabiConfig",
            "SpookyConfig",
            //force of spirit
            "ForbiddenConfig",
            "HallowedConfig",
            "HalllowSConfig",
            "SilverConfig",
            "SpectreConfig",
            "TikiConfig",
            //force of cosmos
            "MeteorConfig",
            "NebulaConfig",
            "SolarConfig",
            "StardustConfig",
            "VortexSConfig",
            "VortexVConfig"
            };

            string[] EnchName = {
            //force of life
            "Beetles",
            "Cactus Needles",
            "Pumpkin Fire",
            "Spider Swarm",
            "Turtle Shell Buff",
            //force of nature
            "Chlorophyte Leaf Crystal",
            "Crimson Regen",
            "Frost Icicles",
            "Jungle Spores",
            "Molten Inferno Buff",
            "Shroomite Stealth",
            //shadow force
            "Dark Artist Effect",
            "Necro Guardian",
            "Shadow Darkness",
            "Shinobi Through Walls",
            "Tabi Dash",
            "Spooky Scythes",
            //force of spirit
            "Forbidden Storm",
            "Hallowed Enchanted Sword Familiar",
            "Hallowed Shield",
            "Silver Sword Familiar",
            "Spectre Orbs",
            "Tiki Minions",
            //force of cosmos
            "Meteor Shower",
            "Nebula Boosters",
            "Solar Shield",
            "Stardust Guardian",
            "Vortex Stealth",
            "Vortex Voids"
            };

            string[] EnchColor = {
            //force of life
            "6D5C85",
            "799e1d",
            "e3651c",
            "6d4e45",
            "f89c5c",
            //force of nature
            "248900",
            "C8364B",
            "7abdb9",
            "71971f",
            "c12b2b",
            "008cf4",
            //shadow force
            "9b5cb0",
            "565643",
            "42356f",
            "935b18",
            "935b18",
            "644e74",
            //force of spirit
            "e7b21c",
            "968564",
            "968564",
            "b4b4cc",
            "accdfc",
            "56A52B",
            //force of cosmos
            "5f4752",
            "fe7ee5",
            "fe9e23",
            "00aeee",
            "00f2aa",
            "00f2aa"
            };

            string[] EnchItem = {
            //force of life
            "BeetleEnchant",
            "CactusEnchant",
            "PumpkinEnchant",
            "SpiderEnchant",
            "TurtleEnchant",
            //force of nature
            "ChlorophyteEnchant",
            "CrimsonEnchant",
            "FrostEnchant",
            "JungleEnchant",
            "MoltenEnchant",
            "ShroomiteEnchant",
            //shadow force
            "DarkArtistEnchant",
            "NecroEnchant",
            "ShadowEnchant",
            "ShinobiEnchant",
            "ShinobiEnchant",
            "SpookyEnchant",
            //force of spirit
            "ForbiddenEnchant",
            "HallowEnchant",
            "HallowEnchant",
            "SilverEnchant",
            "SpectreEnchant",
            "TikiEnchant",
            //force of cosmos
            "MeteorEnchant",
            "NebulaEnchant",
            "SolarEnchant",
            "StardustEnchant",
            "VortexEnchant",
            "VortexEnchant"
            };

            for (int x = 0; x < EnchConfig.Length; x++)
            {
                text = CreateTranslation(EnchConfig[x]);
                text.SetDefault("[i:" + Instance.ItemType(EnchItem[x]) + "][c/" + EnchColor[x] + ": " + EnchName[x] + "]");
                AddTranslation(text);
            }
            #endregion

            #region masomode toggles
            string[] masoTogName = { 
            //deathbringer fairy
            "Slimy Shield Effects",
            "Scythes When Dashing",
            "Skeletron Arms Minion",
            //pure heart
            "Tiny Eaters",
            "Creeper Shield",
            //bionomic cluster
            "Tim's Concoction",
            "Rainbow Slime Minion",
            "Frostfireballs",
            "Attacks Spawn Hearts",
            "Squeaky Toy On Hit",
            "Tentacles On Hit",
            "Inflict Clipped Wings",
            //dubious circutry
            "Inflict Lightning Rod",
            "Probes Minion",
            //heart of the masochist
            "Gravity Control",
            "Stabilized Gravity",
            "Pumpking's Cape Support",
            "Flocko Minion",
            "Saucer Minion",
            "True Eyes Minion",
            //chalice of the moon
            "Celestial Rune Support",
            "Plantera Minion",
            "Lihzahrd Ground Pound",
            "Ancient Visions On Hit",
            "Cultist Minion",
            "Spectral Fishron",
            //lump of flesh
            "Pungent Eye Minion",
            //mutant armor
            "Abominationn Minion",
            "Phantasmal Ring Minion",
            //other
            "Spiky Balls On Hit",
            "Sinister Icon",
            "Boss Recolors (Restart to use)"};

            string[] masoTogConfigName = {
            //deathbringer fairy
            "MasoSlimeConfig",
            "MasoEyeConfig",
            "MasoSkeleConfig",
            //pure heart
            "MasoEaterConfig",
            "MasoBrainConfig",
            //bionomic cluster
            "MasoConcoctionConfig",
            "MasoRainbowConfig",
            "MasoFrigidConfig",
            "MasoNymphConfig",
            "MasoSqueakConfig",
            "MasoPouchConfig",
            "MasoClippedConfig",
            //dubious circutry
            "MasoLightningConfig",
            "MasoProbeConfig",
            //heart of the masochist
            "MasoGravConfig",
            "MasoGrav2Config",
            "MasoPump",
            "MasoFlockoConfig",
            "MasoUfoConfig",
            "MasoTrueEyeConfig",
            //chalice of the moon
            "MasoCelestConfig",
            "MasoPlantConfig",
            "MasoGolemConfig",
            "MasoVisionConfig",
            "MasoCultistConfig",
            "MasoFishronConfig",
            //lump of flesh
            "MasoPugentConfig",
            //mutant armor
            "MasoAbomConfig",
            "MasoRingConfig",
            //other
            "MasoSpikeConfig",
            "MasoIconConfig",
            "MasoBossRecolors"};

            for (int x = 0; x < masoTogName.Length; x++)
            {
                text = CreateTranslation(masoTogConfigName[x]);
                text.SetDefault(masoTogName[x]);
                AddTranslation(text);
            }
            #endregion

            #region pet toggles
            int[] petnums = {
            //NORMAL PETS
            1810,//black cat
            3628,//companion cube
            1837, //cursed sapling
            1242, //dino pet
            3857, //dragon
            994, //eater
            1311, //eye spring
            3060, //face monster
            3855, //gato
            1170, //hornet
            1172, //lizard
            2587, //mini minitaur
            1180, //parrot
            669, //penguin
            1927, //puppy
            1182, //seedling
            1169, //dungeon guardian
            1312, // snowman
            1798, // spider
            1799, //squashling
            1171, //tiki
            1181, //truffle
            753, //turtle
            2420, //zephyr fish
                  //LIGHT PETS
            3062, //crimson heart
            425, //fairy
            3856, //flickerwick
            3043, //magic lanturn
            115, //shadow orb
            3577, //suspicious eye
            1183//wisp
            };

            string[] petTogName = {
            "Black Cat Pet",
            "Companion Cube Pet",
            "Cursed Sapling Pet",
            "Dino Pet",
            "Dragon Pet",
            "Eater Pet",
            "Eye Spring Pet",
            "Face Monster Pet",
            "Gato Pet",
            "Hornet Pet",
            "Lizard Pet",
            "Mini Minotaur Pet",
            "Parrot Pet",
            "Penguin Pet",
            "Puppy Pet",
            "Seedling Pet",
            "Skeletron Pet",
            "Snowman Pet",
            "Spider Pet",
            "Squashling Pet",
            "Tiki Pet",
            "Truffle Pet",
            "Turtle Pet",
            "Zephyr Fish Pet",
            //LIGHT PETS
            "Crimson Heart Pet",
            "Fairy Pet",
            "Flickerwick Pet",
            "Magic Lantern Pet",
            "Shadow Orb Pet",
            "Suspicious Eye Pet",
            "Wisp Pet" };

            string[] petTogConfigName = {
            "PetCatConfig",
            "PetCubeConfig",
            "PetCurseSapConfig",
            "PetDinoConfig",
            "PetDragonConfig",
            "PetEaterConfig",
            "PetEyeSpringConfig",
            "PetFaceMonsterConfig",
            "PetGatoConfig",
            "PetHornetConfig",
            "PetLizardConfig",
            "PetMinitaurConfig",
            "PetParrotConfig",
            "PetPenguinConfig",
            "PetPupConfig",
            "PetSeedConfig",
            "PetDGConfig",
            "PetSnowmanConfig",
            "PetSpiderConfig",
            "PetSquashConfig",
            "PetTikiConfig",
            "PetShroomConfig",
            "PetTurtleConfig",
            "PetZephyrConfig",
            //LIGHT PETS
            "PetHeartConfig",
            "PetNaviConfig",
            "PetFlickerConfig",
            "PetLanturnConfig",
            "PetOrbConfig",
            "PetSuspEyeConfig",
            "PetWispConfig" };

            for (int x = 0; x <= 30; x++)
            {
                text = CreateTranslation(petTogConfigName[x]);
                text.SetDefault("[I:" + petnums[x] + "] " + petTogName[x]);
                AddTranslation(text);
            }

            #endregion

            #region wallet toggles
            string[] prefix = {
        "Warding",
        "Violent",
        "Quick",
        "Lucky",
        "Menacing",
        "Legendary",
        "Unreal",
        "Mythical",
        "Godly",
        "Demonic",
        "Ruthless",
        "Light",
        "Deadly",
        "Rapid"};

            string[] prefixconf = {
        "WalletWardingConfig",
        "WalletViolentConfig",
        "WalletQuickConfig",
        "WalletLuckyConfig",
        "WalletMenacingConfig",
        "WalletLegendaryConfig",
        "WalletUnrealConfig",
        "WalletMythicalConfig",
        "WalletGodlyConfig",
        "WalletDemonicConfig",
        "WalletRuthlessConfig",
        "WalletLightConfig",
        "WalletDeadlyConfig",
        "WalletRapidConfig" };

            for (int x = 0; x <= 13; x++)
            {
                text = CreateTranslation(prefixconf[x]);
                text.SetDefault(prefix[x]);
                AddTranslation(text);
            }
            #endregion

            #region soul toggles
            string[] soultognames = {
            //Universe
            "Melee Speed",
            "Sniper Scope",
            "Universe Attack Speed",
            //dimensions
            "Mining Hunter Buff",
            "Mining Dangersense Buff",
            "Mining Spelunker Buff",
            "Mining Shine Buff",
            "Builder Mode",
            "Spore Sac",
            "Stars On Hit",
            "Bees On Hit",
            "Supersonic Speed Boosts",
            //idk 
            "Eternity Stacking"};

            string[] soultogconfig = {
            //Universe
            "MeleeConfig",
            "SniperConfig",
            "UniverseConfig",
            //dimensions
            "MiningHuntConfig",
            "MiningDangerConfig",
            "MiningSpelunkConfig",
            "MiningShineConfig",
            "BuilderConfig",
            "DefenseSporeConfig",
            "DefenseStarConfig",
            "DefenseBeeConfig",
            "SupersonicConfig",
            //idk 
            "EternityConfig" };

            string[] soultogitemnames = {
            //Universe
            "GladiatorsSoul",
            "SharpshootersSoul",
            "UniverseSoul",
            //dimensions
            "MinerEnchant",
            "MinerEnchant",
            "MinerEnchant",
            "MinerEnchant",
            "WorldShaperSoul",
            "ColossusSoul",
            "ColossusSoul",
            "ColossusSoul",
            "SupersonicSoul",
            //idk 
            "EternitySoul" };

            for (int x = 0; x <= 12; x++)
            {
                text = CreateTranslation(soultogconfig[x]);
                text.SetDefault("[i:" + Instance.ItemType(soultogitemnames[x]) + "][c/" + soulcolor[x] + ": " + soultognames[x] + "]");
                AddTranslation(text);
            }
            #endregion

            #region thorium

            string[] thoriumTogNames = {
            "Air Walkers",
            "Crystal Scorpion",
            "Yuma's Pendant",
            "Head Mirror",
            "Celestial Aura",
            "Ascension Statuette",
            "Mana-Charged Rocketeers",
            "Bronze Lightning",
            "Illumite Missile",
            "Jester Bell",
            "Eye of the Beholder",
            "Terrarium Spirits",
            "Crietz",
            "Yew Wood Crits",
            "Cryo-Magus Damage",
            "White Dwarf Flares",
            "Tide Hunter Foam",
            "Whispering Tentacles",
            "Icy Barrier",
            "Plague Lord's Flask",
            "Tide Turner Globules",
            "Tide Turner Daggers",
            "Folv's Aura",
            "Folv's Bolts",
            "Vampire Gland",
            "Flesh Drops",
            "Dragon Flames",
            "Harbinger Overcharge",
            "Assassin Damage",
            "Pyromancer Bursts",
            "Conduit Shield",
            "Incandescent Spark",
            "Greedy Magnet",
            "Cyber Punk States",
            "Metronome",
            "Mix Tape",
            "Lodestone Resistance",
            "Biotech Probe",
            "Proof of Avarice",
            "Slag Stompers",
            "Spring Steps",
            "Berserker Effect",
            "Bee Booties",
            "Ghastly Carapace",
            "Spirit Trapper Wisps",
            "Warlock Wisps",
            "Dread Speed",
            "Spawn Divers",
            "Demon Blood Effect",
            "Li'l Devil Minion",
            "Li'l Cherub Minion",
            "Sapling Minion",
            "Omega Pet",
            "I.F.O. Pet",
            "Bio-Feeder Pet",
            "Blister Pet",
            "Wyvern Pet",
            "Inspiring Lantern Pet",
            "Lock Box Pet",
            "Life Spirit Pet",
            "Holy Goat Pet",
            "Owl Pet",
            "Jellyfish Pet",
            "Moogle Pet",
            "Maid Pet",
            "Pink Slime Pet",
            "Glitter Pet",
            "Coin Bag Pet"};

            string[] thoriumTogConfig = {
            "ThoriumAirWalkersConfig",
            "ThoriumCrystalScorpionConfig",
            "ThoriumYumasPendantConfig",
            "ThoriumHeadMirrorConfig",
            "ThoriumCelestialAuraConfig",
            "ThoriumAscensionStatueConfig",
            "ThoriumManaBootsConfig",
            "ThoriumBronzeLightningConfig",
            "ThoriumIllumiteMissileConfig",
            "ThoriumJesterBellConfig",
            "ThoriumBeholderEyeConfig",
            "ThoriumTerrariumSpiritsConfig",
            "ThoriumCrietzConfig",
            "ThoriumYewCritsConfig",
            "ThoriumCryoDamageConfig",
            "ThoriumWhiteDwarfConfig",
            "ThoriumTideFoamConfig",
            "ThoriumWhisperingTentaclesConfig",
            "ThoriumIcyBarrierConfig",
            "ThoriumPlagueFlaskConfig",
            "ThoriumTideGlobulesConfig",
            "ThoriumTideDaggersConfig",
            "ThoriumFolvAuraConfig",
            "ThoriumFolvBoltsConfig",
            "ThoriumVampireGlandConfig",
            "ThoriumFleshDropsConfig",
            "ThoriumDragonFlamesConfig",
            "ThoriumHarbingerOverchargeConfig",
            "ThoriumAssassinDamageConfig",
            "ThoriumpyromancerBurstsConfig",
            "ThoriumConduitShieldConfig",
            "ThoriumIncandescentSparkConfig",
            "ThoriumGreedyMagnetConfig",
            "ThoriumCyberStatesConfig",
            "ThoriumMetronomeConfig",
            "ThoriumMixTapeConfig",
            "ThoriumLodestoneConfig",
            "ThoriumBiotechProbeConfig",
            "ThoriumProofAvariceConfig",
            "ThoriumSlagStompersConfig",
            "ThoriumSpringStepsConfig",
            "ThoriumBerserkerConfig",
            "ThoriumBeeBootiesConfig",
            "ThoriumGhastlyCarapaceConfig",
            "ThoriumSpiritWispsConfig",
            "ThoriumWarlockWispsConfig",
            "ThoriumDreadConfig",
            "ThoriumDiverConfig",
            "ThoriumDemonBloodConfig",
            "ThoriumDevilMinionConfig",
            "ThoriumCherubMinionConfig",
            "ThoriumSaplingMinionConfig",
            "ThoriumOmegaPetConfig",
            "ThoriumIFOPetConfig",
            "ThoriumBioFeederPetConfig",
            "ThoriumBlisterPetConfig",
            "ThoriumWyvernPetConfig",
            "ThoriumLanternPetConfig",
            "ThoriumBoxPetConfig",
            "ThoriumSpiritPetConfig",
            "ThoriumGoatPetConfig",
            "ThoriumOwlPetConfig",
            "ThoriumJellyfishPetConfig",
            "ThoriumMooglePetConfig",
            "ThoriumMaidPetConfig",
            "ThoriumSlimePetConfig",
            "ThoriumGlitterPetConfig",
            "ThoriumCoinPetConfig"};

            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "SupersonicSoul", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ConjuristsSoul", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ConjuristsSoul", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "GuardianAngelsSoul", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "CelestialEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "CelestialEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "MalignantEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "BronzeEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "IllumiteEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "JesterEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ValadiumEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "TerrariumEnchant", "ffffff");

            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ThoriumEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "YewWoodEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "CryoMagusEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "WhiteDwarfEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "TideHunterEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "WhisperingEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "IcyEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "PlagueDoctorEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "TideTurnerEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "TideTurnerEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "FolvEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "FolvEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "FleshEnchant", "ffffff");

            
            

            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "FleshEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DragonEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "HarbingerEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "AssassinEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "PyromancerEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ConduitEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DurasteelEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DurasteelEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "CyberPunkEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ConductorEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "NobleEnchant", "ffffff");



            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "LodestoneEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "BiotechEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "GoldEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "MagmaEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "MagmaEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "BerserkerEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "BeeEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "SpectreEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "SpiritTrapperEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "WarlockEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DreadEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ThoriumEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DemonBloodEnchant", "ffffff");

            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "WarlockEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "SacredEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "LivingWoodEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ConduitEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "ConduitEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "MeteorEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "FleshEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DragonEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "GeodeEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "GeodeEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "SacredEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "LifeBinderEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "CryoMagusEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DepthDiverEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "WhiteKnightEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "DreamWeaverEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "IllumiteEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "PlatinumEnchant", "ffffff");
            AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "GoldEnchant", "ffffff");







        #endregion

        #region calamity

        AddToggle("CalamityUrchinConfig", "Victide Sea Urchin", "VictideEnchant", "ffffff");
            AddToggle("CalamityProfanedArtifactConfig", "Profaned Soul Artifact", "TarragonEnchant", "ffffff");
            AddToggle("CalamitySlimeMinionConfig", "Slime God Minion", "StatigelEnchant", "ffffff");
            AddToggle("CalamityReaverMinionConfig", "Reaver Orb Minion", "ReaverEnchant", "ffffff");
            AddToggle("CalamityOmegaTentaclesConfig", "Omega Blue Tentacles", "OmegaBlueEnchant", "ffffff");
            AddToggle("CalamitySilvaMinionConfig", "Silva Crystal Minion", "SilvaEnchant", "ffffff");
            AddToggle("CalamityGodlyArtifactConfig", "Godly Soul Artifact", "SilvaEnchant", "ffffff");
            AddToggle("CalamityMechwormMinionConfig", "Mechworm Minion", "GodSlayerEnchant", "ffffff");
            AddToggle("CalamityNebulousCoreConfig", "Nebulous Core", "GodSlayerEnchant", "ffffff");
            AddToggle("CalamityDevilMinionConfig", "Red Devil Minion", "DemonShadeEnchant", "ffffff");
            AddToggle("CalamityPermafrostPotionConfig", "Permafrost's Concoction", "DaedalusEnchant", "ffffff");
            AddToggle("CalamityDaedalusMinionConfig", "Daedalus Crystal Minion", "DaedalusEnchant", "ffffff");
            AddToggle("CalamityPolterMinesConfig", "Polterghast Mines", "BloodflareEnchant", "ffffff");
            AddToggle("CalamityPlagueHiveConfig", "Plague Hive", "AtaxiaEnchant", "ffffff");
            AddToggle("CalamityChaosMinionConfig", "Chaos Spirit Minion", "AtaxiaEnchant", "ffffff");
            AddToggle("CalamityValkyrieMinionConfig", "Valkyrie Minion", "AerospecEnchant", "ffffff");
            AddToggle("CalamityYharimGiftConfig", "Yharim's Gift", "SilvaEnchant", "ffffff");
            AddToggle("CalamityFungalMinionConfig", "Fungal Clump Minion", "SilvaEnchant", "ffffff");
            AddToggle("CalamityWaifuMinionsConfig", "Elemental Waifus", "AuricEnchant", "ffffff");
            AddToggle("CalamityShellfishMinionConfig", "Shellfish Minions", "MolluskEnchant", "ffffff");
            AddToggle("CalamityAmidiasPendantConfig", "Amidias' Pendant", "MolluskEnchant", "ffffff");
            AddToggle("CalamityGiantPearlConfig", "Giant Pearl", "MolluskEnchant", "ffffff");
            AddToggle("CalamityPoisonSeawaterConfig", "Poisonous Sea Water", "SilvaEnchant", "ffffff");
            AddToggle("CalamityDaedalusEffectsConfig", "Daedalus Effects", "DaedalusEnchant", "ffffff");
            AddToggle("CalamityReaverEffectsConfig", "Reaver Effects", "ReaverEnchant", "ffffff");
            AddToggle("CalamityFabledTurtleConfig", "Fabled Turtle Shell", "ReaverEnchant", "ffffff");
            AddToggle("CalamityAstralStarsConfig", "Astral Stars", "AstralEnchant", "ffffff");
            AddToggle("CalamityAtaxiaEffectsConfig", "Ataxia Effects", "AtaxiaEnchant", "ffffff");
            AddToggle("CalamityXerocEffectsConfig", "Xeroc Effects", "XerocEnchant", "ffffff");
            AddToggle("CalamityTarragonEffectsConfig", "Tarragon Effects", "TarragonEnchant", "ffffff");
            AddToggle("CalamityBloodflareEffectsConfig", "Bloodflare Effects", "BloodflareEnchant", "ffffff");
            AddToggle("CalamityGodSlayerEffectsConfig", "God Slayer Effects", "GodSlayerEnchant", "ffffff");
            AddToggle("CalamitySilvaEffectsConfig", "Silva Effects", "SilvaEnchant", "ffffff");
            AddToggle("CalamityAuricEffectsConfig", "Auric Tesla Effects", "AuricEnchant", "ffffff");
            AddToggle("CalamityElementalQuiverConfig", "Elemental Quiver", "SharpshootersSoul", "ffffff");
            AddToggle("CalamityLuxorGiftConfig", "Luxor's Gift", "VictideEnchant", "ffffff");
            AddToggle("CalamityGladiatorLocketConfig", "Gladiator's Locket", "AerospecEnchant", "ffffff");
            AddToggle("CalamityUnstablePrismConfig", "Unstable Prism", "AerospecEnchant", "ffffff");
            AddToggle("CalamityRegeneratorConfig", "Regenator", "DaedalusEnchant", "ffffff");
            AddToggle("CalamityDivingSuitConfig", "Abyssal Diving Suit", "OmegaBlueEnchant", "ffffff");
            AddToggle("CalamityKendraConfig", "Kendra Pet", "AerospecEnchant", "ffffff");
            AddToggle("CalamityPerforatorConfig", "Perforator Pet", "StatigelEnchant", "ffffff");
            AddToggle("CalamityBearConfig", "Bear Pet", "DaedalusEnchant", "ffffff");
            AddToggle("CalamityThirdSageConfig", "Third Sage Pet", "DaedalusEnchant", "ffffff");
            AddToggle("CalamityBrimlingConfig", "Brimling Pet", "AtaxiaEnchant", "ffffff");
            AddToggle("CalamityDannyConfig", "Danny Pet", "MolluskEnchant", "ffffff");
            AddToggle("CalamitySirenConfig", "Siren Pet", "OmegaBlueEnchant", "ffffff");
            AddToggle("CalamityChibiiConfig", "Chibii Pet", "GodSlayerEnchant", "ffffff");
            AddToggle("CalamityAkatoConfig", "Akato Pet", "SilvaEnchant", "ffffff");
            AddToggle("CalamityFoxConfig", "Fox Pet", "SilvaEnchant", "ffffff");
            AddToggle("CalamityLeviConfig", "Levi Pet", "DemonShadeEnchant", "ffffff");

            #endregion

            #endregion


        }

        public void AddToggle(String toggle, String name, String item, String color)
        {
            ModTranslation text = CreateTranslation(toggle);
            text.SetDefault("[i:" + Instance.ItemType(item) + "][c/" + color + ": " + name + "]");
            AddTranslation(text);
        }

        public override void Unload()
        {
            if (DebuffIDs != null)
                DebuffIDs.Clear();
        }

        public override object Call(params object[] args)
        {
            if ((string)args[0] == "FargoSoulsAI")
            {
                /*int n = (int)args[1];
                Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>().AI(Main.npc[n]);*/
            }
            return base.Call(args);
        }

        //bool sheet
        public override void PostSetupContent()
        {
            try
            {
                FargowiltasLoaded = ModLoader.GetMod("Fargowiltas") != null;

                CalamityCompatibility = new CalamityCompatibility(this).TryLoad() as CalamityCompatibility;
                ThoriumCompatibility = new ThoriumCompatibility(this).TryLoad() as ThoriumCompatibility;
                SoACompatibility = new SoACompatibility(this).TryLoad() as SoACompatibility;

                //FargowiltasCompatibility = new FargowiltasCompatibility(this).TryLoad() as FargowiltasCompatibility;
                MasomodeEXCompatibility = new MasomodeEXCompatibility(this).TryLoad() as MasomodeEXCompatibility;

                DBZMODCompatibility = new DBZMODCompatibility(this).TryLoad() as DBZMODCompatibility;
                ApothCompatibility = new ApothTestModCompatibility(this).TryLoad() as ApothTestModCompatibility;

                DebuffIDs = new List<int> { 20, 22, 23, 24, 36, 39, 44, 46, 47, 67, 68, 69, 70, 80,
                    88, 94, 103, 137, 144, 145, 148, 149, 156, 160, 163, 164, 195, 196, 197, 199 };
                DebuffIDs.Add(BuffType("Antisocial"));
                DebuffIDs.Add(BuffType("Atrophied"));
                DebuffIDs.Add(BuffType("Berserked"));
                DebuffIDs.Add(BuffType("Bloodthirsty"));
                DebuffIDs.Add(BuffType("ClippedWings"));
                DebuffIDs.Add(BuffType("Crippled"));
                DebuffIDs.Add(BuffType("CurseoftheMoon"));
                DebuffIDs.Add(BuffType("Defenseless"));
                DebuffIDs.Add(BuffType("FlamesoftheUniverse"));
                DebuffIDs.Add(BuffType("Flipped"));
                DebuffIDs.Add(BuffType("FlippedHallow"));
                DebuffIDs.Add(BuffType("Fused"));
                DebuffIDs.Add(BuffType("GodEater"));
                DebuffIDs.Add(BuffType("Guilty"));
                DebuffIDs.Add(BuffType("Hexed"));
                DebuffIDs.Add(BuffType("Infested"));
                DebuffIDs.Add(BuffType("IvyVenom"));
                DebuffIDs.Add(BuffType("Jammed"));
                DebuffIDs.Add(BuffType("Lethargic"));
                DebuffIDs.Add(BuffType("LightningRod"));
                DebuffIDs.Add(BuffType("LivingWasteland"));
                DebuffIDs.Add(BuffType("Lovestruck"));
                DebuffIDs.Add(BuffType("MarkedforDeath"));
                DebuffIDs.Add(BuffType("Midas"));
                DebuffIDs.Add(BuffType("MutantNibble"));
                DebuffIDs.Add(BuffType("NullificationCurse"));
                DebuffIDs.Add(BuffType("Oiled"));
                DebuffIDs.Add(BuffType("OceanicMaul"));
                DebuffIDs.Add(BuffType("Purified"));
                DebuffIDs.Add(BuffType("Recovering"));
                DebuffIDs.Add(BuffType("ReverseManaFlow"));
                DebuffIDs.Add(BuffType("Rotting"));
                DebuffIDs.Add(BuffType("Shadowflame"));
                DebuffIDs.Add(BuffType("SqueakyToy"));
                DebuffIDs.Add(BuffType("Stunned"));
                DebuffIDs.Add(BuffType("Swarming"));
                DebuffIDs.Add(BuffType("Unstable"));

                DebuffIDs.Add(BuffType("MutantFang"));
                DebuffIDs.Add(BuffType("MutantPresence"));

                DebuffIDs.Add(BuffType("TimeFrozen"));

                Mod bossChecklist = ModLoader.GetMod("BossChecklist");
                if (bossChecklist != null)
                {
                    bossChecklist.Call("AddBossWithInfo", "Duke Fishron EX", 14.01f, (Func<bool>)(() => FargoSoulsWorld.downedFishronEX), "Fish using a [i:" + ItemType("TruffleWormEX") + "]");
                    bossChecklist.Call("AddBossWithInfo", "Mutant", 14.02f, (Func<bool>)(() => FargoSoulsWorld.downedMutant), "Spawn by throwing [i:" + ItemType("AbominationnVoodooDoll") + "] in lava in Mutant's presence");
                }

                if (ThoriumLoaded)
                {
                    Mod thorium = ModLoader.GetMod("ThoriumMod");
                    ModProjDict.Add(thorium.ProjectileType("IFO"), 1);
                    ModProjDict.Add(thorium.ProjectileType("BioFeederPet"), 2);
                    ModProjDict.Add(thorium.ProjectileType("BlisterPet"), 3);
                    ModProjDict.Add(thorium.ProjectileType("WyvernPet"), 4);
                    ModProjDict.Add(thorium.ProjectileType("SupportLantern"), 5);
                    ModProjDict.Add(thorium.ProjectileType("LockBoxPet"), 6);
                    ModProjDict.Add(thorium.ProjectileType("Devil"), 7);
                    ModProjDict.Add(thorium.ProjectileType("Angel"), 8);
                    ModProjDict.Add(thorium.ProjectileType("LifeSpirit"), 9);
                    ModProjDict.Add(thorium.ProjectileType("HolyGoat"), 10);
                    ModProjDict.Add(thorium.ProjectileType("MinionSapling"), 11);
                    ModProjDict.Add(thorium.ProjectileType("SnowyOwlPet"), 12);
                    ModProjDict.Add(thorium.ProjectileType("JellyfishPet"), 13);
                    ModProjDict.Add(thorium.ProjectileType("LilMog"), 14);
                    ModProjDict.Add(thorium.ProjectileType("Maid1"), 15);
                    ModProjDict.Add(thorium.ProjectileType("PinkSlime"), 16);
                    ModProjDict.Add(thorium.ProjectileType("ShinyPet"), 17);
                    ModProjDict.Add(thorium.ProjectileType("DrachmaBag"), 18);
                }

                if (CalamityLoaded)
                {
                    Mod calamity = ModLoader.GetMod("CalamityMod");
                    ModProjDict.Add(calamity.ProjectileType("KendraPet"), 101);
                    ModProjDict.Add(calamity.ProjectileType("PerforaMini"), 102);
                    ModProjDict.Add(calamity.ProjectileType("ThirdSage"), 103);
                    ModProjDict.Add(calamity.ProjectileType("Bear"), 104);
                    ModProjDict.Add(calamity.ProjectileType("BrimlingPet"), 105);
                    ModProjDict.Add(calamity.ProjectileType("DannyDevitoPet"), 106);
                    ModProjDict.Add(calamity.ProjectileType("SirenYoung"), 107);
                    ModProjDict.Add(calamity.ProjectileType("ChibiiDoggo"), 108);
                    ModProjDict.Add(calamity.ProjectileType("ChibiiDoggoFly"), 109);
                    ModProjDict.Add(calamity.ProjectileType("Akato"), 110);
                    ModProjDict.Add(calamity.ProjectileType("Fox"), 111);
                    ModProjDict.Add(calamity.ProjectileType("Levi"), 112);
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log("FargowiltasSouls PostSetupContent Error: " + e.StackTrace + e.Message);
            }
        }

        public override void AddRecipes()
        {
            ThoriumCompatibility?.TryAddRecipes();
        }

        public override void AddRecipeGroups()
        {
            //drax
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Drax", ItemID.Drax, ItemID.PickaxeAxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", group);

            //cobalt
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", ItemID.CobaltRepeater, ItemID.PalladiumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

            //mythril
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", ItemID.MythrilRepeater, ItemID.OrichalcumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

            //adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);

            CalamityCompatibility?.TryAddRecipeGroups();
            ThoriumCompatibility?.TryAddRecipeGroups();
            SoACompatibility?.TryAddRecipeGroups();

            //evil wood
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood", ItemID.Ebonwood, ItemID.Shadewood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", group);

            //anvil HM
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Anvil", ItemID.MythrilAnvil, ItemID.OrichalcumAnvil);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAnvil", group);

            //forge HM
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Forge", ItemID.AdamantiteForge, ItemID.TitaniumForge);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyForge", group);

            //any adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantite", group);

            //shroomite head
            group = new RecipeGroup(() => Lang.misc[37] + " Shroomite Head Piece", ItemID.ShroomiteHeadgear, ItemID.ShroomiteMask, ItemID.ShroomiteHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShroomHead", group);

            //orichalcum head
            group = new RecipeGroup(() => Lang.misc[37] + " Orichalcum Head Piece", ItemID.OrichalcumHeadgear, ItemID.OrichalcumMask, ItemID.OrichalcumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyOriHead", group);

            //palladium head
            group = new RecipeGroup(() => Lang.misc[37] + " Palladium Head Piece", ItemID.PalladiumHeadgear, ItemID.PalladiumMask, ItemID.PalladiumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPallaHead", group);

            //cobalt head
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Head Piece", ItemID.CobaltHelmet, ItemID.CobaltHat, ItemID.CobaltMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltHead", group);

            //mythril head
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Head Piece", ItemID.MythrilHat, ItemID.MythrilHelmet, ItemID.MythrilHood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilHead", group);

            //titanium head
            group = new RecipeGroup(() => Lang.misc[37] + " Titanium Head Piece", ItemID.TitaniumHeadgear, ItemID.TitaniumMask, ItemID.TitaniumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTitaHead", group);

            //hallowed head
            group = new RecipeGroup(() => Lang.misc[37] + " Hallowed Head Piece", ItemID.HallowedMask, ItemID.HallowedHeadgear, ItemID.HallowedHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyHallowHead", group);

            //adamantite head
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Head Piece", ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamHead", group);

            //chloro head
            group = new RecipeGroup(() => Lang.misc[37] + " Chlorophyte Head Piece", ItemID.ChlorophyteMask, ItemID.ChlorophyteHelmet, ItemID.ChlorophyteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyChloroHead", group);

            //spectre head
            group = new RecipeGroup(() => Lang.misc[37] + " Spectre Head Piece", ItemID.SpectreHood, ItemID.SpectreMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySpectreHead", group);

            //book cases
            group = new RecipeGroup(() => Lang.misc[37] + " Bookcase", new int[]
            {
                ItemID.Bookcase,
                ItemID.BlueDungeonBookcase,
                ItemID.BoneBookcase,
                ItemID.BorealWoodBookcase,
                ItemID.CactusBookcase,
                ItemID.CrystalBookCase,
                ItemID.DynastyBookcase,
                ItemID.EbonwoodBookcase,
                ItemID.FleshBookcase,
                ItemID.FrozenBookcase,
                ItemID.GlassBookcase,
                ItemID.GoldenBookcase,
                ItemID.GothicBookcase,
                ItemID.GraniteBookcase,
                ItemID.GreenDungeonBookcase,
                ItemID.HoneyBookcase,
                ItemID.LivingWoodBookcase,
                ItemID.MarbleBookcase,
                ItemID.MeteoriteBookcase,
                ItemID.MushroomBookcase,
                ItemID.ObsidianBookcase,
                ItemID.PalmWoodBookcase,
                ItemID.PearlwoodBookcase,
                ItemID.PinkDungeonBookcase,
                ItemID.PumpkinBookcase,
                ItemID.RichMahoganyBookcase,
                ItemID.ShadewoodBookcase,
                ItemID.SkywareBookcase,
                ItemID.SlimeBookcase,
                ItemID.SpookyBookcase,
                ItemID.SteampunkBookcase
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBookcase", group);

            //beetle body
            group = new RecipeGroup(() => Lang.misc[37] + " Beetle Body", ItemID.BeetleShell, ItemID.BeetleScaleMail);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", group);

            //phasesabers
            group = new RecipeGroup(() => Lang.misc[37] + " Phasesaber", ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber,
                ItemID.YellowPhasesaber);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPhasesaber", group);

            //vanilla butterflies
            group = new RecipeGroup(() => Lang.misc[37] + " Butterfly", ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly,
                ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyButterfly", group);

            //vanilla squirrels
            group = new RecipeGroup(() => Lang.misc[37] + " Squirrel", ItemID.Squirrel, ItemID.SquirrelRed);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySquirrel", group);

            group = new RecipeGroup(() => Lang.misc[37] + " Gold Pickaxe", ItemID.GoldPickaxe, ItemID.PlatinumPickaxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldPickaxe", group);

            if (ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");

                //jester mask
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Mask", thorium.ItemType("JestersMask"), thorium.ItemType("JestersMask2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterMask", group);
                //jester shirt
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Shirt", thorium.ItemType("JestersShirt"), thorium.ItemType("JestersShirt2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterShirt", group);
                //jester legging
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Leggings", thorium.ItemType("JestersLeggings"), thorium.ItemType("JestersLeggings2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterLeggings", group);
                //evil wood tambourine
                group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood Tambourine", thorium.ItemType("EbonWoodTambourine"), thorium.ItemType("ShadeWoodTambourine"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTambourine", group);
                //fan letter
                group = new RecipeGroup(() => Lang.misc[37] + " Fan Letter", thorium.ItemType("FanLetter"), thorium.ItemType("FanLetter2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyLetter", group);

                //butterflies
                group = new RecipeGroup(() => Lang.misc[37] + " Dungeon Butterfly", thorium.ItemType("BlueDungeonButterfly"), thorium.ItemType("GreenDungeonButterfly"), thorium.ItemType("PinkDungeonButterfly"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDungeonButterfly", group);
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case 0: //server side spawning creepers
                    if (Main.netMode == 2)
                    {
                        byte p = reader.ReadByte();
                        int multiplier = reader.ReadByte();
                        int n = NPC.NewNPC((int)Main.player[p].Center.X, (int)Main.player[p].Center.Y, NPCType("CreeperGutted"), 0,
                            p, 0f, multiplier, 0f);
                        if (n != 200)
                        {
                            Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            NetMessage.SendData(23, -1, -1, null, n);
                        }
                    }
                    break;

                case 1: //server side synchronize pillar data request
                    if (Main.netMode == 2)
                    {
                        byte pillar = reader.ReadByte();
                        if (!Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[1])
                        {
                            Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[1] = true;
                            Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().SetDefaults(Main.npc[pillar]);
                            Main.npc[pillar].life = Main.npc[pillar].lifeMax;
                        }
                    }
                    break;

                case 2: //net updating maso
                    FargoSoulsGlobalNPC fargoNPC = Main.npc[reader.ReadByte()].GetGlobalNPC<FargoSoulsGlobalNPC>();
                    fargoNPC.masoBool[0] = reader.ReadBoolean();
                    fargoNPC.masoBool[1] = reader.ReadBoolean();
                    fargoNPC.masoBool[2] = reader.ReadBoolean();
                    fargoNPC.masoBool[3] = reader.ReadBoolean();
                    break;

                case 3: //rainbow slime/paladin, MP clients syncing to server
                    if (Main.netMode == 1)
                    {
                        byte npc = reader.ReadByte();
                        Main.npc[npc].lifeMax = reader.ReadInt32();
                        float newScale = reader.ReadSingle();
                        Main.npc[npc].position = Main.npc[npc].Center;
                        Main.npc[npc].width = (int)(Main.npc[npc].width / Main.npc[npc].scale * newScale);
                        Main.npc[npc].height = (int)(Main.npc[npc].height / Main.npc[npc].scale * newScale);
                        Main.npc[npc].scale = newScale;
                        Main.npc[npc].Center = Main.npc[npc].position;
                    }
                    break;

                case 4: //moon lord vulnerability synchronization
                    if (Main.netMode == 1)
                    {
                        int ML = reader.ReadByte();
                        Main.npc[ML].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter = reader.ReadInt32();
                        FargoSoulsGlobalNPC.masoStateML = reader.ReadByte();
                    }
                    break;

                case 5: //retinazer laser MP sync
                    if (Main.netMode == 1)
                    {
                        int reti = reader.ReadByte();
                        Main.npc[reti].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[2] = reader.ReadBoolean();
                        Main.npc[reti].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter = reader.ReadInt32();
                    }
                    break;

                case 6: //shark MP sync
                    if (Main.netMode == 1)
                    {
                        int shark = reader.ReadByte();
                        Main.npc[shark].GetGlobalNPC<FargoSoulsGlobalNPC>().SharkCount = reader.ReadByte();
                    }
                    break;

                case 7: //client to server activate dark caster family
                    if (Main.netMode == 2)
                    {
                        int caster = reader.ReadByte();
                        if (Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 == 0)
                            Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 = reader.ReadInt32();
                    }
                    break;

                case 8: //server to clients reset counter
                    if (Main.netMode == 1)
                    {
                        int caster = reader.ReadByte();
                        Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 = 0;
                    }
                    break;

                case 9: //client to server, request heart spawn
                    if (Main.netMode == 2)
                    {
                        int n = reader.ReadByte();
                        Item.NewItem(Main.npc[n].Hitbox, ItemID.Heart);
                    }
                    break;

                case 10: //client to server, sync cultist data
                    if (Main.netMode == 2)
                    {
                        int cult = reader.ReadByte();
                        FargoSoulsGlobalNPC cultNPC = Main.npc[cult].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        cultNPC.Counter += reader.ReadInt32();
                        cultNPC.Counter2 += reader.ReadInt32();
                        cultNPC.Timer += reader.ReadInt32();
                        Main.npc[cult].localAI[3] += reader.ReadSingle();
                    }
                    break;

                case 11: //refresh creeper
                    if (Main.netMode != 0)
                    {
                        byte player = reader.ReadByte();
                        NPC creeper = Main.npc[reader.ReadByte()];
                        if (creeper.active && creeper.type == NPCType("CreeperGutted") && creeper.ai[0] == player)
                        {
                            int damage = creeper.lifeMax - creeper.life;
                            creeper.life = creeper.lifeMax;
                            if (damage > 0)
                                CombatText.NewText(creeper.Hitbox, CombatText.HealLife, damage);
                            if (Main.netMode == 2)
                                creeper.netUpdate = true;
                        }
                    }
                    break;

                case 12: //prime limbs spin
                    if (Main.netMode == 1)
                    {
                        int n = reader.ReadByte();
                        FargoSoulsGlobalNPC limb = Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        limb.masoBool[2] = reader.ReadBoolean();
                        limb.Counter = reader.ReadInt32();
                        Main.npc[n].localAI[3] = reader.ReadSingle();
                    }
                    break;

                case 13: //prime limbs swipe
                    if (Main.netMode == 1)
                    {
                        int n = reader.ReadByte();
                        FargoSoulsGlobalNPC limb = Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        limb.Counter = reader.ReadInt32();
                        limb.Counter2 = reader.ReadInt32();
                    }
                    break;

                case 14: //golem free head resync
                    if (Main.netMode == 1)
                    {
                        int n = reader.ReadByte();
                        FargoSoulsGlobalNPC head = Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        head.masoBool[0] = reader.ReadBoolean();
                        head.masoBool[1] = reader.ReadBoolean();
                        head.masoBool[2] = reader.ReadBoolean();
                        head.Counter = reader.ReadInt32();
                        head.Counter2 = reader.ReadInt32();
                    }
                    break;

                case 77: //server side spawning fishron EX
                    if (Main.netMode == 2)
                    {
                        byte target = reader.ReadByte();
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        FargoSoulsGlobalNPC.spawnFishronEX = true;
                        NPC.NewNPC(x, y, NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, target);
                        FargoSoulsGlobalNPC.spawnFishronEX = false;
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Duke Fishron EX has awoken!"), new Color(50, 100, 255));
                    }
                    break;

                case 78: //confirming fish EX max life
                    int f = reader.ReadInt32();
                    Main.npc[f].lifeMax = reader.ReadInt32();
                    break;

                default:
                    break;
            }

            //BaseMod Stuff
            MsgType msg = (MsgType)reader.ReadByte();
            if (msg == MsgType.ProjectileHostility) //projectile hostility and ownership
            {
                int owner = reader.ReadInt32();
                int projID = reader.ReadInt32();
                bool friendly = reader.ReadBoolean();
                bool hostile = reader.ReadBoolean();
                if (Main.projectile[projID] != null)
                {
                    Main.projectile[projID].owner = owner;
                    Main.projectile[projID].friendly = friendly;
                    Main.projectile[projID].hostile = hostile;
                }
                if (Main.netMode == 2) MNet.SendBaseNetMessage(0, owner, projID, friendly, hostile);
            }
            else
            if (msg == MsgType.SyncAI) //sync AI array
            {
                int classID = reader.ReadByte();
                int id = reader.ReadInt16();
                int aitype = reader.ReadByte();
                int arrayLength = reader.ReadByte();
                float[] newAI = new float[arrayLength];
                for (int m = 0; m < arrayLength; m++)
                {
                    newAI[m] = reader.ReadSingle();
                }
                if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].modNPC != null && Main.npc[id].modNPC is ParentNPC)
                {
                    ((ParentNPC)Main.npc[id].modNPC).SetAI(newAI, aitype);
                }
                else
                if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].modProjectile != null && Main.projectile[id].modProjectile is ParentProjectile)
                {
                    ((ParentProjectile)Main.projectile[id].modProjectile).SetAI(newAI, aitype);
                }
                if (Main.netMode == 2) BaseNet.SyncAI(classID, id, newAI, aitype);
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.musicVolume != 0 && Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (MMWorld.MMArmy && priority <= MusicPriority.Environment)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MonsterMadhouse");
                    priority = MusicPriority.Event;
                }
                if (FargoSoulsGlobalNPC.BossIsAlive(ref FargoSoulsGlobalNPC.mutantBoss, ModContent.NPCType<NPCs.MutantBoss.MutantBoss>())
                    && Main.player[Main.myPlayer].Distance(Main.npc[FargoSoulsGlobalNPC.mutantBoss].Center) < 3000)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/SteelRed");
                    priority = (MusicPriority)12;
                }
            }
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
                   (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;
        }

        public static bool NoZone(NPCSpawnInfo spawnInfo)
        {
            return NoZoneAllowWater(spawnInfo) && !spawnInfo.water;
        }

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && NoInvasion(spawnInfo);
        }

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZone(spawnInfo);
        }

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);
        }

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
        }


        #region Compatibilities

        internal CalamityCompatibility CalamityCompatibility { get; private set; }
        internal bool CalamityLoaded => CalamityCompatibility != null;

        internal ThoriumCompatibility ThoriumCompatibility { get; private set; }
        internal bool ThoriumLoaded => ThoriumCompatibility != null;

        internal SoACompatibility SoACompatibility { get; private set; }
        internal bool SoALoaded => SoACompatibility != null;


        //internal FargowiltasCompatibility FargowiltasCompatibility { get; private set; }
        //internal bool FargowiltasLoaded => FargowiltasCompatibility != null;

        internal MasomodeEXCompatibility MasomodeEXCompatibility { get; private set; }
        internal bool MasomodeEXLoaded => MasomodeEXCompatibility != null;


        internal DBZMODCompatibility DBZMODCompatibility { get; private set; }
        internal bool DBZMODLoaded => DBZMODCompatibility != null;

        internal ApothTestModCompatibility ApothCompatibility { get; private set; }
        internal bool ApothLoaded => ApothCompatibility != null;

        #endregion
    }

    enum MsgType : byte
    {
        ProjectileHostility,
        SyncAI
    }
}
