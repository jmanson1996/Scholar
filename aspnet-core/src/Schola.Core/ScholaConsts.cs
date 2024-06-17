using Schola.Debugging;

namespace Schola
{
    public class ScholaConsts
    {
        public const string LocalizationSourceName = "Schola";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "4d396571a5a940449ea5b04e5ba61b81";
    }
}
