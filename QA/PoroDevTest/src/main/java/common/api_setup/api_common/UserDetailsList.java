package common.api_setup.api_common;

import java.util.ArrayList;
import java.util.List;

public class UserDetailsList {

    public static List<String> userName = new ArrayList<>(List.of("James", "Robert", "John", "Michael",
            "David", "William", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew",
            "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George",
            "Timothy", "Ronald", "Edward", "Jason",
            "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen" ));
    public static List<String> userLastName = new ArrayList<>(List.of("Smith", "Johnson", "Williams",
            "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez",
            "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson",
            "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez",
            "Lewis", "Robinson", "Walker", "Young" ));
    public static List<String> userPosition = new ArrayList<>(List.of("Administration", "Operations",
            "Research and development", "Marketing and sales", "Human resources ", "Customer service",
            "Accounting and finance", "Engineering and Development", "Logistics"));
    public static List<String> passUpperCase = new ArrayList<>(List.of("Q", "F", "K", "E"));
    public static List<String> passLowerCase = new ArrayList<>(List.of("u", "k", "w", "g"));
    public static List<String> passSymbols = new ArrayList<>(List.of("!", "@", "#", "$", "^", "&", "*", "_"));

}

/*
Created lists that will be used in UserDetailsGenerator for randomly pulling one of the name, lastname, position
and generating email and password differently for each request
 */

