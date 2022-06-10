package common.ui_setup;

import org.testng.annotations.DataProvider;

public class DataProviderUtil {

    @DataProvider(name = "passwordForm")
    public static Object[][] invalidPasswordForms() {
        return new Object[][]{
                {"markoilic1!"},
                {"MARKOILIC1!"},
                {"Markoilic!"},
                {"Markoilic1"},
                {"Mark1!"},
            };
    }

    @DataProvider(name = "passwords_with_whitespace")
    public static Object[][] invalidPasswordForm_whitespace() {
        return new Object[][]{
                {"marko ilic1!"},
                {"MARKO ILIC1!"},
                {"Marko ilic !"},
                {"Marko ilic1"},
                {"Mark1 !"},
        };
    }

    @DataProvider(name = "emailForm")
    public static Object[][] invalidEmailForms() {
        return new Object[][]{
                {"marko@gmail.com"},
                {"@marko@boing.rs"},
                {"markoboing.rs"},
                {"MarkoMarko@boingrs"}
            };

    }
}
