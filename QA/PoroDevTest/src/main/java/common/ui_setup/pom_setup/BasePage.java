package common.ui_setup.pom_setup;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.Select;

import java.util.Random;

public abstract class BasePage {
    protected WebDriver driver;

    public BasePage(WebDriver driver) {
        this.driver = driver;
        PageFactory.initElements(driver, this);
    }

    public static void clickElement(WebElement element) {
        element.click();
    }

    public static void sendText(WebElement element, String text) {
        element.clear();
        element.sendKeys(text);
    }

    public static String getTextFromElement(WebElement element) {
        return element.getText();
    }

    public static boolean isEnabled(WebElement element) {
        return element.isEnabled();
    }

    public static boolean isDisplayed(WebElement element) {
        return element.isDisplayed();
    }

    public static void selectViaVisibleText(WebElement element, String text) {
        Select dropDown = new Select(element);
        dropDown.selectByVisibleText(text);
    }

    public static void selectByValue(WebElement element, String value) {
        Select dropDown = new Select(element);
        dropDown.selectByValue(value);
    }

    public static void hoverOverElement(WebElement element, WebDriver driver) {
        Actions hover = new Actions(driver);
        hover.moveToElement(element).build().perform();
    }

    public static void clickOnHoveredElement(WebElement element, WebDriver driver) {
        Actions hover = new Actions(driver);
        hover.moveToElement(element).click().build().perform();
    }

    public static String getRandomBoingEmail() {
        String domain = "abcfefghijklmnprostuwxyz1234567890";
        StringBuilder salt = new StringBuilder();
        Random rnd = new Random();
        while (salt.length() < 10) { // length of the random string.
            int index = (int) (rnd.nextFloat() * domain.length());
            salt.append(domain.charAt(index));
        }
        return salt.toString() + "@boing.rs";
    }
}