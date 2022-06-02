package common.api_setup.api_common;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import java.util.*;


public class UserDetailsGenerator {

/*
Generating the JSON object with randomly created details
 */

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
        user.setRole(rn.nextInt(1));         // as it has to be 0 or 1
        user.setPosition(UserDetailsList.userPosition.get(rn.nextInt(UserDetailsList.userPosition.size())));
        user.setAvatarUrl("string");   // will have to be added later



        return user;
    }
/*
Method for transforming object to JSON
 */
    public static String convertToJson(Object object) {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();  // calling the gson
        return gson.toJson(object);  // returning JSON value
    }

}
