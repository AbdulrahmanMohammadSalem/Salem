using System;
using System.Collections.Generic;

namespace Salem.Extensions {
    public static class PremitiveExtensions {
        public static bool In<T>(this T value, params T[] values) {
            foreach (var V in values)
                if (EqualityComparer<T>.Default.Equals(value, V))
                    return true;

            return false;
        }

        public static bool Between<T>(this T value, T value1, T value2) where T : struct, IComparable<T> {
            return value.CompareTo(value1) >= 0 && value.CompareTo(value2) <= 0; 
        }

        public static T ClampBetween<T>(this T value, T min, T max) where T : struct, IComparable<T> {
            return value.CompareTo(min) <= 0 ? min : (value.CompareTo(max) >= 0 ? max : value);
        }
    }
}
