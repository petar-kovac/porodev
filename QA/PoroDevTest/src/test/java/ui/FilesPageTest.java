package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PoroDevPom.FilesPage;
import common.ui_setup.pom_setup.PoroDevPom.HomePage;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.junit.Assert;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.io.IOException;

public class FilesPageTest extends BaseTest{


    protected final Logger logger = LoggerFactory.getLogger(FilesPageTest.class);
    protected FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);

    protected FileControlUtil fileUpload = new FileControlUtil(FileControlUtil.FE_UPLOAD_PROPERTIES);
    protected HomePage homePage;
    protected RegistrationPage registrationPage;
    protected LoginPage loginPage;
    protected FilesPage filesPage;

    public FilesPageTest() throws IOException {
    }

    @BeforeMethod
    public void setUp() throws InterruptedException {
        registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);
        loginPage = new LoginPage(driver);
        homePage = new HomePage(driver);
        filesPage = new FilesPage(driver);
        loginPage.logInUser(file.getValue("VALID_EMAIL"), file.getValue("VALID_PASS"));
        BasePage.waitForElementVisibility(homePage.we_userFileUpload_message, driver);
        logger.info("User successfully logged in.");
        homePage.leadToFilesPage();
    }

    @Test(description = "User click on file and check if file name in sider is matching")
    public void checkFileInSider() {
        BasePage.waitForElementVisibility(filesPage.we_fileName_text, driver);
        String fileName = BasePage.getTextFromElement(filesPage.we_fileName_text);
        filesPage.clickOnFirstFileInList();
        String fileNameOnSider = BasePage.getTextFromElement(filesPage.we_filenameSider_text);
        Assert.assertEquals("File name: " + fileName, fileNameOnSider);
    }








}


