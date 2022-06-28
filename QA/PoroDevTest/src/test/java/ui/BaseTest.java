package ui;

import common.ui_setup.DriverSetup;
import common.ui_setup.DriverType;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;

import java.io.IOException;

public class BaseTest {
    protected DriverSetup driverSetup;
    protected WebDriver driver;

    @BeforeClass
    public void setupBeforeTestClass() {
        System.out.println("BEFORE CLASS");
        driverSetup = new DriverSetup(DriverType.CHROME);
        driver = driverSetup.getDriver();
    };

    @BeforeMethod
    public void setupBeforeMethod() {
        System.out.println("BEFORE METHOD");
        /*driverSetup = new DriverSetup(DriverType.CHROME);
        driver = driverSetup.getDriver();*/
    };

    @AfterMethod
    public void tearDownAfterMethod() {
        System.out.println("AFTER METHOD");
        driverSetup.closeBrowserTab();
    };

    @AfterClass
    public void tearDownAfterTestClass() {
        System.out.println("AFTER CLASS");
    };
}
