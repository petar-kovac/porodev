package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import io.qameta.allure.Feature;
import io.restassured.specification.RequestSpecification;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

@Feature("Delete User/ Negative test cases")
public class DeleteUser extends ApiConfig {

    public static RequestSpecification api_requestSpec;

    // Sending the
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the delete user request with invalid email attribute")
    public void deleteUserByInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .when()
                .body(UserDetailsGenerator.createEmailJsonReq(invalidEmailList))
                .delete(Endpoints.USER_DELETE)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));


    }
    // Sending delete request with valid but non-existing email
    @Test(description = "Sending the delete request for non-existing user")
    public void deleteUserByNonExistingEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .body(UserDetailsGenerator.createEmailJsonReq("zivko@boing.rs"))
                .delete(Endpoints.USER_DELETE)
                .then()
                .statusCode(anyOf(is(404), is(400)));
    }
}