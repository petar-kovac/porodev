package ui;

import common.ui_setup.FileControlUtil;
import common.ui_setup.SetupConstants;
import common.ui_setup.pom_setup.BasePage;
import common.ui_setup.pom_setup.PoroDevPom.RegistrationPage;
import org.testng.Assert;
import org.testng.annotations.Test;

import java.io.IOException;
import java.util.Set;

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
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_user_registration_isSuccessful("Successful registration");
    }

    @Test
    public void register_without_firstName() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                "",
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_firstName_isRequired("First name is required");
    }

    @Test
    public void register_without_lastName() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                "",
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));
        registrationPage.assert_lastName_isRequired("Last name is required");
    }

    @Test
    public void register_without_email() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                "",
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_email_isRequired("Email is required");
    }

    @Test
    public void register_without_password() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                "",
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_password_isRequired("Password is required");
    }

    @Test
    public void register_without_confirmationPassword() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                "",
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_confirmationPassword_isRequired("Confirm password");
    }

    @Test
    public void register_without_department() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                "",
                fileControlUtil.getValue("VALID_POSITION"));

        registrationPage.assert_department_isRequired("Department is required");
    }

    @Test
    public void register_without_position() throws InterruptedException {
        RegistrationPage registrationPage = new RegistrationPage(driver, SetupConstants.BASE_URL);

        registrationPage.registerUser(
                fileControlUtil.getValue("VALID_FIRSTNAME"),
                fileControlUtil.getValue("VALID_LASTNAME"),
                fileControlUtil.getValue("VALID_EMAIL"),
                fileControlUtil.getValue("VALID_PASS"),
                fileControlUtil.getValue("VALID_CONFIRMATION_PASS"),
                fileControlUtil.getValue("VALID_DEPARTMENT"),
                "");

        registrationPage.assert_position_isRequired("Position is required");
    }
}