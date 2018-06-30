using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class WorldBossSimulation : Simulation
{
    public WorldBossSimulation(int _difficultyModifier, int playerNumber, HeroPanel[] heroPanel)
    {
        difficultyModifier = _difficultyModifier;
        heroes = new Character[playerNumber];
        for (int i = 0; i < playerNumber; i++)
        {
            heroes[i] = heroPanel[i].GetHero();
        }
    }

    public IEnumerator Simulation(int fightCount, int boss, System.Action<float> callback, Func<bool, bool> InvokeStopSim)
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        int p;

        
        int games = fightCount;
        int gameDivider = Convert.ToInt32(games / 100);
        float win = 0;
        float lose = 0;
        bool stopSim = false;
        int safetyNet = 2000;


        foreach (Character hero in heroes)
        {  //initialisation

            hero.InitialiseHero();
        }
        for (p = 0; p < games; p++)
        {
            if (InvokeStopSim(false)) break;
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
            if (InvokeStopSim(turnCount > safetyNet))
            {
                //UnityEngine.Debug.Log("Turncount is " + turnCount.ToString());
                stopSim = true;
            }
            while (heroesAlive && enemiesAlive && !stopSim)
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
                if (InvokeStopSim(turnCount > safetyNet))
                {
                    UnityEngine.Debug.Log("Turncount is " + turnCount.ToString());
                    UnityEngine.Debug.Log("hero hp = " + heroes[0].hp + " hero power = " + heroes[0].power + " hero agility = " + heroes[0].agility);
                    stopSim = true;
                }

            }
            if (stopSim) break;
            if (heroesAlive)
            {
                win++;
            }
            else
            {
                lose++;
            }
            if (p % gameDivider == 0 && p > 0)
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
        //int bossType = rand.Next(3);
        Character placeholder;
        Character bossPlaceholder;
        if (boss == 0)
        {
            Character dpsPlaceholder = GetOrlagDPS(Logic.random.Next(3));
            Character tankPlaceholder = GetOrlagTank(Logic.random.Next(2));
            placeholder = GetOrlagMix(Logic.random.Next(5));
            bossPlaceholder = GetOrlagBoss(Logic.random.Next(3));
            List<Character> enemyList = new List<Character>() { dpsPlaceholder, tankPlaceholder, placeholder, bossPlaceholder };
            enemies = enemyList.OrderByDescending(mob => mob.priority).ToArray();
        }
        else
        {
            enemies = new Character[2];
            bossPlaceholder = GetNetherBoss(Logic.random.Next(3));
            placeholder = GetNetherMix(Logic.random.Next(5));
            if (bossPlaceholder.priority > placeholder.priority)
            {
                enemies[0] = bossPlaceholder;
                enemies[1] = placeholder;
            }
            else
            {
                enemies[1] = bossPlaceholder;
                enemies[0] = placeholder;
            }
        }
        foreach (Character mob in enemies)
        {
            mob.InitialiseMobs();
        }
    }
    private Character GetOrlagBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(9, 22, 9, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 5f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "BlueOrc");
            case 1:
                return new Character(11, 18, 11, 30f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "GreenOrc");
            default:
                return new Character(10, 20, 10, 10f, 50f, 10f, 10f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "PurpleOrc");
        }
    }
    private Character GetOrlagMix(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "ArcherOrc");
            case 1:
                return new Character(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1, isNotHero, "MageOrc");
            case 2:
                return new Character(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "AssassinOrc");
            case 3:
                return new Character(0.4f, 11f, 0.6f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 10f, 0f, 0f, 0f, 0f, difficultyModifier, 4, isNotHero, "MeatOrc");
            default:
                return new Character(2.2f, 7f, 2.8f, 10f, 50f, 0f, 0f, 15f, 2.5f, 4.5f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3, isNotHero, "BruiserOrc");
        }
    }
    private Character GetOrlagDPS(int index)
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
    private Character GetOrlagTank(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(0.4f, 11f, 0.6f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 10f, 0f, 0f, 0f, 0f, difficultyModifier, 4, isNotHero, "MeatOrc");
            default:
                return new Character(2.2f, 7f, 2.8f, 10f, 50f, 0f, 0f, 15f, 2.5f, 4.5f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3, isNotHero, "BruiserOrc");
        }
    }

    private Character GetNetherBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(7f, 18f, 7f, 10f, 50f, 0f, 0f, 0f, 12.5f, 6f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2.9, isNotHero, "BlueNether");
            case 1:
                return new Character(7.5f, 17f, 7.5f, 30f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "PurpleNether");
            default:
                return new Character(8f, 16f, 8f, 50f, 50, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "YellowNether");
        }
    }
    private Character GetNetherMix(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(4f, 6f, 4f, 10f, 250f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "DemonNether");
            case 1:
                return new Character(4f, 5f, 4f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1, isNotHero, "ImpNether");
            case 2:
                return new Character(5.5f, 3f, 5.5f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1, isNotHero, "MageNether");
            case 3:
                return new Character(5f, 4.5f, 5f, 10f, 50f, 0f, 0f, 0f, 2, 5f, 0f, 15f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "BeastNether");
            default:
                return new Character(3.5f, 7f, 3.5f, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3, isNotHero, "TankNether");
        }
    }

    public void Simulation(int boss, Action<float> callback, Func<bool, bool> stopSim, Action<bool> simOutcome)
    {
    }
}
