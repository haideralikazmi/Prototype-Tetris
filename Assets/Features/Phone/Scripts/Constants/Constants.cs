using UnityEngine;

namespace HAK.Core
{
    public partial class Constants
    {
        public struct PhoneCharging
        {
            public static readonly string BatteryFull = "Full";
            public static readonly string BatteryEmpty = "Empty";
            public static readonly string CurrentlyCharging = "Charging";

            public static readonly Color32 BatteryFullColorCode = new (78,212,51,255);
            public static readonly Color32 BatteryEmptyColorCode = new (211,51,51,255);
            
        }
    }
}
