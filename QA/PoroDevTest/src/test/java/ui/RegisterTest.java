package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.testng.Assert;
import org.testng.annotations.Test;

import java.io.IOException;

public class RegisterTest extends BaseTest{
    protected FileControlUtil fileControlUtil;

    public RegisterTest() throws IOException {
        fileControlUtil = new FileControlUtil(FileControlUtil.REGISTRATION_DATA_PROPERTIES);
    }

    @Test
    public void register_with_valid_credentials() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));
        Assert.assertEquals(elementControl.getTextFromElement(registrationPage.successful_registrationMsg), "Successful registration");
    }

    @Test
    public void register_without_firstName() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                "",
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));
        Assert.assertEquals(elementControl.getTextFromElement(registrationPage.firstName_errorMsg), "Please input your first name");
    }
}
