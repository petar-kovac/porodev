package api_test.negative_tests;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import io.qameta.allure.Feature;
import org.testng.annotations.Test;

import java.io.IOException;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.anyOf;
import static org.hamcrest.CoreMatchers.is;


@Feature("Update user /Negative test cases")
public class UpdateUser extends ApiConfig {

    private final FileControlUtil file = new FileControlUtil(FileControlUtil.BE_REGISTER_PROPERTIES);

    public UpdateUser() throws IOException {
    }

    //Updating the user with invalid name

    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class,
    description = "Update user request with invalid entry for the name attribute")
    public void updateInvalidName(String InvalidNameList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createUpdateJsonReq(
                        InvalidNameList,
                        file.getValue("VALID_FIRSTNAME"),
                        file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS")

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }


    //Updating user with invalid last name
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class,
    description = "Update user request with invalid entry for the lastname attribute")
    public void updateInvalidLastname(String invalidLastNameList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createUpdateJsonReq(
                        file.getValue("VALID_FIRSTNAME"),
                        invalidLastNameList,
                        file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS")

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }


    // Updating user with invalid email
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class,
    description = "Update user request with invalid entry for the email attribute")
    public void updateInvalidEmail(String invalidEmailList) {
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createUpdateJsonReq(
                        file.getValue("VALID_FIRSTNAME"),
                        file.getValue("VALID_LASTNAME"),
                        invalidEmailList,
                        file.getValue("VALID_PASS")

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(anyOf(is(400), is(404)));
    }


    // Updating user with invalid password
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = DataProviderBeUtil.class,
    description = "Update user request with invalid entry for the password attribute")
    public void updateInvalidPass(String invalidPasswordList) {

        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createUpdateJsonReq(
                        file.getValue("VALID_FIRSTNAME"),
                        file.getValue("VALID_LASTNAME"),
                        file.getValue("VALID_EMAIL"),
                        invalidPasswordList

                ))
                .when()
                .put(Endpoints.UPDATE_USER)
                .then().statusCode(400);
    }
}


