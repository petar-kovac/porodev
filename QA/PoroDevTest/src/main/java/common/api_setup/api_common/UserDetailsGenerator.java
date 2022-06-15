package common.api_setup.api_common;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import org.testng.annotations.DataProvider;

import java.util.*;


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
                {"$$"}
        };

    }


    @DataProvider(name = "invalidEmailList")
    public Object[][] invalidEmailList() {
        Object[][] email = new Object[][]{
                {""},
                {"name@live.com"},
                {"@boing.rs"},
                {"@boing.rs@boing.rs"},
                {"@boing.rs@live.com"},
                {"iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii" +
                "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii@boing.rs"},
                {"%%%@boing.rs"},
                {"with space@boing.rs"}
        };

        return email;
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



    public static String convertToJson(Object object) {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();  // calling the gson
        return gson.toJson(object);  // returning JSON value
    }

}
