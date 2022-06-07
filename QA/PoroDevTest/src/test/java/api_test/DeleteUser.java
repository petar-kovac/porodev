package api_test;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class DeleteUser extends ApiConfig {

    // Sending the
    @Test(dataProvider = "email", dataProviderClass = UserDetailsGenerator.class)
    public void deleteUserByInvalidEmail(String val) {

        given().relaxedHTTPSValidation()
                .when()
                .delete(Endpoints.USER_DELETE+val)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));


    }
    // Sending delete request with valid but non-existing email
    @Test
    public void deleteUserByNonExistingEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .delete(Endpoints.USER_DELETE+"zivko@boing.rs")
                .then()
                .statusCode(404);
    }
}
