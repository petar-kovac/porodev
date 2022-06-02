package TestSetup;

import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;

public abstract class BaseTest {

    @BeforeClass
    public abstract void setupBeforeTestClass();

    @BeforeMethod
    public abstract void setupBeforeMethod();

    @AfterMethod
    public abstract void tearDownAfterMethod();

    @AfterClass
    public abstract void tearDownAfterTestClass();
}
