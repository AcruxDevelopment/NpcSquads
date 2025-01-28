using NpcSquads;

namespace Testing;

public class Soldier(Weapon weapon, byte health = 100) : Member
{
	public byte Health { get; set; } = health;
	public Weapon Weapon { get; } = weapon;

	public override byte GetRoleSuitability(Enum squadRole)
	{
		return (byte)(Weapon.SuitableSlotRoles[squadRole] * ((float)Health / 100));
	}

	public override string ToString()
	{
		return $"{Weapon.Name} {Health}%";
	}
}
