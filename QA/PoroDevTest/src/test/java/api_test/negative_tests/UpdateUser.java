package api_test.negative_tests;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;

public class UpdateUser extends ApiConfig {

    //Updating the user with invalid name

    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class)
    public void updateInvalidName(String InvalidNameList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        InvalidNameList,
                "dean",
                        "james.dean@boing.rs",
                        "StringString1!"

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }


    //Updating user with invalid last name
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class)
    public void updateInvalidLastname(String invalidLastNameList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        "James",
                        invalidLastNameList,
                        "james.dean@boing.rs",
                        "StringString1!"

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }


    // Updating user with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class)
    public void updateInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        "James",
                        "Dean",
                        invalidEmailList,
                        "StringString1!"

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(anyOf(is(400), is(404)));
    }


    // Updating user with invalid password
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = DataProviderBeUtil.class)
    public void updateInvalidPass(String invalidPasswordList) {

        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        "James",
                        "Dean",
                        "james.dean@boing.rs",
                        invalidPasswordList

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }
}


