package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.testng.Assert;

public class RegistrationPage extends BasePage {
    public RegistrationPage(WebDriver driver) {
        super(driver);
    }

    public RegistrationPage(WebDriver driver, String url) {
        super(driver);
        driver.get(url);
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
    @FindBy(xpath = "//div[contains(text(),'Please input your first name')]")
    public WebElement we_firstName_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your last name')]")
    public WebElement we_lastName_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your username')]")
    public WebElement we_email_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your password')]")
    public WebElement we_password_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please confirm your password')]")
    public WebElement we_passwordConfirmation_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your department')]")
    public WebElement we_department_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your position')]")
    public WebElement we_position_errorMsg;

    //REQUIRED MESSAGE SECTION
    @FindBy(xpath = "//span[contains(text(),'First name is required')]")
    WebElement we_firstName_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Last name is required')]")
    WebElement we_lastName_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Email is required')]")
    WebElement we_email_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Password is required')]")
    WebElement we_password_requiredMsg;
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[5]/span[2]")
    WebElement we_confirmPass_requiredMsg;
    @FindBy(xpath = "//*[@id=\"registerForm\"]/div[5]/span[2]")
    WebElement we_confirmPass_errorMsg_while_pass_isNotTyped;
    @FindBy(xpath = "//span[contains(text(),'Department is required')]")
    WebElement we_department_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Position is required')]")
    WebElement we_position_requiredMsg;

    //SUCCESSFUL MESSAGE SECTION
    @FindBy(xpath = "//span[contains(text(),'Successful registration')]")
    public WebElement successful_registrationMsg;

    public void registerUser(String firstName, String lastName, String email, String password, String confirmPass, String department, String position) throws InterruptedException {
        BasePage.clickElement(we_createNewAccount_button);
        Thread.sleep(2000);
        BasePage.sendText(we_firstName_inputField, firstName);
        BasePage.sendText(we_lastName_inputField, lastName);
        BasePage.sendText(we_email_inputField, email);
        BasePage.sendText(we_password_inputField, password);
        BasePage.sendText(we_confirmPass_inputField, confirmPass);
        BasePage.sendText(we_department_inputField, department);
        BasePage.sendText(we_position_inputField, position);
        BasePage.clickElement(we_register_button);
        Thread.sleep(3000);
    }

    //ASSERT METHODS
    public void assert_user_registration_isSuccessful(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(successful_registrationMsg), expectedResults);
    }

    public void assert_firstName_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_firstName_requiredMsg), expectedResults);
    }

    public void assert_lastName_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_lastName_requiredMsg), expectedResults);
    }

    public void assert_email_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_email_requiredMsg), expectedResults);
    }

    public void assert_password_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_password_requiredMsg), expectedResults);
    }

    public void assert_confirmationPassword_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_confirmPass_requiredMsg), expectedResults);
    }

    public void assert_department_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_department_requiredMsg), expectedResults);
    }

    public void assert_position_isRequired(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_position_requiredMsg), expectedResults);
    }
}
