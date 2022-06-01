package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;

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
}
