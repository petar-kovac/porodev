package ui;

import com.fasterxml.jackson.databind.ser.Serializers;
import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PoroDevPom.HomePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.openqa.selenium.WebDriver;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.Assert;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import java.io.IOException;

public class HomePageTest extends BaseTest{
    protected final Logger logger = LoggerFactory.getLogger(HomePageTest.class);
    protected FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);
    protected HomePage homePage;
    protected RegistrationPage registrationPage;
    protected LoginPage loginPage;

    public HomePageTest() throws IOException {
    }

    // For the user login
    @BeforeMethod
    public void setUp() throws InterruptedException {
        registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        loginPage = new LoginPage(driver);
        homePage = new HomePage(driver);
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
        BasePage.waitForElementVisibility(registrationPage.we_successfulRegistration_message, driver);

        loginPage.logInUser(currentEmail, file.getValue("VALID_PASS"));
        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully logged in.");
    }

    @Test(priority = 1)
    public void check_profile_info() {
        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.assert_profileInfo_isDisplayed("Profile info is not displayed");
        logger.info("Profile info is displayed");
    }

   @Test(priority = 2)
    public void edit_firstName_profileInfo() {
        homePage.goTo_profilePage();
       logger.info("User is on Home page");


       homePage.changeProfileAttribute(
               homePage.we_editName_button,
                homePage.we_editAttribute_entry,
               "NewName",
               homePage.we_profileFirstName_text);

                String changedName = BasePage.getTextFromElement(homePage.we_profileLastName_text);
                Assert.assertEquals(changedName,"newName");

    }

    @Test(priority = 3)
    public void edit_lastName_profileInfo() {
        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.changeProfileAttribute(
                homePage.we_editLastName_button,
                homePage.we_editAttribute_entry,
                "NewLastName",
                homePage.we_profileLastName_text);

                String changedLastName = BasePage.getTextFromElement(homePage.we_profileLastName_text);
                Assert.assertEquals(changedLastName, "NewLastName");

    }

    @Test(priority = 4)
    public void edit_password_profileInfo() {
        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.changeProfileAttribute(
                homePage.we_editPassword_button,
                homePage.we_editAttribute_entry,
                "NewPass##22",
                homePage.we_profileLastName_text
        );

        homePage.logOutUser();

       /* loginPage.logInUser(currentEmail, file.getValue("VALID_PASS"));
        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully logged in.");*/

    }

    @Test(priority = 5)
    public void upload_file_functionality() {
        //TODO test that user can upload file and assert that is uploaded
    }

    @Test(priority = 6)
    public void show_more_files_functionality() {
        //TODO test where user clicks show more files button from Home page, it lands on Files page
    }

    @Test(priority = 7)
    public void logout_user() {
        //TODO test user logout functionality, assert that is logged out
    }
}
