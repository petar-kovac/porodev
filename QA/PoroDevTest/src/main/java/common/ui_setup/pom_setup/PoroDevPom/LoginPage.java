package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.DriverSetup;
import common.ui_setup.SetupConstants;
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
        DriverSetup.navigateToUrl(SetupConstants.BASE_URL, driver);
    }

    //element section
    @FindBy(name = "email")
    WebElement we_emailInputField;
    @FindBy(name = "password")
    WebElement we_passInputField;
    @FindBy(className = "ant-btn-primary")
    WebElement we_loginButton;
    //ASSERTION ELEMENTS
    @FindBy(xpath = "//body/div[@id='root']/div[1]/div[1]/form[1]/div[1]/span[2]")
    WebElement we_email_requiredMsg;
    @FindBy(xpath = "//body/div[@id='root']/div[1]/div[1]/form[1]/div[2]/span[3]")
    WebElement we_pass_requiredMsg;
    @FindBy(xpath = "//span[contains(text(),'Email is invalid')]")
    WebElement we_email_invalidMsg;
    @FindBy(xpath = "//span[contains(text(),'Wrong password')]")
    WebElement we_pass_invalidMsg;
    @FindBy(xpath = "//span[contains(text(),'Successful login')]")
    WebElement we_successfulLogin;
    @FindBy(className = "ant-message")
    WebElement we_badRequestError;
    @FindBy(className = "sc-ftvSup")
    List<WebElement> we_listOfErrors;

    //functional methods section

    public void login(String email, String password) throws InterruptedException {
        BasePage.sendText(we_emailInputField,email);
        BasePage.sendText(we_passInputField, password);
        BasePage.clickElement(we_loginButton);
        Thread.sleep(2000);
    }

    //ASSERT METHODS
    public void assert_login(String expectedResults) {

        String currentError = "";
        for (WebElement element : we_listOfErrors) {
            if (!BasePage.getTextFromElement(element).equals("")) {
                currentError = BasePage.getTextFromElement(element);
                break;
            }
        }
        System.out.println(currentError);
        Assert.assertEquals(currentError, expectedResults);
    }
    public void assert_valid_login(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_successfulLogin), expectedResults);
    }

    public void assert_login_with_valid_notExistingCredentials(String expectedResults) {
        Assert.assertEquals(BasePage.getTextFromElement(we_badRequestError), expectedResults);
    }
}