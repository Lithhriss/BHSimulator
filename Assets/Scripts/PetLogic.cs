using System.Collections;
using System.Collections.Generic;
using System;

class PetLogic
{
	public static void TeamHealPet(int l)
	{
		int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.072);
		float healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.324 * Simulation.hero[l].power);

		bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(20f);

		if (critroll)
		{
			healValue *= Simulation.hero[l].critDamage;
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			healValue *= 2;
		}
		if (petRoll)
		{
			for (int i = 0; i < 5; i++)
			{
				if (Simulation.hero[i].hp > 0)
				{
					Simulation.hero[i].hp += Convert.ToInt32(healValue);
					if (Simulation.hero[i].hp >= Simulation.hero[i].maxHp)
					{
						Simulation.hero[i].hp = Simulation.hero[i].maxHp;
					}
				}
			}
		}
	}

	public static void OffPetProc(int l)
	{
		int attackModifier = Convert.ToInt32(0.54 * Simulation.hero[l].power);
		int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.63);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll((float)20);

		if (critRoll)
		{
			attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			attackValue *= 2;
		}
		if (petRoll)
		{
			Simulation.hpDummy -= attackValue;
		}

	}

	public static void SuperOffPetProc(int l)
	{
		int attackModifier = Convert.ToInt32(Simulation.hero[l].power * 0.37);
		int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 1.668);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(10f);

		if (critRoll)
		{
			attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			attackValue *= 2;
		}
		if (petRoll)
		{
			Simulation.hpDummy -= attackValue;
		}

	}

	public static void SpreadHealPet(int l)
	{
		int i;
		int target = 0;
		int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.14);
		int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.66 * Simulation.hero[l].power);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(20f);

		if (critRoll)
		{
			healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			healValue *= 2;
		}
		if (petRoll)
		{
			for (i = 0; i < healValue; i++)
			{
				target = Logic.HealLogic();
				Simulation.hero[target].hp++;
				if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
				{
					Simulation.hero[target].hp = Simulation.hero[target].maxHp;
				}
			}
		}
	}

	public static void SuperSpreadHealPet(int l)
	{
		int i;
		int target = 0;
		int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.288);
		int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 1.296 * Simulation.hero[l].power);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll((float)10);

		if (critRoll)
		{
			healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			healValue *= 2;
		}
		if (petRoll)
		{
			for (i = 0; i < healValue; i++)
			{
				target = Logic.HealLogic();
				Simulation.hero[target].hp++;
				if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
				{
					Simulation.hero[target].hp = Simulation.hero[target].maxHp;
				}
			}
		}
	}

	public static void TeamShieldPet(int l)
	{
		int i;
		int shieldModifier = Convert.ToInt32(Simulation.hero[l].power * 0.06);
		float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.27 * Simulation.hero[l].power);

		bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(20f);

		if (critroll)
		{
			shieldValue *= Simulation.hero[l].critDamage;
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			shieldValue *= 2;
		}
		if (petRoll)
		{
			for (i = 0; i < 5; i++)
			{
				if (Simulation.hero[i].hp > 0)
				{
					Simulation.hero[i].shield += Convert.ToInt32(shieldValue);
					if (Simulation.hero[i].shield >= Simulation.hero[i].maxShield)
					{
						Simulation.hero[i].shield = Simulation.hero[i].maxShield;
					}
				}
			}
		}
	}

	public static void SuperTeamShieldPet(int l)
	{
		int i;
		int shieldModifier = Convert.ToInt32(Simulation.hero[l].power * 0.12);
		float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.54 * Simulation.hero[l].power);

		bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(10f);

		if (critroll)
		{
			shieldValue *= Simulation.hero[l].critDamage;
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			shieldValue *= 2;
		}
		if (petRoll)
		{
			for (i = 0; i < 5; i++)
			{
				if (Simulation.hero[i].hp > 0)
				{
					Simulation.hero[i].shield += Convert.ToInt32(shieldValue);
					if (Simulation.hero[i].shield >= Simulation.hero[i].maxShield)
					{
						Simulation.hero[i].shield = Simulation.hero[i].maxShield;
					}
				}
			}
		}
	}


	public static void SuperSelfHealPet(int l)
	{
		int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.454);
		int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.89 * Simulation.hero[l].power);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll((float)10);

		if (critRoll)
		{
			healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			healValue *= 2;
		}
		if (petRoll)
		{
			Simulation.hero[l].hp += healValue;
			if (Simulation.hero[l].hp > Simulation.hero[l].maxHp)
			{
				Simulation.hero[l].hp = Simulation.hero[l].maxHp;
			}
		}
	}

	public static void TargetWeakestOffPet(int l)
	{
		int attackModifier = Convert.ToInt32(Simulation.hero[l].power * 0.64);
		int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.48);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(20f);

		if (critRoll)
		{
			attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			attackValue *= 2;
		}
		if (petRoll)
		{
			Simulation.hpDummy -= attackValue;
		}

	}

	public static void TargetWeakestHealPet(int l)
	{
		int i;
		int target = 0;
		int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.288);
		int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.576 * Simulation.hero[l].power);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(20f);

		if (critRoll)
		{
			healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			healValue *= 2;
		}
		if (petRoll)
		{
			target = Logic.HealLogic();
			Simulation.hero[target].hp += healValue;
			if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
			{
				Simulation.hero[target].hp = Simulation.hero[target].maxHp;
			}
		}
	}

	public static void TeamHealShieldpet (int l)
	{
		int regenModifier = Convert.ToInt32(Simulation.hero[l].power * 0.034);
		float regenValue = Convert.ToInt32(UnityEngine.Random.Range(0, regenModifier) + 0.153 * Simulation.hero[l].power);

		bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll(20f);

		if (critroll)
		{
			regenValue *= Simulation.hero[l].critDamage;
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			regenValue *= 2;
		}
		if (petRoll)
		{
			for (int i = 0; i < 5; i++)
			{
				if (Simulation.hero[i].hp > 0)
				{
					Simulation.hero[i].hp += Convert.ToInt32(regenValue);
					if (Simulation.hero[i].hp >= Simulation.hero[i].maxHp)
					{
						Simulation.hero[i].hp = Simulation.hero[i].maxHp;
					}
					Simulation.hero[i].shield += Convert.ToInt32(regenValue);
					if (Simulation.hero[i].shield >= Simulation.hero[i].maxShield)
					{
						Simulation.hero[i].shield = Simulation.hero[i].maxShield;
					}
				}
			}
		}
	}

	public static void RandomOffPpet(int l)
	{
		int attackModifier = Convert.ToInt32(1.76 * Simulation.hero[l].power);
		int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.22);

		bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
		bool petRoll = Logic.RNGroll((float)20);

		if (critRoll)
		{
			attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
		}
		if (Logic.RNGroll(Simulation.hero[l].empowerChance))
		{
			attackValue *= 2;
		}
		if (petRoll)
		{
			Simulation.hpDummy -= attackValue;
		}
	}

	public static void PetSelection(int k)
	{
		switch (Simulation.hero[k].pet)
		{
			case Hero.Pet.Nelson:
				OffPetProc(k);
				break;
			case Hero.Pet.Gemmi:
				TeamHealPet(k);
				break;
			case Hero.Pet.Boogie:
				SpreadHealPet(k);
				break;
			case Hero.Pet.Nemo:
				SuperOffPetProc(k);
				break;
			case Hero.Pet.Crem:
				SuperSpreadHealPet(k);
				break;
			case Hero.Pet.Boiguh:
				TeamShieldPet(k);
				break;
			case Hero.Pet.Nerder:
				SuperSelfHealPet(k);
				break;
			case Hero.Pet.Quimby:
				TargetWeakestOffPet(k);
				break;
			case Hero.Pet.Snut:
				SuperTeamShieldPet(k);
				break;
			case Hero.Pet.Wuvboi:
				TeamHealShieldpet(k);
				break;
			case Hero.Pet.Buvboi:
				RandomOffPpet(k);
				break;
			case Hero.Pet.Skulldemort:
				TargetWeakestHealPet(k);
				break;
		}
	}
}