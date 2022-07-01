package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import org.testng.annotations.Test;

import java.io.IOException;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class LogInUser extends ApiConfig {

    private final FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);

    public LogInUser() throws IOException {
    }


    // Sending the request with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class)
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


    // Sending request with invalid password
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = DataProviderBeUtil.class)
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
    @Test
    public void logInNonExistingUser() {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        "zivko@boing.rs",
                        "Password###4576"
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(400);
    }

    // Existing email, invalid password

    @Test
    public void logInInvalidPassword() {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        file.getValue("VALID_EMAIL"),
                        "Password$3"
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(400);
    }


}
