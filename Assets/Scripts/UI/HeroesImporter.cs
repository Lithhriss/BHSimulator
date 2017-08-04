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
			"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
			hero.power,
			hero.stamina,
			hero.agility,
			hero.critChance,
			hero.critDamage,
			hero.dsChance,
			hero.blockChance,
			hero.evadeChance,
			hero.deflectChance,
			hero.absorbChance,
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
		String line;

		lines = csv.Split('\n');

		for (var i = 0; i < heroes.Length; i++)
		{
			heroes[i].setFieldsFromHero(GetHeroFromCsvLine(lines[i]));
		}
	}

	public Hero GetHeroFromCsvLine(String csvLine)
	{
		Hero hero;
		String[] values;

		values = csvLine.Split(',');

		hero = new Hero();
		hero.power = Convert.ToInt32(values[0]);
		hero.stamina = Convert.ToInt32(values[1]);
		hero.agility = Convert.ToInt32(values[2]);
		hero.critChance = Convert.ToSingle(values[3]);
		hero.critDamage = Convert.ToSingle(values[4]);
		hero.dsChance = Convert.ToSingle(values[5]);
		hero.blockChance = Convert.ToSingle(values[6]);
		hero.evadeChance = Convert.ToSingle(values[7]);
		hero.deflectChance = Convert.ToSingle(values[8]);
		hero.absorbChance = Convert.ToSingle(values[9]);
		hero.powerRunes = Convert.ToSingle(values[10]);
		hero.staminaRunes = Convert.ToSingle(values[11]);
		hero.agilityRunes = Convert.ToSingle(values[12]);
		hero.unity = Convert.ToBoolean(values[13]);
		hero.bushidoBonus = Convert.ToBoolean(values[14]);
		hero.divinityBonus = HeroPanel.GetDivinityBonusFromString(values[15]);
		hero.metaRune = HeroPanel.GetMetaRuneFromString(values[16]);
		hero.pet = HeroPanel.GetPetFromString(values[17]);
		hero.weapon = HeroPanel.GetWeaponFromString(values[18]);

		return hero;
	}
}
