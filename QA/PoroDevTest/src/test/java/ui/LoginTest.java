package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.PomConstants;
import common.ui_setup.pom_setup.PoroDevPom.HomePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.testng.annotations.Test;

import java.io.IOException;

public class LoginTest extends BaseTest {
    protected FileControlUtil fileControlLogin;
    protected FileControlUtil fileControlRegister;

    public LoginTest() throws IOException {
        fileControlLogin = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
    }

    @Test(priority = 0)
    public void login_with_validCredentials() throws InterruptedException, IOException {
        fileControlRegister = new FileControlUtil(FileControlUtil.REGISTRATION_DATA_PROPERTIES);

        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        registrationPage.registerUser(
                fileControlRegister.getValue("VALID_FIRSTNAME"),
                fileControlRegister.getValue("VALID_LASTNAME"),
                fileControlRegister.getValue("VALID_EMAIL"),
                fileControlRegister.getValue("VALID_PASS"),
                fileControlRegister.getValue("VALID_PASS"),
                fileControlRegister.getValue("VALID_DEPARTMENT"),
                fileControlRegister.getValue("VALID_POSITION"));


        LoginPage loginPage = new LoginPage(driver);
        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("VALID_PASS_CREATED_USER"));

        HomePage homePage = new HomePage(driver);
        homePage.logOutUser();

        loginPage.assert_login(homePage.we_userFileUpload_message, PomConstants.HOME_PAGE_USER_FILE_UPLOAD);
    }

    @Test(priority = 1)
    public void login_with_invalidFormPassword() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("INVALID_FORM_PASS"));

        loginPage.assert_login_errorMessages("Wrong password");
    }

    @Test(priority = 2)
    public void login_with_valid_notMatchingUserPassword() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlLogin.getValue("VALID_NOT_MATCHING_USER_PASSWORD"));

        loginPage.assert_login(loginPage.we_badRequestError, PomConstants.LOGIN_PAGE_BAD_REQUEST);
    }

    @Test(priority = 3)
    public void login_with_invalidFormEmail() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.logInUser(
                fileControlLogin.getValue("INVALID_FORM_EMAIL"),
                "");

        loginPage.assert_login_errorMessages("Email is invalid");
    }

    @Test(priority = 4)
    public void login_with_notExistingUserEmail() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_NOT_EXISTING_USER_EMAIL"),
                fileControlLogin.getValue("VALID_FORM_PASS"));

        loginPage.assert_login(loginPage.we_badRequestError, PomConstants.LOGIN_PAGE_BAD_REQUEST);
    }

    @Test(priority = 5)
    public void login_without_email() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.logInUser(
                "",
                fileControlLogin.getValue("VALID_PASS_CREATED_USER"));

        loginPage.assert_login_errorMessages("This field is required");
    }

    @Test(priority = 6)
    public void login_without_password() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.logInUser(
                fileControlLogin.getValue("VALID_EMAIL_CREATED_USER"),
                "");

        loginPage.assert_login_errorMessages("This field is required");
    }
}