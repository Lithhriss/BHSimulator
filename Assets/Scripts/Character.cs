using System.Collections.Generic;
using System;
using System.Linq;

public enum PetType
{
    None,
    //legendary
    Nelson,
    Gemmi,
    Boogie,
    Nemo,
    Crem,
    Boiguh,
    Nerder,
    Quimby,
    Snut,
    Wuvboi,
    Buvboi,
    Skulldemort,
    Toebert,
    Urgoff,
    Fuvboi,
    Karlorr,
    Pumkwim,
    //epic
    EpicBoogie,
    EpicNemo,
    EpicNerder,
    EpicPumkwim,
    EpicCrem,
    EpicSnut,
    Pritza,
    Sparklez,
    Dug,
    Gumgum,
    Phony,
    Waldo,
    Beanz,
    Rutledge,
    Log,
    Melvin,
    Bryan,
    Nacl,
    Mewwo,
    Gusty,
    Dot
}

public class Character
{
    public static Random random = new Random(Guid.NewGuid().GetHashCode());


    public bool _isHero
    {
        get
        {
            if (weapon != Weapon.None) return true;
            else return false;
        }
    }
    public string name;
    // Base Stats
    public int power;
    public int stamina;
    public int agility;

    public List<Skill> skillList = new List<Skill>();
    public double priority;

    // Combat Stats
    public int hp;
    public int maxHp;
    public int sp;
    private float spBonus;
    public int shield;
    public int maxShield;
    public float hpPerc;
    public float turnRate;
    public float interval;
    public float counter;

    // Specials
    public float critChance;
    public float critDamage;
    public float empowerChance;
    public float dsChance;
    public float blockChance;
    public float evadeChance;
    public float deflectChance;
    public float absorbChance;

    // Runes
    public float powerRunes;
    public float staminaRunes;
    public float agilityRunes;
    public float lifeSteal;
    public float damageReduction;

    // state
    public bool alive { get { return hp > 0; } }
    public bool drain;
    public bool unity;
    public bool redirect;
    public bool redirectRune;
    public bool bushidoBonus;
    public bool aresBonus;
    public bool lunarBonus;
    public bool selfInjure;

    //mythic bonuses
    public bool necrosisBonus;
    public bool hysteriaBonus;
    public bool nightVisageBonus;
    public bool consumptionBonus;
    public bool decayBonus;

    // Pet
    public Pet pet;
    public int PetLevel;
    public PetProcType petProcType;
    public string pets;
    public PetType petName;
    public Weapon weapon;
    public enum Weapon
    {
        None,
        Bow,
        Spear,
        Sword,
        Staff,
        Axe,
        Laser,
        DemonStaff
    }
    public MetaRune metaRune;
    public enum MetaRune
    {
        None,
        Redirect,
        spRegen

    }
    public DivinityBonus divinityBonus;
    public enum DivinityBonus
    {
        None,
        Bonus_2_of_3,
        Bonus_3_of_3
    }

    //Set Bonuses
    public ObliterationBonus obliterationBonus;
    public enum ObliterationBonus
    {
        None,
        Bonus_2_of_4,
        Bonus_3_of_4,
        Bonus_4_of_4
    }

    public MARUBonus maruBonus;
    public enum MARUBonus
    {
        None,
        Bonus_2_of_4,
        Bonus_3_of_4,
        Bonus_4_of_4
    }

    public ConductionBonus conductionBonus;
    public enum ConductionBonus
    {
        None,
        Bonus_2_of_4,
        Bonus_3_of_4,
        Bonus_4_of_4
    }

    public TatersBonus tatersBonus;
    public enum TatersBonus
    {
        None,
        Bonus_2_of_3,
        Bonus_3_of_3
    }

    public IllustriousBonus illustriousBonus;
    public enum IllustriousBonus
    {
        None,
        Bonus_2_of_3,
        Bonus_3_of_3
    }

    //p2w bonuses
    public bool gateKeeperBonus;


    public Character(float pow, float sta, float agi, float crit, float critdmg, float emp, float ds, float block, float evade, float deflect, float absorb, float prunes, float starunes, float agirunes, float redrunes, int diffMod, double prior, bool ishero, string nam = null)
    {
        power = Convert.ToInt32(pow * diffMod);
        stamina = Convert.ToInt32(sta * diffMod);
        agility = Convert.ToInt32(agi * diffMod);
        critChance = crit;
        critDamage = (100f + critdmg) / 100f;
        empowerChance = emp;
        dsChance = ds;
        blockChance = block;
        evadeChance = evade;
        deflectChance = deflect;
        absorbChance = absorb;
        powerRunes = (100f + prunes) / 100f;
        staminaRunes = (100f + starunes) / 100f;
        agilityRunes = (100f + agirunes) / 100f;
        damageReduction = (100f - redrunes) / 100f;
        priority = prior;
        drain = false;
        selfInjure = false;
        name = nam;
    }
    public Character()
    { }

    // Predefined 
    public static readonly Dictionary<string, Character> predefined = new Dictionary<string, Character>() {
        {
            "Default Hero",
            new Character {
                // Base Stats
                power         = 500,
                stamina       = 500,
                agility       = 500,
                // Specials
                critChance    = 10,
                critDamage    = 50f,
                dsChance      = 0,
                blockChance   = 0f,
                evadeChance   = 2.5f,
                deflectChance = 0f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                petName           = PetType.None,
                weapon        = Weapon.None,
                metaRune      = MetaRune.None,
                // set bonuses
                unity         = false,
                //divinityBonus = true
            }
        },
              {
            "Default DPS",
            new Character {
                // Base Stats
                power         = 1000,
                stamina       = 250,
                agility       = 400,
                // Specials
                critChance    = 17,
                critDamage    = 50f,
                dsChance      = 20,
                blockChance   = 0f,
                evadeChance   = 2.5f,
                deflectChance = 0f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 4f,
                staminaRunes  = 0f,
                agilityRunes  = 4f,
                // Pet
                petName           = PetType.Gemmi,
                weapon        = Weapon.Sword,
                metaRune      = MetaRune.None,
                // set bonuses
                unity         = true,
                bushidoBonus  = true,
                divinityBonus = DivinityBonus.Bonus_2_of_3
            }
        },
               {
            "Default Tank",
            new Character {
                // Base Stats
                power         = 700,
                stamina       = 1000,
                agility       = 400,
                // Specials
                critChance    = 10,
                critDamage    = 50f,
                dsChance      = 0,
                blockChance   = 36f,
                evadeChance   = 12.5f,
                deflectChance = 8f,
                absorbChance  = 6f,
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                petName           = PetType.Boogie,
                weapon        = Weapon.Staff,
                metaRune      = MetaRune.Redirect,
                // set bonuses
                obliterationBonus = ObliterationBonus.Bonus_4_of_4,
                decayBonus = true
            }
        },
        {
            "Shadown88",
            new Character {
                // Base Stats
                power         = 1350,
                stamina       = 540,
                agility       = 480,
                // Specials
                critChance    = 15f,
                critDamage    = 100f,
                dsChance      = 10f,
                blockChance   = 0f,
                evadeChance   = 2.5f,
                deflectChance = 0f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 10.5f,
                staminaRunes  = 0f,
                agilityRunes  = 10f,
                // Pet
                petName           = PetType.Boiguh,
                weapon        = Weapon.Spear,
                metaRune      = MetaRune.spRegen,
                // set bonuses
                necrosisBonus = true,
                conductionBonus = ConductionBonus.Bonus_4_of_4
                //divinityBonus = true
            }
        },
        {
            "SSS1",
            new Character {
                // Base Stats
                power         = 600,
                stamina       = 205,
                agility       = 555,
                // Specials
                critChance    = 25f,
                critDamage    = 50f,
                dsChance      = 10f,
                blockChance   = 0f,
                evadeChance   = 2.5f,
                deflectChance = 0f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 16f,
                staminaRunes  = 0f,
                agilityRunes  = 2.5f,
                // Pet
                petName           = PetType.Nelson,
                weapon        = Weapon.Bow
            }
        },
        {
            "Tobeyg44",
            new Character {
                // Base Stats
                power         = 600,
                stamina       = 205,
                agility       = 600,
                // Specials
                critChance    = 29f,
                critDamage    = 50f,
                dsChance      = 7.5f,
                blockChance   = 0f,
                evadeChance   = 2.5f,
                deflectChance = 0f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 22f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                petName           = PetType.Nelson,
                weapon        = Weapon.Bow
            }
        },
        {
            "SilskeofGH",
            new Character {
                // Base Stats
                power         = 452,
                stamina       = 704,
                agility       = 101,
                // Specials
                critChance    = 10f,
                critDamage    = 50f,
                dsChance      = 2.5f,
                blockChance   = 31f,
                evadeChance   = 14f,
                deflectChance = 5f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                petName           = PetType.Gemmi,
                weapon        = Weapon.Bow
            }
        },
        {
            "Borealis",
            new Character {
                // Base Stats
                power         = 100,
                stamina       = 1010,
                agility       = 100,
                // Specials
                critChance    = 10f,
                critDamage    = 50f,
                dsChance      = 2.5f,
                blockChance   = 40f,
                evadeChance   = 12.5f,
                deflectChance = 5f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                petName           = PetType.Gemmi,
                weapon        = Weapon.Bow,
                metaRune      = MetaRune.Redirect
            }
        }

    };

    public void InitialiseHero()
    {
        powerRunes = (100f + powerRunes) / 100f;
        agilityRunes = (100f + agilityRunes) / 100f;
        critDamage = (100f + critDamage) / 100f;
        staminaRunes = (100f + staminaRunes) / 100f;
        damageReduction = (100f - damageReduction) / 100f;
        turnRate = Logic.TurnRate(power, agility);
        power = Convert.ToInt32(power * powerRunes);
        turnRate *= agilityRunes;
        hp = Convert.ToInt32(stamina * 10 * staminaRunes);
        maxHp = hp;
        maxShield = Convert.ToInt32(maxHp / 2);
        interval = 100 / turnRate;
        counter = 0;
        sp = 0;
        drain = false;
        AttributeHeroSkills();
        InitialisePet();
    }

    private void InitialisePet()
    {
        switch (petName)
        {
            case PetType.Gemmi:
                pet = new Pet((20f + PetLevel * 0.5f), 36f, 10f, PetAbilty.TeamHeal, PetProcType.AllType);
                break;
            case PetType.Nelson:
                pet = new Pet((20f + PetLevel * 0.5f), 95f, 30f, PetAbilty.ClosestAttack, PetProcType.AllType);
                break;
            case PetType.Boiguh:
                pet = new Pet((20f + PetLevel * 0.5f), 30f, 10f, PetAbilty.TeamShield, PetProcType.AllType);
                break;
            case PetType.Quimby:
                pet = new Pet((20f + PetLevel * 0.5f), 80f, 40f, PetAbilty.WeakestAttack, PetProcType.AllType);
                break;
            case PetType.Wuvboi:
                pet = new Pet((20f + PetLevel * 0.5f), 17f, 10f, PetAbilty.TeamHealShield, PetProcType.AllType);
                break;
            case PetType.Buvboi:
                pet = new Pet((20f + PetLevel * 0.5f), 115f, 80f, PetAbilty.RandomAttack, PetProcType.AllType);
                break;
            case PetType.Skulldemort:
                pet = new Pet((20f + PetLevel * 0.5f), 72f, 20f, PetAbilty.WeakestHeal, PetProcType.AllType);
                break;
            case PetType.Fuvboi:
                pet = new Pet((10f + PetLevel * 0.25f), 34f, 20f, PetAbilty.TeamHealShield, PetProcType.AllType);
                break;
            case PetType.Toebert:
                float toeBertScaling = 11.35f;
                if (petProcType != PetProcType.AllType)
                {
                    if (petProcType == PetProcType.PerHit) toeBertScaling = 10.4f;
                    else toeBertScaling = 11.7f;
                }
                pet = new Pet((petProcType == PetProcType.AllType? 30f + PetLevel * 0.75f : 61.5f + PetLevel * 1.5f), toeBertScaling, 10f, PetAbilty.TeamHealShield, petProcType);
                break;
            case PetType.Urgoff:
                float urgoffScaling = 48f;
                if (petProcType == PetProcType.PerHit) urgoffScaling = 43f;
                pet = new Pet((petProcType == PetProcType.AllType ? 30f + PetLevel * 0.75f : 61.5f + PetLevel * 1.5f), urgoffScaling, 10f, PetAbilty.SpreahHeal, petProcType);
                break;
            case PetType.Karlorr:
                pet = new Pet((petProcType == PetProcType.AllType ? 30f + PetLevel * 0.75f : 61.5f + PetLevel * 1.5f), 53f, 10f, PetAbilty.TeamHeal, petProcType);
                break;
            case PetType.Boogie:
                pet = new Pet((20f + PetLevel * 0.5f), 72f, 10f, PetAbilty.SpreahHeal, PetProcType.AllType);
                break;
            case PetType.Nemo:
                pet = new Pet((10f + PetLevel * 0.25f), 190f, 10f, PetAbilty.ClosestAttack, PetProcType.AllType);
                break;
            case PetType.Nerder:
                pet = new Pet((10f + PetLevel * 0.25f), 108f, 20f, PetAbilty.SelfHeal, PetProcType.AllType);
                break;
            case PetType.Crem:
                pet = new Pet((10f + PetLevel * 0.25f), 144, 10f, PetAbilty.SpreahHeal, PetProcType.AllType);
                break;
            case PetType.Snut:
                pet = new Pet((10f + PetLevel * 025f), 60, 10f, PetAbilty.TeamShield, PetProcType.AllType);
                break;
            case PetType.Pumkwim:
                pet = new Pet((20f + PetLevel * 0.5f), 54f, 40f, PetAbilty.AOEAttack, PetProcType.AllType);
                break;
                //epic
            case PetType.EpicBoogie:
                pet = new Pet((30f + PetLevel), 72f, 10f, PetAbilty.SpreahHeal, PetProcType.GetHit);
                break;
            case PetType.EpicNemo:
                pet = new Pet((15f + PetLevel * 0.5f), 190f, 10f, PetAbilty.ClosestAttack, PetProcType.GetHit);
                break;
            case PetType.EpicNerder:
                pet = new Pet((15f + PetLevel * 0.5f), 108f, 20f, PetAbilty.SelfHeal, PetProcType.PerHit);
                break;
            case PetType.EpicPumkwim:
                pet = new Pet((30f + PetLevel), 54f, 40f, PetAbilty.AOEAttack, PetProcType.GetHit);
                break;
            case PetType.EpicCrem:
                pet = new Pet((15f + PetLevel * 0.5f), 144f, 10f, PetAbilty.SpreahHeal, PetProcType.PerTurn);
                break;
            case PetType.EpicSnut:
                pet = new Pet((15f + PetLevel * 0.5f), 52, 10f, PetAbilty.TeamShield, PetProcType.PerHit);
                break;
            case PetType.Pritza:
                pet = new Pet((30f + PetLevel), 95f, 30f, PetAbilty.ClosestAttack, PetProcType.PerTurn);
                break;
            case PetType.Sparklez:
                pet = new Pet((30f + PetLevel), 85f, 30f, PetAbilty.ClosestAttack, PetProcType.PerHit);
                break;
            case PetType.Dug:
                pet = new Pet((30f + PetLevel), 95f, 30f, PetAbilty.ClosestAttack, PetProcType.GetHit);
                break;
            case PetType.Gumgum:
                pet = new Pet((30f + PetLevel), 36f, 10f, PetAbilty.TeamHeal, PetProcType.PerTurn);
                break;
            case PetType.Phony:
                pet = new Pet((30f + PetLevel), 32f, 10f, PetAbilty.TeamHeal, PetProcType.PerHit);
                break;
            case PetType.Waldo:
                pet = new Pet((30f + PetLevel), 36f, 10f, PetAbilty.TeamHeal, PetProcType.GetHit);
                break;
            case PetType.Beanz:
                pet = new Pet((30f + PetLevel), 30f, 10f, PetAbilty.TeamShield, PetProcType.PerTurn);
                break;
            case PetType.Rutledge:
                pet = new Pet((30f + PetLevel), 26f, 10f, PetAbilty.TeamShield, PetProcType.PerHit);
                break;
            case PetType.Log:
                pet = new Pet((30f + PetLevel), 30f, 10f, PetAbilty.TeamShield, PetProcType.GetHit);
                break;
            case PetType.Melvin:
                pet = new Pet((30f + PetLevel), 80f, 40f, PetAbilty.WeakestAttack, PetProcType.PerTurn);
                break;
            case PetType.Bryan:
                pet = new Pet((30f + PetLevel), 70f, 40f, PetAbilty.WeakestAttack, PetProcType.PerHit);
                break;
            case PetType.Nacl:
                pet = new Pet((30f + PetLevel), 80f, 40f, PetAbilty.WeakestAttack, PetProcType.GetHit);
                break;
            case PetType.Mewwo:
                pet = new Pet((45f + PetLevel * 1.5f), 11f, 10f, PetAbilty.TeamHealShield, PetProcType.PerTurn);
                break;
            case PetType.Gusty:
                pet = new Pet((45f + PetLevel * 1.5f), 9.7f, 10f, PetAbilty.TeamHealShield, PetProcType.PerHit);
                break;
            case PetType.Dot:
                pet = new Pet((45f + PetLevel * 1.5f), 11f, 10f, PetAbilty.TeamHealShield, PetProcType.GetHit);
                break;
            default:
                pet = new Pet();
                break;
        }
    }

    public void InitialiseMobs()
    {
        turnRate = Logic.TurnRate(power, agility);
        power = Convert.ToInt32(power * powerRunes);
        turnRate *= agilityRunes;
        hp = Convert.ToInt32(stamina * 10 * staminaRunes);
        maxHp = hp;
        maxShield = Convert.ToInt32(maxHp / 2);
        interval = 100 / turnRate;
        counter = 0;
        sp = 0;
        drain = false;
        AttributeMobSkills();
        pet = new Pet();
    }

    private void AttributeHeroSkills()
    {
        skillList.Add(new Skill(1f, 0.1f, 10, 0, SkillType.Normal));
        switch (weapon)
        {
            case Weapon.Axe:
                skillList.Add(new Skill(0.3f, 0.3f, 20, 2, SkillType.AOEDrain));
                skillList.Add(new Skill(1.72f, 0.3f, 10, 2, SkillType.Closest));
                skillList.Add(new Skill(1.8f, 0.3f, 60, 4, SkillType.Target));
                skillList.Add(new Skill(1.5f, 0.1f, 50, 6, SkillType.SpreadHeal));
                break;
            case Weapon.Sword:
                skillList.Add(new Skill(0.82f, 0.1f, 20, 2, SkillType.Pierce3));
                skillList.Add(new Skill(1.35f, 0.1f, 10, 2, SkillType.Target));
                skillList.Add(new Skill(0.8f, 0.1f, 60, 4, SkillType.AOE));
                skillList.Add(new Skill(1.25f, 0.1f, 50, 6, SkillType.Drain));
                break;
            case Weapon.Bow:
                skillList.Add(new Skill(0.6f, 0.2f, 15, 2, SkillType.AOE));
                skillList.Add(new Skill(1.35f, 0.3f, 15, 2, SkillType.Target));
                skillList.Add(new Skill(2.1f, 0.2f, 30, 4, SkillType.Furthest));
                skillList.Add(new Skill(0.5f, 0.2f, 30, 6, SkillType.AOEDrain));
                break;
            case Weapon.Spear:
                skillList.Add(new Skill(1.57f, 0.3f, 15, 2, SkillType.Furthest));
                skillList.Add(new Skill(0.82f, 0.2f, 15, 2, SkillType.Pierce3));
                skillList.Add(new Skill(1.8f, 0.2f, 30, 4, SkillType.Target));
                skillList.Add(new Skill(2.87f, 0.3f, 30, 6, SkillType.Closest));
                break;
            case Weapon.Staff:
                skillList.Add(new Skill(0.82f, 0.2f, 65, 2, SkillType.TargetHeal));
                skillList.Add(new Skill(1.42f, 0.2f, 5, 2, SkillType.Weakest));
                skillList.Add(new Skill(0.8f, 0.2f, 10, 4, SkillType.AOE));
                skillList.Add(new Skill(2.25f, 0.2f, 10, 6, SkillType.Target));
                break;
            case Weapon.Laser:
                skillList.Add(new Skill(0.39f, 0.5f, 15, 2, SkillType.Ricochet4));
                skillList.Add(new Skill(1.35f, 0.5f, 15, 2, SkillType.Target));
                skillList.Add(new Skill(1.72f, 0.5f, 30, 2, SkillType.Closest));
                skillList.Add(new Skill(1.2f, 0.5f, 30, 2, SkillType.SpreadHeal));
                break;
            case Weapon.DemonStaff:
                skillList.Add(new Skill(1.42f, 0.2f, 5, 2, SkillType.Weakest));
                skillList.Add(new Skill(0.3f, 0.2f, 10, 2, SkillType.AOEDrain));
                skillList.Add(new Skill(0.82f, 0.2f, 65, 2, SkillType.TargetHeal));
                skillList.Add(new Skill(1.8f, 0.2f, 10, 4, SkillType.Target));
                break;
        }
        if (unity) skillList.Add(new Skill(0.9f, 0.2f, 10, 2, SkillType.Unity));
    }

    private void AttributeMobSkills()
    {
        skillList.Add(new Skill(1f, 0.1f, 10, 0, SkillType.Normal));
        switch (name)
        {
            case "BlueOrc":
                skillList.Add(new Skill(0.3f, 0.35f, 30, 2, SkillType.AOEDrain));
                skillList.Add(new Skill(1.12f, 0.35f, 10, 2, SkillType.SelfHeal));
                skillList.Add(new Skill(0.8f, 0.35f, 20, 4, SkillType.AOE));
                skillList.Add(new Skill(1.9f, 0.35f, 30, 4, SkillType.Weakest));
                break;
            case "GreenOrc":
                skillList.Add(new Skill(0.82f, 0.3f, 30, 2, SkillType.TargetHeal));
                skillList.Add(new Skill(1.42f, 0.3f, 10, 2, SkillType.Weakest));
                skillList.Add(new Skill(0.8f, 0.3f, 25, 4, SkillType.AOE));
                skillList.Add(new Skill(1.8f, 0.3f, 25, 4, SkillType.Target));
                break;
            case "PurpleOrc":
                skillList.Add(new Skill(1.35f, 0.4f, 30, 2, SkillType.Target));
                skillList.Add(new Skill(1.57f, 0.4f, 10, 2, SkillType.Furthest));
                skillList.Add(new Skill(0.8f, 0.4f, 20, 4, SkillType.AOE));
                skillList.Add(new Skill(1.2f, 0.4f, 30, 4, SkillType.SpreadHeal));
                break;
            case "ArcherOrc":
                skillList.Add(new Skill(1.35f, 0.2f, 30, 2, SkillType.Target));
                skillList.Add(new Skill(1.42f, 0.2f, 60, 2, SkillType.Weakest));
                break;
            case "AssassinOrc":
                skillList.Add(new Skill(1.42f, 0.4f, 70, 2, SkillType.Weakest));
                skillList.Add(new Skill(0.75f, 0.4f, 20, 2, SkillType.Drain));
                break;
            case "BruiserOrc":
                skillList.Add(new Skill(0.75f, 0.3f, 30, 2, SkillType.Drain));
                skillList.Add(new Skill(1.35f, 0.3f, 10, 2, SkillType.Target));
                skillList.Add(new Skill(1.3f, 0.3f, 50, 4, SkillType.Pierce2));
                break;
            case "MeatOrc":
                skillList.Add(new Skill(0.75f, 0.4f, 20, 2, SkillType.Drain));
                skillList.Add(new Skill(1.12f, 0.4f, 30, 2, SkillType.SelfHeal));
                skillList.Add(new Skill(0.9f, 0.4f, 30, 4, SkillType.SpreadHeal));
                skillList.Add(new Skill(0.3f, 0.4f, 10, 4, SkillType.AOEHeal));
                break;
            case "MageOrc":
                skillList.Add(new Skill(0.9f, 0.2f, 50, 2, SkillType.SpreadHeal));
                skillList.Add(new Skill(1.35f, 0.2f, 20, 2, SkillType.Target));
                skillList.Add(new Skill(1.9f, 0.2f, 20, 4, SkillType.Weakest));
                break;
            case "BlueNether":
                skillList.Add(new Skill(0.75f, 0.3f, 5, 2, SkillType.Drain));
                skillList.Add(new Skill(1.57f, 0.3f, 25, 2, SkillType.Furthest));
                skillList.Add(new Skill(0.4f, 0.3f, 30, 4, SkillType.AOEDrain));
                skillList.Add(new Skill(1.5f, 0.3f, 30, 4, SkillType.SelfHeal));
                break;
            case "PurpleNether":
                skillList.Add(new Skill(1.8f, 0.4f, 30, 2, SkillType.Execute));
                skillList.Add(new Skill(0.82f, 0.4f, 10, 2, SkillType.TargetHeal));
                skillList.Add(new Skill(1.9f, 0.4f, 25, 4, SkillType.Weakest));
                skillList.Add(new Skill(2.6f, 0.4f, 25, 4, SkillType.Random));
                break;
            case "YellowNether":
                skillList.Add(new Skill(1.95f, 0.5f, 20, 2, SkillType.Random));
                skillList.Add(new Skill(1.57f, 0.5f, 20, 2, SkillType.Furthest));
                skillList.Add(new Skill(1.2f, 0.5f, 25, 4, SkillType.SpreadHeal));
                skillList.Add(new Skill(0.8f, 0.5f, 25, 4, SkillType.AOE));
                break;
            case "ImpNether":
                skillList.Add(new Skill(0.9f, 0.4f, 50, 2, SkillType.SpreadHeal));
                skillList.Add(new Skill(1.95f, 0.4f, 20, 2, SkillType.Random));
                skillList.Add(new Skill(1.3f, 0.4f, 20, 4, SkillType.Pierce2));
                break;
            case "MageNether":
                skillList.Add(new Skill(0.82f, 0.4f, 50, 2, SkillType.TargetHeal));
                skillList.Add(new Skill(1.42f, 0.4f, 50, 2, SkillType.Weakest));
                skillList.Add(new Skill(1.8f, 0.4f, 50, 4, SkillType.Target));
                break;
            case "BeastNether":
                skillList.Add(new Skill(1.72f, 0.3f, 15, 2, SkillType.Closest));
                skillList.Add(new Skill(1.95f, 0.3f, 25, 2, SkillType.Random));
                skillList.Add(new Skill(1f, 0.3f, 50, 4, SkillType.Drain));
                break;
            case "TankNether":
                ;
                skillList.Add(new Skill(1.57f, 0.2f, 20, 2, SkillType.Furthest));
                skillList.Add(new Skill(2.29f, 0.2f, 70, 4, SkillType.Closest));
                break;
            case "DemonNether":
                skillList.Add(new Skill(1.35f, 0.3f, 10, 2, SkillType.Target));
                skillList.Add(new Skill(1.95f, 0.3f, 10, 2, SkillType.Random));
                skillList.Add(new Skill(1f, 0.3f, 35, 4, SkillType.Drain));
                skillList.Add(new Skill(0.4f, 0.3f, 35, 4, SkillType.AOEHeal));
                break;
            case "Kaleido":
                skillList.Add(new Skill(1.72f, 0.4f, 20, 2, SkillType.Closest));
                skillList.Add(new Skill(1.57f, 0.4f, 20, 2, SkillType.Furthest));
                skillList.Add(new Skill(1.80f, 0.4f, 25, 4, SkillType.Target));
                skillList.Add(new Skill(1.1f, 0.4f, 25, 4, SkillType.TargetHeal));
                break;
            case "Woodbeard":
                skillList.Add(new Skill(1.72f, 0.3f, 15, 2, SkillType.Closest));
                skillList.Add(new Skill(0.97f, 0.3f, 35, 2, SkillType.Pierce2));
                skillList.Add(new Skill(0.3f, 0.3f, 30, 2, SkillType.AOEDrain));
                skillList.Add(new Skill(2.7f, 0.3f, 10, 4, SkillType.Execute));
                break;
            case "Robomech":
                skillList.Add(new Skill(0.52f, 0.2f, 5, 2, SkillType.Closest));
                skillList.Add(new Skill(0.6f, 0.2f, 5, 2, SkillType.AOE));
                skillList.Add(new Skill(1.9f, 0.2f, 50, 4, SkillType.Weakest));
                skillList.Add(new Skill(1.2f, 0.2f, 30, 4, SkillType.AOEHeal));
                break;
        }
    }

    public void Revive()
    {
        hp = maxHp;
        shield = 0;
        counter = 0;
        sp = 0;
        redirect = true;
    }

    public void IncrementCounter()
    {
        counter++;
    }

    public void IncrementSp()
    {
        sp++;
        if (necrosisBonus)
        {
            if (Logic.RNGroll(10f))  sp += 2;
        }
        if (metaRune == MetaRune.spRegen && hpPerc > 0.99f)
        {
            spBonus += 0.5f;
            if (spBonus >= 1f)
            {
                spBonus--;
                sp++;
            }
        }
    }
    public void SubstractCounter()
    {
        counter -= interval;
    }
    public void ChooseSkill(Character[] party, Character[] opponents)
    {
        Boolean isAoeAccepted = WorldBossSimulation.IsAoeEnabled(opponents);
        Boolean isHealingNeeded = Logic.IsHealingNeeded(party);
        int skillRange;
        int skillRoll;
        int skillInc = 0;

		skillRange = Convert.ToInt32(skillList.Where(skill => skill.Cost <= sp && (skill.IsTarget == Boolean.True || skill.IsAOE == isAoeAccepted || skill.IsHealing == isHealingNeeded)).Sum(skill => skill.Weight));
        skillRoll = random.Next(skillRange);
        for (int i = 0; i < skillList.Count; i++)
        {
			if (skillList[i].Cost <= sp && (skillList[i].IsTarget == Boolean.True || skillList[i].IsAOE == isAoeAccepted || skillList[i].IsHealing == isHealingNeeded))
            {
                skillInc += skillList[i].Weight;
                if (skillRoll < skillInc)
                {
                    sp -= skillList[i].Cost;
                    skillList[i].ApplySkill(this, party, opponents);
                    break;
                }
            }
        }
    }

    // Returns a predefined Hero struct
    public static Character GetPredefined(string name)
    {
        return predefined[name];
    }
}
