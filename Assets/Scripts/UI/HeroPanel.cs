using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : MonoBehaviour
{
    // References
    public Dropdown predefined;
    
    // Base Stats
    public InputField power;
    public InputField stamina;
    public InputField agility;
    
    // Specials
    public InputField critChance;
    public InputField critDamage;
    public InputField dsChance;
    public InputField blockChance;
    public InputField evadeChance;
    public InputField deflectChance;

    // Runes
    public InputField powerRunes;
    public InputField staminaRunes;
    public InputField agilityRunes;

    // Pet
    public Dropdown pet;

    // Awake
    public void Awake()
    {
        // Predefines
        List<string> predefines = new List<string>() { "None" };
        foreach (KeyValuePair<string, Hero> keyValuePair in Hero.predefined)
        {
            predefines.Add(keyValuePair.Key);
        }
        predefined.ClearOptions();
        predefined.AddOptions(predefines);

        // Pets
        pet.ClearOptions();
        pet.AddOptions(new List<string>(Enum.GetNames(typeof(Hero.Pet))));
    }

    // Predefine Switch
    public void PredefinedSwitch()
    {
        string heroName = predefined.options[predefined.value].text;

        // Hero Load
        Hero hero;
        if (heroName == "None")
        {
            // Empty Hero
            hero = new Hero();
        }
        else
        {
            hero = Hero.GetPredefined(heroName);
        }

        // Base Stats
        power.text         = Convert.ToString(hero.power);
        stamina.text       = Convert.ToString(hero.stamina);
        agility.text       = Convert.ToString(hero.agility);
        // Specials
        critChance.text    = Convert.ToString(hero.critChance);
        critDamage.text    = Convert.ToString(hero.critDamage);
        dsChance.text      = Convert.ToString(hero.dsChance);
        blockChance.text   = Convert.ToString(hero.blockChance);
        evadeChance.text   = Convert.ToString(hero.evadeChance);
        deflectChance.text = Convert.ToString(hero.deflectChance);
        // Runes
        powerRunes.text    = Convert.ToString(hero.powerRunes);
        staminaRunes.text  = Convert.ToString(hero.staminaRunes);
        agilityRunes.text  = Convert.ToString(hero.agilityRunes);
        // Pet
        for (int i = 0; i < pet.options.Count; i++)
        {
            if (pet.options[i].text == hero.pet.ToString())
            {
                pet.value = i;
                break;
            }
        }
    }

    // Return a Hero struct
    public Hero GetHeroStruct()
    {
        Hero.Pet currentPet;
        switch (pet.options[pet.value].text)
        {
            case "Nemo":
                currentPet = Hero.Pet.Nemo;
                break;

            case "Boogie":
                currentPet = Hero.Pet.Boogie;
                break;

            case "Nelson":
                currentPet = Hero.Pet.Nelson;
                break;

            default:   // Gemmi
                currentPet = Hero.Pet.Gemmi;
                break;
        }
        return new Hero {
            // Base Stats
            power         = Convert.ToInt32(power.text),
            stamina       = Convert.ToInt32(stamina.text),
            agility       = Convert.ToInt32(agility.text),
            // Specials
            critChance    = Convert.ToSingle(critChance.text),
            critDamage    = Convert.ToSingle(critDamage.text),
            dsChance      = Convert.ToSingle(dsChance.text),
            blockChance   = Convert.ToSingle(blockChance.text),
            evadeChance   = Convert.ToSingle(evadeChance.text),
            deflectChance = Convert.ToSingle(deflectChance.text),
            // Runes
            powerRunes    = Convert.ToSingle(powerRunes.text),
            staminaRunes  = Convert.ToSingle(staminaRunes.text),
            agilityRunes  = Convert.ToSingle(agilityRunes.text),
            // Pet
            pet           = currentPet
        };
    }
}
