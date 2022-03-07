namespace Helpers
{
    public static class AppConfiguration
    {
        private static readonly int sessionDurationInMinutes = 90;

        public static string ConnectionString { get; set; }

        public static string WebRootPath { get; set; }

        public static DateTime GetTokenExpirationDate => DateTime.UtcNow.AddMinutes(sessionDurationInMinutes);

        public static string MailServicesKey { get; set; }
    }
}