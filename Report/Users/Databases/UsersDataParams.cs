namespace Report.Users.Databases
{
    public struct UsersDataParams
    {
        public const string SIGN_UP_TX_NAME = "@TX_NAME";
        public const string SIGN_UP_TX_PASSWORD = "@TX_PASSWORD";
        public const string SIGN_UP_ID_LANGUAGE = "@ID_LANGUAGE";
        public const string SIGN_UP_TX_EMAIL = "@TX_EMAIL";
        public const string SIGN_UP_DT_CREATED = "@DT_CREATED";
        public const string SIGN_UP_ID_CREATED_BY = "@ID_CREATED_BY";
        public const string SIGN_UP_ID_STATUS = "@ID_STATUS";
        public const string SIGN_UP_TX_TOKEN = "@TX_TOKEN";


        public const string SIGN_IN_TX_EMAIL = "@TX_EMAIL";
        public const string SIGN_IN_TX_PASSWORD = "@TX_PASSWORD";

        public const string FORGOT_PASSWORD_TX_EMAIL = "@TX_EMAIL";

        public const string CHANGE_PASSWORD_ID_USER = "@ID_USER";
        public const string CHANGE_PASSWORD_TX_OLD_PASSWORD = "@TX_OLD_PASSWORD";
        public const string CHANGE_PASSWORD_TX_NEW_PASSWORD = "@TX_NEW_PASSWORD";

        public const string UPDATE_AVATAR_TX_AVATAR = "@TX_EMAIL";

        public const string UPDATE_ID_USER = "@ID_USER";
        public const string UPDATE_TX_NAME = "@TX_NAME";

        public const string ACTIVATE_TX_CODE = "@TX_CODE";

    }
}
