package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class LogInUser extends ApiConfig {


    // Sending the request with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class)
    public void logInInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(
                        invalidEmailList,
                        "stringString1!"
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
                        "john.dean@boing.rs",
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
                        "jadranko@boing.rs",
                        "Password$3"
                ))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(400);
    }


}
