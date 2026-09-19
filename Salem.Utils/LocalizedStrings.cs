using Salem.Utils.String_Resources;
using System;

namespace Salem.Utils {
    public static class LocalizedStrings {
        public static string[] GetCountryNames() => new string[] {
            CountryNames.Afghanistan, CountryNames.Albania, CountryNames.Algeria,
            CountryNames.Andorra, CountryNames.Angola, CountryNames.AntiguaAndBarbuda,
            CountryNames.Argentina, CountryNames.Armenia, CountryNames.Austria,
            CountryNames.Azerbaijan, CountryNames.Bahrain, CountryNames.Bangladesh,
            CountryNames.Barbados, CountryNames.Belarus, CountryNames.Belgium,
            CountryNames.Belize, CountryNames.Benin, CountryNames.Bhutan,
            CountryNames.Bolivia, CountryNames.BosniaAndHerzegovina, CountryNames.Botswana,
            CountryNames.Brazil, CountryNames.Brunei, CountryNames.Bulgaria,
            CountryNames.BurkinaFaso, CountryNames.Burundi, CountryNames.CaboVerde,
            CountryNames.Cambodia, CountryNames.Cameroon, CountryNames.Canada,
            CountryNames.CentralAfricanRepublic, CountryNames.Chad, CountryNames.ChannelIslands,
            CountryNames.Chile, CountryNames.China, CountryNames.Colombia,
            CountryNames.Comoros, CountryNames.Congo, CountryNames.CostaRica,
            CountryNames.Côted_Ivoire, CountryNames.Croatia, CountryNames.Cuba,
            CountryNames.Cyprus, CountryNames.CzechRepublic, CountryNames.Denmark,
            CountryNames.Djibouti, CountryNames.Dominica, CountryNames.DominicanRepublic,
            CountryNames.DRCongo, CountryNames.Ecuador, CountryNames.Egypt,
            CountryNames.ElSalvador, CountryNames.EquatorialGuinea, CountryNames.Eritrea,
            CountryNames.Estonia, CountryNames.Eswatini, CountryNames.Ethiopia,
            CountryNames.FaeroeIslands, CountryNames.Finland, CountryNames.France,
            CountryNames.FrenchGuiana, CountryNames.Gabon, CountryNames.Gambia,
            CountryNames.Georgia, CountryNames.Germany, CountryNames.Ghana,
            CountryNames.Gibraltar, CountryNames.Greece, CountryNames.Grenada,
            CountryNames.Guatemala, CountryNames.Guinea, CountryNames.GuineaBissau,
            CountryNames.Guyana, CountryNames.Haiti, CountryNames.HolySee,
            CountryNames.Honduras, CountryNames.HongKong, CountryNames.Hungary,
            CountryNames.Iceland, CountryNames.India, CountryNames.Indonesia,
            CountryNames.Iran, CountryNames.Iraq, CountryNames.Ireland,
            CountryNames.IsleofMan, CountryNames.Israel, CountryNames.Italy,
            CountryNames.Jamaica, CountryNames.Japan, CountryNames.Jordan,
            CountryNames.Kazakhstan, CountryNames.Kenya, CountryNames.Kuwait,
            CountryNames.Kyrgyzstan, CountryNames.Laos, CountryNames.Latvia,
            CountryNames.Lebanon, CountryNames.Lesotho, CountryNames.Liberia,
            CountryNames.Libya, CountryNames.Liechtenstein, CountryNames.Lithuania,
            CountryNames.Luxembourg, CountryNames.Macao, CountryNames.Madagascar,
            CountryNames.Malawi, CountryNames.Malaysia, CountryNames.Maldives,
            CountryNames.Mali, CountryNames.Malta, CountryNames.Mauritania,
            CountryNames.Mauritius, CountryNames.Mayotte, CountryNames.Mexico,
            CountryNames.Moldova, CountryNames.Monaco, CountryNames.Mongolia,
            CountryNames.Montenegro, CountryNames.Morocco, CountryNames.Mozambique,
            CountryNames.Myanmar, CountryNames.Namibia, CountryNames.Nepal,
            CountryNames.Netherlands, CountryNames.Nicaragua, CountryNames.Niger,
            CountryNames.Nigeria, CountryNames.NorthKorea, CountryNames.NorthMacedonia,
            CountryNames.Norway, CountryNames.Oman, CountryNames.Pakistan, 
            CountryNames.Palestine, CountryNames.Panama, CountryNames.Paraguay,
            CountryNames.Peru, CountryNames.Philippines, CountryNames.Poland, 
            CountryNames.Portugal, CountryNames.Qatar, CountryNames.Réunion, 
            CountryNames.Romania, CountryNames.Russia, CountryNames.Rwanda,
            CountryNames.SaintHelena, CountryNames.SaintKittsAndNevis, CountryNames.SaintLucia,
            CountryNames.SaintVincentAndTheGrenadines, CountryNames.SanMarino, CountryNames.SaoTomePrincipe,
            CountryNames.SaudiArabia, CountryNames.Senegal, CountryNames.Serbia,
            CountryNames.Seychelles, CountryNames.SierraLeone, CountryNames.Singapore,
            CountryNames.Slovakia, CountryNames.Slovenia, CountryNames.Somalia, 
            CountryNames.SouthAfrica, CountryNames.SouthKorea, CountryNames.SouthSudan,
            CountryNames.Spain, CountryNames.SriLanka, CountryNames.Sudan,
            CountryNames.Suriname, CountryNames.Sweden, CountryNames.Switzerland,
            CountryNames.Syria, CountryNames.Taiwan, CountryNames.Tajikistan,
            CountryNames.Tanzania, CountryNames.Thailand, CountryNames.TheBahamas,
            CountryNames.TimorLeste, CountryNames.Togo, CountryNames.TrinidadAndTobago,
            CountryNames.Tunisia, CountryNames.Turkey, CountryNames.Turkmenistan,
            CountryNames.Uganda, CountryNames.Ukraine, CountryNames.UnitedArabEmirates,
            CountryNames.UnitedKingdom, CountryNames.UnitedStates, CountryNames.Uruguay,
            CountryNames.Uzbekistan, CountryNames.Venezuela, CountryNames.Vietnam,
            CountryNames.WesternSahara, CountryNames.Yemen, CountryNames.Zambia,
            CountryNames.Zimbabwe
        };

        public static string[] GetDayOfWeekNames() => new string[] {
            DayOfWeekNames.Sunday, DayOfWeekNames.Monday, DayOfWeekNames.Tuesday,
            DayOfWeekNames.Wednesday, DayOfWeekNames.Thursday, DayOfWeekNames.Friday, 
            DayOfWeekNames.Saturday
        };

        public static string[] GetMonthNames() => new string[] {
            MonthNames.January, MonthNames.February, MonthNames.March,
            MonthNames.April, MonthNames.May, MonthNames.June,
            MonthNames.July, MonthNames.August, MonthNames.September,
            MonthNames.October, MonthNames.November, MonthNames.December
        };

        public static string[] GetPaperSizeNames() => new string[] {
            PaperSizeNames.Legal, PaperSizeNames.Letter, PaperSizeNames.Tabloid, 
            PaperSizeNames.A0, PaperSizeNames.A1, PaperSizeNames.A2, 
            PaperSizeNames.A3, PaperSizeNames.A4, PaperSizeNames.A5
        };
    }
}
