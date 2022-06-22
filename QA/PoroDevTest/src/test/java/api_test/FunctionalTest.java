package api_test;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.User;
import common.api_setup.api_common.UserDetailsGenerator;
import common.api_setup.api_common.UserSerialization;
import io.qameta.allure.Feature;
import io.qameta.allure.Severity;
import io.qameta.allure.SeverityLevel;
import jdk.jfr.Description;
import org.testng.annotations.Test;
import static org.hamcrest.Matchers.equalTo;



import static io.restassured.RestAssured.given;


public class FunctionalTest extends ApiConfig {



    //Register user
    @Test(priority = 0, dataProvider = "PojoRegularEntry", dataProviderClass = UserDetailsGenerator.class,
            description = "Sending the request for user register with regular input")
    public void registerUserByJSON(User providedUser) {
        given().relaxedHTTPSValidation()
                .body(providedUser)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then();
    }

    // Update user (change any of the details)

    @Test (priority = 1, dataProvider = "PojoRegularEntry", dataProviderClass = UserDetailsGenerator.class,
    description = "Sending the request for update the users information with regular input")
    public void updateUserByJson(User providedUser) {
        given().relaxedHTTPSValidation()
                .body(providedUser)
                .when()
                .put(Endpoints.UPDATE_USER)
                .then();
    }

    // Get the user details with sending the email in request

    @Test (priority = 2, description = "Sending the request for getting the user information with " +
            "the regular email")
    public void getUserByEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL+ Endpoints.EMAIL_PATH + "john.dean@boing.rs")
                .then().body("name", equalTo("John"));
    }

    //Sending the log in request
    @Test (priority = 3, description = "Sending the request for log in with regular email and password entry")
    public void logInUser() {
        String logInJSONObject = "{\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"password\": \"stringString1!\"\n" +
                "}";

        given().relaxedHTTPSValidation()
                .body(logInJSONObject)
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().body("name", equalTo("John"));
    }

    // sending the request for deleting the user with sending email
    @Test (priority = 4, description = "Sending the request for deleting the user with regular email")
    public void deleteUser() {
        String jsonRequestWithEmail = "{\n" +
                "  \"email\": \"john.dean@boing.rs\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .when()
                .body(jsonRequestWithEmail)
                .delete(Endpoints.USER_DELETE)
                .then();
    }


}

