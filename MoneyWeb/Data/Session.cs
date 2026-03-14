namespace MoneyWeb.Data
{
    public static class Session
    {
        private const string USER_ID_KEY = "UsuarioLogadoId";
        private const string USER_NAME_KEY = "UsuarioLogadoNome";

        public static void SetUsuarioLogado(this ISession session, int id, string nome)
        {
            session.SetInt32(USER_ID_KEY, id);
            session.SetString(USER_NAME_KEY, nome);
        }

        public static int? GetUsuarioLogadoId(this ISession session)
        {
            return session.GetInt32(USER_ID_KEY);
        }

        public static string? GetUsuarioLogadoNome(this ISession session)
        {
            return session.GetString(USER_NAME_KEY);
        }

        public static void ClearUsuarioLogado(this ISession session)
        {
            session.Remove(USER_ID_KEY);
            session.Remove(USER_NAME_KEY);
        }

        public static bool IsUsuarioLogado(this ISession session)
        {
            return session.GetUsuarioLogadoId().HasValue;
        }
    }
}