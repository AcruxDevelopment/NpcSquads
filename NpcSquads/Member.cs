namespace NpcSquads;

/// <summary>
/// Represents an entity that can be
/// a member of a <see cref="NpcSquads.Squad"/>.
/// </summary>
public abstract class Member
{
	/// <summary>
	/// The <see cref="NpcSquads.Squad"/> where this member belongs to.<br/>
	/// This property is managed by said <see cref="NpcSquads.Squad"/>.
	/// </summary>
	public Squad? Squad {  get; internal set; }

	/// <summary>
	/// The <see cref="Slot"/> where this member belongs to.<br/>
	/// This property is managed by said <see cref="Slot"/>.
	/// </summary>
	public Slot? SquadSlot { get; internal set; }

	#region Squad Management
	/// <summary>
	/// Make this member join a <see cref="NpcSquads.Squad"/>.<br/>
	/// If already in one, leave it.
	/// </summary>
	/// <param name="squad">The squad to join to.</param>
	public void JoinSquad(Squad squad)
	{
		squad.AddMember(this);
	}

	/// <summary>
	/// Make this member leave its <see cref="Squad"/>.<br/>
	/// If not in one, do nothing.
	/// </summary>
	/// <param name="squad">The squad to join to.</param>
	public void LeaveSquad()
	{
		Squad?.RemoveMember(this);
	}
	#endregion

	#region Slot Management
	public void LeaveSlot()
	{
		if (SquadSlot == null) return;
		SquadSlot.Occupant = null;
	}
	#endregion

	/// <summary>
	/// Get the suitability level for the given squad role.
	/// </summary>
	/// <param name="squadRole">The squad role to check this member's suitability.</param>
	/// <returns>An unsigned numeric value representing the suitability level.</returns>
	public abstract byte GetRoleSuitability(Enum squadRole);
}
