package api_test;


import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import static io.restassured.RestAssured.*;


public class RegisterUser extends ApiConfig {



    // Register user with invalid entry in the name field
    @Test(dataProvider = "name", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidName(String val) {
        String userBodyJson = "{\n" +
                "  \"name\": \""+val+"\",\n" +
                "  \"lastname\": \"Dean\",\n" +
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
                .then().statusCode(500);
    }
    //Register user with invalid entry in the name field
    @Test(dataProvider = "name", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidLastname(String val){
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \""+val+"\",\n" +
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
                .then().statusCode(500);
    }
    // Register user with invalid entry in email field
    @Test(dataProvider = "email", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidEmail(String val){
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \""+val+"\",\n" +
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
    @Test(dataProvider = "password", dataProviderClass = UserDetailsGenerator.class)
    public void registerInvalidPass(String val){
        String userBodyJson = "{\n" +
                "  \"name\": \"John\",\n" +
                "  \"lastname\": \"Dean\",\n" +
                "  \"email\": \"john.dean@boing.rs\",\n" +
                "  \"password\": \""+val+"\",\n" +
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







}

