package common.api_setup.api_common;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import common.api_setup.Endpoints;
import com.jayway.jsonpath.JsonPath;
import java.util.*;

import static io.restassured.RestAssured.given;


public class UserDetailsGenerator {




    public static User generateRandomUserDetails() {
        Random rn = new Random();
        User user = new User();
        user.setName(UserDetailsList.userName.get(rn.nextInt(UserDetailsList.userName.size())));
        user.setLastname(UserDetailsList.userLastName.get(rn.nextInt(UserDetailsList.userLastName.size())));
        user.setEmail("user" + rn.nextInt(100000) + 1 + "@boing.rs");
        user.setPassword("user" + UserDetailsList.passUpperCase.get(rn.nextInt(UserDetailsList.passUpperCase.size())) +
                UserDetailsList.passLowerCase.get(rn.nextInt(UserDetailsList.passLowerCase.size())) +
                UserDetailsList.passSymbols.get(rn.nextInt(UserDetailsList.passSymbols.size()))+
                rn.nextInt(10000) + 1);
        user.setDepartment(0);                                // will have to be added later
        user.setRole(rn.nextInt(1));
        user.setPosition(UserDetailsList.userPosition.get(rn.nextInt(UserDetailsList.userPosition.size())));
        user.setAvatarUrl("string");   // will have to be added later



        return user;
    }


    // Converting the pojo object to json payload
    public static String convertToJson(Object object) {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();  // calling the gson
        return gson.toJson(object);  // returning JSON value
    }

    // Taking the value from json based on defined endpoint and attribute
    public static String takeValueFromJsonResp(String endpoint, String attribute){
        String json = given().relaxedHTTPSValidation().when().get(endpoint).asString();
        return JsonPath.read(json, "$."+attribute).toString();
    }

    public static String takeValueFromResponseWithToken(String endpoint, String token, int number, String attribute) {
        String json = given().relaxedHTTPSValidation().header("Authorization", "Bearer " + token).when().get(endpoint).asString();
        return JsonPath.read(json, "$.content.[" + number + "]" + attribute).toString();
    }

    // Taking the jwt token attribute based on users email and password
    public static String takeTokenValueFromJson(String email, String password){
        String loginJsonObject = "{\n" +
                "  \"email\": \""+email+"\",\n" +
                "  \"password\": \""+password+"\"\n" +
                "}";
        String json = given().relaxedHTTPSValidation().
                body(loginJsonObject)
                .when()
                .post(Endpoints.USER_LOGIN).asString();

        return JsonPath.read(json, "$.jwt").toString();
    }

    // Generating JSON body for runtime with two numbers
    public static String createRuntimeJsonReq(String fileID, String firstNmb, String secondNmb) {
        return "{\n" +
                "  \"projectId\": \" "+fileID+"\",\n" +
                "  \"arguments\": [\n" +
                "    \"" + firstNmb + "\",\n" +
                "    \"" + secondNmb + "\"\n" +
                "  ]\n" +
                "}";
    }

    // Generating JSON req for Register or Update
    public static String createRegisterJsonReq(
            String name,
            String lastname,
            String email,
            String password) {
        return "{\n" +
                "  \"name\": \""+name+"\",\n" +
                "  \"lastname\": \""+lastname+"\",\n" +
                "  \"email\": \""+email+"\",\n" +
                "  \"password\": \""+password+"\",\n" +
                "  \"department\": 0,\n" +
                "  \"position\": \"string\",\n" +
                "  \"avatarUrl\": \"string\"\n" +
                "}";
    }

    public static String createUpdateJsonReq(
            String name,
            String lastname,
            String email,
            String password) {
        return "{\n" +
                "  \"avatarUrl\": \"string\",\n" +
                "  \"department\": 0,\n" +
                "  \"email\": \""+email+"\",\n" +
                "  \"lastname\": \""+lastname+"\",\n" +
                "  \"name\": \""+name+"\",\n" +
                "  \"passwordUnhashed\": \""+password+"\",\n" +
                "  \"position\": \"string\",\n" +
                "  \"role\": 0\n" +
                "}";
    }


    // Generating JSON req for LogIn operation

    public static String createLogInJsonReq (String email, String password) {
        return "{\n" +
                "  \"email\": \"" +email + "\",\n" +
                "  \"password\": \""+password+"\"\n" +
                "}";
    }

    public static String createEmailJsonReq (String email) {
        return "{\n" +
                "  \"email\": \""+email+"\"\n" +
                "}";
    }
}
