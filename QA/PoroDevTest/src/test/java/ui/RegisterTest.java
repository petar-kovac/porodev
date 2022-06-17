package ui;

import common.ui_setup.FileControl;
import common.ui_setup.pom_setup.PoroDevPom.LoginPage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.testng.Assert;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.io.IOException;

public class RegisterTest extends BaseTest{
    protected FileControl fileControl = new FileControl(FileControl.REGISTRATION_DATA_PROPERTIES);

    public RegisterTest() throws IOException {
    }

    @Test
    public void register_with_valid_credentials() throws InterruptedException {
        registrationPage.registerUser(
                fileControl.getValue("VALID_FIRSTNAME"),
                fileControl.getValue("VALID_LASTNAME"),
                fileControl.getValue("VALID_EMAIL"),
                fileControl.getValue("VALID_PASS"),
                fileControl.getValue("VALID_DEPARTMENT"),
                fileControl.getValue("VALID_POSITION"));
        Assert.assertEquals(elementControl.getTextFromElement(registrationPage.successful_registrationMsg), "Successful registration");
    }

    @Test
    public void register_without_firstName() throws InterruptedException {
        registrationPage.registerUser(
                "",
                fileControl.getValue("VALID_LASTNAME"),
                fileControl.getValue("VALID_EMAIL"),
                fileControl.getValue("VALID_PASS"),
                fileControl.getValue("VALID_DEPARTMENT"),
                fileControl.getValue("VALID_POSITION"));
        Assert.assertEquals(elementControl.getTextFromElement(registrationPage.firstName_errorMsg), "Please input your first name");
    }
}
