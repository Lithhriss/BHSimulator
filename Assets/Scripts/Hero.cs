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
    public int skillPoints;

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

    // Pet
    public Pet pet;
    public enum Pet
    {
        None,
        Nelson,
        Gemmi
    }

    // Predefined Heroes
    public static readonly Dictionary<string, Hero> predefined = new Dictionary<string, Hero>() {
        {
            "Shadown88",
            new Hero {
                // Base Stats
                power         = 829,
                stamina       = 185,
                agility       = 204,
                // Specials
                critChance    = 0.15f,
                critDamage    = 0.5f,
                dsChance      = 0.15f,
                blockChance   = 0f,
                evadeChance   = 0.025f,
                deflectChance = 0f,
                // Runes
                powerRunes    = 0f,
                staminaRunes  = 0f,
                agilityRunes  = 0.01f,
                // Pet
                pet           = Pet.Gemmi
            }
        },
        {
            "SSS1",
            new Hero {
                // Base Stats
                power         = 568,
                stamina       = 205,
                agility       = 555,
                // Specials
                critChance    = 0.25f,
                critDamage    = 1f,
                dsChance      = 0.05f,
                blockChance   = 0f,
                evadeChance   = 0.025f,
                deflectChance = 0f,
                // Runes
                powerRunes    = 0.05f,
                staminaRunes  = 0f,
                agilityRunes  = 0.025f,
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