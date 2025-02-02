namespace NpcSquads.Squads;

/// <summary>
/// A <see cref="Squads.Squad"/> slot to assign roles
/// to <see cref="Member"/>s based on their suitability.
/// </summary>
public class Slot
{

    private Member? occupant;

    /// <summary>
    /// The role the <see cref="Occupant"/> will serve in the <see cref="Squad"/>.
    /// </summary>
    public Enum Role { get; }

    /// <summary>
    /// The <see cref="Squad"/> this slot belongs to.
    /// </summary>
    public Squad Squad { get; }

    /// <summary>
    /// The <see cref="Member"/> occupying this slot.<br/>
    /// </summary>
    public Member? Occupant
    {
        get => occupant;
        internal set
        {
            // Corroborate new member belongs to this slot's squad
            if (value != null && value.Squad != Squad)
                throw new InvalidOperationException($"Cannot assign given member '{value}' as an occupant of the slot whose squad is '{Squad}' since said member doesn't belong to the slot's squad.");

            // Remove current occupant
            if (occupant != null)
            {
                occupant.SquadSlot = null;
            }

            // Handle null as new ocuppant
            if (value == null)
            {
                occupant = null;
                return;
            }

            // Remove new member from its current slot
            value.LeaveSlot();

            // Set new value as ocuppant
            value.SquadSlot = this;
            occupant = value;
        }
    }

    public override string ToString()
    {
        return $"{Role.ToString().PadRight(10, ' ')}: {Occupant?.ToString() ?? ""}";
    }

    /// <summary>
    /// Create a new <see cref="Slot"/> for a <see cref="Squads.Squad"/>.<br/>
    /// This constructor should only be created by the target <see cref="Squads.Squad"/>.
    /// </summary>
    /// <param name="role"></param>
    /// <param name=""></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal Slot(Enum role, Squad squad)
    {
        if (role == null) throw new ArgumentNullException();
        Role = role;
        Squad = squad;
    }
}
