package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;

public class RegistrationPage extends BasePage {
    public RegistrationPage(WebDriver driver) {
        super(driver);
    }

    public RegistrationPage(WebDriver driver, String url) {
        super(driver);
        driver.get(url);
    }

    //FIELD SECTION
    @FindBy(id = "basic_name")
    WebElement firstName_field;
    @FindBy(id = "basic_lastname")
    WebElement lastName_field;
    @FindBy(id = "basic_email")
    WebElement email_field;
    @FindBy(id = "basic_password")
    WebElement password_field;
    @FindBy(id = "basic_department")
    WebElement department_field;
    @FindBy(id = "basic_position")
    WebElement position_field;

    //BUTTONS SECTION
    @FindBy(xpath = "//span[contains(text(),'Register')]")
    WebElement register_button;
    @FindBy(xpath = "//span[contains(text(),'Create new account')]")
    WebElement createNewAccount_button;

    //ERROR MESSAGE SECTION
    @FindBy(xpath = "//div[contains(text(),'Please input your first name')]")
    public WebElement firstName_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your last name')]")
    public WebElement lastName_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your username')]")
    public WebElement email_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your password')]")
    public WebElement password_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please confirm your password')]")
    public WebElement passwordConfirmation_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your department')]")
    public WebElement department_errorMsg;
    @FindBy(xpath = "//div[contains(text(),'Please input your position')]")
    public WebElement position_errorMsg;

    //SUCCESSFUL MESSAGE SECTION
    @FindBy(xpath = "//span[contains(text(),'Successful registration')]")
    public WebElement successful_registrationMsg;

    public void enterFirstName(String firstName) {
        BasePage.sendText(firstName_field, firstName);
    }
    public void enterLastName(String lastName) {
        BasePage.sendText(lastName_field, lastName);
    }
    public void enterEmail(String email) {
        BasePage.sendText(email_field, email);
    }
    public void enterPassword(String pass) {
        BasePage.sendText(password_field, pass);
    }
    public void enterDepartment(String department) {
        BasePage.sendText(department_field, department);
    }
    public void enterPosition(String position) {
        BasePage.sendText(position_field, position);
    }
    public void clickRegisterButton() {
        BasePage.clickElement(register_button);
    }

    public void registerUser(String firstName, String lastName, String email, String password, String department, String position) throws InterruptedException {
        elementControl.clickElement(createNewAccount_button);
        Thread.sleep(2000);
        enterFirstName(firstName);
        enterLastName(lastName);
        enterEmail(email);
        enterPassword(password);
        enterDepartment(department);
        enterPosition(position);
        clickRegisterButton();
        Thread.sleep(3000);
    }
}
