namespace OnlinePaymentSystem.Session
{
    public static class SessionData
    {
        public static int currentUserId { get; set; }
        public static bool isLoggedIn => currentUserId != 0;
    }
}
