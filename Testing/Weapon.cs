namespace Testing;

public class Weapon(string name, Dictionary<Enum, byte> suitableSlotRoles)
{
	public string Name { get; } = name;
	public Dictionary<Enum, byte> SuitableSlotRoles { get; } = suitableSlotRoles;
}
