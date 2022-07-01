package common.api_setup.api_common;
import org.testng.annotations.DataProvider;

public class DataProviderBeUtil {

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
        return new Object[][]{
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

    @DataProvider(name = "PojoRegularEntry")
    public Object[][] functionalTestPojo() {
        return new Object[][]{
                {new User("John", "Do", "john.dean@boing.rs", "stringString1!",
                        0, 1, "string", "string")}
        };
    }
}
