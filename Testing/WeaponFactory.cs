using static Testing.SoldierSlotRole;
namespace Testing;

public static class WeaponFactory
{
	public static Weapon MakePistol()
		=> new Weapon("Pistol", new()
		{
		{ SUPPORT, 255 },
		{ COVER, 200 },
		{ FRONTLINE, 127 },
		{ OCCLUSOR, 90 },
		{ HEAVY, 15 },
		{ SUPRESSOR, 6 },
		{ SNIPER, 3 }
		});

	public static Weapon MakeSmg()
		=> new Weapon("Smg", new()
		{
		{ FRONTLINE, 255 },
		{ SUPPORT, 200 },
		{ COVER, 160 },
		{ OCCLUSOR, 129 },
		{ SUPRESSOR, 90 },
		{ HEAVY, 20 },
		{ SNIPER, 7 }
		});

	public static Weapon MakeAR2()
		=> new Weapon("AR2", new()
		{
		{ FRONTLINE, 200 },
		{ COVER, 255 },
		{ OCCLUSOR, 127 },
		{ SUPPORT, 110 },
		{ SUPRESSOR, 75 },
		{ HEAVY, 30 },
		{ SNIPER, 17 }
		});

	public static Weapon MakesShotgun()
		=> new Weapon("Shotgun", new()
		{
		{ HEAVY, 255 },
		{ OCCLUSOR, 200 },
		{ COVER, 170 },
		{ FRONTLINE, 122 },
		{ SUPRESSOR, 90 },
		{ SUPPORT, 60 },
		{ SNIPER, 1 }
		});

	public static Weapon MakesMachinegun()
		=> new Weapon("Machinegun", new()
		{
		{ SUPRESSOR, 255 },
		{ HEAVY, 200 },
		{ OCCLUSOR, 255 },
		{ FRONTLINE, 190 },
		{ SNIPER, 127 },
		{ COVER, 90 },
		{ SUPPORT, 50 }
		});

	public static Weapon MakesSniper()
		=> new Weapon("Sniper", new()
		{
		{ SNIPER, 255 },
		{ OCCLUSOR, 185 },
		{ COVER, 172 },
		{ SUPRESSOR, 100 },
		{ FRONTLINE, 55 },
		{ HEAVY, 33 },
		{ SUPPORT, 7 }
		});
}
