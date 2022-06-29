package common.api_setup.api_common;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.jayway.jsonpath.JsonPath;
import common.api_setup.Endpoints;
import org.testng.annotations.DataProvider;

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




    @DataProvider(name = "invalidNameOrLastNameList")
    public Object[][] invalidNameList() {
        return new Object[][]{
                {""},
                {"iiiiiiiiiiiiiiiiiiiiii"},
                {"StringWith space"},
                {"String44"},
                {"Sting 33"},
                {"44"},
                {"$$$$"},
                {"String####"},
        };

    }


    @DataProvider(name = "invalidEmailList")
    public Object[][] invalidEmailList() {
        Object[][] email = new Object[][]{
                {""},
                {"noDomain"},
                {"name@live.com"},
                {"@boing.rs"},
                {"@boing.rs@boing.rs"},
                {"@boing.rs@live.com"},
                {"iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii" +
                "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii@boing.rs"},
                {"%%%@boing.rs"},
                {"with space@boing.rs"},
                {"boing.rs@gmail.com"}
        };

        return email;
    }

    @DataProvider(name = "invalidIdAttributeList")
    public Object[][] invalidUserIDlList() {
        Object[][] userID = new Object[][]{
                {""},
                {"strings"},
                {"1234567890222223"},
                {"etrfybab-099c-4869-a6ba-6fd9od397c2c"},
                {"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"}
        };
        return userID;
    }

    @DataProvider(name = "invalidPasswordList")
    public Object[][] invalidPasswordList() {
        Object[][] password = new Object[][]{
                {""},
                {"string"},
                {"Pass1234"},
                {"Passs!!!!"},
                {"P1@"},
                {"pass##@222"}
        };

        return password;
    }

    @DataProvider(name = "invalidJwtHeaderTokenList")
    public Object[][] invalidTokenHeaderList() {
        Object[][] invalidTokenHeaderList = new Object[][]{
                {""},
                {"1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111."},
                {"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA."},
                {"................................................................................................................."},
                {"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L"},
                {"eyjhbgcioijodhrwoi8vd3d3lnczlm9yzy8ymdaxlza0l3htbgrzawctbw9yzsnobwfjlxnoytuxmiisinr5cci6ikpxvcisimn0esi6ikpxvcj9."}
        };

       return invalidTokenHeaderList;

    }

    @DataProvider(name = "invalidJwtPayloadTokenList")
        Object[][] invalidJwtPayloadTokenList() {
        Object[][] invalidJwtPayloadTokenList = new Object[][]{
                {""},
                {"Payload"},
                {"111111111111111111111111111111111111111111111111111111111111111111111111111111111111."},
                {"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa."},
                {"....................................................................................."},
                {"eyjjzci6ijvjmzczote2ltrlmjgtndmwyi1ingfhltm2mjq3zwfhyzc2myisimv4cci6mty1nja2njcwoh0."},
                {"EYJJZCI6IJVJMZCZOTE2LTRLMJGTNDMWYI1INGFHLTM2MJQ3ZWFHYZC2MYISIMV4CCI6MTY1NJA2NJCWOH0."}

        };

        return invalidJwtPayloadTokenList;
    }

    @DataProvider(name = "invalidJwtSignatureTokenList")
        Object[][] invalidJwtSignatureTokenList() {
        Object[][] invalidJwtSignatureTokenList = new Object[][]{
                {""},
                {"Payload"},
                {"11111111111111111111111111111111111111111111111111111111111111111111111111111111111111"},
                {"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"},
                {"......................................................................................"},
                {"bosu__ip1me42a90h_gpnptvi77a6qiwifvjpnldygz0nx4jdjm-434y24oy51qxsgwvwyfaf3gda2dvox8veg"},
                {"BOSU__IP1ME42A90H_GPNPTVI77A6QIWIFVJPNLDYGZ0NX4JDJM-434Y24OY51QXSGWVWYFAF3GDA2DVOX8VEG"}

        };

        return invalidJwtSignatureTokenList;
    }

    public static String validJwtHeader = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9y" +
            "Zy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.";

    public static String incorectJwtPayload = "eyJJZCI6IjVjMzczOTE2LTRlMjgtNDMwYi1iNGFhLTM2MjQ3ZWFhYzc2MyIsImV4cCI6MTY1NjA4MzY4Nn0.";

    public static String incorectJwtSignature = "tAt7oxNmdTHSnr5hUr_mjdH8B8elavo6LDeiXwIhg1OqjaeXj4V4L8OsumMIGSb61gweyjsWl1UuobnnQMJ1Ng";


    @DataProvider(name = "invalidFileIdList")
    Object[][] invalidFileIdList() {
        Object[][] invalidFileIdList = new Object[][]{
                {""},
                {"111111111111111111111111111111111111"},
                {"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"},
                {"e06246ba-d280-48bd-ab60-1739cee98c74e06246ba-d280-48bd-ab60-1739cee98c74"}
        };

        return invalidFileIdList;
            }


    @DataProvider(name = "CombinationOfInvalidEntryList")
    public Object[][] invalidEntryCombinationList() {
        return new Object[][]{
                {new User("Jadranko", "Jankovic", "jadranko@boing.rs", // existing password
                "Pass##3", 2, 0, "string", "")},
                {new User("Jadranko", "Jankovic", "", "", // Empty email and password field
                        2, 0, "String", "")},
                {new User("", "", "newuser1234@boing.rs", // empty name and lastname
                        "Pass##3", 2, 0, "string", "")},
                {new User("Jadranko Jankovic", "Jankoivc Jadranko", // white space in all fields
                        "jadranko jankovic@boing.rs", "Pass 44#",
                        2, 0, "stri ng", "ss ff")},
                {new User("", "", "", "", // empty all string fields
                        2, 0, "", "")}
        };
    }

    @DataProvider(name = "PojoRegularEntry")
    public Object[][] functionalTestPojo() {
        return new Object[][]{
                {new User("John", "Do", "john.dean@boing.rs", "stringString1!",
                        0, 1, "string", "string")}
        };
    }




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

}
