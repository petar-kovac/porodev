package api_test;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class UpdateUser extends ApiConfig {

    //Updating the user with invalid name

    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = UserDetailsGenerator.class)
    public void updateInvalidName(String InvalidNameList) {
        String userBodyJson = "{\n" +
                "  \"name\": \"" + InvalidNameList + "\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"usertesting@boing.rs\",\n" +
                "  \"passwordUnhashed\": \"pas@eeeG33\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }
    //Updating user with invalid last name
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = UserDetailsGenerator.class)
    public void updateInvalidLastname(String invalidLastNameList) {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"" + invalidLastNameList + "\",\n" +
                "  \"email\": \"usertesting@boing.rs\",\n" +
                "  \"passwordUnhashed\": \"pas@eeeG33\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }
    // Updating user with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = UserDetailsGenerator.class)
    public void updateInvalidEmail(String invalidEmailList) {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"" + invalidEmailList + "\",\n" +
                "  \"passwordUnhashed\": \"pas@eeeG33\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(anyOf(is(400), is(404)));
    }
    // Updating user with invalid password
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = UserDetailsGenerator.class)
    public void updateInvalidPass(String invalidPasswordList) {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"usertesting@boing.rs\",\n" +
                "  \"passwordUnhashed\": \"" + invalidPasswordList + "\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }
}


