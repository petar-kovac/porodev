package ui;

import common.ui_setup.DriverSetup;
import common.ui_setup.DriverType;
import common.ui_setup.ElementControl;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;

import java.io.IOException;

public class BaseTest {
    protected DriverSetup driverSetup;
    protected WebDriver driver;
    protected LoginPage loginPage;
    protected ElementControl elementControl;

    @BeforeClass
    public void setupBeforeTestClass() {
        System.out.println("BEFORE CLASS");
    };

    @BeforeMethod
    public void setupBeforeMethod() throws IOException {
        System.out.println("BEFORE METHOD");
        driverSetup = new DriverSetup(DriverType.CHROME);
        driver = driverSetup.getDriver();
        driverSetup.navigateToUrl(SetupConstants.BASE_URL);
        loginPage = new LoginPage(driver);
        elementControl = new ElementControl(driver);
    };

    @AfterMethod
    public void tearDownAfterMethod() {
        driverSetup.closeBrowserTab();
        System.out.println("AFTER METHOD");
    };

    @AfterClass
    public void tearDownAfterTestClass() {
        System.out.println("AFTER CLASS");
    };
}
