package common.ui_setup.pom_setup.PoroDevPom;

import common.ui_setup.DriverSetup;
import common.ui_setup.pom_setup.BasePage;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.testng.Assert;

public class FilesPage extends BasePage {

    public FilesPage(WebDriver driver, String url) {
        super(driver);
        DriverSetup.navigateToUrl(driver, url);
    }

    public FilesPage(WebDriver driver) {
        super(driver);
    }


    //Text assertion section
    @FindBy(xpath = "//a[contains(text(),'Files')]")
    public WebElement we_files_title;

    @FindBy(xpath = "//div/div/div[4]/div[2]/p")
    public WebElement we_filenameSider_text;

    @FindBy(xpath = "//div/div[3]/div[2]/div[1]/div/div/div/div[1]")
    public WebElement  we_fileName_text;

    @FindBy(xpath = "//div/div[4]/div[2]/p/text()[3]")
    public WebElement we_fileNameOnSideer_text;





    // Buttons section

    @FindBy(css = "[data-testid='list-card']")
    public WebElement we_firstFileOnList_button;


    // Entry section



    //Methods section
    public void clickOnFirstFileInList(){
        BasePage.clickElement(we_firstFileOnList_button);
    }









}
