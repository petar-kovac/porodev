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
    @FindBy(xpath = "//div/div/div/div[2]/p[2]")
    public WebElement we_profileFirstName_text;
    @FindBy(xpath = "//div/div[3]/p[2]")
    public WebElement we_profileLastName_text;
    @FindBy(xpath = "//div[4]/p[2]")
    public WebElement we_profileEmail_text;
    @FindBy(xpath = "//span[contains(text(),'success')]")
    public WebElement we_upload_confirmation;
    @FindBy (xpath = "//a[contains(text(),'Files')]")
    public WebElement we_files_title;

    //Button section
    @FindBy(className = "ant-dropdown-trigger")
    public WebElement we_settings_button;
    @FindBy(xpath = "//a[contains(text(),'Profile')]")
    public WebElement we_profile_button;
    @FindBy(css = "[data-icon='edit']")
    public WebElement we_editName_button;
    @FindBy(xpath = "//div[2]/div[3]/button[2]")
    public WebElement we_confirmChange_button;
    @FindBy(xpath = "//main/div/div/div/div[3]/div")
    public WebElement we_editLastName_button;
    @FindBy(xpath = "//div[5]/div/span")
    public WebElement we_editPassword_button;
    @FindBy(xpath = "//span[contains(text(),'Logout')]")
    public WebElement we_logOut_button;
    @FindBy(css = "[data-testid='upload']")
    public WebElement we_upload_button;
    @FindBy(css = "[data-testid='undefined-button']")
    public WebElement we_showMoreFiles_button;
    @FindBy(className = "ant-layout-sider-trigger")
    public WebElement we_sideExpander_button;

    //Entry section
    @FindBy(className = "ant-input")
    public WebElement we_editAttribute_entry;
    @FindBy(xpath = "//div[2]/span/div[1]/span/input")
    public WebElement we_uploadFile_field;




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
                                       String newAttribute,  WebElement elementForAsserting){
        BasePage.clickElement(elementForEditingButton);
        BasePage.sendText(editingFieldElement, newAttribute);
        BasePage.clickElement(we_confirmChange_button);
        BasePage.waitForElementVisibility(elementForAsserting, driver);
    }

    public void uploadFile(String filePath, String fileNAME) {
        //waitForElementVisibility(we_uploadFile_field, driver);
        we_uploadFile_field.sendKeys(filePath);
        waitForElementVisibility(we_upload_confirmation, driver);
        String uploadConfirmMessage = we_upload_confirmation.getText();
        Assert.assertEquals(uploadConfirmMessage, fileNAME + " file uploaded successfully.");

    }

    public void leadToFilesPage() {
        we_showMoreFiles_button.click();
        waitForElementVisibility(we_files_title, driver);
        String filesTitle = we_files_title.getText();
        Assert.assertEquals(filesTitle, "Files");
    }

}
