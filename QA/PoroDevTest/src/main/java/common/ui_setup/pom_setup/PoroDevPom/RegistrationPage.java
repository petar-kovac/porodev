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

public class RegistrationPage extends BasePage {
    private final Logger logger = LoggerFactory.getLogger(RegistrationPage.class);

    public RegistrationPage(WebDriver driver) {
        super(driver);
    }

    public RegistrationPage(WebDriver driver, String url) {
        super(driver);
        DriverSetup.navigateToUrl(driver, url);
    }

    //FIELD SECTION
    @FindBy(name = "name")
    WebElement we_firstName_inputField;
    @FindBy(name = "lastname")
    WebElement we_lastName_inputField;
    @FindBy(name = "email")
    WebElement we_email_inputField;
    @FindBy(name = "password")
    WebElement we_password_inputField;
    @FindBy(name = "confirmPassword")
    WebElement we_confirmPass_inputField;
    @FindBy(name = "department")
    WebElement we_department_inputField;
    @FindBy(name = "position")
    WebElement we_position_inputField;

    //BUTTONS SECTION
    @FindBy(css = "[data-testid='registerForm-button']")
    WebElement we_register_button;
    @FindBy(css = "[data-testid='toggle-button']")
    WebElement we_createNewAccount_button;

    //ERROR MESSAGE SECTION
    @FindBy(xpath = "//span[contains(text(),'Request failed with status code 400')]")
    public WebElement we_requestFailed_statusCode400;

    //REQUIRED MESSAGE SECTION
    @FindBy(xpath = "//span[@data-testid='error-message']")
    List<WebElement> we_errorFields;

    //Assertion messages
    @FindBy(xpath = "//span[contains(text(),'Successful registration')]")
    public
    WebElement we_successfulRegistration_message;

    //Methods section
    public void registerUser(String firstName, String lastName, String email, String password, String confirmPass, String department, String position)  {

        BasePage.clickElement(we_createNewAccount_button);
        BasePage.sendText(we_firstName_inputField, firstName);
        BasePage.sendText(we_lastName_inputField, lastName);
        BasePage.sendText(we_email_inputField, email);
        BasePage.sendText(we_password_inputField, password);
        BasePage.sendText(we_confirmPass_inputField, confirmPass);
        BasePage.sendText(we_department_inputField, department);
        BasePage.sendText(we_position_inputField, position);
        BasePage.clickElement(we_register_button);
    }

    //ASSERT METHODS
    public void assert_user_registration(WebElement element, String expectedResults, String customErrorMessage) {
        logger.info("Element text: {}",  BasePage.getTextFromElement(element));
        Assert.assertEquals(BasePage.getTextFromElement(element), expectedResults, customErrorMessage);
    }

    public void assert_errorMessage(String expectedResults, String customErrorMessage) {
        String currentError = "";

        for (WebElement element : we_errorFields) {
            if (!BasePage.getTextFromElement(element).equals("")) {
                currentError = BasePage.getTextFromElement(element);
                break;
            }
        }
        logger.info("Expected error message: {}", currentError);
        Assert.assertEquals(currentError, expectedResults, customErrorMessage);
    }
}