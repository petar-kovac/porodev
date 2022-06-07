package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import org.testng.Assert;
import org.testng.annotations.Test;

import java.io.IOException;

public class LoginTest extends BaseTest {
    protected FileControlUtil fileControlUtil;

    public LoginTest() throws IOException {
        fileControlUtil = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
    }

    @Test(priority = 0)
    public void login_with_validCredentials() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.login(
                fileControlUtil.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlUtil.getValue("VALID_PASS_CREATED_USER"));
        loginPage.assert_valid_login();
    }

    @Test(priority = 1)
    public void login_with_invalidFormPassword() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.login(
                "",
                fileControlUtil.getValue("INVALID_FORM_PASS"));
        loginPage.assert_login_with_invalid_form_password("Wrong password");
    }

    @Test(priority = 2)
    public void login_with_valid_notExistingUserPassword() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.login(
                fileControlUtil.getValue("VALID_EMAIL_CREATED_USER"),
                fileControlUtil.getValue("VALID_NOT_EXISTING_USER_PASSWORD"));
        loginPage.assert_login_with_valid_notExistingCredentials("Cannot read properties of undefined (reading 'data')");
    }

    @Test(priority = 3)
    public void login_with_invalidFormEmail() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.login(
                fileControlUtil.getValue("INVALID_FORM_EMAIL"),
                "");
        loginPage.assert_login_with_invalid_form_email("Invalid email");
    }

    @Test(priority = 4)
    public void login_with_notExistingUserEmail() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.login(
                fileControlUtil.getValue("VALID_NOT_EXISTING_USER_EMAIL"),
                fileControlUtil.getValue("VALID_NOT_EXISTING_USER_PASSWORD"));
        loginPage.assert_login_with_valid_notExistingCredentials("Cannot read properties of undefined (reading 'data')");
    }

    @Test(priority = 5)
    public void login_without_email() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);

        loginPage.login(
                "",
                fileControlUtil.getValue("VALID_PASS_CREATED_USER"));
        loginPage.assert_email_isRequired("Email is required");
    }

    @Test(priority = 6)
    public void login_without_password() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);
        loginPage.login(fileControlUtil.getValue("VALID_EMAIL_CREATED_USER"), "");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.pass_requiredMsg), "Password is required");
    }

    @Test(priority = 7)
    public void login_without_bothCredentials() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);
        loginPage.login("", "");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.email_requiredMsg), "Email is required");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.pass_requiredMsg), "Password is required");
    }

    @Test(priority = 8)
    public void login_with_invalidEmail_invalidPassword() throws InterruptedException {
        LoginPage loginPage = new LoginPage(driver, SetupConstants.BASE_URL);
        loginPage.login(
                fileControlUtil.getValue("INVALID_FORM_EMAIL"),
                fileControlUtil.getValue("INVALID_FORM_PASS"));
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.email_invalidMsg), "Invalid email");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.pass_invalidMsg), "Wrong password");
    }
}
