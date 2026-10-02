using Salem.Utils.Image_Resources;
using System.ComponentModel;
using System.Drawing;

namespace Salem.Utils {
    /// <summary>
    /// Specifies country codes as enumeration values, each representing a recognized sovereign state or territory.
    /// </summary>
    /// <remarks>Enumeration values correspond to countries and territories, suitable for use in applications
    /// requiring standardized country identification.</remarks>
    public enum Countries : byte {
        Afghanistan = 1, Albania, Algeria, Andorra, Angola, AntiguaAndBarbuda, Argentina, Armenia,
        Austria, Azerbaijan, Bahrain, Bangladesh, Barbados, Belarus, Belgium, Belize,
        Benin, Bhutan, Bolivia, BosniaAndHerzegovina, Botswana, Brazil, Brunei, Bulgaria,
        BurkinaFaso, Burundi, CaboVerde, Cambodia, Cameroon, Canada, CentralAfricanRepublic, Chad,
        ChannelIslands, Chile, China, Colombia, Comoros, Congo, CostaRica, CôtedIvoire,
        Croatia, Cuba, Cyprus, CzechRepublic, Denmark, Djibouti, Dominica, DominicanRepublic,
        DRCongo, Ecuador, Egypt, ElSalvador, EquatorialGuinea, Eritrea, Estonia, Eswatini,
        Ethiopia, FaeroeIslands, Finland, France, FrenchGuiana, Gabon, Gambia, Georgia,
        Germany, Ghana, Gibraltar, Greece, Grenada, Guatemala, Guinea, GuineaBissau,
        Guyana, Haiti, HolySee, Honduras, HongKong, Hungary, Iceland, India,
        Indonesia, Iran, Iraq, Ireland, IsleOfMan, Israel, Italy, Jamaica,
        Japan, Jordan, Kazakhstan, Kenya, Kuwait, Kyrgyzstan, Laos, Latvia,
        Lebanon, Lesotho, Liberia, Libya, Liechtenstein, Lithuania, Luxembourg, Macao,
        Madagascar, Malawi, Malaysia, Maldives, Mali, Malta, Mauritania, Mauritius,
        Mayotte, Mexico, Moldova, Monaco, Mongolia, Montenegro, Morocco, Mozambique,
        Myanmar, Namibia, Nepal, Netherlands, Nicaragua, Niger, Nigeria, NorthKorea,
        NorthMacedonia, Norway, Oman, Pakistan, Palestine, Panama, Paraguay, Peru, Philippines,
        Poland, Portugal, Qatar, Réunion, Romania, Russia, Rwanda, SaintHelena,
        SaintKittsAndNevis, SaintLucia, SaintVincentAndTheGrenadines, SanMarino, SaoTomePrincipe, SaudiArabia, Senegal, Serbia,
        Seychelles, SierraLeone, Singapore, Slovakia, Slovenia, Somalia, SouthAfrica, SouthKorea,
        SouthSudan, Spain, SriLanka, Sudan, Suriname, Sweden, Switzerland,
        Syria, Taiwan, Tajikistan, Tanzania, Thailand, TheBahamas, TimorLeste, Togo,
        TrinidadAndTobago, Tunisia, Turkey, Turkmenistan, Uganda, Ukraine, UnitedArabEmirates, UnitedKingdom,
        UnitedStates, Uruguay, Uzbekistan, Venezuela, Vietnam, WesternSahara, Yemen, Zambia, Zimbabwe
    }

    public class CountryDTO {
        public CountryDTO(Countries countryID, string localizedCountryName, Image countryFlagImage) {
            CountryID = countryID;
            LocalizedCountryName = localizedCountryName;
            CountryFlagImage = countryFlagImage;
        }

        public Countries CountryID { get; }
        public string LocalizedCountryName { get; }
        public Image CountryFlagImage { get; }

        public override string ToString() => LocalizedCountryName;

        public static Bitmap FindFlagImage_Square64(Countries countryID) {
            switch (countryID) {
                case Countries.Afghanistan: return CountryFlags_Square64.Flag_of_Afghanistan_Flat_Square_64x64;
                case Countries.Albania: return CountryFlags_Square64. Flag_of_Albania_Flat_Square_64x64;
                case Countries.Algeria: return CountryFlags_Square64. Flag_of_Algeria_Flat_Square_64x64;
                case Countries.Andorra: return CountryFlags_Square64. Flag_of_Andorra_Flat_Square_64x64;
                case Countries.Angola: return CountryFlags_Square64. Flag_of_Angola_Flat_Square_64x64;
                case Countries.AntiguaAndBarbuda: return CountryFlags_Square64. Flag_of_Antigua_and_Barbuda_Flat_Square_64x64;
                case Countries.Argentina: return CountryFlags_Square64. Flag_of_Argentina_Flat_Square_64x64;
                case Countries.Armenia: return CountryFlags_Square64. Flag_of_Armenia_Flat_Square_64x64;
                case Countries.Austria: return CountryFlags_Square64. Flag_of_Austria_Flat_Square_64x64;
                case Countries.Azerbaijan: return CountryFlags_Square64. Flag_of_Azerbaijan_Flat_Square_64x64;
                case Countries.Bahrain: return CountryFlags_Square64. Flag_of_Bahrain_Flat_Square_64x64;
                case Countries.Bangladesh: return CountryFlags_Square64. Flag_of_Bangladesh_Flat_Square_64x64;
                case Countries.Barbados: return CountryFlags_Square64. Flag_of_Barbados_Flat_Square_64x64;
                case Countries.Belarus: return CountryFlags_Square64. Flag_of_Belarus_Flat_Square_64x64;
                case Countries.Belgium: return CountryFlags_Square64. Flag_of_Belgium_Flat_Square_64x64;
                case Countries.Belize: return CountryFlags_Square64. Flag_of_Belize_Flat_Square_64x64;
                case Countries.Benin: return CountryFlags_Square64. Flag_of_Benin_Flat_Square_64x64;
                case Countries.Bhutan: return CountryFlags_Square64. Flag_of_Bhutan_Flat_Square_64x64;
                case Countries.Bolivia: return CountryFlags_Square64. Flag_of_Bolivia_Flat_Square_64x64;
                case Countries.BosniaAndHerzegovina: return CountryFlags_Square64. Flag_of_Bosnia_and_Herzegovina_Flat_Square_64x64;
                case Countries.Botswana: return CountryFlags_Square64. Flag_of_Botswana_Flat_Square_64x64;
                case Countries.Brazil: return CountryFlags_Square64. Flag_of_Brazil_Flat_Square_64x64;
                case Countries.Brunei: return CountryFlags_Square64. Flag_of_Brunei_Flat_Square_64x64;
                case Countries.Bulgaria: return CountryFlags_Square64. Flag_of_Bulgaria_Flat_Square_64x64;
                case Countries.BurkinaFaso: return CountryFlags_Square64. Flag_of_Burkina_Faso_Flat_Square_64x64;
                case Countries.Burundi: return CountryFlags_Square64. Flag_of_Burundi_Flat_Square_64x64;
                case Countries.CaboVerde: return CountryFlags_Square64. Flag_of_Cape_Verde_Flat_Square_64x64;
                case Countries.Cambodia: return CountryFlags_Square64. Flag_of_Cambodia_Flat_Square_64x64;
                case Countries.Cameroon: return CountryFlags_Square64. Flag_of_Cameroon_Flat_Square_64x64;
                case Countries.Canada: return CountryFlags_Square64. Flag_of_Canada_Flat_Square_64x64;
                case Countries.CentralAfricanRepublic: return CountryFlags_Square64. Flag_of_Central_African_Republic_Flat_Square_64x64;
                case Countries.Chad: return CountryFlags_Square64. Flag_of_Chad_Flat_Square_64x64;
                case Countries.ChannelIslands: return CountryFlags_Square64. No_Image_64x64;
                case Countries.Chile: return CountryFlags_Square64. Flag_of_Chile_Flat_Square_64x64;
                case Countries.China: return CountryFlags_Square64. Flag_of_Peoples_Republic_of_China_Flat_Square_64x64;
                case Countries.Colombia: return CountryFlags_Square64. Flag_of_Colombia_Flat_Square_64x64;
                case Countries.Comoros: return CountryFlags_Square64. Flag_of_Comoros_Flat_Square_64x64;
                case Countries.Congo: return CountryFlags_Square64. Flag_of_Republic_of_Congo_Flat_Square_64x64;
                case Countries.CostaRica: return CountryFlags_Square64. Flag_of_Costa_Rica_Flat_Square_64x64;
                case Countries.CôtedIvoire: return CountryFlags_Square64. Flag_of_Côte_dIvoire_Flat_Square_64x64;
                case Countries.Croatia: return CountryFlags_Square64. Flag_of_Croatia_Flat_Square_64x64;
                case Countries.Cuba: return CountryFlags_Square64. Flag_of_Cuba_Flat_Square_64x64;
                case Countries.Cyprus: return CountryFlags_Square64. Flag_of_Cyprus_Flat_Square_64x64; ;
                case Countries.CzechRepublic: return CountryFlags_Square64. Flag_of_Czech_Republic_Flat_Square_64x64;
                case Countries.Denmark: return CountryFlags_Square64. Flag_of_Denmark_Flat_Square_64x64;
                case Countries.Djibouti: return CountryFlags_Square64. Flag_of_Djibouti_Flat_Square_64x64;
                case Countries.Dominica: return CountryFlags_Square64. Flag_of_Dominica_Flat_Square_64x64;
                case Countries.DominicanRepublic: return CountryFlags_Square64. Flag_of_Dominican_Republic_Flat_Square_64x64;
                case Countries.DRCongo: return CountryFlags_Square64. Flag_of_Democratic_Republic_of_Congo_Flat_Square_64x64;
                case Countries.Ecuador: return CountryFlags_Square64. Flag_of_Ecuador_Flat_Square_64x64;
                case Countries.Egypt: return CountryFlags_Square64. Flag_of_Egypt_Flat_Square_64x64;
                case Countries.ElSalvador: return CountryFlags_Square64. Flag_of_El_Salvador_Flat_Square_64x64;
                case Countries.EquatorialGuinea: return CountryFlags_Square64. Flag_of_Equatorial_Guinea_Flat_Square_64x64; ;
                case Countries.Eritrea: return CountryFlags_Square64. Flag_of_Eritrea_Flat_Square_64x64;
                case Countries.Estonia: return CountryFlags_Square64. Flag_of_Estonia_Flat_Square_64x64;
                case Countries.Eswatini: return CountryFlags_Square64. Flag_of_Eswatini_Flat_Square_64x64;
                case Countries.Ethiopia: return CountryFlags_Square64. Flag_of_Ethiopia_Flat_Square_64x64;
                case Countries.FaeroeIslands: return CountryFlags_Square64. Flag_of_Faroe_Islands_Flat_Square_64x64;
                case Countries.Finland: return CountryFlags_Square64. Flag_of_Finland_Flat_Square_64x64;
                case Countries.France: return CountryFlags_Square64. Flag_of_France_Flat_Square_64x64;
                case Countries.FrenchGuiana: return CountryFlags_Square64. Flag_of_French_Guiana_Flat_Square_64x64;
                case Countries.Gabon: return CountryFlags_Square64. Flag_of_Gabon_Flat_Square_64x64;
                case Countries.Gambia: return CountryFlags_Square64. Flag_of_Gambia_Flat_Square_64x64;
                case Countries.Georgia: return CountryFlags_Square64. Flag_of_Georgia_Flat_Square_64x64;
                case Countries.Germany: return CountryFlags_Square64. Flag_of_Germany_Flat_Square_64x64;
                case Countries.Ghana: return CountryFlags_Square64. Flag_of_Ghana_Flat_Square_64x64;
                case Countries.Gibraltar: return CountryFlags_Square64. Flag_of_Gibraltar_Flat_Square_64x64;
                case Countries.Greece: return CountryFlags_Square64. Flag_of_Greece_Flat_Square_64x64;
                case Countries.Grenada: return CountryFlags_Square64. Flag_of_Grenada_Flat_Square_64x64;
                case Countries.Guatemala: return CountryFlags_Square64. Flag_of_Guatemala_Flat_Square_64x64;
                case Countries.Guinea: return CountryFlags_Square64. Flag_of_Guinea_Flat_Square_64x64;
                case Countries.GuineaBissau: return CountryFlags_Square64. Flag_of_Guinea_Bissau_Flat_Square_64x64;
                case Countries.Guyana: return CountryFlags_Square64. Flag_of_Guyana_Flat_Square_64x64;
                case Countries.Haiti: return CountryFlags_Square64. Flag_of_Haiti_Flat_Square_64x64;
                case Countries.HolySee: return CountryFlags_Square64. Flag_of_Vatican_City_Flat_Square_64x64;
                case Countries.Honduras: return CountryFlags_Square64. Flag_of_Honduras_Flat_Square_64x64;
                case Countries.HongKong: return CountryFlags_Square64. Flag_of_Hong_Kong_Flat_Square_64x64;
                case Countries.Hungary: return CountryFlags_Square64. Flag_of_Hungary_Flat_Square_64x64;
                case Countries.Iceland: return CountryFlags_Square64. Flag_of_Iceland_Flat_Square_64x64;
                case Countries.India: return CountryFlags_Square64. Flag_of_India_Flat_Square_64x64;
                case Countries.Indonesia: return CountryFlags_Square64. Flag_of_Indonesia_Flat_Square_64x64;
                case Countries.Iran: return CountryFlags_Square64. Flag_of_Iran_Flat_Square_64x64;
                case Countries.Iraq: return CountryFlags_Square64. Flag_of_Iraq_Flat_Square_64x64;
                case Countries.Ireland: return CountryFlags_Square64. Flag_of_Ireland_Flat_Square_64x64;
                case Countries.IsleOfMan: return CountryFlags_Square64. Flag_of_Isle_of_Mann_Flat_Square_64x64;
                case Countries.Israel: return CountryFlags_Square64. Flag_of_Israel_Flat_Square_64x64;
                case Countries.Italy: return CountryFlags_Square64. Flag_of_Italy_Flat_Square_64x64;
                case Countries.Jamaica: return CountryFlags_Square64. Flag_of_Jamaica_Flat_Square_64x64;
                case Countries.Japan: return CountryFlags_Square64. Flag_of_Japan_Flat_Square_64x64;
                case Countries.Jordan: return CountryFlags_Square64. Flag_of_Jordan_Flat_Square_64x64;
                case Countries.Kazakhstan: return CountryFlags_Square64. Flag_of_Kazakhstan_Flat_Square_64x64;
                case Countries.Kenya: return CountryFlags_Square64. Flag_of_Kenya_Flat_Square_64x64;
                case Countries.Kuwait: return CountryFlags_Square64. Flag_of_Kuwait_Flat_Square_64x64;
                case Countries.Kyrgyzstan: return CountryFlags_Square64. Flag_of_Kyrgyzstan_Flat_Square_64x64;
                case Countries.Laos: return CountryFlags_Square64. Flag_of_Laos_Flat_Square_64x64;
                case Countries.Latvia: return CountryFlags_Square64. Flag_of_Latvia_Flat_Square_64x64;
                case Countries.Lebanon: return CountryFlags_Square64. Flag_of_Lebanon_Flat_Square_64x64;
                case Countries.Lesotho: return CountryFlags_Square64. Flag_of_Lesotho_Flat_Square_64x64;
                case Countries.Liberia: return CountryFlags_Square64. Flag_of_Liberia_Flat_Square_64x64;
                case Countries.Libya: return CountryFlags_Square64. Flag_of_Libya_Flat_Square_64x64;
                case Countries.Liechtenstein: return CountryFlags_Square64. Flag_of_Liechtenstein_Flat_Square_64x64;
                case Countries.Lithuania: return CountryFlags_Square64. Flag_of_Lithuania_Flat_Square_64x64;
                case Countries.Luxembourg: return CountryFlags_Square64. Flag_of_Luxembourg_Flat_Square_64x64;
                case Countries.Macao: return CountryFlags_Square64. Flag_of_Macau_Flat_Square_64x64;
                case Countries.Madagascar: return CountryFlags_Square64. Flag_of_Madagascar_Flat_Square_64x64;
                case Countries.Malawi: return CountryFlags_Square64. Flag_of_Malawi_Flat_Square_64x64;
                case Countries.Malaysia: return CountryFlags_Square64. Flag_of_Malaysia_Flat_Square_64x64;
                case Countries.Maldives: return CountryFlags_Square64. Flag_of_Maldives_Flat_Square_64x64;
                case Countries.Mali: return CountryFlags_Square64. Flag_of_Mali_Flat_Square_64x64;
                case Countries.Malta: return CountryFlags_Square64. Flag_of_Malta_Flat_Square_64x64;
                case Countries.Mauritania: return CountryFlags_Square64. Flag_of_Mauritania_Flat_Square_64x64;
                case Countries.Mauritius: return CountryFlags_Square64. Flag_of_Mauritius_Flat_Square_64x64;
                case Countries.Mayotte: return CountryFlags_Square64. Flag_of_Mayotte_Flat_Square_64x64;
                case Countries.Mexico: return CountryFlags_Square64. Flag_of_Mexico_Flat_Square_64x64;
                case Countries.Moldova: return CountryFlags_Square64. Flag_of_Moldova_Flat_Square_64x64;
                case Countries.Monaco: return CountryFlags_Square64. Flag_of_Monaco_Flat_Square_64x64;
                case Countries.Mongolia: return CountryFlags_Square64. Flag_of_Mongolia_Flat_Square_64x64;
                case Countries.Montenegro: return CountryFlags_Square64. Flag_of_Montenegro_Flat_Square_64x64;
                case Countries.Morocco: return CountryFlags_Square64. Flag_of_Morocco_Flat_Square_64x64;
                case Countries.Mozambique: return CountryFlags_Square64. Flag_of_Mozambique_Flat_Square_64x64;
                case Countries.Myanmar: return CountryFlags_Square64. Flag_of_Myanmar_Flat_Square_64x64;
                case Countries.Namibia: return CountryFlags_Square64. Flag_of_Namibia_Flat_Square_64x64;
                case Countries.Nepal: return CountryFlags_Square64. Flag_of_Nepal_Flat_Square_64x64;
                case Countries.Netherlands: return CountryFlags_Square64. Flag_of_Netherlands_Flat_Square_64x64;
                case Countries.Nicaragua: return CountryFlags_Square64. Flag_of_Nicaragua_Flat_Square_64x64;
                case Countries.Niger: return CountryFlags_Square64. Flag_of_Niger_Flat_Square_64x64;
                case Countries.Nigeria: return CountryFlags_Square64. Flag_of_Nigeria_Flat_Square_64x64;
                case Countries.NorthKorea: return CountryFlags_Square64. Flag_of_North_Korea_Flat_Square_64x64;
                case Countries.NorthMacedonia: return CountryFlags_Square64. Flag_of_North_Macedonia_Flat_Square_64x64;
                case Countries.Norway: return CountryFlags_Square64. Flag_of_Norway_Flat_Square_64x64;
                case Countries.Oman: return CountryFlags_Square64. Flag_of_Oman_Flat_Square_64x64;
                case Countries.Pakistan: return CountryFlags_Square64. Flag_of_Pakistan_Flat_Square_64x64;
                case Countries.Panama: return CountryFlags_Square64. Flag_of_Panama_Flat_Square_64x64;
                case Countries.Paraguay: return CountryFlags_Square64. Flag_of_Paraguay_Flat_Square_64x64;
                case Countries.Peru: return CountryFlags_Square64. Flag_of_Peru_Flat_Square_64x64;
                case Countries.Philippines: return CountryFlags_Square64. Flag_of_Philippines_Flat_Square_64x64;
                case Countries.Poland: return CountryFlags_Square64. Flag_of_Poland_Flat_Square_64x64;
                case Countries.Portugal: return CountryFlags_Square64. Flag_of_Portugal_Flat_Square_64x64;
                case Countries.Qatar: return CountryFlags_Square64. Flag_of_Qatar_Flat_Square_64x64;
                case Countries.Réunion: return CountryFlags_Square64. Flag_of_Reunion_Radiant_Volcano_Flat_Square_64x64;
                case Countries.Romania: return CountryFlags_Square64. Flag_of_Romania_Flat_Square_64x64;
                case Countries.Russia: return CountryFlags_Square64. Flag_of_Russia_Flat_Square_64x64;
                case Countries.Rwanda: return CountryFlags_Square64. Flag_of_Rwanda_Flat_Square_64x64;
                case Countries.SaintHelena: return CountryFlags_Square64. Flag_of_Saint_Helena_Flat_Square_64x64;
                case Countries.SaintKittsAndNevis: return CountryFlags_Square64. Flag_of_Saint_Kitts_and_Nevis_Flat_Square_64x64;
                case Countries.SaintLucia: return CountryFlags_Square64. Flag_of_Saint_Lucia_Flat_Square_64x64;
                case Countries.SaintVincentAndTheGrenadines: return CountryFlags_Square64. Flag_of_Saint_Vincent_and_the_Grenadines_Flat_Square_64x64;
                case Countries.SanMarino: return CountryFlags_Square64. Flag_of_San_Marino_Flat_Square_64x64;
                case Countries.SaoTomePrincipe: return CountryFlags_Square64. Flag_of_Sao_Tome_and_Principe_Flat_Square_64x64;
                case Countries.SaudiArabia: return CountryFlags_Square64. Flag_of_Saudi_Arabia_Flat_Square_64x64;
                case Countries.Senegal: return CountryFlags_Square64. Flag_of_Senegal_Flat_Square_64x64;
                case Countries.Serbia: return CountryFlags_Square64. Flag_of_Serbia_Flat_Square_64x64;
                case Countries.Seychelles: return CountryFlags_Square64. Flag_of_Seychelles_Flat_Square_64x64;
                case Countries.SierraLeone: return CountryFlags_Square64. Flag_of_Sierra_Leone_Flat_Square_64x64;
                case Countries.Singapore: return CountryFlags_Square64. Flag_of_Singapore_Flat_Square_64x64;
                case Countries.Slovakia: return CountryFlags_Square64. Flag_of_Slovakia_Flat_Square_64x64;
                case Countries.Slovenia: return CountryFlags_Square64. Flag_of_Slovenia_Flat_Square_64x64; ;
                case Countries.Somalia: return CountryFlags_Square64. Flag_of_Somalia_Flat_Square_64x64;
                case Countries.SouthAfrica: return CountryFlags_Square64. Flag_of_South_Africa_Flat_Square_64x64;
                case Countries.SouthKorea: return CountryFlags_Square64. Flag_of_South_Korea_Flat_Square_64x64;
                case Countries.SouthSudan: return CountryFlags_Square64. Flag_of_South_Sudan_Flat_Square_64x64;
                case Countries.Spain: return CountryFlags_Square64. Flag_of_Spain_Flat_Square_64x64;
                case Countries.SriLanka: return CountryFlags_Square64. Flag_of_Sri_Lanka_Flat_Square_64x64;
                case Countries.Palestine: return CountryFlags_Square64. Flag_of_Palestine_Flat_Square_64x64;
                case Countries.Sudan: return CountryFlags_Square64. Flag_of_Sudan_Flat_Square_64x64;
                case Countries.Suriname: return CountryFlags_Square64. Flag_of_Suriname_Flat_Square_64x64;
                case Countries.Sweden: return CountryFlags_Square64. Flag_of_Sweden_Flat_Square_64x64;
                case Countries.Switzerland: return CountryFlags_Square64. Flag_of_Switzerland_Flat_Square_64x64;
                case Countries.Syria: return CountryFlags_Square64. Flag_of_Syria_Flat_Square_64x64;
                case Countries.Taiwan: return CountryFlags_Square64. Flag_of_Taiwan_Republic_of_China_Flat_Square_64x64;
                case Countries.Tajikistan: return CountryFlags_Square64. Flag_of_Tajikistan_Flat_Square_64x64;
                case Countries.Tanzania: return CountryFlags_Square64. Flag_of_Tanzania_Flat_Square_64x64;
                case Countries.Thailand: return CountryFlags_Square64. Flag_of_Thailand_Flat_Square_64x64;
                case Countries.TheBahamas: return CountryFlags_Square64. Flag_of_Bahamas_Flat_Square_64x64;
                case Countries.TimorLeste: return CountryFlags_Square64. Flag_of_East_Timor_Flat_Square_64x64;
                case Countries.Togo: return CountryFlags_Square64. Flag_of_Togo_Flat_Square_64x64;
                case Countries.TrinidadAndTobago: return CountryFlags_Square64. Flag_of_Trinidad_and_Tobago_Flat_Square_64x64;
                case Countries.Tunisia: return CountryFlags_Square64. Flag_of_Tunisia_Flat_Square_64x64;
                case Countries.Turkey: return CountryFlags_Square64. Flag_of_Turkey_Flat_Square_64x64;
                case Countries.Turkmenistan: return CountryFlags_Square64. Flag_of_Turkmenistan_Flat_Square_64x64;
                case Countries.Uganda: return CountryFlags_Square64. Flag_of_Uganda_Flat_Square_64x64;
                case Countries.Ukraine: return CountryFlags_Square64. Flag_of_Ukraine_Flat_Square_64x64;
                case Countries.UnitedArabEmirates: return CountryFlags_Square64. Flag_of_United_Arab_Emirates_Flat_Square_64x64;
                case Countries.UnitedKingdom: return CountryFlags_Square64. Flag_of_United_Kingdom_Flat_Square_64x64;
                case Countries.UnitedStates: return CountryFlags_Square64. Flag_of_United_States_Flat_Square_64x64;
                case Countries.Uruguay: return CountryFlags_Square64. Flag_of_Uruguay_Flat_Square_64x64;
                case Countries.Uzbekistan: return CountryFlags_Square64. Flag_of_Uzbekistan_Flat_Square_64x64;
                case Countries.Venezuela: return CountryFlags_Square64. Flag_of_Venezuela_Flat_Square_64x64;
                case Countries.Vietnam: return CountryFlags_Square64. Flag_of_Vietnam_Flat_Square_64x64;
                case Countries.WesternSahara: return CountryFlags_Square64. No_Image_64x64;
                case Countries.Yemen: return CountryFlags_Square64. Flag_of_Yemen_Flat_Square_64x64;
                case Countries.Zambia: return CountryFlags_Square64. Flag_of_Zambia_Flat_Square_64x64;
                case Countries.Zimbabwe: return CountryFlags_Square64. Flag_of_Zimbabwe_Flat_Square_64x64;
                default: throw new InvalidEnumArgumentException();
            }
        }
    }
}
