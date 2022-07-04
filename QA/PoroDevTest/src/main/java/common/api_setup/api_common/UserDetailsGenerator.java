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

    // Strings as a valid parts of jwt token

    public static String validJwtHeader = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9y" +
            "Zy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.";

    public static String incorectJwtPayload = "eyJJZCI6IjVjMzczOTE2LTRlMjgtNDMwYi1iNGFhLTM2MjQ3ZWFhYzc2MyIsImV4cCI6MTY1NjA4MzY4Nn0.";

    public static String incorectJwtSignature = "tAt7oxNmdTHSnr5hUr_mjdH8B8elavo6LDeiXwIhg1OqjaeXj4V4L8OsumMIGSb61gweyjsWl1UuobnnQMJ1Ng";




    // Converting the pojo object to json payload
    public static String convertToJson(Object object) {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();  // calling the gson
        return gson.toJson(object);  // returning JSON value
    }

    // Taking the value from json based on defined endpoint and attribute
    public static String takeValueFromJsonResp(String endpoint, String attribute){
        String json = given().relaxedHTTPSValidation().when().get(endpoint).asString();
        String attributeValue = JsonPath.read(json, "$."+attribute).toString();
        return attributeValue;
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
        String fetchedToken = JsonPath.read(json, "$.jwt").toString();

        return fetchedToken;
    }

    // Generating JSON body for runtime with two numbers
    public static String createRuntimeJsonReq(String fileID, String firstNmb, String secondNmb) {
        String generatedRuntimeJsonReq = "{\n" +
                "  \"projectId\": \""+fileID+"\",\n" +
                "  \"arguments\": [\n" +
                "    \"" + firstNmb + "\",\n" +
                "    \"" + secondNmb + "\"\n" +
                "  ]\n" +
                "}";
        return generatedRuntimeJsonReq;

    }

    // Generating JSON req for Register or Update
    public static String createRegisterJsonReq(
            String name,
            String lastname,
            String email,
            String password) {
         String generatedRegisterJsonReq = "{\n" +
                 "  \"name\": \""+name+"\",\n" +
                 "  \"lastname\": \""+lastname+"\",\n" +
                 "  \"email\": \""+email+"\",\n" +
                 "  \"password\": \""+password+"\",\n" +
                 "  \"department\": 0,\n" +
                 "  \"position\": \"string\",\n" +
                 "  \"avatarUrl\": \"string\"\n" +
                 "}";
         return generatedRegisterJsonReq;
    }

    public static String createUpdateJsonReq(
            String name,
            String lastname,
            String email,
            String password) {
        String generatedUpgradeJsonReq = "{\n" +
                "  \"avatarUrl\": \"string\",\n" +
                "  \"department\": 0,\n" +
                "  \"email\": \""+email+"\",\n" +
                "  \"lastname\": \""+lastname+"\",\n" +
                "  \"name\": \""+name+"\",\n" +
                "  \"passwordUnhashed\": \""+password+"\",\n" +
                "  \"position\": \"string\",\n" +
                "  \"role\": 0\n" +
                "}";
        return generatedUpgradeJsonReq;
    }


    // Generating JSON req for LogIn operation

    public static String createLogInJsonReq (
            String email,
            String password
    ) {
        String generatedLogInJsonReq = "{\n" +
                "  \"email\": \"" +email + "\",\n" +
                "  \"password\": \""+password+"\"\n" +
                "}";
        return generatedLogInJsonReq;
    }

    public static String createEmailJsonReq (String email) {
        String emailBody = "{\n" +
                "  \"email\": \""+email+"\"\n" +
                "}";
        return emailBody;
    }



}
