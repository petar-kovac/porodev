package api_test;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;

public class FunctionalTest extends ApiConfig {

    //Register user
    @Test(priority = 1)
    public void registerUserByJSON() {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"password\": \"stringString1!\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then();
    }

    // Update user (change any of the details)

    @Test (priority = 2)
    public void updateUserByJson() {
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Rather\",\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"passwordUnhashed\": \"stringString1!\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .put(Endpoints.UPDATE_USER)
                .then();
    }

    // Get the user details with sending the email in request

    @Test (priority = 3)
    public void getUserByEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL+"john.dean@boing.rs")
                .then();
    }

    //Sending the log in request
    @Test (priority = 4)
    public void logInUser() {
        String logInJSONObject = "{\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"password\": \"stringString1!\"\n" +
                "}";

        given().relaxedHTTPSValidation()
                .body(logInJSONObject)
                .when()
                .post(Endpoints.USER_LOGIN)
                .then();
    }

    // sending the request for deleting the user with sending email
    @Test (priority = 5)
    public void deleteUser() {
        given().relaxedHTTPSValidation()
                .when()
                .delete(Endpoints.USER_DELETE+"john.dean@boing.rs")
                .then();
    }


}

