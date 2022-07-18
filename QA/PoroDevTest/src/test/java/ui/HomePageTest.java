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

    protected FileControlUtil fileUpload = new FileControlUtil(FileControlUtil.FE_UPLOAD_PROPERTIES);
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

/*        registrationPage.registerUser(
                file.getValue("VALID_FIRSTNAME"),
                file.getValue("VALID_LASTNAME"),
                currentEmail,
                file.getValue("VALID_PASS"),
                file.getValue("VALID_PASS"),
                file.getValue("VALID_DEPARTMENT"),
                file.getValue("VALID_POSITION"));
        logger.info("User successfully registered.");
        BasePage.waitForElementVisibility(registrationPage.we_successfulRegistration_message, driver);*/

        loginPage.logInUser(file.getValue("VALID_EMAIL"), file.getValue("VALID_PASS"));
        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully logged in.");
    }

    @Test(priority = 1, description = "User reaching the profile page")
    public void check_profile_info() {
        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.assert_profileInfo_isDisplayed("Profile info is not displayed");
        logger.info("Profile info is displayed");
    }

   @Test(priority = 2, description = "User editing the first name")
    public void edit_firstName_profileInfo() {
        homePage.goTo_profilePage();
       logger.info("User is on Home page");


       homePage.changeProfileAttribute(
               homePage.we_editName_button,
                homePage.we_editAttribute_entry,
               file.getValue("VALID_CHANGED_NAME"),
               homePage.we_profileFirstName_text);

                String changedName = BasePage.getTextFromElement(homePage.we_profileFirstName_text);
                Assert.assertEquals(changedName,file.getValue("VALID_CHANGED_NAME"));
                logger.info("User successfully changed first name");

    }

    @Test(priority = 3, description = "User editing the last name")
    public void edit_lastName_profileInfo() {
        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.changeProfileAttribute(
                homePage.we_editLastName_button,
                homePage.we_editAttribute_entry,
                file.getValue("VALID_CHANGED_LAST_NAME"),
                homePage.we_profileLastName_text);

                String changedLastName = BasePage.getTextFromElement(homePage.we_profileLastName_text);
                Assert.assertEquals(changedLastName, file.getValue("VALID_CHANGED_LAST_NAME"));
                logger.info("User successfully changed last name");

    }

    @Test(priority = 4, description = "User editing the password")
    public void edit_password_profileInfo() {
        homePage.goTo_profilePage();
        logger.info("User is on Home page");

        homePage.changeProfileAttribute(
                homePage.we_editPassword_button,
                homePage.we_editAttribute_entry,
                file.getValue("VALID_CHANGED_PASS"),
                homePage.we_profileLastName_text
        );

        String usersCurrentEmail = BasePage.getTextFromElement(homePage.we_profileEmail_text);

        homePage.logOutUser();

        loginPage.logInUser(usersCurrentEmail, file.getValue("VALID_CHANGED_PASS"));
        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully changed the password.");

    }

    @Test(priority = 5, description = "User uploading the file")
    public void upload_file_functionality() {
        homePage.uploadFile(fileUpload.getValue("VALID_FILE_PATH"),
                fileUpload.getValue("VALID_FILE_NAME"));
        logger.info("User successfully uploaded the file");
    }

    @Test(priority = 6, description = "User leading to files page from home page")
    public void show_more_files_functionality() {
        homePage.leadToFilesPage();
        logger.info("User successfully landed on files page");
    }

    @Test(priority = 7, description = "User logging out")
    public void logout_user() {
        loginPage = new LoginPage(driver);
        homePage.logOutUser();
        loginPage.assert_thatElement_isDisplayed(loginPage.we_login_title,
                "The user is not logged out");
        logger.info("User successfully logged out");
    }
}
