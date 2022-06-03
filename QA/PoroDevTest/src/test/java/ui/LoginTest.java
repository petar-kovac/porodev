package ui;

import common.ui_setup.FileControl;
import org.testng.Assert;
import org.testng.annotations.Test;

import java.io.IOException;

public class LoginTest extends BaseTest {
    public FileControl fileControl = new FileControl(FileControl.LOGIN_DATA_PROPERTIES);
    ;

    public LoginTest() throws IOException {
    }

    @Test(priority = 0)
    public void login_with_validCredentials() throws IOException, InterruptedException {
        System.out.println("first test");

        loginPage.login_and_assert(fileControl.getValue("VALID_EMAIL"), fileControl.getValue("VALID_PASS"));
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.successfulLogin), "Successful login");
    }

    @Test(priority = 1)
    public void login_with_invalidPassword() throws IOException, InterruptedException {
        System.out.println("second test");

        loginPage.login_and_assert(fileControl.getValue("VALID_EMAIL"), fileControl.getValue("INVALID_PASS"));
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.errorAfterBadCredentials), "Request failed with status code 401");
    }

    @Test(priority = 2)
    public void login_with_invalidEmail() throws InterruptedException {
        System.out.println("Third test");

        loginPage.login_and_assert(fileControl.getValue("INVALID_EMAIL"), fileControl.getValue("VALID_PASS"));
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.errorAfterBadCredentials), "Request failed with status code 401");
    }

    @Test(priority = 3)
    public void login_without_email() throws InterruptedException {
        System.out.println("Fourth test");

        loginPage.login_and_assert("", fileControl.getValue("VALID_PASS"));
        System.out.println(elementControl.getTextFromElement(loginPage.emailErrorMsg));
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.emailErrorMsg), "Please input your username");
    }

    @Test(priority = 4)
    public void login_without_password() throws InterruptedException {
        System.out.println("Fifth test");

        loginPage.login_and_assert(fileControl.getValue("VALID_EMAIL"), "");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.passErrorMsg), "Please input your password");
    }

    @Test(priority = 5)
    public void login_without_bothCredentials() throws InterruptedException {
        System.out.println("Sixth test");

        loginPage.login_and_assert("", "");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.emailErrorMsg), "Please input your username");
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.passErrorMsg), "Please input your password");
    }

    @Test(priority = 6)
    public void login_with_invalidEmail_invalidPassword() throws InterruptedException {
        loginPage.login_and_assert(fileControl.getValue("INVALID_EMAIL"), fileControl.getValue("INVALID_PASS"));
        Assert.assertEquals(elementControl.getTextFromElement(loginPage.errorAfterBadCredentials), "Request failed with status code 401");
    }
}
