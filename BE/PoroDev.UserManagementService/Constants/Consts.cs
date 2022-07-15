namespace PoroDev.UserManagementService.Constants
{
    public static class Consts
    {
        public const string EMAIL_DOMAIN = "boing.rs";

        public const int MIN_PASSWORD_LENGTH = 8;
        public const int PASSWORD_MIN_UPPERCASE_LETTERS = 1;
        public const int PASSSWORD_MIN_LOWERCASE_LETTERS = 1;
        public const int PASSWORD_MIN_NUMBERS = 1;
        public const int PASSWORD_MIN_SPECIAL_CHARACTERS = 1;

        public const string SPECIAL_CHARACTERS_EMAIL_STRING = @"%!@#$%^&*()?/><,:;'\|}]{[~`+=" + "\"";
        public const string SPECIAL_CHARACTERS_STRING = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";

        public const int MAX_EMAIL_LENGTH = 50;
        public const int MAX_NAME_AND_LASTNAME_LENGTH = 20;
        public const int MAX_POSITION_LENGTH = 50;

        public const string SECRET_KEY = "this is a custom Secret Key for authentication";

        public const string LOGIN_FAIL_ERROR = "Email or password is not valid.";

        public const string PASSWORD_MIN_LENGTH_ERROR = "Password must contain at least 8 characters!";
        public const string PASSWORD_MIN_UPPERCASE_ERROR = "Password must contain at least 1 uppercase letter!";
        public const string PASSWORD_MIN_LOWERCASE_ERROR = "Password must contain at least 1 uppercase letter!";
        public const string PASSWORD_MIN_NUMBER_ERROR = "Password must contain at least 1 number!";
        public const string PASSWORD_MIN_SPECIAL_ERROR = "Password must contain at least 1 special character!";

        public const string EMAIL_EMPTY_ERROR = "Email cannot be empty!";
        public const string EMAIL_LENGTH_ERROR = "Email cannot exceed 50 characters!";
        public const string EMAIL_FORMAT_ERROR = "Email format is invalid!";
        public const string EMAIL_WHITESPACE_ERROR = "Email cannot contain whitespace!";
        public const string EMAIL_DOMAIN_ERROR = "Email domain is invalid!";
        public const string EMAIL_EXISTS_ERROR = "User with that email already exists!";
        public const string EMAIL_SPECIAL_CHARACTERS_ERROR = "Email can only contain '.', '-' and '_' characters!";

        public const string FULLNAME_EMPTY_ERROR = "Name or lastname cannot be empty!";
        public const string FULLNAME_TOO_LONG_ERROR = "Name or lastname cannot exceed 20 characters!";
        public const string FULLNAME_WHITESPACE_ERROR = "Name or lastname cannot contain whitespace!";
        public const string FULLNAME_NUMBER_ERROR = "Name or lastname cannot contain numbers!";
        public const string FULLNAME_SPECIAL_CHARACTER_ERORR = "Name or lastname cannot contain special characters!";

        public const string POSITION_EMPTY_ERROR = "Postion cannot be empty!";
        public const string POSITION_TOO_LONG_ERROR = "Postion cannot exceed 50 characters!";
        public const string POSITION_NUMBER_ERROR = "Position cannot contain numbers!";
        public const string POSITION_SPECIAL_CHARACTER_ERROR = "Position cannot contain special characters!";
        public const string FailedToRegisterUserExceptionMessage = "Failed to send verification email, please try to register again.";
    }
}