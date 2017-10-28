using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HeroesImporter : MonoBehaviour
{		
	[DllImport("__Internal")]
	public static extern void ShowDialog(string text);

	public Button cancelImportButton;
	public Button openOverlayButton;
	public Button submitImportButton;
	public GameObject importOverlay;
	public HeroPanel[] heroes;
	public InputField importInputField;

	void Start()
	{
		openOverlayButton.GetComponent<Button>().onClick.AddListener(OpenOverlayButton_onClick);
		submitImportButton.GetComponent<Button>().onClick.AddListener(SubmitImport_onClick);
		cancelImportButton.GetComponent<Button>().onClick.AddListener(CancelImport_onClick);

		Launch.onRun += OnSimulationRun;
	}

	void OnSimulationRun()
	{
		openOverlayButton.interactable = !Launch.instance.IsRunning;
	}

	public void OpenOverlayButton_onClick()
	{
		String heroesCsv = GetHeroesCsv();

		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			ShowDialog(heroesCsv);
		}
		else
		{
			importInputField.text = heroesCsv;
			importOverlay.SetActive(true);
		}
	}

	public void SubmitImport_onClick()
	{
		importOverlay.SetActive(false);
		UpdateHeroPanelsFromCsv(importInputField.text);
	}

	public void CancelImport_onClick()
	{
		importOverlay.SetActive(false);
	}

	private String GetHeroesCsv()
	{
		StringBuilder sb;

		sb = new StringBuilder();
		for (var i = 0; i < heroes.Length; i++)
		{
			sb.AppendLine(GetCsvLineFromHero(heroes[i].GetHeroStruct()));
		}

		return sb.ToString();
	}

	private String GetCsvLineFromHero(Hero hero)
	{
		return String.Format(
			"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}",
			hero.power,
			hero.stamina,
			hero.agility,
			hero.critChance,
			hero.critDamage,
			hero.dsChance,
            hero.empowerChance,
			hero.blockChance,
			hero.evadeChance,
			hero.deflectChance,
			hero.absorbChance,
            hero.damageReduction,
			hero.powerRunes,
			hero.staminaRunes,
			hero.agilityRunes,
			hero.unity,
			hero.bushidoBonus,
			hero.divinityBonus,
			hero.metaRune,
			hero.pet,
			hero.weapon
		);
	}

	public void UpdateHeroPanelsFromCsv(String csv)
	{
		String[] lines;
		

		lines = csv.Split('\n');

		for (var i = 0; i < heroes.Length; i++)
		{
			heroes[i].SetFieldsFromHero(GetHeroFromCsvLine(lines[i]));
		}
	}

	public Hero GetHeroFromCsvLine(String csvLine)
	{
		Hero hero;
		String[] values;

		values = csvLine.Split(',');

        hero = new Hero()
        {
            power = Convert.ToInt32(values[0]),
            stamina = Convert.ToInt32(values[1]),
            agility = Convert.ToInt32(values[2]),
            critChance = Convert.ToSingle(values[3]),
            critDamage = Convert.ToSingle(values[4]),
            dsChance = Convert.ToSingle(values[5]),
            empowerChance = Convert.ToSingle(values[6]),
            blockChance = Convert.ToSingle(values[7]),
            evadeChance = Convert.ToSingle(values[8]),
            deflectChance = Convert.ToSingle(values[9]),
            absorbChance = Convert.ToSingle(values[10]),
            damageReduction = Convert.ToSingle(values[11]),
            powerRunes = Convert.ToSingle(values[12]),
            staminaRunes = Convert.ToSingle(values[13]),
            agilityRunes = Convert.ToSingle(values[14]),
            unity = Convert.ToBoolean(values[15]),
            bushidoBonus = Convert.ToBoolean(values[16]),
            divinityBonus = HeroPanel.GetDivinityBonusFromString(values[17]),
            metaRune = HeroPanel.GetMetaRuneFromString(values[18]),
            pet = HeroPanel.GetPetFromString(values[19]),
            weapon = HeroPanel.GetWeaponFromString(values[20])
        };
        Debug.Log(values[20] + "$");
		return hero;
	}
}
