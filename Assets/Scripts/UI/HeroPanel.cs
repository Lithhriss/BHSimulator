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

    // Pet
    public Dropdown pet;
    public Dropdown weapon;
    public Dropdown metaRune;
    public Dropdown divinity;


    // mythic bonuses
    public Toggle NecrosisBonus;
    public Toggle HysteriaBonus;
    public Toggle NightVisageBonus;
    public Toggle ConsumptionBonus;

    // set bonuses
    public Toggle AresBonus;
    public Toggle BushidoBonus;
    public Toggle LunarBonus;
    public Toggle UnityBonus;
    public Dropdown DivinityBonus;
    public Dropdown ObliterationBonus;
    public Dropdown MaruBonus;
    public Dropdown ConductionBonus;
    public Dropdown IllustriousBonus;
    public Dropdown TatersBonus;

    public Toggle GateKeeperBonus;





    // Awake
    public void Awake()
    {
        // Predefines
        List<string> predefines = new List<string>() { "None" };
        foreach (KeyValuePair<string, Character> keyValuePair in Character.predefined)
        {
            predefines.Add(keyValuePair.Key);
        }
        predefined.ClearOptions();
        predefined.AddOptions(predefines);

        // Pets
        pet.ClearOptions();
        pet.AddOptions(new List<string>(Enum.GetNames(typeof(PetType))));

        // Weapons
        weapon.ClearOptions();
        weapon.AddOptions(new List<string>(Enum.GetNames(typeof(Character.Weapon))));

        //meta
        metaRune.ClearOptions();
        metaRune.AddOptions(new List<string>(Enum.GetNames(typeof(Character.MetaRune))));

        //Divinity
        divinity.ClearOptions();
        divinity.AddOptions(new List<string>(Enum.GetNames(typeof(Character.DivinityBonus))));
    }

    // Predefine Switch
    public void PredefinedSwitch()
    {
        string heroName = predefined.options[predefined.value].text;

        // Hero Load
        Character hero;
        if (heroName == "None")
        {
            // Empty Hero
            hero = new Character();
        }
        else
        {
            hero = Character.GetPredefined(heroName);
        }

		SetFieldsFromHero(hero);
    }

	public void SetFieldsFromHero(Character hero)
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
        UnityBonus.isOn    = hero.unity;

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
    public Character GetHeroStruct()
    {
        return new Character {
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

            unity           = UnityBonus.isOn,
            bushidoBonus    = BushidoBonus.isOn,
			divinityBonus   = GetDivinityBonusFromString(divinity.options[divinity.value].text),

            // Pet
			metaRune        = GetMetaRuneFromString(metaRune.options[metaRune.value].text),
			pet             = GetPetFromString(pet.options[pet.value].text),
			weapon          = GetWeaponFromString(weapon.options[weapon.value].text)
        };
    }

	public static Character.DivinityBonus GetDivinityBonusFromString(String s)
	{
		Character.DivinityBonus divinityBonus;

		switch (s)
		{
			case "Bonus_2_of_3":
				divinityBonus = Character.DivinityBonus.Bonus_2_of_3;
				break;
			case "Bonus_3_of_3":
				divinityBonus = Character.DivinityBonus.Bonus_3_of_3;
				break;
			default:
				divinityBonus = Character.DivinityBonus.None;
				break;
		}

		return divinityBonus;
	}

	public static Character.MetaRune GetMetaRuneFromString(String s)
	{
		Character.MetaRune metaRune;

		switch (s)
		{
			case "Redirect":
				metaRune = Character.MetaRune.Redirect;
				break;
			default:
				metaRune = Character.MetaRune.None;
				break;
		}

		return metaRune;
	}

	public static PetType GetPetFromString(String s)
	{
		PetType pet;

		switch (s)
		{
			case "Nemo":
				pet =PetType.Nemo;
				break;

			case "Boogie":
				pet =PetType.Boogie;
				break;

			case "Nelson":
				pet =PetType.Nelson;
				break;

			case "Gemmi":
				pet =PetType.Gemmi;
				break;

			case "Crem":
				pet =PetType.Crem;
				break;

			case "Boiguh":
				pet =PetType.Boiguh;
				break;

			case "Nerder":
				pet =PetType.Nerder;
				break;

            case "Buvboi":
                pet =PetType.Buvboi;
                break;

            case "Wuvboi":
                pet =PetType.Wuvboi;
                break;

            case "Snut":
                pet =PetType.Snut;
                break;

            case "Quimby":
                pet =PetType.Quimby;
                break;

            case "Skulldemort":
                pet =PetType.Skulldemort;
                break;

            default:
				pet =PetType.None;
				break;
		}

		return pet;
	}

	public static Character.Weapon GetWeaponFromString(String s)
	{
		Character.Weapon weapon;
        
        switch (s)
		{
			case "Axe":
				weapon = Character.Weapon.Axe;
				break;
			case "Bow":
				weapon = Character.Weapon.Bow;
				break;
			case "Spear":
				weapon = Character.Weapon.Spear;
				break;
			case "Staff":
				weapon = Character.Weapon.Staff;
				break;
			case "Sword":
				weapon = Character.Weapon.Sword;
				break;
			default:
				weapon = Character.Weapon.None;
				break;
		}
        return weapon;
	}
}
