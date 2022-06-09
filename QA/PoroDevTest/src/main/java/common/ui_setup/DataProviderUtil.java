package common.ui_setup;

import org.testng.annotations.DataProvider;

public class DataProviderUtil {

    @DataProvider(name = "passwordForm")
    public static Object[][] invalidPasswordForms() {
        Object[][] passwordForm = new Object[][]{
                {"markoilic1!"},
                {"MARKOILIC1!"},
                {"Markoilic!"},
                {"Markoilic1"},
                {"Mark1!"},
                {"Marko ilic 1 !"}
            };
        return passwordForm;
    }

    @DataProvider(name = "emailForm")
    public static Object[][] invalidEmailForms() {
        Object[][] passwordForm = new Object[][]{
                {"marko@gmail.com"},
                {"@marko@boing.rs"},
                {"markoboing.rs"},
                {"MarkoMarko@boingrs"}
            };
        return passwordForm;

    }
}
