using System.Collections.Generic;
using System;
using System.Linq;

public enum PetType
{
    None,
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
    Skulldemort
}

public class Character
{
    public static Random random = new Random(Guid.NewGuid().GetHashCode());


    public bool isHero;
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

    // Pet
    public string pets;
    public PetType pet;
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
        Redirect

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
        isHero = ishero;
    }
    public Character()
    { }

    // Predefined 
    public static readonly Dictionary<string, Character> predefined = new Dictionary<string, Character>() {
        /*{
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
                pet           = Pet.None,
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
                pet           = Pet.Gemmi,
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
                power         = 500,
                stamina       = 1000,
                agility       = 100,
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
                pet           = Pet.Boiguh,
                weapon        = Weapon.Axe,
                metaRune      = MetaRune.Redirect,
                // set bonuses
                unity         = false,
                bushidoBonus  = false,
                divinityBonus = DivinityBonus.None
            }
        },
        {
            "Shadown88",
            new Character {
                // Base Stats
                power         = 1085,
                stamina       = 135,
                agility       = 69,
                // Specials
                critChance    = 15f,
                critDamage    = 50f,
                dsChance      = 18f,
                blockChance   = 0f,
                evadeChance   = 2.5f,
                deflectChance = 0f,
                absorbChance  = 0f,
                // Runes
                powerRunes    = 15.5f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                pet           = Pet.Gemmi,
                weapon        = Weapon.Bow,
                metaRune      = MetaRune.None,
                // set bonuses
                unity         = true,
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
                pet           = Pet.Nelson,
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
                pet           = Pet.Nelson,
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
                pet           = Pet.Gemmi,
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
                pet           = Pet.Gemmi,
                weapon        = Weapon.Bow,
                metaRune      = MetaRune.Redirect
            }
        }*/

    };

    public void InitialiseHero()
    {
        //powerRunes = (100f + powerRunes) / 100f;
        //agilityRunes = (100f + agilityRunes) / 100f;
        //critDamage = (100f + critDamage) / 100f;
        //staminaRunes = (100f + staminaRunes) / 100f;
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
                skillList.Add(new Skill(1.42f, 0.2f, 5, 2, SkillType.Closest));
                skillList.Add(new Skill(0.8f, 0.2f, 10, 4, SkillType.AOE));
                skillList.Add(new Skill(2.25f, 0.2f, 10, 6, SkillType.Target));
                break;
            case Weapon.Laser:
                skillList.Add(new Skill(0.39f, 0.5f, 15, 2, SkillType.Ricochet));
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
            case "TankNether":;
                skillList.Add(new Skill(1.57f, 0.2f, 20, 2, SkillType.Furthest));
                skillList.Add(new Skill(2.29f, 0.2f, 70, 4, SkillType.Closest));
                break;
            case "DemonNether":
                skillList.Add(new Skill(1.35f, 0.3f, 10, 2, SkillType.Target));
                skillList.Add(new Skill(1.95f, 0.3f, 10, 2, SkillType.Random));
                skillList.Add(new Skill(1f, 0.3f, 35, 4, SkillType.Drain));
                skillList.Add(new Skill(0.4f, 0.3f, 35, 4, SkillType.AOEHeal));
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
    }
    public void SubstractCounter()
    {
        counter -= interval;
    }
    public void ChooseSkill(Character[] party, Character[] opponents)
    {
        bool isAoeAccepted = WorldBossSimulation.IsAoeEnabled(opponents);
        int skillRange;
        int skillRoll;
        int skillInc = 0;

        skillRange = Convert.ToInt32(skillList.Where(skill => skill.Cost <= sp && (skill.IsAOE == false) || (skill.IsAOE == isAoeAccepted)).Sum(skill => skill.Cost));
        skillRoll = random.Next(skillRange);
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].Cost <= sp && ((skillList[i].IsAOE == false) || (skillList[i].IsAOE == isAoeAccepted)))
            {
                skillInc += skillList[i].Cost;
                if (skillRange < skillInc)
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


/*
 
        if (GetOpponentCount > 2)
        {
            skillRange = Convert.ToInt32(skillList.Where(skill => skill.Cost <= sp).Sum(skill => skill.Cost));
            skillRoll = random.Next(skillRange);
            for (int i = 0; i < skillList.Count; i++)
            {
                int skillInc = 0;
                if (skillList[i].Cost <= sp)
                {
                    skillInc += skillList[i].Cost;
                    if (skillRange < skillInc)
                    {
                        //execute skill
                    }
                }
            }
        }
        else
        {
            skillRange = Convert.ToInt32(skillList.Where(skill => skill.Cost <= sp && !skill.IsAOE).Sum(skill => skill.Cost));
            skillRoll = random.Next(skillRange);
            for (int i = 0; i < skillList.Count; i++)
            {
                int skillInc = 0;
                if (skillList[i].Cost <= sp && !skillList[i].IsAOE)
                {
                    skillInc += skillList[i].Cost;
                    if (skillRange < skillInc)
                    {
                        //execute skill
                    }
                }
            }
        }
 */
