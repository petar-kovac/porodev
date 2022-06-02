package ui;

import org.testng.annotations.Test;

public class LoginTest extends BaseTest {

    @Test(priority = 0)
    public void login_with_no_email() {
        System.out.println("This is first test");
        loginPage.enterPassword("Password123!");
        loginPage.clickLoginButton();
    }

    @Test(priority = 1)
    public void login_with_valid_credentials() {
        System.out.println("This is second test");
        loginPage.enterPassword("Password123!");
        loginPage.enterEmail("marko.ilic@boing.rs");
        loginPage.clickLoginButton();
    }
}
