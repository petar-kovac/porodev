package api_test.negative_tests;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.User;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import io.qameta.allure.Feature;
import org.testng.annotations.Test;

import java.io.IOException;

import static io.restassured.RestAssured.*;


@Feature("Register user/Negative test cases")
public class RegisterUser extends ApiConfig {

    private final FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);

    public RegisterUser() throws IOException {
    }



    // Register user with invalid entry in the name field
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class,
    description = "Register user request with invalid entry in the name attribute")
    public void registerInvalidName(String invalidNameList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterJsonReq(
                        invalidNameList,
                        file.getValue("VALID_LASTNAME"),
                        file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS"))
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }


    //Register user with invalid entry in the name field
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class,
    description = "Register user request with invalid entry in the lastname attribute")
    public void registerInvalidLastname(String InvalidLastNameList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterJsonReq(
                        file.getValue("VALID_FIRSTNAME"),
                        InvalidLastNameList,
                        file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS"))
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }


    // Register user with invalid entry in email field
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class,
    description = "Register user request with invalid entry in email attribute")
    public void registerInvalidEmail(String invalidEmailList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterJsonReq(
                        file.getValue("VALID_FIRSTNAME"),
                        file.getValue("VALID_LASTNAME"),
                        invalidEmailList,
                        file.getValue("VALID_PASS"))
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }
    //Register user with invalid entry in the password field
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = DataProviderBeUtil.class,
    description = "Register user request with invalid entry in the password attribute")
    public void registerInvalidPass(String invalidPasswordList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterJsonReq(
                        file.getValue("VALID_FIRSTNAME"),
                        file.getValue("VALID_LASTNAME"),
                        file.getValue("VALID_EMAIL"),
                        invalidPasswordList)
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }

    @Test(dataProvider = "CombinationOfInvalidEntryList", dataProviderClass = DataProviderBeUtil.class,
    description = "Register user request with the combination of invalid attributes")
    public void registerPojoUser(User invalidEntry){
        given().relaxedHTTPSValidation()
                .body(invalidEntry)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }







}

