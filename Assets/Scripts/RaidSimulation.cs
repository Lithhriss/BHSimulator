using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class RaidSimulation : Simulation
{


    public RaidSimulation(int diffMod, int playerNumber, HeroPanel[] heroPanel)
    {
        difficultyModifier = diffMod;
        heroes = new Character[playerNumber];
        for (int i = 0; i < playerNumber; i++)
        {
            heroes[i] = heroPanel[i].GetHero();
        }
    }

    public IEnumerator Simulation(int fightCount, int boss, System.Action<float> callback, Func<bool, bool> stopSim)
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        int p;
        //if (boss == 3)
        //{
        //    enemies = new Character[2];
        //}
        float win = 0;
        float lose = 0;
        bool breakSim = false;
        int safetyNet = 2000;

        int games = fightCount;//number of times fight will run.
        int gameDivider = Convert.ToInt32(games / 100);
        progressionBar = 0;

        foreach (Character hero in heroes)
        {  //initialisation
            hero.InitialiseHero();
        }
        for (p = 0; p < games; p++)
        {
            if (stopSim(false)) break; 
            int turnCount = 0;
            float trCounter = 0;
            SetupEnemies(boss);
            Character[] charArray = new Character[heroes.Length + enemies.Length];
            int charIndex = 0;
            foreach (Character hero in heroes)
            {
                hero.Revive();
                trCounter += hero.turnRate;
                charArray[charIndex] = hero;
                charIndex++;
            }
            foreach (Character enemy in enemies)
            {
                trCounter += enemy.turnRate;
                charArray[charIndex] = enemy;
                charIndex++;
            }
            charArray = charArray.OrderByDescending(chr => chr.turnRate).ToArray();

            while (heroesAlive && enemiesAlive)
            {
                foreach (Character character in charArray)
                {
                    if (character.alive)
                    {
                        character.IncCounter();
                        if (character.counter > trCounter)
                        {
                            if (character._isHero)
                            {
                                character.IncSp(heroes);
                                character.ActivateOnTurnPassives(heroes, enemies);
                                if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                                if (simOver) break;
                                character.ChooseSkill(heroes, enemies);
                                turnCount++;
                            }
                            else
                            {
                                character.IncSp(enemies);
                                if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                                if (simOver) break;
                                character.ChooseSkill(enemies, heroes);
                            }
                            character.SubCount(trCounter);
                        }
                    }
                }
                if (stopSim(turnCount > safetyNet))
                {
                    UnityEngine.Debug.Log("Turncount is " + turnCount.ToString());
                    UnityEngine.Debug.Log("hero hp = " + heroes[0].hp + " hero power = " + heroes[0].power + " hero agility = " + heroes[0].agility);
                    UnityEngine.Debug.Log("boss hp = " + enemies[0].hp + " boss power = " + enemies[0].power + " boss agility = " + enemies[0].agility);
                    breakSim = true;
                    break;
                }
            }
            if (breakSim) break;
            if (heroesAlive)
            {
                win++;
            }
            else
            {
                lose++;
            }
            if ((float)p % gameDivider == 0 && p > 0)
            {
                progressionBar += 1;
                slider.value = progressionBar;
                winRate = (win / p) * 100;
                yield return null;
            }
        }


        winRate = (win / p) * 100;
        callback(winRate);
    }

    protected override void SetupEnemies(int boss)
    {
        enemies = new Character[1];
        enemies[0] = GetRaidBoss(boss);
        enemies[0].InitialiseMobs();
    }




    private Character GetRaidBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Kaleido");
            case 1:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "Woodbeard");
            case 2:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Robomech");
            default:
                return new Character(8, 23, 5, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "Zol"); // prio subject to change
        }
    }

    private Character GetRaidTrash(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "ArcherOrc");
            case 1:
                return new Character(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1, isNotHero, "MageOrc");
            default:
                return new Character(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "AssassinOrc");
        }
    }

    public void Log(string str)
    {
        UnityEngine.Debug.Log(str);
    }
    public void Simulation(int boss, Action<float> callback, Func<bool, bool> stopSim, Action<bool> simOutcome)
    {
        if (stopSim(false)) return;
        int safetyNet = 2000;

        foreach (var hero in heroes) hero.InitialiseHero();
        SetupEnemies(boss);
        int turnCount = 0;
        float trCounter = 0;

        Character[] charArray = new Character[heroes.Length + enemies.Length];
        int charIndex = 0;
        foreach (var hero in heroes)
        {
            hero.Revive();
            trCounter += hero.turnRate;
            charArray[charIndex] = hero;
            charIndex++;
        }
        foreach (var enemy in enemies)
        {
            trCounter += enemy.turnRate;
            charArray[charIndex] = enemy;
            charIndex++;
        }
        charArray = charArray.OrderByDescending(chr => chr.turnRate).ToArray();
        //UnityEngine.Debug.Log("player stamine  " + heroes[0].hp.ToString());
        while (heroesAlive && enemiesAlive)
        {
            foreach (var character in charArray)
            {
                if (character.alive)
                {
                    character.IncCounter();
                    if (character.counter > trCounter)
                    {
                        if (character._isHero)
                        {
                            character.IncSp(heroes);
                            character.ActivateOnTurnPassives(heroes, enemies);
                            if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                            if (simOver) break;
                            character.ChooseSkill(heroes, enemies);
                            turnCount++;
                        } else
                        {
                            character.IncSp(enemies);
                            if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                            if (simOver) break;
                            character.ChooseSkill(enemies, heroes);
                        }
                        character.SubCount(trCounter);
                    }
                }
            }
            if (stopSim(turnCount > safetyNet))
            {
                //UnityEngine.Debug.Log("turncount is " + turnCount.ToString() + " and safetyNet is " + safetyNet.ToString());
                return;
            }
        }
        simOutcome(heroesAlive);
        callback(0);
    }


}