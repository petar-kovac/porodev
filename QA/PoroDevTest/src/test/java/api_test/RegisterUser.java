package api_test;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.User;
import common.api_setup.api_common.UserDetailsGenerator;
import common.api_setup.api_common.UserSerialization;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import static io.restassured.RestAssured.*;


public class RegisterUser extends ApiConfig {



    // Register user with invalid entry in the name field
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidName(String invalidNameList){
    String userBodyJson = "{\n" +
            "  \"name\": \""+invalidNameList+"\",\n" +
            "  \"lastname\": \"Dean\", \n" +
            "  \"email\": \"john.dean@boing.rs\",\n" +
            "  \"password\": \"stringString1!\",\n" +
            "  \"department\": 0,\n" +
            "  \"role\": 1,\n" +
            "  \"position\": \"string\",\n" +
            "  \"avatarUrl\": \"string\"\n" +
            "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }
    //Register user with invalid entry in the name field
    @Test(dataProvider = "invalidNameOrLastNameList", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidLastname(String InvalidLastNameList){
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \""+InvalidLastNameList+"\",\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"password\": \"stringString1!\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }
    // Register user with invalid entry in email field
    @Test(dataProvider = "invalidEmailList", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidEmail(String invalidEmailList){
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \""+invalidEmailList+"\",\n" +
                "  \"password\": \"stringString1!\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }
    //Register user with invalid entry in the password field
    @Test(dataProvider = "invalidPasswordList", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidPass(String invalidPasswordList){
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"password\": \""+invalidPasswordList+"\",\n" +
                "  \"department\": 0,\n" +
                "  \"role\": 1,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .body(userBodyJson)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }

    @Test(dataProvider = "CombinationOfInvalidEntryList", dataProviderClass = UserDetailsGenerator.class)
    public void registerPojoUser(User providedUser){

        given().relaxedHTTPSValidation()
                .body(providedUser)
                .when()
                .post(Endpoints.REGISTER_USER)
                .then().statusCode(400);
    }







}

