package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.DriverSetup;
import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.Assert;

import java.util.List;

public class LoginPage extends BasePage {
    private final Logger logger = LoggerFactory.getLogger(LoginPage.class);

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

    public
    @FindBy(xpath = "//*[@id=\"root\"]/div/div/div/button")
    WebElement we_loginButton;

    //ASSERTION ELEMENTS
    @FindBy(xpath = "//span[contains(text(),'Successful login')]")
    public WebElement we_successfulLogin;
    @FindBy(className = "ant-message")
    public WebElement we_badRequestError;
    @FindBy(xpath = "//span[@data-testid='error-message']")
    List<WebElement> we_listOfErrors;
    @FindBy(id = "loginForm")
    public WebElement we_login_form;
    @FindBy(xpath = "//span[contains(text(),'Request failed with status code 400')]")
    public WebElement we_badRequest_statusCode400;

    @FindBy(xpath = "//span[contains(text(),'Create new account')]")
    public WebElement we_login_title;


    //functional methods section

    public void logInUser(String email, String password) {
        BasePage.sendText(we_emailInputField,email);
        BasePage.sendText(we_passInputField, password);
        BasePage.waitForElementToBeClickable(we_loginButton, driver);
        BasePage.clickElement(we_loginButton);
    }

    //ASSERT METHODS
    public void assert_login_errorMessages(String expectedResults, String customErrorMessage) {

        String currentError = "";
        for (WebElement element : we_listOfErrors) {
            if (!BasePage.getTextFromElement(element).equals("")) {
                currentError = BasePage.getTextFromElement(element);
                break;
            }
        }
        Assert.assertEquals(currentError, expectedResults, customErrorMessage);
    }

    public void assert_login(WebElement element, String expectedResults, String customErrorMessage) {
        Assert.assertEquals(BasePage.getTextFromElement(element), expectedResults, customErrorMessage);
    }

    public void assert_thatElement_isDisplayed(WebElement element, String customErrorMessage) {
        Assert.assertTrue(BasePage.isDisplayed(element), customErrorMessage);
    }
}