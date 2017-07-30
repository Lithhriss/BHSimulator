using System.Collections.Generic;

public struct Hero
{
    // Base Stats
    public int power;
    public int stamina;
    public int agility;
    
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

    // state
    public bool alive;
    public bool drain;
    public bool unity;
    public bool redirect;
    public bool redirectRune;
    public bool bushidoBonus;
    //public bool divinityBonus;

    // Pet
    public string pets;
    public Pet pet;
    public enum Pet
    {
        None,
        Nelson,
        Gemmi,
        Boogie,
        Nemo,
        Crem, 
        Boiguh, 
        Nerder
    }
    public Weapon weapon;
    public enum Weapon
    {
        None,
        Bow,
        Spear,
        Sword,
        Staff,
        Axe
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

    // Predefined Heroes
    public static readonly Dictionary<string, Hero> predefined = new Dictionary<string, Hero>() {
        {
            "Default Hero",
            new Hero {
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
            "Shadown88",
            new Hero {
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
            new Hero {
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
            new Hero {
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
            new Hero {
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
            new Hero {
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
        }

    };
    
    // Hero Alive?
    public bool Alive()
    {
        return hp > 0;
    }

    // Heal the Hero
    public void Heal(int health)
    {
        hp += health;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    // Returns a predefined Hero struct
    public static Hero GetPredefined(string name)
    {
        return predefined[name];
    }
}