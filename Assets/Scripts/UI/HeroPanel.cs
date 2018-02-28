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
    public Dropdown petType;
    public Dropdown petProcType;
    public InputField PetProcChance;
    public Dropdown weapon;
    public Dropdown metaRune;
    public Dropdown divinity;


    // mythic bonuses
    public Toggle NecrosisBonus;
    public Toggle HysteriaBonus;
    public Toggle NightVisageBonus;
    public Toggle ConsumptionBonus;
    public Toggle DecayBonus;

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



    public void ShowProcTypeOnSpecificPet()
    {
        if (pet.options[pet.value].text == PetType.Toebert.ToString() ||
            pet.options[pet.value].text == PetType.Urgoff.ToString())
        {
            petProcType.gameObject.SetActive(true);
        }
        else
        {
            petProcType.gameObject.SetActive(false);
        }
    }

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
        petProcType.ClearOptions();
        petProcType.AddOptions(new List<string>(Enum.GetNames(typeof(PetProcType))));
        petProcType.gameObject.SetActive(false);

        // Weapons
        weapon.ClearOptions();
        weapon.AddOptions(new List<string>(Enum.GetNames(typeof(Character.Weapon))));

        //meta
        metaRune.ClearOptions();
        metaRune.AddOptions(new List<string>(Enum.GetNames(typeof(Character.MetaRune))));

        //Divinity
        DivinityBonus.ClearOptions();
        DivinityBonus.AddOptions(new List<string>(Enum.GetNames(typeof(Character.DivinityBonus))));
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
        AresBonus.isOn = hero.aresBonus;
        BushidoBonus.isOn = hero.bushidoBonus;
        LunarBonus.isOn = hero.lunarBonus;
        GateKeeperBonus.isOn = hero.gateKeeperBonus;



        //mythic bonuses
        NecrosisBonus.isOn = hero.necrosisBonus;
        HysteriaBonus.isOn = hero.hysteriaBonus;
        NightVisageBonus.isOn = hero.nightVisageBonus;
        ConsumptionBonus.isOn = hero.consumptionBonus;
        DecayBonus.isOn = hero.decayBonus;


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
		for (int i = 0; i < DivinityBonus.options.Count; i++)
		{
			if (DivinityBonus.options[i].text == hero.divinityBonus.ToString())
			{
                DivinityBonus.value = i;
				break;
			}
		}

        // oblit
        for (int i = 0; i < ObliterationBonus.options.Count; i++)
        {
            if (ObliterationBonus.options[i].text == hero.obliterationBonus.ToString())
            {
                ObliterationBonus.value = i;
                break;
            }
        }

        // maru
        for (int i = 0; i < MaruBonus.options.Count; i++)
        {
            if (MaruBonus.options[i].text == hero.maruBonus.ToString())
            {
                MaruBonus.value = i;
                break;
            }
        }

        // conduction
        for (int i = 0; i < ConductionBonus.options.Count; i++)
        {
            if (ConductionBonus.options[i].text == hero.conductionBonus.ToString())
            {
                ConductionBonus.value = i;
                break;
            }
        }

        // illus
        for (int i = 0; i < IllustriousBonus.options.Count; i++)
        {
            if (IllustriousBonus.options[i].text == hero.illustriousBonus.ToString())
            {
                IllustriousBonus.value = i;
                break;
            }
        }

        // taters
        for (int i = 0; i < TatersBonus.options.Count; i++)
        {
            if (TatersBonus.options[i].text == hero.tatersBonus.ToString())
            {
                TatersBonus.value = i;
                break;
            }
        }

    }

    // Return a Hero struct
    public Character GetHero()
    {
        return new Character {
            // Base Stats
            power = Convert.ToInt32(power.text),
            stamina = Convert.ToInt32(stamina.text),
            agility = Convert.ToInt32(agility.text),
            // Specials
            critChance = Convert.ToSingle(critChance.text),
            critDamage = Convert.ToSingle(critDamage.text),
            dsChance = Convert.ToSingle(dsChance.text),
            blockChance = Convert.ToSingle(blockChance.text),
            evadeChance = Convert.ToSingle(evadeChance.text),
            deflectChance = Convert.ToSingle(deflectChance.text),
            absorbChance = Convert.ToSingle(absorbChance.text),
            empowerChance = Convert.ToSingle(empowerRunes.text),
            damageReduction = Convert.ToSingle(damageReduction.text),
            // Runes
            powerRunes = Convert.ToSingle(powerRunes.text),
            staminaRunes = Convert.ToSingle(staminaRunes.text),
            agilityRunes = Convert.ToSingle(agilityRunes.text),
            //Set Bonuses

            unity = UnityBonus.isOn,
            bushidoBonus = BushidoBonus.isOn,
            lunarBonus = LunarBonus.isOn,
            aresBonus = AresBonus.isOn,
            gateKeeperBonus = GateKeeperBonus.isOn,
			divinityBonus   = GetDivinityBonusFromString(DivinityBonus.options[DivinityBonus.value].text),
            obliterationBonus = GetOblitBonusFromString(ObliterationBonus.options[ObliterationBonus.value].text),
            maruBonus = GetMarutBonusFromString(MaruBonus.options[MaruBonus.value].text),
            conductionBonus = GetConducBonusFromString(ConductionBonus.options[ConductionBonus.value].text),
            illustriousBonus = GetIllustBonusFromString(IllustriousBonus.options[IllustriousBonus.value].text),
            tatersBonus = GetTatersBonusFromString(TatersBonus.options[TatersBonus.value].text),
            //Mythic

            necrosisBonus = NecrosisBonus.isOn,
            hysteriaBonus = HysteriaBonus.isOn,
            nightVisageBonus = NightVisageBonus.isOn,
            consumptionBonus = ConsumptionBonus.isOn,
            decayBonus = DecayBonus.isOn,


        // Pet
            metaRune        = GetMetaRuneFromString(metaRune.options[metaRune.value].text),
			pet             = GetPetFromString(pet.options[pet.value].text),
            petProcType     = GetProcTypeFromString(petProcType.options[petProcType.value].text),
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

    public static Character.ObliterationBonus GetOblitBonusFromString(String s)
    {
        Character.ObliterationBonus oblitBonus;

        switch (s)
        {
            case "Bonus_2_of_4":
                oblitBonus = Character.ObliterationBonus.Bonus_2_of_4;
                break;
            case "Bonus_3_of_4":
                oblitBonus = Character.ObliterationBonus.Bonus_3_of_4;
                break;
            case "Bonus_4_of_4":
                oblitBonus = Character.ObliterationBonus.Bonus_4_of_4;
                break;
            default:
                oblitBonus = Character.ObliterationBonus.None;
                break;
        }

        return oblitBonus;
    }

    public static Character.MARUBonus GetMarutBonusFromString(String s)
    {
        Character.MARUBonus maruBonus;

        switch (s)
        {
            case "Bonus_2_of_4":
                maruBonus = Character.MARUBonus.Bonus_2_of_4;
                break;
            case "Bonus_3_of_4":
                maruBonus = Character.MARUBonus.Bonus_3_of_4;
                break;
            case "Bonus_4_of_4":
                maruBonus = Character.MARUBonus.Bonus_4_of_4;
                break;
            default:
                maruBonus = Character.MARUBonus.None;
                break;
        }

        return maruBonus;
    }

    public static Character.ConductionBonus GetConducBonusFromString(String s)
    {
        Character.ConductionBonus conducBonus;

        switch (s)
        {
            case "Bonus_2_of_4":
                conducBonus = Character.ConductionBonus.Bonus_2_of_4;
                break;
            case "Bonus_3_of_4":
                conducBonus = Character.ConductionBonus.Bonus_3_of_4;
                break;
            case "Bonus_4_of_4":
                conducBonus = Character.ConductionBonus.Bonus_4_of_4;
                break;
            default:
                conducBonus = Character.ConductionBonus.None;
                break;
        }

        return conducBonus;
    }

    public static Character.TatersBonus GetTatersBonusFromString(String s)
    {
        Character.TatersBonus tatersBonus;

        switch (s)
        {
            case "Bonus_2_of_3":
                tatersBonus = Character.TatersBonus.Bonus_2_of_3;
                break;
            case "Bonus_3_of_3":
                tatersBonus = Character.TatersBonus.Bonus_3_of_3;
                break;
            default:
                tatersBonus = Character.TatersBonus.None;
                break;
        }

        return tatersBonus;
    }

    public static Character.IllustriousBonus GetIllustBonusFromString(String s)
    {
        Character.IllustriousBonus illustBonus;

        switch (s)
        {
            case "Bonus_2_of_3":
                illustBonus = Character.IllustriousBonus.Bonus_2_of_3;
                break;
            case "Bonus_3_of_3":
                illustBonus = Character.IllustriousBonus.Bonus_3_of_3;
                break;
            default:
                illustBonus = Character.IllustriousBonus.None;
                break;
        }

        return illustBonus;
    }


    public static Character.MetaRune GetMetaRuneFromString(String s)
	{
		Character.MetaRune metaRune;

		switch (s)
		{
			case "Redirect":
				metaRune = Character.MetaRune.Redirect;
				break;
            case "spRegen":
                metaRune = Character.MetaRune.spRegen;
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

    public static PetProcType GetProcTypeFromString(String s)
    {
        PetProcType petProcType = PetProcType.AllType;
        switch (s)
        {
            case "PerHit":
                petProcType = PetProcType.PerHit;
                break;
            case "OnHit":
                petProcType = PetProcType.GetHit;
                break;
            case "PerTurn":
                petProcType = PetProcType.PerTurn;
                break;
            case "AllType":
                petProcType = PetProcType.AllType;
                break;
        }
        return petProcType;
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
            case "Laser":
                weapon = Character.Weapon.Laser;
                break;
            case "DemonStaff":
                weapon = Character.Weapon.DemonStaff;
                break;
            default:
				weapon = Character.Weapon.None;
				break;
		}
        return weapon;
	}
}
