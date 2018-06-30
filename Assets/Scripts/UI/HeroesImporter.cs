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
    public GameObject[] heroContainers;
    private HeroPanel[] heroes;
    public InputField importInputField;

    void Start()
    {
        openOverlayButton.GetComponent<Button>().onClick.AddListener(OpenOverlayButton_onClick);
        submitImportButton.GetComponent<Button>().onClick.AddListener(SubmitImport_onClick);
        cancelImportButton.GetComponent<Button>().onClick.AddListener(CancelImport_onClick);

        //Launch.onRun += OnSimulationRun;
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
        InitHeroArray();
        sb = new StringBuilder();
        for (var i = 0; i < heroes.Length; i++)
        {
            sb.AppendLine(GetCsvLineFromHero(heroes[i].GetHero()));
        }

        return sb.ToString();
    }

    private String GetCsvLineFromHero(Character hero)
    {
        return String.Format(
            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},END",
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
            hero.metaRune,
            hero.weapon,
            hero.petName, // 17
            hero.PetLevel,
            hero.petProcType,
            //hero.necrosisBonus,
            //hero.hysteriaBonus,
            //hero.nightVisageBonus,
            //hero.consumptionBonus,
            //hero.decayBonus,
            //hero.aresBonus,
            //hero.bushidoBonus,
            //hero.lunarBonus,
            //hero.unity,
            //hero.divinityBonus,
            //hero.obliterationBonus,
            //hero.maruBonus,
            //hero.conductionBonus,
            //hero.illustriousBonus,
            hero.setArray[0].GetBonus(),
            hero.setArray[0].GetPieceCount(),
            hero.setArray[1].GetBonus(),
            hero.setArray[1].GetPieceCount(),
            hero.setArray[2].GetBonus(),
            hero.setArray[2].GetPieceCount(),
            hero.mythicArray[0],
            hero.mythicArray[1],
            hero.mythicArray[2],
            hero.mythicArray[3],
            hero.mythicArray[4],
            hero.mythicArray[5],

            hero.gateKeeperBonus
        );
    }


    public void UpdateHeroPanelsFromCsv(String csv)
    {
        String[] lines;
        InitHeroArray();

        lines = csv.Split('\n');

        for (var i = 0; i < heroes.Length; i++)
        {
            heroes[i].SetFieldsFromHero(GetHeroFromCsvLine(lines[i]));
        }
    }

    public Character GetHeroFromCsvLine(String csvLine)
    {
        Character hero;
        String[] values;

        values = csvLine.Split(',');

        hero = new Character()
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
            metaRune = HeroPanel.GetMetaRuneFromString(values[15]),
            weapon = HeroPanel.GetWeaponFromString(values[16]),
            petName = HeroPanel.GetPetFromString(values[17]),
            PetLevel = HeroPanel.GetPetLevelFromString(values[18]),
            petProcType = HeroPanel.GetProcTypeFromString(values[19]),
            //necrosisBonus = Convert.ToBoolean(values[20]),
            //hysteriaBonus = Convert.ToBoolean(values[21]),
            //nightVisageBonus = Convert.ToBoolean(values[22]),
            //consumptionBonus = Convert.ToBoolean(values[23]),
            //decayBonus = Convert.ToBoolean(values[24]),
            //aresBonus = Convert.ToBoolean(values[25]),
            //bushidoBonus = Convert.ToBoolean(values[26]),
            //lunarBonus = Convert.ToBoolean(values[27]),
            //unity = Convert.ToBoolean(values[28]),
            //divinityBonus = HeroPanel.GetDivinityBonusFromString(values[29]),
            //obliterationBonus = HeroPanel.GetOblitBonusFromString(values[30]),
            //maruBonus = HeroPanel.GetMarutBonusFromString(values[31]),
            //conductionBonus = HeroPanel.GetConducBonusFromString(values[32]),
            //illustriousBonus = HeroPanel.GetIllustBonusFromString(values[33]),
            setArray = new Set[] 
            {
                new Set(HeroPanel.GetSetBonusFromString(values[20]), Convert.ToInt32(values[21])),
                new Set(HeroPanel.GetSetBonusFromString(values[22]), Convert.ToInt32(values[23])),
                new Set(HeroPanel.GetSetBonusFromString(values[24]), Convert.ToInt32(values[25]))
            },
            mythicArray = new MythicBonus[]
            {
                HeroPanel.GetMythicBonusFromString(values[26]),
                HeroPanel.GetMythicBonusFromString(values[27]),
                HeroPanel.GetMythicBonusFromString(values[28]),
                HeroPanel.GetMythicBonusFromString(values[29]),
                HeroPanel.GetMythicBonusFromString(values[30]),
                HeroPanel.GetMythicBonusFromString(values[31]),
            },
            gateKeeperBonus = Convert.ToBoolean(values[32])
        };


        return hero;
    }

    private void InitHeroArray()
    {
        heroes = new HeroPanel[5];
        for (int i = 0; i < heroContainers.Length; i++)
        {
            heroes[i] = heroContainers[i].GetComponentInChildren<HeroPanel>();
        }
    }
}
