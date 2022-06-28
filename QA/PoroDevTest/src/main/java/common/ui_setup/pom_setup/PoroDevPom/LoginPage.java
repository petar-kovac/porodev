package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.DriverSetup;
import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.testng.Assert;

import java.util.List;

public class LoginPage extends BasePage {

    public LoginPage(WebDriver driver) {
        super(driver);
    }

    public LoginPage(WebDriver driver, String url) {
        super(driver);
        DriverSetup.navigateToUrl(driver, url);
    }

    //element section
    @FindBy(name = "email")
    WebElement we_emailInputField;
    @FindBy(name = "password")
    WebElement we_passInputField;
    @FindBy(className = "ant-btn-default")
    WebElement we_loginButton;

    //ASSERTION ELEMENTS
    @FindBy(xpath = "//span[contains(text(),'Successful logInUser')]")
    public WebElement we_successfulLogin;
    @FindBy(className = "ant-message")
    public WebElement we_badRequestError;
    @FindBy(xpath = "//span[@data-testid='error-message']")
    List<WebElement> we_listOfErrors;
    @FindBy(id = "loginForm")
    public WebElement we_login_form;

    //functional methods section

    public void logInUser(String email, String password) {
        BasePage.sendText(we_emailInputField,email);
        BasePage.sendText(we_passInputField, password);
        BasePage.clickElement(we_loginButton);
    }

    //ASSERT METHODS
    public void assert_login_errorMessages(String expectedResults) {

        String currentError = "";
        for (WebElement element : we_listOfErrors) {
            if (!BasePage.getTextFromElement(element).equals("")) {
                currentError = BasePage.getTextFromElement(element);
                break;
            }
        }
        Assert.assertEquals(currentError, expectedResults);
    }

    public void assert_login(WebElement element, String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(element), expectedResults);
    }

    public void assert_thatElement_isDisplayed(WebElement element) {
        Assert.assertTrue(BasePage.isDisplayed(element));
    }
}