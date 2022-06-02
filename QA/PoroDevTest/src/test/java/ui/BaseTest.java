package ui;

import common.ui_setup.DriverSetup;
import common.ui_setup.DriverType;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;

public class BaseTest {
    protected DriverSetup driverSetup;
    protected WebDriver driver;
    protected LoginPage loginPage;

    @BeforeClass
    public void setupBeforeTestClass() {
        System.out.println("BEFORE CLASS");
    };

    @BeforeMethod
    public void setupBeforeMethod() {
        System.out.println("BEFORE METHOD");
        driverSetup = new DriverSetup(DriverType.CHROME);
        driver = driverSetup.getDriver();
        loginPage = new LoginPage(driver);
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
