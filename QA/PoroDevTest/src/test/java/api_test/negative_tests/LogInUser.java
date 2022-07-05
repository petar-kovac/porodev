package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import io.qameta.allure.Feature;
import org.testng.annotations.Test;

import java.io.IOException;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

@Feature("LogIn User/Negative test cases")
public class LogInUser extends ApiConfig {

    private final FileControlUtil file = new FileControlUtil(FileControlUtil.BE_REGISTER_PROPERTIES);

    public LogInUser() throws IOException {
    }


    // Sending the request with invalid email (invalid email list form dataprovider)
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the log in request with invalid email attribute")
    public void logInInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        invalidEmailList,
                        file.getValue("VALID_PASS")
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then()
                .statusCode(anyOf(is(400), is(401), is(500)));
    }


    // Sending request with invalid password (invalid password list from dataprovider)
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the log in request with invalid password attribute")
    public void logInInvalidPassword(String invalidPasswordList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        file.getValue("VALID_EMAIL"),
                        invalidPasswordList
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then()
                .statusCode(anyOf(is(400), is(401), is(500)));
    }

    // Valid but non-existing email and password
    @Test(description = "Sending the log in request with non existing user details")
    public void logInNonExistingUser() {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        file.getValue("NON_EXISTING_EMAIL"),
                        file.getValue("NON_EXISTING_PASS")
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(400);
    }

    // Existing email, non-existing password

    @Test(description = "Sending the log in request with valid, non-existing password")
    public void logInInvalidPassword() {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        file.getValue("VALID_EMAIL"),
                        file.getValue("NON_EXISTING_PASS")
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(400);
    }


}
