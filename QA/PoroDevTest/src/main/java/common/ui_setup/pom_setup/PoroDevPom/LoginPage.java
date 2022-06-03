package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import java.io.IOException;

public class LoginPage extends BasePage {

    public LoginPage(WebDriver driver) {
        super(driver);
    }

    //element section
    @FindBy(id = "basic_email")
    WebElement emailField;
    @FindBy(id = "basic_password")
    WebElement passField;
    @FindBy(className = "ant-btn-primary")
    WebElement loginButton;
    //ASSERTION ELEMENTS
    @FindBy(xpath = "//div[contains(text(),'Please input your username')]")
    public WebElement emailErrorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your password')]")
    public WebElement passErrorMsg;
    @FindBy(xpath = "//span[contains(text(),'Request failed with status code 401')]")
    public WebElement errorAfterBadCredentials;
    @FindBy(className = "hvVmBA")
    public WebElement userFirstNameAfterLogin;
    @FindBy(xpath = "//span[contains(text(),'Successful login')]")
    public WebElement successfulLogin;

    //methods section
    public void enterEmail(String email) {
        elementControl.sendText(emailField, email);
    }

    public void enterPassword(String pass) {
        elementControl.sendText(passField, pass);
    }

    public void clickLoginButton() {
        elementControl.clickElement(loginButton);
    }

    public void login_and_assert(String email, String password) throws InterruptedException {
        enterEmail(email);
        enterPassword(password);
        clickLoginButton();
        Thread.sleep(2000);
    }
}
