package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PomConstants;
import common.ui_setup.pom_setup.PoroDevPom.HomePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.io.IOException;

public class LoginTest extends BaseTest {
    protected final Logger logger = LoggerFactory.getLogger(LoginTest.class);
    protected FileControlUtil fileControlLogin = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
    protected FileControlUtil fileControlRegister = new FileControlUtil(FileControlUtil.REGISTRATION_DATA_PROPERTIES);
    protected RegistrationPage registrationPage;
    protected HomePage homePage;
    protected LoginPage loginPage;

    public LoginTest() throws IOException {
    }

    @BeforeMethod
    public void setup() {
        registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        homePage = new HomePage(driver);
        loginPage = new LoginPage(driver);
    }

    @Test
    public void login_with_validCredentials() {
        String email = BasePage.getRandomBoingEmail();
        registrationPage.registerUser(
                fileControlRegister.getValue("VALID_FIRSTNAME"),
                fileControlRegister.getValue("VALID_LASTNAME"),
                email,
                fileControlRegister.getValue("VALID_PASS"),
                fileControlRegister.getValue("VALID_PASS"),
                fileControlRegister.getValue("VALID_DEPARTMENT"),
                fileControlRegister.getValue("VALID_POSITION"));
        logger.info("User successfully registered.");

        loginPage.logInUser(
                email,
                fileControlLogin.getValue("VALID_PASS_CREATED_USER"));
        logger.info("User successfully logged in.");

        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);

        loginPage.assert_login(homePage.we_userFileUpload_message, PomConstants.HOME_PAGE_USER_FILE_UPLOAD, "Login was not successful");
    }

    @Test(priority = 1)
    public void login_with_invalidFormPassword() {
        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("INVALID_FORM_PASS"));

        loginPage.assert_login_errorMessages(PomConstants.WRONG_PASSWORD_ERROR, "Password form should be invalid");
        logger.info("Password form is invalid");
    }

    @Test(priority = 2)
    public void login_with_valid_notMatchingUserPassword() {
        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("VALID_NOT_MATCHING_USER_PASSWORD"));
        BasePage.waitForElementVisibility(loginPage.we_badRequest_statusCode400,driver);

        loginPage.assert_login(loginPage.we_badRequest_statusCode400, PomConstants.REQUEST_FAIL_STATUS_CODE_400, "Error message 'Request failed with status code 400' should pop up.");
        logger.info("Request failed with status code 400.");
    }

    @Test(priority = 3)
    public void login_with_invalidFormEmail() {
        loginPage.logInUser(
                fileControlLogin.getValue("INVALID_FORM_EMAIL"),
                "");

        loginPage.assert_login_errorMessages(PomConstants.EMAIL_ERROR_INVALID, "Email form should be invalid");
        logger.info("Email is invalid");
    }

    @Test(priority = 4)
    public void login_with_notExistingUserEmail() {
        loginPage.logInUser(
                fileControlLogin.getValue("VALID_NOT_EXISTING_USER_EMAIL"),
                fileControlLogin.getValue("VALID_FORM_PASS"));
        BasePage.waitForElementVisibility(loginPage.we_badRequest_statusCode400,driver);

        loginPage.assert_login(loginPage.we_badRequest_statusCode400, PomConstants.REQUEST_FAIL_STATUS_CODE_400, "Error message 'Request failed with status code 400' should pop up.");
        logger.info("Request failed with status code 400.");
    }

    @Test(priority = 5)
    public void login_without_email() {
        loginPage.logInUser(
                "",
                fileControlLogin.getValue("VALID_PASS_CREATED_USER"));

        loginPage.assert_login_errorMessages(PomConstants.REQUIRED_FIELD_ERROR, "'This field is required' message should pop up.");
        logger.info("Email field is required");
    }

    @Test(priority = 6)
    public void login_without_password() {
        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                "");

        loginPage.assert_login_errorMessages(PomConstants.REQUIRED_FIELD_ERROR, "'This field is required' message should pop up.");
        logger.info("Password field is required.");
    }
}