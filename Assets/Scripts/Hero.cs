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

    // Runes
    public float powerRunes;
    public float staminaRunes;
    public float agilityRunes;

    // state
    public bool alive;

    // Pet
    public string pets;
    public Pet pet;
    public enum Pet
    {
        None,
        Nelson,
        Gemmi,
        Boogie,
        Nemo
    }

    // Predefined Heroes
    public static readonly Dictionary<string, Hero> predefined = new Dictionary<string, Hero>() {
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
                // Runes
                powerRunes    = 15.5f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                pet           = Pet.Gemmi
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
                // Runes
                powerRunes    = 16f,
                staminaRunes  = 0f,
                agilityRunes  = 2.5f,
                // Pet
                pet           = Pet.Nelson
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
                // Runes
                powerRunes    = 22f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                pet           = Pet.Nelson
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
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                pet           = Pet.Gemmi
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
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0f,
                // Pet
                pet           = Pet.Gemmi
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