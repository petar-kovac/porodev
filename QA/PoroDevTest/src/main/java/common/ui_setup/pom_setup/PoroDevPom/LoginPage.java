package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.testng.Assert;

public class LoginPage extends BasePage {

    public LoginPage(WebDriver driver) {
        super(driver);
    }

    public LoginPage(WebDriver driver, String url) {
        super(driver);
        driver.get(url);
    }

    //element section
    @FindBy(name = "email")
    WebElement emailField;
    @FindBy(name = "password")
    WebElement passField;
    @FindBy(className = "ant-btn-primary")
    WebElement loginButton;
    //ASSERTION ELEMENTS
    @FindBy(xpath = "//span[contains(text(),'Email is required')]")
    public WebElement email_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Password is required')]")
    public WebElement pass_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Invalid email')]")
    public WebElement email_invalidMsg;
    @FindBy(xpath = "//span[contains(text(),'Wrong password')]")
    public WebElement pass_invalidMsg;
    @FindBy(xpath = "//span[contains(text(),'Successful login')]")
    public WebElement successfulLogin;
    @FindBy(className = "ant-message")
    public WebElement badRequestError;

    //methods section
    public void enterEmail(String email) {
        BasePage.sendText(emailField, email);
    }

    public void enterPassword(String pass) {
        BasePage.sendText(passField, pass);
    }

    public void clickLoginButton() {
        BasePage.clickElement(loginButton);
    }

    public void login(String email, String password) throws InterruptedException {
        enterEmail(email);
        enterPassword(password);
        clickLoginButton();
        Thread.sleep(2000);
    }

    //ASSERT METHODS
    public void assert_valid_login() {
        Assert.assertEquals(BasePage.getTextFromElement(successfulLogin), "Successful login");
    }

    public void assert_login_with_invalid_form_password(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(pass_invalidMsg), expectedResults);
    }

    public void assert_login_with_invalid_form_email(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(email_invalidMsg), expectedResults);
    }

    public void assert_login_with_valid_notExistingCredentials(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(badRequestError), expectedResults);
    }

    public void assert_email_isRequired(String expectedResult) {
        Assert.assertEquals(BasePage.getTextFromElement(email_requiredMsg), expectedResult);
    }

    public void assert_password_isRequired(String expectedResult) {
        Assert.assertEquals(BasePage.getTextFromElement(pass_requiredMsg), expectedResult);
    }
}
