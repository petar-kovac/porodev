using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadUser;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.RegisterUser;
using System.Security.Cryptography;
using static PoroDev.UserManagementService.Constants.Consts;

namespace PoroDev.UserManagementService.Helpers
{
    public static class UserManagementValidator
    {
        private static void CheckPassword(string password)
        {
            if (password.Length < MIN_PASSWORD_LENGTH)
                throw new PasswordFormatException(PASSWORD_MIN_LENGTH_ERROR);
            if (!password.Any(char.IsUpper))
                throw new PasswordFormatException(PASSWORD_MIN_UPPERCASE_ERROR);

            if (!password.Any(char.IsLower))
                throw new PasswordFormatException(PASSWORD_MIN_LOWERCASE_ERROR);

            if (!password.Any(char.IsDigit))
                throw new PasswordFormatException(PASSWORD_MIN_NUMBER_ERROR);

            if (!CheckStringForCharacters(password, SPECIAL_CHARACTERS_STRING))
                throw new PasswordFormatException(PASSWORD_MIN_SPECIAL_ERROR);
        }

        private static async Task CheckEmail(string email, IRequestClient<UserReadByEmailRequestServiceToDatabase> _readUserByEmailClient)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EmailFormatException(EMAIL_EMPTY_ERROR);

            if (email.Length > MAX_EMAIL_LENGTH)
                throw new EmailFormatException(EMAIL_LENGTH_ERROR);

            var splitEmail = email.Split('@');

            if (splitEmail.Length != 2 || string.IsNullOrWhiteSpace(splitEmail[0]))
                throw new EmailFormatException(EMAIL_FORMAT_ERROR);

            if (splitEmail[0].Any(x => char.IsWhiteSpace(x)))
                throw new EmailFormatException(EMAIL_WHITESPACE_ERROR);

            if (CheckStringForCharacters(splitEmail[0], SPECIAL_CHARACTERS_EMAIL_STRING))
                throw new EmailFormatException(EMAIL_SPECIAL_CHARACTERS_ERROR);

            if (!splitEmail[1].Equals(EMAIL_DOMAIN))
                throw new EmailFormatException(EMAIL_DOMAIN_ERROR);

            if ((await _readUserByEmailClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByEmailRequestServiceToDatabase() { Email = email })).Message.Entity != null)
                throw new EmailFormatException(EMAIL_EXISTS_ERROR);
        }

        private static bool CheckStringForCharacters(string stringToCheck, string specialCharacters)
        {
            char[] specialChArray = specialCharacters.ToCharArray();
            bool flag = false;

            foreach (char ch in specialChArray)
            {
                if (stringToCheck.Contains(ch))
                    flag = true;
            }
            return flag;
        }

        private static void GetHashAndSalt(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static void CheckFullName(string name, string lastname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname) || name.Equals(string.Empty) || lastname.Equals(string.Empty))
                throw new FullNameFormatException(FULLNAME_EMPTY_ERROR);

            if (name.Length > MAX_NAME_AND_LASTNAME_LENGTH || lastname.Length > MAX_NAME_AND_LASTNAME_LENGTH)
                throw new FullNameFormatException(FULLNAME_TOO_LONG_ERROR);

            if (name.Any(x => char.IsWhiteSpace(x)) || lastname.Any(x => char.IsWhiteSpace(x)))
                throw new FullNameFormatException(FULLNAME_WHITESPACE_ERROR);

            if (name.Any(x => char.IsNumber(x)) || lastname.Any(x => char.IsNumber(x)))
                throw new FullNameFormatException(FULLNAME_NUMBER_ERROR);

            if (CheckStringForCharacters(name, SPECIAL_CHARACTERS_STRING) || CheckStringForCharacters(lastname, SPECIAL_CHARACTERS_STRING))
                throw new FullNameFormatException(FULLNAME_SPECIAL_CHARACTER_ERORR);
        }

        private static void CheckPosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position) || position.Equals(string.Empty))
                throw new PositionFormatException(POSITION_EMPTY_ERROR);

            if (position.Length > MAX_POSITION_LENGTH)
                throw new PositionFormatException(POSITION_TOO_LONG_ERROR);

            if (position.Any(x => char.IsNumber(x)))
                throw new PositionFormatException(POSITION_NUMBER_ERROR);

            if (CheckStringForCharacters(position, SPECIAL_CHARACTERS_STRING))
                throw new PositionFormatException(POSITION_SPECIAL_CHARACTER_ERROR);
        }

        public static async Task<CommunicationModel<RegisterUserResponse>> CheckUserFields(RegisterUserRequestGatewayToService registerModel, IRequestClient<UserReadByEmailRequestServiceToDatabase> requestClient)
        {
            try
            {
                CheckFullName(registerModel.Name, registerModel.Lastname);
                await CheckEmail(registerModel.Email, requestClient);
                CheckPassword(registerModel.Password);
                CheckPosition(registerModel.Position);
            }
            catch (EmailFormatException ex)
            {

                string exceptionType = nameof(EmailFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<RegisterUserResponse>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }
            catch (PasswordFormatException ex)
            {
                string exceptionType = nameof(PasswordFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<RegisterUserResponse>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }
            catch (FullNameFormatException ex)
            {
                string exceptionType = nameof(FullNameFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<RegisterUserResponse>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }
            catch (PositionFormatException ex)
            {
                string exceptionType = nameof(PositionFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<RegisterUserResponse>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }

            return new CommunicationModel<RegisterUserResponse>() { Entity = null, ExceptionName = null, HumanReadableMessage = null };
        }
    }
}
