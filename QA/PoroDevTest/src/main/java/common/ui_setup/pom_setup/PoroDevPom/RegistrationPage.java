package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.DriverSetup;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;

import java.time.Duration;
import java.util.List;

public class RegistrationPage extends BasePage {
    private WebDriverWait wait;
    private WebElement element;

    public RegistrationPage(WebDriver driver) {
        super(driver);
    }

    public RegistrationPage(WebDriver driver, String url) {
        super(driver);
        DriverSetup.navigateToUrl(SetupConstants.BASE_URL, driver);
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
    @FindBy(xpath = "//span[contains(text(),'Register')]")
    WebElement we_register_button;
    @FindBy(xpath = "//span[contains(text(),'Create new account')]")
    WebElement we_createNewAccount_button;

    //ERROR MESSAGE SECTION
    @FindBy(xpath = "//body/div[@id='root']/div[1]/div[1]/form[1]/div[1]/span[2]")
    public WebElement we_firstName_errorMsg;
    @FindBy(xpath = "//body/div[@id='root']/div[1]/div[1]/form[1]/div[2]/span[2]")
    public WebElement we_lastName_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Email is invalid')]")
    public WebElement we_email_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Wrong password')]")
    public WebElement we_password_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Whitespace not allowed')]")
    public WebElement we_password_whiteSpace_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Passwords should match')]")
    public WebElement we_confirmPass_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Must be a number')]")
    public WebElement we_department_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Letters & whitespace only')]")
    public WebElement we_position_errorMsg;
    @FindBy(xpath = "//span[contains(text(),'Request failed with status code 500')]")
    public WebElement we_requestFailed_statusCode500;
    @FindBy(xpath = "//span[contains(text(),'Request failed with status code 400')]")
    public WebElement we_requestFailed_statusCode400;

    //REQUIRED MESSAGE SECTION
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[1]/span[2]")
    public WebElement we_firstName_requiredMsg;
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[2]/span[2]")
    public WebElement we_lastName_requiredMsg;
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[3]/span[2]")
    public WebElement we_email_requiredMsg;
    @FindBy(xpath = "//body/div[@id='root']/div[1]/div[1]/form[1]/div[4]/span[3]")
    public WebElement we_password_requiredMsg;
    @FindBy(xpath = "//body/div[@id='root']/div[1]/div[1]/form[1]/div[5]/span[3]")
    public WebElement we_confirmPass_requiredMsg;
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[6]/span[2]")
    public WebElement we_department_requiredMsg;
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[7]/span[2]")
    public WebElement we_position_requiredMsg;
    @FindBy(className = "sc-hHLeRK")
    List<WebElement> we_errorFields;

    //SUCCESSFUL MESSAGE SECTION
    @FindBy(xpath = "//span[contains(text(),'Successful registration')]")
    public WebElement we_successful_registrationMsg;

    public void registerUser(String firstName, String lastName, String email, String password, String confirmPass, String department, String position) throws InterruptedException {


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

    public void explicitWait(WebElement element) {
        wait = new WebDriverWait(driver, Duration.ofSeconds(SetupConstants.ELEMENT_DETECTION_TIMEOUT));
        wait.until(ExpectedConditions.visibilityOf(element));
    }

    //ASSERT METHODS
    public void assert_user_registration(WebElement element, String expectedResults) {
        System.out.println("error: " + BasePage.getTextFromElement(element));
        Assert.assertEquals(BasePage.getTextFromElement(element), expectedResults);
    }

    public void assert_errorMessage(String expectedResults) {
        String currentError = "";

        for (WebElement element : we_errorFields) {
            if (!BasePage.getTextFromElement(element).equals("")) {
                currentError = BasePage.getTextFromElement(element);
                break;
            }
        }
        System.out.println("error: " + currentError);
        Assert.assertEquals(currentError, expectedResults);
    }
}