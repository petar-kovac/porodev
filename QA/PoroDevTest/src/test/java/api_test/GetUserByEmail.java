package api_test;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;
import static org.hamcrest.CoreMatchers.anyOf;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;

public class GetUserByEmail extends ApiConfig {


    //Sending request for user with invalid email
    @Test(dataProvider = "email", dataProviderClass = UserDetailsGenerator.class)
    public void getUserByInvalidEmail(String val) {

        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL+val)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

    }
    // Valid but non-existing email
    @Test
    public void getUserByNonExistingEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL+"zivko@boing.rs")
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

    }

}
