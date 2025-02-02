using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NpcSquads.Squads
{
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class SquadSlotRolesAttribute : Attribute
    {
        public static bool HasAttribute(Type type)
        {
            return type.GetCustomAttribute(typeof(SquadSlotRolesAttribute)) != null;
        }
    }
}
