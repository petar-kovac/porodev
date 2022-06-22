package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;

public class HomePage extends BasePage {
    public HomePage(WebDriver driver) {
        super(driver);
    }

    //Text assertion elements
    @FindBy(xpath = "//div[contains(text(),'User file upload:')]")
    public WebElement we_userFileUpload_message;

    //Button section
    @FindBy(className = "ant-dropdown-trigger")
    public WebElement we_settings_button;
    @FindBy(xpath = "//span[contains(text(),'Logout')]")
    public WebElement we_logOut_button;

    //Method section
    public void logOut() {
        BasePage.clickElement(we_settings_button);
        BasePage.clickElement(we_logOut_button);
    }
}
