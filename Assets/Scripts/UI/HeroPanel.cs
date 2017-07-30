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
    public InputField absorbChance;

    // Runes
    public InputField powerRunes;
    public InputField staminaRunes;
    public InputField agilityRunes;

    public Toggle unitySkill;
    public Toggle bushidoBonus;
    public Toggle divinityBonus;
    // Pet
    public Dropdown pet;
    public Dropdown weapon;
    public Dropdown metaRune;
    public Dropdown divinity;

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

        // Weapons
        weapon.ClearOptions();
        weapon.AddOptions(new List<string>(Enum.GetNames(typeof(Hero.Weapon))));

        //meta
        metaRune.ClearOptions();
        metaRune.AddOptions(new List<string>(Enum.GetNames(typeof(Hero.MetaRune))));

        //Divinity
        divinity.ClearOptions();
        divinity.AddOptions(new List<string>(Enum.GetNames(typeof(Hero.DivinityBonus))));
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
        absorbChance.text  = Convert.ToString(hero.absorbChance);

        //set bonuses
        unitySkill.isOn    = hero.unity;
        //divinityBonus.isOn = hero.divinityBonus;

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

        //weapon
        for (int i = 0; i < weapon.options.Count; i++)
        {
            if (weapon.options[i].text == hero.weapon.ToString())
            {
                weapon.value = i;
                break;
            }
        }

        // meta bonus
        for (int i = 0; i < metaRune.options.Count; i++)
        {
            if (metaRune.options[i].text == hero.metaRune.ToString())
            {
                metaRune.value = i;
                break;
            }
        }
        // divinity
        for (int i = 0; i < divinity.options.Count; i++)
        {
            if (divinity.options[i].text == hero.divinityBonus.ToString())
            {
                divinity.value = i;
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

            case "Crem":
                currentPet = Hero.Pet.Crem;
                break;

            case "Boiguh":
                currentPet = Hero.Pet.Boiguh;
                break;

            case "Nerder":
                currentPet = Hero.Pet.Nerder;
                break;

            default:   // Gemmi
                currentPet = Hero.Pet.Gemmi;
                break;
        }

        Hero.Weapon currentWeapon;
        switch (weapon.options[weapon.value].text)
        {
            case "Axe":
                currentWeapon = Hero.Weapon.Axe;
                break;
            case "Bow":
                currentWeapon = Hero.Weapon.Bow;
                break;
            case "Staff":
                currentWeapon = Hero.Weapon.Staff;
                break;
            case "Sword":
                currentWeapon = Hero.Weapon.Sword;
                break;
            default: //spear
                currentWeapon = Hero.Weapon.Spear;
                break;
        }
        Hero.MetaRune currentMetaRune;
        switch (metaRune.options[metaRune.value].text)
        {
            case "Redirect":
                currentMetaRune = Hero.MetaRune.Redirect;
                break;
            /*case "HealBonus":
                currentMetaRune = Hero.MetaRune.HealBonus;
                break;*/
            default: //spear
                currentMetaRune = Hero.MetaRune.None;
                break;
        }
        Hero.DivinityBonus currentDivinity;
        switch (divinity.options[divinity.value].text)
        {
            case "Bonus_2_of_3":
                currentDivinity = Hero.DivinityBonus.Bonus_2_of_3;
                break;
            case "Bonus_3_of_3":
                currentDivinity = Hero.DivinityBonus.Bonus_3_of_3;
                break;
            default: //spear
                currentDivinity = Hero.DivinityBonus.None;
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
            absorbChance  = Convert.ToSingle(absorbChance.text),
            // Runes
            powerRunes    = Convert.ToSingle(powerRunes.text),
            staminaRunes  = Convert.ToSingle(staminaRunes.text),
            agilityRunes  = Convert.ToSingle(agilityRunes.text),
            //Set Bonuses

            unity         = unitySkill.isOn,
            bushidoBonus  = bushidoBonus.isOn,
            divinityBonus = currentDivinity,

            // Pet
            metaRune      = currentMetaRune,
            pet           = currentPet,
            weapon        = currentWeapon
        };
    }
}
