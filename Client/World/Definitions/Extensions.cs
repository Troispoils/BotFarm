using System;

namespace Client.World.Definitions
{
    public static class Extensions
    {
        public static bool IsHorde(this Race race)
        {
            return race == Race.Bloodelf ||
                   race == Race.Orc ||
                   race == Race.Tauren ||
                   race == Race.Troll ||
                   race == Race.Undead;
        }

        public static bool IsAlliance(this Race race)
        {
            return race == Race.Draenei ||
                   race == Race.Dwarf ||
                   race == Race.Gnome ||
                   race == Race.Human ||
                   race == Race.Nightelf;
        }

        public static bool HasAnyFlag(this IConvertible value, IConvertible flag)
        {
            var uFlag = flag.ToUInt64(null);
            var uThis = value.ToUInt64(null);

            return (uThis & uFlag) != 0;
        }
    }
}

