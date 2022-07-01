package ui;

import common.ui_setup.DataProviderUtil;
import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PomConstants;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.io.IOException;

public class RegisterTest extends BaseTest{
    protected FileControlUtil fileControlUtil = new FileControlUtil(FileControlUtil.REGISTRATION_DATA_PROPERTIES);
    private final Logger logger = LoggerFactory.getLogger(RegisterTest.class);
    protected RegistrationPage registrationPage;

    public RegisterTest() throws IOException {
    }

    @BeforeMethod
    public void setup() {
        registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
    }

    @Test(priority = 1)
    public void register_with_valid_credentials() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));
        logger.info("User successfully registered");
        BasePage.waitForElementVisibility(registrationPage.we_successfulRegistration_message, driver);

        registrationPage.assert_user_registration(
                registrationPage.we_successfulRegistration_message,
                PomConstants.SUCCESSFUL_REGISTRATION,
                "User registration was not successful");
    }

    @Test(priority = 2, dataProvider = "validEmailForms", dataProviderClass = DataProviderUtil.class)
    public void register_with_valid_emailForms(String validEmailForms) {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                validEmailForms,
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));
        BasePage.waitForElementVisibility(registrationPage.we_successfulRegistration_message, driver);

        registrationPage.assert_user_registration(
                registrationPage.we_successfulRegistration_message,
                PomConstants.SUCCESSFUL_REGISTRATION,
                "User registration was not successful.");
    }

    @Test(priority = 3)
    public void register_without_firstName() {
        registrationPage.registerUser(
                "",
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 4)
    public void register_without_lastName() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                "",
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 5)
    public void register_without_email() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                "",
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 6)
    public void register_without_password() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                "",
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 7)
    public void register_without_confirmationPassword() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                "",
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 8)
    public void register_without_department() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                "",
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 9)
    public void register_without_position() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                "");

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR,
                "'This field is required' message should pop up.");
    }

    @Test(priority = 10)
    public void register_with_invalidForm_firstName() {
        registrationPage.registerUser(
                fileControlUtil.getValue("INVALID_FIRSTNAME_NUMBERS"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.FULL_NAME_ERROR_ONLY_LETTERS,
                "'Only letters allowed' error message should pop up.");
    }

    @Test(priority = 11)
    public void register_with_invalidForm_lastName() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("INVALID_LASTNAME_NUMBERS"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.FULL_NAME_ERROR_ONLY_LETTERS,
                "'Only letters allowed' error message should pop up.");
    }

    @Test(priority = 12)
    public void register_with_invalidForm_firstName_moreThan20Char() {
        registrationPage.registerUser(
                fileControlUtil.getValue("INVALID_FIRSTNAME_MORE_THAN_20"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.FULL_NAME_ERROR_MORE_THAN_20,
                "'Must be < 20 characters' error message should pop up.");
    }

    @Test(priority = 13)
    public void register_with_invalidForm_lastName_moreThan20Char() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("INVALID_LASTNAME_MORE_THAN_20"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.FULL_NAME_ERROR_MORE_THAN_20,
                "'Must be < 20 characters' error message should pop up.");
    }



    @Test(priority = 14, dataProvider = "invalidFormEmail", dataProviderClass = DataProviderUtil.class)
    public void register_with_invalidForm_email(String invalidEmailForm) {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                invalidEmailForm,
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.EMAIL_ERROR_INVALID,
                "'Email is invalid' message should pop up.");
    }

    @Test(priority = 15)
    public void register_with_invalidForm_email_moreThan50Char() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("INVALID_EMAIL_MORE_THAN_50"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.EMAIL_ERROR_MORE_THAN_50,
                "'Must be < 50 characters' error message should pop up.");
    }

    @Test(priority = 16)
    public void register_with_already_existing_email() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_ALREADY_REGISTERED_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));
        BasePage.waitForElementVisibility(registrationPage.we_requestFailed_statusCode400, driver);

        registrationPage.assert_user_registration(
                registrationPage.we_requestFailed_statusCode400,
                PomConstants.REQUEST_FAIL_STATUS_CODE_400,
                "'Request failed with status code 400' error message should pop up.");
    }

    @Test(priority = 17, dataProvider = "invalidPasswordForm", dataProviderClass = DataProviderUtil.class)
    public void register_with_invalidFormPass(String invalidPasswordForms) {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                invalidPasswordForms,
                invalidPasswordForms,
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.WRONG_PASSWORD_ERROR,
                "'Wrong password' error message should pop up.");
    }

    @Test(priority = 18, dataProvider = "invalidPassForm_with_whitespace", dataProviderClass = DataProviderUtil.class)
    public void register_with_invalidFormPass_whitespace_betweenChar(String pass_with_whiteSpace) {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                pass_with_whiteSpace,
                pass_with_whiteSpace,
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.PASSWORD_SPACE_ERROR,
                "'Whitespace not allowed' error message should pop up.");
    }

    @Test(priority = 19)
    public void register_withNotMatching_confirmPassword() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("INVALID_PASS_NO_NUMBER"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.PASSWORD_MATCH,
                "'Password should match' error message should pop up.");
    }

    @Test(priority = 20)
    public void register_with_wrongFormat_department() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("INVALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.DEPARTMENT_ERROR,
                "'Must be a number' error message should pop up.");
    }

    @Test(priority = 21)
    public void register_with_wrongFormat_position() {
        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("INVALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.POSITION_ERROR,
                "'Letters & whitespace only' error message should pop up.");
    }
}