namespace NpcSquads.Squads;

/// <summary>
/// Organizes a collection of <see cref="Member"/>s 
/// strategically based on <see cref="Slot"/>s.
/// </summary>
public class Squad
{
    private List<Member> members = new();
    private List<Slot> slots = new();

    /// <summary>
    /// The entities that are part of this squad.
    /// </summary>
    public IReadOnlyList<Member> Members
        => members.AsReadOnly();

    /// <summary>
    /// The <see cref="Slot"/>s of this squad.
    /// </summary>
    public IReadOnlyList<Slot> Slots
        => slots.AsReadOnly();

    /// <summary>
    /// Whether more <see cref="Member"/>s can be added to this squad.
    /// </summary>
    public bool CanAddMoreMembers
        => members.Count < slots.Count;


    #region Member Management
    /// <summary>
    /// Adds a member to this <see cref="Squad"/>.
    /// <para>
    /// An <see cref="InvalidOperationException"/> will be thrown if there are no space left to add a <see cref="Member"/>.
    /// </para>
    /// </summary>
    /// <param name="member">The member to add.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void AddMember(Member member)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (member.Squad == this) return;
        if (!CanAddMoreMembers) throw new InvalidOperationException($"Cannot add more members to this squad ({this}), There are no more slots free.");

        // Make member leave its current squad first (if applicable)
        member.Squad?.RemoveMember(member);

        // Add references to the squad and member to eachother
        member.Squad = this;
        members.Add(member);
    }

    /// <summary>
    /// Remove a member from this <see cref="Squad"/>.
    /// </summary>
    /// <param name="member">The member to remove.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void RemoveMember(Member member)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (member.Squad != this) return;

        // Make the member leave its current slot
        if (member.SquadSlot != null)
            member.SquadSlot.Occupant = null;

        // Make the member leave its current squad
        member.Squad = null;
        members.Remove(member);
    }
    #endregion

    #region Slot Management
    /// <summary>
    /// Automatically assings the <see cref="Members"/> into the <see cref="Slots"/>
    /// based on each <see cref="Member"/> suitability for that <see cref="Slot"/>'s role.
    /// </summary>
    public void ReassignSlots()
    {
        ReassignSlots(slots.Count, members.ToHashSet());
    }

    private void ReassignSlots(int slotLimit, HashSet<Member> members)
    {
        // The slots that will be reassigned in this call
        List<Slot> slotRange = slots[..slotLimit];

        // Clear the slots from the slotRange
        foreach (var slot in slotRange)
            slot.Occupant = null;

        while (members.Count != 0)
        {
            // The members that were assigned in the current slots interation
            // (the next foreach)
            HashSet<Member> assignedMembers = [];

            foreach (var slot in slotRange)
            {
                if (slot.Occupant != null) continue;

                var member = GetMemeberFor(slot.Role, members);
                if (member == null) continue;

                var memberPreviousSlot = member.SquadSlot;
                slot.Occupant = member;
                assignedMembers.Add(member);

                if (memberPreviousSlot != null && AreAllSlotsOfRoleEmpty(memberPreviousSlot.Role, slotRange))
                {
                    int branchSlotLimit = slotRange.IndexOf(memberPreviousSlot) + 1;
                    ReassignSlots(branchSlotLimit, GetMembersFromSlots(slotRange, branchSlotLimit));
                }
            }

            foreach (var member in assignedMembers)
                members.Remove(member);
        }

        #region Local Functions
        HashSet<Member> GetMembersFromSlots(List<Slot> slots, int slotLimit)
        {
            List<Slot> slotRange = slots[..slotLimit];

            HashSet<Member> members = [];
            foreach (var slot in slotRange)
            {
                if (slot.Occupant == null) continue;
                members.Add(slot.Occupant);
            }
            return members;
        }

        bool AreAllSlotsOfRoleEmpty(Enum slotRole, IEnumerable<Slot> slots)
        {
            return slots.Where(x => x.Role == slotRole).All(x => x.Occupant == null);
        }

        Member? GetMemeberFor(Enum slotRole, HashSet<Member> members)
        {
            Member? mostSuitableMember = null;
            int mostSuitableSuitability = int.MinValue;

            foreach (var member in members)
            {
                int currSuitability = member.GetRoleSuitability(slotRole);
                if (currSuitability > mostSuitableSuitability)
                {
                    mostSuitableMember = member;
                    mostSuitableSuitability = currSuitability;
                }
            }

            return mostSuitableMember;
        }
        #endregion
    }
    #endregion

    public override string ToString()
    {
        string str = "[SQUAD]\n";
        int i = 0;
        foreach (var slot in slots)
        {
            str += $"[{i.ToString().PadRight(2, ' ')}] {slot}\n";
            ++i;
        }
        return str;
    }

    /// <summary>
    /// Creates a squad specifying the amount of slots and their roles
    /// in the form of an <see cref="Enum"/> array.
    /// <para>
    /// An <see cref="ArgumentException"/> will be thrown
    /// if the slot roles are not of the same <see cref="Enum"/> type.
    /// </para>
    /// </summary>
    /// <param name="slotRoles">The slot roles for this squad.</param>
    /// <exception cref="ArgumentException"></exception>
    public Squad(Enum[] slotRoles)
    {
        // Check types from the slotRoles argument
        if (slotRoles.Length != 0)
        {
            Type expectedSlotRolesType = slotRoles[0].GetType();
            if (!SquadSlotRolesAttribute.HasAttribute(expectedSlotRolesType))
                throw new ArgumentException("The enum type doesnt contain the SquadSlotRolesAttribute");

            bool areAllSlotRolesTypeTheSame = slotRoles.All(x => x.GetType() == expectedSlotRolesType);
            if (!areAllSlotRolesTypeTheSame)
                throw new ArgumentException("All the given values should be of the same enum type.", nameof(slotRoles));
        }

        // Sort the slot roles by hierachical order
        slotRoles = slotRoles.OrderByDescending(x => Convert.ToByte(x)).ToArray();

        foreach (var slotRole in slotRoles)
        {
            slots.Add(new(slotRole, this));
        }
    }

    public Squad(Enum[] slotRoles, Member[] members) : this(slotRoles)
    {
        foreach (var member in members)
        {
            AddMember(member);
        }
        ReassignSlots();
    }
}
