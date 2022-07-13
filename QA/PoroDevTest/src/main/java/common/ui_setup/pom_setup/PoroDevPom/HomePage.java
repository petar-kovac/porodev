package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.DriverSetup;
import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.testng.Assert;

public class HomePage extends BasePage {

    public HomePage(WebDriver driver, String url) {
        super(driver);
        DriverSetup.navigateToUrl(driver, url);
    }
    public HomePage(WebDriver driver) {
        super(driver);
    }

    //Text assertion elements
    @FindBy(xpath = "//div[contains(text(),'User file upload:')]")
    public WebElement we_userFileUpload_message;
    @FindBy(xpath = "//h2[contains(text(),'Profile Info')]")
    public WebElement we_profileInfo_message;

    //Button section
    @FindBy(className = "ant-dropdown-trigger")
    public WebElement we_settings_button;
    @FindBy(xpath = "//a[contains(text(),'Profile')]")
    public WebElement we_profile_button;

    @FindBy(css = "[data-icon='edit']")
    public WebElement we_editName_button;
    @FindBy(className = "ant-input")
    public WebElement we_editAttribute_entry;
    @FindBy(xpath = "//div/div/div/div[2]/p[2]")
    public WebElement we_profileFirstName_text;
    @FindBy(xpath = "//div[2]/div[3]/button[2]")
    public WebElement we_confirmChange_button;

    @FindBy(xpath = "//main/div/div/div/div[3]/div")
    public WebElement we_editLastName_button;

    @FindBy(xpath = "//div/div[3]/p[2]")
    public WebElement we_profileLastName_text;

    @FindBy(css = "anticon anticon-edit")
    public WebElement we_editPassword_button;
    
    @FindBy(xpath = "//span[contains(text(),'Logout')]")
    public WebElement we_logOut_button;
    @FindBy(css = "[data-testid='upload']")
    public WebElement we_upload_button;
    @FindBy(css = "[data-testid='undefined-button']")
    public WebElement we_showMoreFiles_button;
    @FindBy(className = "ant-layout-sider-trigger")
    public WebElement we_sideExpander_button;

    //Method section
    public void logOutUser() {
        BasePage.clickElement(we_settings_button);
        BasePage.clickElement(we_logOut_button);
    }

    public void goTo_profilePage() {
        BasePage.clickElement(we_settings_button);
        BasePage.clickElement(we_profile_button);
    }

    public void assert_profileInfo_isDisplayed(String customErrorMessage) {
        Assert.assertTrue(BasePage.isDisplayed(we_profileInfo_message),customErrorMessage);
    }

    public void changeProfileAttribute (WebElement elementForEditingButton, WebElement editingFieldElement,
                                       String newName,  WebElement elementForAsserting){
        BasePage.clickElement(elementForEditingButton);
        BasePage.sendText(editingFieldElement, newName);
        BasePage.clickElement(we_confirmChange_button);
        BasePage.waitForElementVisibility(elementForAsserting, driver);


    }

}
