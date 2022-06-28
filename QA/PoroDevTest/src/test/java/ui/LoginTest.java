package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.PomConstants;
import common.ui_setup.pom_setup.PoroDevPom.HomePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.io.IOException;

public class LoginTest extends BaseTest {
    protected FileControlUtil fileControlLogin;
    protected FileControlUtil fileControlRegister;
    protected LoginPage loginPage;
    protected HomePage homePage;
    protected RegistrationPage registrationPage;

    @BeforeMethod
    public void setUp() throws IOException {
        fileControlLogin = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
        fileControlRegister = new FileControlUtil(FileControlUtil.REGISTRATION_DATA_PROPERTIES);
        registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        homePage = new HomePage(driver);
        loginPage = new LoginPage(driver, SetupConstants.BASE_URL);
    }

    @Test
    public void login_with_validCredentials() {

        registrationPage.registerUser(
                fileControlRegister.getValue("VALID_FIRSTNAME"),
                fileControlRegister.getValue("VALID_LASTNAME"),
                fileControlRegister.getValue("VALID_EMAIL"),
                fileControlRegister.getValue("VALID_PASS"),
                fileControlRegister.getValue("VALID_PASS"),
                fileControlRegister.getValue("VALID_DEPARTMENT"),
                fileControlRegister.getValue("VALID_POSITION"));

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("VALID_PASS_CREATED_USER"));


        homePage.logOutUser();

        loginPage.assert_login(homePage.we_userFileUpload_message, PomConstants.HOME_PAGE_USER_FILE_UPLOAD, "Login was not successful");
    }

    @Test(priority = 1)
    public void login_with_invalidFormPassword() {

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("INVALID_FORM_PASS"));

        loginPage.assert_login_errorMessages(PomConstants.WRONG_PASSWORD_ERROR, "Password form should be invalid");
    }

    @Test(priority = 2)
    public void login_with_valid_notMatchingUserPassword() {

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("VALID_NOT_MATCHING_USER_PASSWORD"));

        loginPage.assert_login(loginPage.we_badRequestError, PomConstants.LOGIN_PAGE_BAD_REQUEST, "Error message 'Cannot read properties of undefined (reading 'data')' should pop up.");
    }

    @Test(priority = 3)
    public void login_with_invalidFormEmail() {

        loginPage.logInUser(
                fileControlLogin.getValue("INVALID_FORM_EMAIL"),
                "");

        loginPage.assert_login_errorMessages(PomConstants.EMAIL_ERROR_INVALID, "Email form should be invalid");
    }

    @Test(priority = 4)
    public void login_with_notExistingUserEmail() {

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_NOT_EXISTING_USER_EMAIL"),
                fileControlLogin.getValue("VALID_FORM_PASS"));

        loginPage.assert_login(loginPage.we_badRequestError, PomConstants.LOGIN_PAGE_BAD_REQUEST, "Error message 'Cannot read properties of undefined (reading 'data')' should pop up.");
    }

    @Test(priority = 5)
    public void login_without_email() {

        loginPage.logInUser(
                "",
                fileControlLogin.getValue("VALID_PASS_CREATED_USER"));

        loginPage.assert_login_errorMessages(PomConstants.REQUIRED_FIELD_ERROR, "'This field is required' message should pop up.");
    }

    @Test(priority = 6)
    public void login_without_password() {

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                "");

        loginPage.assert_login_errorMessages(PomConstants.REQUIRED_FIELD_ERROR, "'This field is required' message should pop up.");
    }
}