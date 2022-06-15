package api_test;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import io.restassured.specification.RequestSpecification;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class DeleteUser extends ApiConfig {

    public static RequestSpecification api_requestSpec;

    // Sending the
    @Test(dataProvider = "invalidEmailList", dataProviderClass = UserDetailsGenerator.class)
    public void deleteUserByInvalidEmail(String invalidEmailList) {

        given().relaxedHTTPSValidation()
                .when()
                .delete(Endpoints.USER_DELETE + Endpoints.EMAIL_PATH + invalidEmailList)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));


    }
    // Sending delete request with valid but non-existing email
    @Test
    public void deleteUserByNonExistingEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .delete(Endpoints.USER_DELETE + Endpoints.EMAIL_PATH + "zivko@boing.rs")
                .then()
                .statusCode(404);
    }
}
