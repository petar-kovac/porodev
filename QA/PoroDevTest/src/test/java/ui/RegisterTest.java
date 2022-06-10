package ui;

import common.ui_setup.DataProviderUtil;
import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PomConstants;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.testng.annotations.Test;

import java.io.IOException;

public class RegisterTest extends BaseTest{
    protected FileControlUtil fileControlUtil;

    public RegisterTest() throws IOException {
        fileControlUtil = new FileControlUtil(FileControlUtil.REGISTRATION_DATA_PROPERTIES);
    }

    @Test(priority = 1)
    public void register_with_valid_credentials() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.explicitWait(registrationPage.we_successful_registrationMsg);
        registrationPage.assert_user_registration(PomConstants.SUCCESSFUL_REGISTRATION);
    }

    @Test(priority = 2)
    public void register_without_firstName() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                "",
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 3)
    public void register_without_lastName() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                "",
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 4)
    public void register_without_email() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                "",
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 5)
    public void register_without_password() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                "",
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 6)
    public void register_without_confirmationPassword() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                "",
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 7)
    public void register_without_department() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                "",
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 8)
    public void register_without_position() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                "");

        registrationPage.assert_errorMessage(PomConstants.REQUIRED_FIELD_ERROR);
    }

    @Test(priority = 9)
    public void register_with_invalidForm_firstName_lastName() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("INVALID_FIRSTNAME"),
                fileControlUtil.getValue("INVALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.FULL_NAME_ERROR);
    }

    @Test(priority = 10, dataProvider = "emailForm", dataProviderClass = DataProviderUtil.class)
    public void register_with_invalidForm_email(String emailFromDataProvider) throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                emailFromDataProvider,
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.EMAIL_ERROR);
    }

    @Test(priority = 11, dataProvider = "passwordForm", dataProviderClass = DataProviderUtil.class)
    public void register_with_invalidFormPass(String passwordsFromDataProvider) throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                passwordsFromDataProvider,
                passwordsFromDataProvider,
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.WRONG_PASSWORD_ERROR);
    }

    @Test(priority = 12, dataProvider = "passwords_with_whitespace", dataProviderClass = DataProviderUtil.class)
    public void register_with_invalidFormPass_whitespace_betweenChar(String passwordsFromDataProvider) throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                passwordsFromDataProvider,
                passwordsFromDataProvider,
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.PASSWORD_SPACE_ERROR);
    }

    @Test(priority = 13)
    public void register_withNotMatching_confirmPassword()throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("INVALID_PASS_NO_NUMBER"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.PASSWORD_MATCH);
    }

    @Test(priority = 14)
    public void register_with_wrongFormat_department() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                BasePage.getRandomBoingEmail(),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("INVALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_errorMessage(PomConstants.DEPARTMENT_ERROR);
    }
}