package api_test.negative_tests;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import org.testng.annotations.Test;

import static org.hamcrest.CoreMatchers.anyOf;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;

public class GetUser extends ApiConfig {


    //Sending request for user with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class)
    public void getUserByInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL + Endpoints.EMAIL_PATH + invalidEmailList)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

    }
    // Valid but non-existing email
    @Test
    public void getUserByNonExistingEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL + Endpoints.EMAIL_PATH + "zivko@boing.rs")
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

    }

    @Test(dataProvider = "invalidIdAttributeList", dataProviderClass = DataProviderBeUtil.class)
    public void getUserByInvalidIdAttribute(String invalidIdAttributeList) {
            given().relaxedHTTPSValidation()
                    .when()
                    .get(Endpoints.GET_USER_BY_ID + Endpoints.ID_PATH + invalidIdAttributeList)
                    .then()
                    .statusCode(400);
    }

}
