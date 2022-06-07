package api_test;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;

public class UpdateUser extends ApiConfig {

    //Updating the user with invalid name
    @Test(dataProvider = "name", dataProviderClass = UserDetailsGenerator.class)
    public void updateInvalidName(String val) {
        String userBodyJson = "{\n" +
                "  \"name\": \"" + val + "\",\n" +
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
    @Test(dataProvider = "name", dataProviderClass = UserDetailsGenerator.class)
    public void updateInvalidLastname(String val) {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"" + val + "\",\n" +
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
    @Test(dataProvider = "email", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidEmail(String val) {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"" + val + "\",\n" +
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
                .then().statusCode(404);
    }
    // Updating user with invalid password
    @Test(dataProvider = "password", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidPass(String val) {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"usertesting@boing.rs\",\n" +
                "  \"passwordUnhashed\": \"" + val + "\",\n" +
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


