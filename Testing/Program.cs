using NpcSquads;
using static Testing.SoldierSlotRole;
namespace Testing;

internal class Program
{
	static void Main(string[] args)
	{
		Weapon pistol = WeaponFactory.MakePistol();
		Weapon smg = WeaponFactory.MakeSmg();
		Weapon ar2 = WeaponFactory.MakeAR2();
		Weapon shotgun = WeaponFactory.MakesShotgun();
		Weapon machinegun = WeaponFactory.MakesMachinegun();
		Weapon sniper = WeaponFactory.MakesSniper();

		Soldier cp1 = new Soldier(smg);
		Soldier cp2 = new Soldier(pistol);
		Soldier cp3 = new Soldier(shotgun);
		Soldier soldier = new Soldier(ar2);
		Soldier supressor = new Soldier(machinegun, 50);
		Soldier sniperSoldier = new Soldier(sniper);

		Squad squad = new([
			FRONTLINE,
			FRONTLINE,
			FRONTLINE,
			SUPPORT,
			SUPPORT,
			COVER,
			COVER,
			HEAVY,
			SUPRESSOR,
			SNIPER
		],
		[
			cp1, cp2, cp3, soldier, supressor, sniperSoldier
		]);

		Console.WriteLine(squad);
	}
}
