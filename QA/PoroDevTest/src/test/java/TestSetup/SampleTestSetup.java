package TestSetup;

import common.ui_setup.DriverSetup;
import common.ui_setup.DriverType;
import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;

public class SampleTestSetup extends BaseTest{
    protected DriverSetup driverSetup;
    protected WebDriver driver;

    @BeforeClass
    @Override
    public void setupBeforeTestClass() {
        System.out.println("This will trigger first");
        driverSetup = new DriverSetup(DriverType.CHROME);
    }

    @BeforeMethod
    @Override
    public void setupBeforeMethod() {
        System.out.println("This will trigger second after test class method");
        driverSetup.navigateToUrl("https://www.google.rs/webhp?source=search_app&gws_rd=cr,ssl&ei=zzA0VO2GE4n_ygP1l4GAAw");
        driver = driverSetup.getDriver();
    }

    @AfterMethod
    @Override
    public void tearDownAfterMethod() {
        driverSetup.closeBrowserTab();
    }

    @AfterClass
    @Override
    public void tearDownAfterTestClass() {
        driverSetup.quitBrowser();
    }
}
