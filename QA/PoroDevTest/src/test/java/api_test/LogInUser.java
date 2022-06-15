package api_test;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class LogInUser extends ApiConfig {


    // Sending the request with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = UserDetailsGenerator.class)
    public void logInInvalidEmail(String invalidEmailList) {
        String userBodyJson = "{\n"+
                "  \"email\": \""+invalidEmailList+"\",\n" +
                "  \"password\": \"stringString1!\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.USER_LOGIN)
                .then()
                .statusCode(anyOf(is(400), is(401), is(500)));
    }


    // Sending request with invalid password
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = UserDetailsGenerator.class)
    public void logInInvalidPassword(String invalidPasswordList) {
        String userBodyJson = "{\n"+
                "  \"email\": \"jadranko@boing.rs\",\n" +
                "  \"password\": \""+invalidPasswordList+"\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.USER_LOGIN)
                .then()
                .statusCode(anyOf(is(400), is(401), is(500)));
    }

    // Valid but non-existing email and password
    @Test
    public void logInNonExistingUser() {
        String userBodyJson = "{\n"+
                "  \"email\": \"zivko@boing.rs\",\n" +
                "  \"password\": \"Password###4576\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(401);
    }

    // Existing email, invalid password

    @Test
    public void logInInvalidPassword() {
        String userBodyJson = "{\n"+
                "  \"email\": \"jadranko@boing.rs\",\n" +
                "  \"password\": \"Password###4576\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().statusCode(401);
    }


}
