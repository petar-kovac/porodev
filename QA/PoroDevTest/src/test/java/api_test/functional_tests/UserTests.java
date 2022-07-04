package api_test.functional_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.User;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import io.qameta.allure.Feature;
import org.testng.annotations.Test;

import java.io.IOException;

import static org.hamcrest.Matchers.equalTo;
import static io.restassured.RestAssured.given;


@Feature("CRUD operations / Functional test cases")
public class UserTests extends ApiConfig {

    private final FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);

    public UserTests() throws IOException {
    }


    //Register user
    @Test(priority = 0, dataProvider = "PojoRegularEntry", dataProviderClass = DataProviderBeUtil.class,
            description = "Sending the request for user register with regular input")
    public void registerUserByJSON(User providedUser) {
        given().relaxedHTTPSValidation()
                .body(providedUser)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(200);
    }


    // Update user (change any of the details)

    @Test (priority = 1, description = "Sending the request for update the users information with regular input")
    public void updateUserByJson() {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createUpdateJsonReq(
                        "John",
                        "Doe",
                        file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS")))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(200);
    }

    // Get the user details with sending the email in request

    @Test (priority = 2, description = "Sending the request for getting the user information with " +
            "the regular email")
    public void getUserByEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL+ Endpoints.EMAIL_PATH + file.getValue("VALID_EMAIL"))
                .then().body("name", equalTo("John"));

    }

    // Fetching the id attribute with method takeValueFromJsonResp and sending the request for getting the user with id
    @Test(priority = 3, description = "Sending the request for getting the user with that id value")
    public void getUserById() {
        String userIdFromJson = UserDetailsGenerator.takeValueFromJsonResp
                (Endpoints.GET_USER_BY_EMAIL+ Endpoints.EMAIL_PATH
                        + file.getValue("VALID_EMAIL"), "id");
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_ID + Endpoints.ID_PATH + userIdFromJson)
                .then().body("name", equalTo("John"));
    }

    //Sending the log in request
    @Test (priority = 4, description = "Sending the request for log in with regular email and password entry")
    public void logInUser() {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createLogInJsonReq(file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS")))
                .when()
                .post(Endpoints.USER_LOGIN)
                .then().body("name", equalTo("John"));
    }

    // Taking the jwt token value with takeTokenValueFromJson method and sending the runtime request
    @Test(priority = 5, description = "Sending the request for runtime with jwt token in header")
    public void runtime() {
        String token = UserDetailsGenerator
                .takeTokenValueFromJson(file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS"));
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer "+token)
                .when()
                .body(UserDetailsGenerator.createRuntimeJsonReq(
                        "e06246ba-d280-48bd-ab60-1739cee98c74",
                        "1",
                        "2"))
                .post(Endpoints.RUNTIME)
                .then().statusCode(200);
    }



    // Sending the request for deleting the user with sending email
    @Test (priority = 6, description = "Sending the request for deleting the user with regular email")
    public void deleteUser() {
        given().relaxedHTTPSValidation()
                .when()
                .body(UserDetailsGenerator.createEmailJsonReq(file.getValue("VALID_EMAIL")))
                .delete(Endpoints.USER_DELETE)
                .then().statusCode(200);
    }

}

