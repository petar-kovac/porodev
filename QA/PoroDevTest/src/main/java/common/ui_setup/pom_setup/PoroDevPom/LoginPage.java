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
    WebElement we_emailInputField;
    @FindBy(name = "password")
    WebElement we_passInputField;
    @FindBy(className = "ant-btn-primary")
    WebElement we_loginButton;
    //ASSERTION ELEMENTS
    @FindBy(xpath = "//span[contains(text(),'Email is required')]")
    WebElement we_email_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Password is required')]")
    WebElement we_pass_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Invalid email')]")
    WebElement we_email_invalidMsg;
    @FindBy(xpath = "//span[contains(text(),'Wrong password')]")
    WebElement we_pass_invalidMsg;
    @FindBy(xpath = "//span[contains(text(),'Successful login')]")
    WebElement we_successfulLogin;
    @FindBy(className = "ant-message")
    WebElement we_badRequestError;

    //functional methods section

    public void login(String email, String password) throws InterruptedException {
        BasePage.sendText(we_emailInputField,email);
        BasePage.sendText(we_passInputField, password);
        BasePage.clickElement(we_loginButton);
        Thread.sleep(2000);
    }

    //ASSERT METHODS
    public void assert_valid_login(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_successfulLogin), expectedResults);
    }

    public void assert_login_with_invalid_form_password(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_pass_invalidMsg), expectedResults);
    }

    public void assert_login_with_invalid_form_email(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_email_invalidMsg), expectedResults);
    }

    public void assert_login_with_valid_notExistingCredentials(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_badRequestError), expectedResults);
    }

    public void assert_email_isRequired(String expectedResult) {
        Assert.assertEquals(BasePage.getTextFromElement(we_email_requiredMsg), expectedResult);
    }

    public void assert_password_isRequired(String expectedResult) {
        Assert.assertEquals(BasePage.getTextFromElement(we_pass_requiredMsg), expectedResult);
    }
}