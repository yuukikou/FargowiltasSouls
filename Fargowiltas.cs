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
            
            AddToggle("WoodHeader", "Force of Timber", "TimberForce", "ffffff");
            AddToggle("BorealConfig", "Boreal Snowballs", "BorealWoodEnchant", "8B7464");
            AddToggle("MahoganyConfig", "Mahogany Hook Speed", "RichMahoganyEnchant", "b56c64");
            AddToggle("EbonConfig", "Ebonwood Shadowflame", "EbonwoodEnchant", "645a8d");
            AddToggle("ShadeConfig", "Blood Geyser On Hit", "ShadewoodEnchant", "586876");
            AddToggle("PalmConfig", "Palmwood Sentry", "PalmWoodEnchant", "b78d56");
            AddToggle("PearlConfig", "Pearlwood Rain", "PearlwoodEnchant", "ad9a5f");

            AddToggle("EarthHeader", "Force of Earth", "EarthForce", "ffffff");
            AddToggle("AdamantiteConfig", "Adamantite Projectile Splitting", "AdamantiteEnchant", "dd557d");
            AddToggle("CobaltConfig", "Cobalt Shards", "CobaltEnchant", "3da4c4");
            AddToggle("MythrilConfig", "Mythril Weapon Speed", "MythrilEnchant", "9dd290");
            AddToggle("OrichalcumConfig", "Orichalcum Fireballs", "OrichalcumEnchant", "eb3291");
            AddToggle("PalladiumConfig", "Palladium Healing", "PalladiumEnchant", "f5ac28");
            AddToggle("TitaniumConfig", "Titanium Shadow Dodge", "TitaniumEnchant", "828c88");

            AddToggle("TerraHeader", "Terra Force", "TerraForce", "ffffff");
            AddToggle("CopperConfig", "Copper Lightning", "CopperEnchant", "d56617");
            AddToggle("IronMConfig", "Iron Magnet", "IronEnchant", "988e83");
            AddToggle("IronSConfig", "Iron Shield", "IronEnchant", "988e83");
            AddToggle("CthulhuShield", "Shield of Cthulhu", "IronEnchant", "988e83");
            AddToggle("TinConfig", "Tin Crits", "TinEnchant", "a28b4e");
            AddToggle("TungstenConfig", "Tungsten Effect", "TungstenEnchant", "b0d2b2");

            AddToggle("WillHeader", "Force of Will", "WillForce", "ffffff");
            AddToggle("GladiatorConfig", "Gladiator Rain", "GladiatorEnchant", "9c924e");
            AddToggle("GoldConfig", "Gold Lucky Coin", "GoldEnchant", "e7b21c");
            AddToggle("RedRidingConfig", "Red Riding Super Bleed", "RedRidingEnchant", "c01b3c");
            AddToggle("ValhallaConfig", "Valhalla Knockback", "ValhallaKnightEnchant", "93651e");

            AddToggle("LifeHeader", "Force of Life", "LifeForce", "ffffff");
            AddToggle("BeetleConfig", "Beetles", "BeetleEnchant", "6D5C85");
            AddToggle("CactusConfig", "Cactus Needles", "CactusEnchant", "799e1d");
            AddToggle("PumpkinConfig", "Pumpkin Fire", "PumpkinEnchant", "e3651c");
            AddToggle("SpiderConfig", "Spider Swarm", "SpiderEnchant", "6d4e45");
            AddToggle("TurtleConfig", "Turtle Shell Buff", "TurtleEnchant", "f89c5c");

            AddToggle("NatureHeader", "Force of Nature", "NatureForce", "ffffff");
            AddToggle("ChlorophyteConfig", "Chlorophyte Leaf Crystal", "ChlorophyteEnchant", "248900");
            AddToggle("CrimsonConfig", "Crimson Regen", "CrimsonEnchant", "C8364B");
            AddToggle("FrostConfig", "Frost Icicles", "FrostEnchant", "7abdb9");
            AddToggle("JungleConfig", "Jungle Spores", "JungleEnchant", "71971f");
            AddToggle("MoltenConfig", "Molten Inferno Buff", "MoltenEnchant", "c12b2b");
            AddToggle("ShroomiteConfig", "Shroomite Stealth", "ShroomiteEnchant", "008cf4");

            AddToggle("ShadowHeader", "Shadow Force", "ShadowForce", "ffffff");
            AddToggle("DarkArtConfig", "Dark Artist Effect", "DarkArtistEnchant", "9b5cb0");
            AddToggle("NecroConfig", "Necro Guardian", "NecroEnchant", "565643");
            AddToggle("ShadowConfig", "Shadow Darkness", "ShadowEnchant", "42356f");
            AddToggle("ShinobiConfig", "Shinobi Through Walls", "ShinobiEnchant", "935b18");
            AddToggle("ShinobiTabiConfig", "Tabi Dash", "ShinobiEnchant", "935b18");
            AddToggle("SpookyConfig", "Spooky Scythes", "SpookyEnchant", "644e74");

            AddToggle("SpiritHeader", "Force of Spirit", "SpiritForce", "ffffff");
            AddToggle("ForbiddenConfig", "Forbidden Storm", "ForbiddenEnchant", "e7b21c");
            AddToggle("HallowedConfig", "Hallowed Enchanted Sword Familiar", "HallowEnchant", "968564");
            AddToggle("HalllowSConfig", "Hallowed Shield", "HallowEnchant", "968564");
            AddToggle("SilverConfig", "Silver Sword Familiar", "SilverEnchant", "b4b4cc");
            AddToggle("SpectreConfig", "Spectre Orbs", "SpectreEnchant", "accdfc");
            AddToggle("TikiConfig", "Tiki Minions", "TikiEnchant", "56A52B");

            AddToggle("CosmoHeader", "Force of Cosmos", "CosmoForce", "ffffff");
            AddToggle("MeteorConfig", "Meteor Shower", "MeteorEnchant", "5f4752");
            AddToggle("NebulaConfig", "Nebula Boosters", "NebulaEnchant", "fe7ee5");
            AddToggle("SolarConfig", "Solar Shield", "SolarEnchant", "fe9e23");
            AddToggle("StardustConfig", "Stardust Guardian", "StardustEnchant", "00aeee");
            AddToggle("VortexSConfig", "Vortex Stealth", "VortexEnchant", "00f2aa");
            AddToggle("VortexVConfig", "Vortex Voids", "VortexEnchant", "00f2aa");

            #endregion

            #region masomode toggles

            //Masomode Header
            AddToggle("MasoBossRecolors", "Boss Recolors (Restart to use)", "Masochist", "ffffff");

            AddToggle("MasoSlimeConfig", "Slimy Shield Effects", "SlimyShield", "ffffff");
            AddToggle("MasoEyeConfig", "Scythes When Dashing", "AgitatedLens", "ffffff");
            AddToggle("MasoSkeleConfig", "Skeletron Arms Minion", "NecromanticBrew", "ffffff");

            AddToggle("MasoEaterConfig", "Tiny Eaters", "CorruptHeart", "ffffff");
            AddToggle("MasoBrainConfig", "Creeper Shield", "GuttedHeart", "ffffff");

            AddToggle("MasoConcoctionConfig", "Tim's Concoction", "TimsConcoction", "ffffff");
            AddToggle("MasoRainbowConfig", "Rainbow Slime Minion", "ConcentratedRainbowMatter", "ffffff");
            AddToggle("MasoFrigidConfig", "Frostfireballs", "FrigidGemstone", "ffffff");
            AddToggle("MasoNymphConfig", "Attacks Spawn Hearts", "NymphPerfume", "ffffff");
            AddToggle("MasoSqueakConfig", "Squeaky Toy On Hit", "SqueakyToy", "ffffff");
            AddToggle("MasoPouchConfig", "Tentacles On Hit", "WretchedPouch", "ffffff");
            AddToggle("MasoClippedConfig", "Inflict Clipped Wings", "DragonFang", "ffffff");

            AddToggle("MasoLightningConfig", "Inflict Lightning Rod", "SecurityWallet", "ffffff");
            AddToggle("MasoProbeConfig", "Probes Minion", "SecurityWallet", "ffffff");

            AddToggle("MasoGravConfig", "Gravity Control", "SecurityWallet", "ffffff");
            AddToggle("MasoGrav2Config", "Stabilized Gravity", "SecurityWallet", "ffffff");
            AddToggle("MasoPump", "Pumpking's Cape Support", "SecurityWallet", "ffffff");
            AddToggle("MasoFlockoConfig", "Flocko Minion", "SecurityWallet", "ffffff");
            AddToggle("MasoUfoConfig", "Saucer Minion", "SecurityWallet", "ffffff");
            AddToggle("MasoTrueEyeConfig", "True Eyes Minion", "SecurityWallet", "ffffff");

            AddToggle("MasoCelestConfig", "Celestial Rune Support", "SecurityWallet", "ffffff");
            AddToggle("MasoPlantConfig", "Plantera Minion", "SecurityWallet", "ffffff");
            AddToggle("MasoGolemConfig", "Lihzahrd Ground Pound", "SecurityWallet", "ffffff");
            AddToggle("MasoVisionConfig", "Ancient Visions On Hit", "SecurityWallet", "ffffff");
            AddToggle("MasoCultistConfig", "Cultist Minion", "SecurityWallet", "ffffff");
            AddToggle("MasoFishronConfig", "Spectral Fishron", "SecurityWallet", "ffffff");
            AddToggle("MasoPugentConfig", "Pungent Eye Minion", "SecurityWallet", "ffffff");

            AddToggle("MasoAbomConfig", "Abominationn Minion", "SecurityWallet", "ffffff");
            AddToggle("MasoRingConfig", "Phantasmal Ring Minion", "SecurityWallet", "ffffff");

            AddToggle("MasoSpikeConfig", "Spiky Balls On Hit", "SecurityWallet", "ffffff");
            AddToggle("MasoIconConfig", "Sinister Icon", "SecurityWallet", "ffffff");
            
            AddToggle("WalletHeader", "Security Wallet", "SecurityWallet", "ffffff");

            #endregion

            #region pet toggles
            
            AddToggle("PetCatConfig", "Black Cat Pet", 1810, "ffffff");
            AddToggle("PetCubeConfig", "Companion Cube Pet", 3628, "ffffff");
            AddToggle("PetCurseSapConfig", "Cursed Sapling Pet", 1837, "ffffff");
            AddToggle("PetDinoConfig", "Dino Pet", 1242, "ffffff");
            AddToggle("PetDragonConfig", "Dragon Pet", 3857, "ffffff");
            AddToggle("PetEaterConfig", "Eater Pet", 994, "ffffff");
            AddToggle("PetEyeSpringConfig", "Eye Spring Pet", 1311, "ffffff");
            AddToggle("PetFaceMonsterConfig", "Face Monster Pet", 3060, "ffffff");
            AddToggle("PetGatoConfig", "Gato Pet", 3855, "ffffff");
            AddToggle("PetHornetConfig", "Hornet Pet", 1170, "ffffff");
            AddToggle("PetLizardConfig", "Lizard Pet", 1172, "ffffff");
            AddToggle("PetMinitaurConfig", "Mini Minotaur Pet", 2587, "ffffff");
            AddToggle("PetParrotConfig", "Parrot Pet", 1180, "ffffff");
            AddToggle("PetPenguinConfig", "Penguin Pet", 669, "ffffff");
            AddToggle("PetPupConfig", "Puppy Pet", 1927, "ffffff");
            AddToggle("PetSeedConfig", "Seedling Pet", 1182, "ffffff");
            AddToggle("PetDGConfig", "Skeletron Pet", 1169, "ffffff");
            AddToggle("PetSnowmanConfig", "Snowman Pet", 1312, "ffffff");
            AddToggle("PetSpiderConfig", "Spider Pet", 1798, "ffffff");
            AddToggle("PetSquashConfig", "Squashling Pet", 1799, "ffffff");
            AddToggle("PetTikiConfig", "Tiki Pet", 1171, "ffffff");
            AddToggle("PetShroomConfig", "Truffle Pet", 1181, "ffffff");
            AddToggle("PetTurtleConfig", "Turtle Pet", 753, "ffffff");
            AddToggle("PetZephyrConfig", "Zephyr Fish Pet", 2420, "ffffff");
            AddToggle("PetHeartConfig", "Crimson Heart Pet", 3062, "ffffff");
            AddToggle("PetNaviConfig", "Fairy Pet", 425, "ffffff");
            AddToggle("PetFlickerConfig", "Flickerwick Pet", 3856, "ffffff");
            AddToggle("PetLanturnConfig", "Magic Lantern Pet", 3043, "ffffff");
            AddToggle("PetOrbConfig", "Shadow Orb Pet", 115, "ffffff");
            AddToggle("PetSuspEyeConfig", "Suspicious Eye Pet", 3577, "ffffff");
            AddToggle("PetWispConfig", "Wisp Pet", 1183, "ffffff");

            #endregion

            #region soul toggles

            AddToggle("MeleeConfig", "Melee Speed", "GladiatorsSoul", "ffffff");
            AddToggle("SniperConfig", "Sniper Scope", "SharpshootersSoul", "ffffff");
            AddToggle("UniverseConfig", "Universe Attack Speed", "UniverseSoul", "ffffff");
            AddToggle("MiningHuntConfig", "Mining Hunter Buff", "MinerEnchant", "ffffff");
            AddToggle("MiningDangerConfig", "Mining Dangersense Buff", "MinerEnchant", "ffffff");
            AddToggle("MiningSpelunkConfig", "Mining Spelunker Buff", "MinerEnchant", "ffffff");
            AddToggle("MiningShineConfig", "Mining Shine Buff", "MinerEnchant", "ffffff");
            AddToggle("BuilderConfig", "Builder Mode", "WorldShaperSoul", "ffffff");
            AddToggle("DefenseSporeConfig", "Spore Sac", "ColossusSoul", "ffffff");
            AddToggle("DefenseStarConfig", "Stars On Hit", "ColossusSoul", "ffffff");
            AddToggle("DefenseBeeConfig", "Bees On Hit", "ColossusSoul", "ffffff");
            AddToggle("SupersonicConfig", "Supersonic Speed Boosts", "SupersonicSoul", "ffffff");
            AddToggle("EternityConfig", "Eternity Stacking", "EternitySoul", "ffffff");

            #endregion

            #region thorium
            //thorium header

            AddToggle("ThoriumAirWalkersConfig", "Air Walkers", "SupersonicSoul", "ffffff");
            AddToggle("ThoriumCrystalScorpionConfig", "Crystal Scorpion", "ConjuristsSoul", "ffffff");
            AddToggle("ThoriumYumasPendantConfig", "Yuma's Pendant", "ConjuristsSoul", "ffffff");
            AddToggle("ThoriumHeadMirrorConfig", "Head Mirror", "GuardianAngelsSoul", "ffffff");
            AddToggle("ThoriumCelestialAuraConfig", "Celestial Aura", "CelestialEnchant", "ffffff");
            AddToggle("ThoriumAscensionStatueConfig", "Ascension Statuette", "CelestialEnchant", "ffffff");
            AddToggle("ThoriumManaBootsConfig", "Mana-Charged Rocketeers", "MalignantEnchant", "ffffff");
            AddToggle("ThoriumBronzeLightningConfig", "Bronze Lightning", "BronzeEnchant", "ffffff");
            AddToggle("ThoriumIllumiteMissileConfig", "Illumite Missile", "IllumiteEnchant", "ffffff");
            AddToggle("ThoriumJesterBellConfig", "Jester Bell", "JesterEnchant", "ffffff");
            AddToggle("ThoriumBeholderEyeConfig", "Eye of the Beholder", "ValadiumEnchant", "ffffff");
            AddToggle("ThoriumTerrariumSpiritsConfig", "Terrarium Spirits", "TerrariumEnchant", "ffffff");
            AddToggle("ThoriumCrietzConfig", "Crietz", "ThoriumEnchant", "ffffff");
            AddToggle("ThoriumYewCritsConfig", "Yew Wood Crits", "YewWoodEnchant", "ffffff");
            AddToggle("ThoriumCryoDamageConfig", "Cryo-Magus Damage", "CryoMagusEnchant", "ffffff");
            AddToggle("ThoriumWhiteDwarfConfig", "White Dwarf Flares", "WhiteDwarfEnchant", "ffffff");
            AddToggle("ThoriumTideFoamConfig", "Tide Hunter Foam", "TideHunterEnchant", "ffffff");
            AddToggle("ThoriumWhisperingTentaclesConfig", "Whispering Tentacles", "WhisperingEnchant", "ffffff");
            AddToggle("ThoriumIcyBarrierConfig", "Icy Barrier", "IcyEnchant", "ffffff");
            AddToggle("ThoriumPlagueFlaskConfig", "Plague Lord's Flask", "PlagueDoctorEnchant", "ffffff");
            AddToggle("ThoriumTideGlobulesConfig", "Tide Turner Globules", "TideTurnerEnchant", "ffffff");
            AddToggle("ThoriumTideDaggersConfig", "Tide Turner Daggers", "TideTurnerEnchant", "ffffff");
            AddToggle("ThoriumFolvAuraConfig", "Folv's Aura", "FolvEnchant", "ffffff");
            AddToggle("ThoriumFolvBoltsConfig", "Folv's Bolts", "FolvEnchant", "ffffff");
            AddToggle("ThoriumVampireGlandConfig", "Vampire Gland", "FleshEnchant", "ffffff");
            AddToggle("ThoriumFleshDropsConfig", "Flesh Drops", "FleshEnchant", "ffffff");
            AddToggle("ThoriumDragonFlamesConfig", "Dragon Flames", "DragonEnchant", "ffffff");
            AddToggle("ThoriumHarbingerOverchargeConfig", "Harbinger Overcharge", "HarbingerEnchant", "ffffff");
            AddToggle("ThoriumAssassinDamageConfig", "Assassin Damage", "AssassinEnchant", "ffffff");
            AddToggle("ThoriumpyromancerBurstsConfig", "Pyromancer Bursts", "PyromancerEnchant", "ffffff");
            AddToggle("ThoriumConduitShieldConfig", "Conduit Shield", "ConduitEnchant", "ffffff");
            AddToggle("ThoriumIncandescentSparkConfig", "Incandescent Spark", "DurasteelEnchant", "ffffff");
            AddToggle("ThoriumGreedyMagnetConfig", "Greedy Magnet", "DurasteelEnchant", "ffffff");
            AddToggle("ThoriumCyberStatesConfig", "Cyber Punk States", "CyberPunkEnchant", "ffffff");
            AddToggle("ThoriumMetronomeConfig", "Metronome", "ConductorEnchant", "ffffff");
            AddToggle("ThoriumMixTapeConfig", "Mix Tape", "NobleEnchant", "ffffff");
            AddToggle("ThoriumLodestoneConfig", "Lodestone Resistance", "LodestoneEnchant", "ffffff");
            AddToggle("ThoriumBiotechProbeConfig", "Biotech Probe", "BiotechEnchant", "ffffff");
            AddToggle("ThoriumProofAvariceConfig", "Proof of Avarice", "GoldEnchant", "ffffff");
            AddToggle("ThoriumSlagStompersConfig", "Slag Stompers", "MagmaEnchant", "ffffff");
            AddToggle("ThoriumSpringStepsConfig", "Spring Steps", "MagmaEnchant", "ffffff");
            AddToggle("ThoriumBerserkerConfig", "Berserker Effect", "BerserkerEnchant", "ffffff");
            AddToggle("ThoriumBeeBootiesConfig", "Bee Booties", "BeeEnchant", "ffffff");
            AddToggle("ThoriumGhastlyCarapaceConfig", "Ghastly Carapace", "SpectreEnchant", "ffffff");
            AddToggle("ThoriumSpiritWispsConfig", "Spirit Trapper Wisps", "SpiritTrapperEnchant", "ffffff");
            AddToggle("ThoriumWarlockWispsConfig", "Warlock Wisps", "WarlockEnchant", "ffffff");
            AddToggle("ThoriumDreadConfig", "Dread Speed", "DreadEnchant", "ffffff");
            AddToggle("ThoriumDiverConfig", "Spawn Divers", "ThoriumEnchant", "ffffff");
            AddToggle("ThoriumDemonBloodConfig", "Demon Blood Effect", "DemonBloodEnchant", "ffffff");
            AddToggle("ThoriumDevilMinionConfig", "Li'l Devil Minion", "WarlockEnchant", "ffffff");
            AddToggle("ThoriumCherubMinionConfig", "Li'l Cherub Minion", "SacredEnchant", "ffffff");
            AddToggle("ThoriumSaplingMinionConfig", "Sapling Minion", "LivingWoodEnchant", "ffffff");
            AddToggle("ThoriumOmegaPetConfig", "Omega Pet", "ConduitEnchant", "ffffff");
            AddToggle("ThoriumIFOPetConfig", "I.F.O. Pet", "ConduitEnchant", "ffffff");
            AddToggle("ThoriumBioFeederPetConfig", "Bio-Feeder Pet", "MeteorEnchant", "ffffff");
            AddToggle("ThoriumBlisterPetConfig", "Blister Pet", "FleshEnchant", "ffffff");
            AddToggle("ThoriumWyvernPetConfig", "Wyvern Pet", "DragonEnchant", "ffffff");
            AddToggle("ThoriumLanternPetConfig", "Inspiring Lantern Pet", "GeodeEnchant", "ffffff");
            AddToggle("ThoriumBoxPetConfig", "Lock Box Pet", "GeodeEnchant", "ffffff");
            AddToggle("ThoriumSpiritPetConfig", "Life Spirit Pet", "SacredEnchant", "ffffff");
            AddToggle("ThoriumGoatPetConfig", "Holy Goat Pet", "LifeBinderEnchant", "ffffff");
            AddToggle("ThoriumOwlPetConfig", "Owl Pet", "CryoMagusEnchant", "ffffff");
            AddToggle("ThoriumJellyfishPetConfig", "Jellyfish Pet", "DepthDiverEnchant", "ffffff");
            AddToggle("ThoriumMooglePetConfig", "Moogle Pet", "WhiteKnightEnchant", "ffffff");
            AddToggle("ThoriumMaidPetConfig", "Maid Pet", "DreamWeaverEnchant", "ffffff");
            AddToggle("ThoriumSlimePetConfig", "Pink Slime Pet", "IllumiteEnchant", "ffffff");
            AddToggle("ThoriumGlitterPetConfig", "Glitter Pet", "PlatinumEnchant", "ffffff");
            AddToggle("ThoriumCoinPetConfig", "Coin Bag Pet", "GoldEnchant", "ffffff");

        #endregion

            #region calamity
            //calamity header
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

        //for vanilla items reeeee
        public void AddToggle(String toggle, String name, int item, String color)
        {
            ModTranslation text = CreateTranslation(toggle);
            text.SetDefault("[i:" + item + "][c/" + color + ": " + name + "]");
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
