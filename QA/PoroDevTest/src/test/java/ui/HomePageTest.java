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

public class HomePageTest extends BaseTest{
    protected final Logger logger = LoggerFactory.getLogger(HomePageTest.class);

    // For the user login
    @BeforeMethod
    public void setUp() throws IOException {
        FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        HomePage homePage = new HomePage(driver);
        LoginPage loginPage = new LoginPage(driver);
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
        BasePage.explicitWait(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully logged in.");
    }

    @Test(priority = 1)
    public void check_profile_info() {
        HomePage homePage = new HomePage(driver);

        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.assert_profileInfo_isDisplayed();
        logger.info("Profile info is displayed");
    }

    @Test(priority = 2)
    public void edit_firstName_profileInfo() {

    }

    @Test(priority = 3)
    public void edit_lastName_profileInfo() {

    }

    @Test(priority = 4)
    public void edit_password_profileInfo() {

    }

    @Test(priority = 5)
    public void upload_file_functionality() {

    }

    @Test(priority = 6)
    public void show_more_files_functionality() {

    }

    @Test(priority = 7)
    public void logout_user() {

    }
}
