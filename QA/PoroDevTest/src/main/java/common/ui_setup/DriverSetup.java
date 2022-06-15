package common.ui_setup;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.edge.EdgeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

import java.util.concurrent.TimeUnit;

public class DriverSetup {
    private WebDriver driver;

    public DriverSetup(DriverType browserType) {

        switch(browserType) {
            case CHROME -> {
                WebDriverManager.chromedriver().setup();
                this.driver = new ChromeDriver();
            }
            case FIREFOX -> {
                WebDriverManager.firefoxdriver().setup();
                this.driver = new FirefoxDriver();
            }
            case EDGE -> {
                WebDriverManager.edgedriver().setup();
                this.driver = new EdgeDriver();
            }
        }
        driver.manage().window().maximize();
        driver.manage().deleteAllCookies();
    }

    public void navigateToUrl(String url) {
        driver.manage().timeouts().pageLoadTimeout(SetupConstants.PAGE_LOAD_TIME, TimeUnit.SECONDS);
        driver.manage().timeouts().implicitlyWait(SetupConstants.ELEMENT_DETECTION_TIMEOUT, TimeUnit.SECONDS);
        driver.get(url);
    }

    public void closeBrowserTab() {
        driver.close();
    }

    public void quitBrowser() {
        driver.quit();
    }

    public WebDriver getDriver() {
        return driver;
    }

    public void setDriver(WebDriver driver) {
        this.driver = driver;
    }
}
