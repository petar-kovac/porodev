package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PoroDevPom.HomePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import java.io.IOException;

public class FunctionalTest extends BaseTest{
    protected final Logger logger = LoggerFactory.getLogger(FunctionalTest.class);
    protected FileControlUtil file;
    protected RegistrationPage registrationPage;
    protected HomePage homePage;
    protected LoginPage loginPage;

    @BeforeMethod
    public void setup() throws IOException {
        file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);
        registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        homePage = new HomePage(driver);
        loginPage = new LoginPage(driver);
    }

    @Test(priority = 0, description = "First basic user flow test")
    public void user_register_login_logout_flow() {
        String currentEmail = BasePage.getRandomBoingEmail();

        registrationPage.registerUser(
                file.getValue("VALID_FIRSTNAME"),
                file.getValue("VALID_LASTNAME"),
                currentEmail,
                file.getValue("VALID_PASS"),
                file.getValue("VALID_PASS"),
                file.getValue("VALID_DEPARTMENT"),
                file.getValue("VALID_POSITION"));
        logger.info("User successfully registered.");

        loginPage.logInUser(currentEmail, file.getValue("VALID_PASS"));
        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully logged in.");

        homePage.logOutUser();
        logger.info("User successfully logged out.");

        loginPage.assert_thatElement_isDisplayed(loginPage.we_login_form, "Login form is not displayed");
        logger.info("Login form is displayed");
    }
}
