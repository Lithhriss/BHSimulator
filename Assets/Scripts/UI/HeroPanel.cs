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
    public InputField damageReduction;

    // Runes
    public InputField powerRunes;
    public InputField staminaRunes;
    public InputField agilityRunes;
    public InputField empowerRunes;

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

		SetFieldsFromHero(hero);
    }

	public void SetFieldsFromHero(Hero hero)
	{
		// Base Stats
		power.text           = Convert.ToString(hero.power);
		stamina.text         = Convert.ToString(hero.stamina);
		agility.text         = Convert.ToString(hero.agility);
		// Specials
		critChance.text      = Convert.ToString(hero.critChance);
		critDamage.text      = Convert.ToString(hero.critDamage);
		dsChance.text        = Convert.ToString(hero.dsChance);
		blockChance.text     = Convert.ToString(hero.blockChance);
		evadeChance.text     = Convert.ToString(hero.evadeChance);
		deflectChance.text   = Convert.ToString(hero.deflectChance);
		absorbChance.text    = Convert.ToString(hero.absorbChance);
        empowerRunes.text    = Convert.ToString(hero.empowerChance);
        damageReduction.text = Convert.ToString(hero.damageReduction);
        //set bonuses
        unitySkill.isOn    = hero.unity;

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
        return new Hero {
            // Base Stats
            power           = Convert.ToInt32(power.text),
            stamina         = Convert.ToInt32(stamina.text),
            agility         = Convert.ToInt32(agility.text),
            // Specials
            critChance      = Convert.ToSingle(critChance.text),
            critDamage      = Convert.ToSingle(critDamage.text),
            dsChance        = Convert.ToSingle(dsChance.text),
            blockChance     = Convert.ToSingle(blockChance.text),
            evadeChance     = Convert.ToSingle(evadeChance.text),
            deflectChance   = Convert.ToSingle(deflectChance.text),
            absorbChance    = Convert.ToSingle(absorbChance.text),
            empowerChance   = Convert.ToSingle(empowerRunes.text),
            damageReduction = Convert.ToSingle(damageReduction.text),
            // Runes
            powerRunes      = Convert.ToSingle(powerRunes.text),
            staminaRunes    = Convert.ToSingle(staminaRunes.text),
            agilityRunes    = Convert.ToSingle(agilityRunes.text),
            //Set Bonuses

            unity           = unitySkill.isOn,
            bushidoBonus    = bushidoBonus.isOn,
			divinityBonus   = GetDivinityBonusFromString(divinity.options[divinity.value].text),

            // Pet
			metaRune        = GetMetaRuneFromString(metaRune.options[metaRune.value].text),
			pet             = GetPetFromString(pet.options[pet.value].text),
			weapon          = GetWeaponFromString(weapon.options[weapon.value].text)
        };
    }

	public static Hero.DivinityBonus GetDivinityBonusFromString(String s)
	{
		Hero.DivinityBonus divinityBonus;

		switch (s)
		{
			case "Bonus_2_of_3":
				divinityBonus = Hero.DivinityBonus.Bonus_2_of_3;
				break;
			case "Bonus_3_of_3":
				divinityBonus = Hero.DivinityBonus.Bonus_3_of_3;
				break;
			default:
				divinityBonus = Hero.DivinityBonus.None;
				break;
		}

		return divinityBonus;
	}

	public static Hero.MetaRune GetMetaRuneFromString(String s)
	{
		Hero.MetaRune metaRune;

		switch (s)
		{
			case "Redirect":
				metaRune = Hero.MetaRune.Redirect;
				break;
			default:
				metaRune = Hero.MetaRune.None;
				break;
		}

		return metaRune;
	}

	public static Hero.Pet GetPetFromString(String s)
	{
		Hero.Pet pet;

		switch (s)
		{
			case "Nemo":
				pet = Hero.Pet.Nemo;
				break;

			case "Boogie":
				pet = Hero.Pet.Boogie;
				break;

			case "Nelson":
				pet = Hero.Pet.Nelson;
				break;

			case "Gemmi":
				pet = Hero.Pet.Gemmi;
				break;

			case "Crem":
				pet = Hero.Pet.Crem;
				break;

			case "Boiguh":
				pet = Hero.Pet.Boiguh;
				break;

			case "Nerder":
				pet = Hero.Pet.Nerder;
				break;

            case "Buvboi":
                pet = Hero.Pet.Buvboi;
                break;

            case "Wuvboi":
                pet = Hero.Pet.Wuvboi;
                break;

            case "Snut":
                pet = Hero.Pet.Snut;
                break;

            case "Quimby":
                pet = Hero.Pet.Quimby;
                break;

            case "Skulldemort":
                pet = Hero.Pet.Skulldemort;
                break;

            default:
				pet = Hero.Pet.None;
				break;
		}

		return pet;
	}

	public static Hero.Weapon GetWeaponFromString(String s)
	{
		Hero.Weapon weapon;
        
        switch (s)
		{
			case "Axe":
				weapon = Hero.Weapon.Axe;
				break;
			case "Bow":
				weapon = Hero.Weapon.Bow;
				break;
			case "Spear":
				weapon = Hero.Weapon.Spear;
				break;
			case "Staff":
				weapon = Hero.Weapon.Staff;
				break;
			case "Sword":
				weapon = Hero.Weapon.Sword;
				break;
			default:
				weapon = Hero.Weapon.None;
				break;
		}
        return weapon;
	}
}
