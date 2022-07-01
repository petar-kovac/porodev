package api_test.negative_tests;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.User;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;
import static io.restassured.RestAssured.*;


public class RegisterUser extends ApiConfig {



    // Register user with invalid entry in the name field
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class)
    public void registerInvalidName(String invalidNameList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        invalidNameList,
                        "Dean",
                        "john.dean@boing.rs",
                        "stringString1!")
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }


    //Register user with invalid entry in the name field
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = DataProviderBeUtil.class)
    public void registerInvalidLastname(String InvalidLastNameList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        "James",
                        InvalidLastNameList,
                        "john.dean@boing.rs",
                        "stringString1!")
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }


    // Register user with invalid entry in email field
    @Test(dataProvider = "invalidEmailList", dataProviderClass = DataProviderBeUtil.class)
    public void registerInvalidEmail(String invalidEmailList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        "James",
                        "Dean",
                        invalidEmailList,
                        "stringString1!")
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }
    //Register user with invalid entry in the password field
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = DataProviderBeUtil.class)
    public void registerInvalidPass(String invalidPasswordList){
        given().relaxedHTTPSValidation()
                .body(UserDetailsGenerator.createRegisterOrUpdateJsonReq(
                        "James",
                        "Dean",
                        "james.dean@boing.rs",
                        invalidPasswordList)
                )
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }

    @Test(dataProvider = "CombinationOfInvalidEntryList", dataProviderClass = DataProviderBeUtil.class)
    public void registerPojoUser(User invalidEntry){
        given().relaxedHTTPSValidation()
                .body(invalidEntry)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }







}

