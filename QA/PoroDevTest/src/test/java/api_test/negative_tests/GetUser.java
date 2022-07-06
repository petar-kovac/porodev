package api_test.negative_tests;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.ui_setup.FileControlUtil;
import io.qameta.allure.Feature;
import org.testng.annotations.Test;

import java.io.IOException;

import static org.hamcrest.CoreMatchers.anyOf;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;

@Feature("Get user by Email/ID / Negative test cases")
public class GetUser extends ApiConfig {

    private final FileControlUtil file = new FileControlUtil(FileControlUtil.BE_REGISTER_PROPERTIES);

    public GetUser() throws IOException {
    }




    //Get user by invalid email (invalid email list from dataprovider)
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the request for get user details by email with invalid email attribute")
    public void getUserByInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL + Endpoints.EMAIL_PATH + invalidEmailList)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

    }
    // Get user by valid but non-existing email
    @Test(description = "Sending the request for get user details by email with non-existing email attribute")
    public void getUserByNonExistingEmail() {
        given().relaxedHTTPSValidation()
                .when()
                .get(Endpoints.GET_USER_BY_EMAIL + Endpoints.EMAIL_PATH
                        + file.getValue("NON_EXISTING_EMAIL"))
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

    }

    // Get user by invalid id attribute (invalid id attribute list from dataprovider)
    @Test(dataProvider = "invalidIdAttributeList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending request for getting user details with invalid id attribute")
    public void getUserByInvalidIdAttribute(String invalidIdAttributeList) {
            given().relaxedHTTPSValidation()
                    .when()
                    .get(Endpoints.GET_USER_BY_ID + Endpoints.ID_PATH + invalidIdAttributeList)
                    .then()
                    .statusCode(400);
    }

}
