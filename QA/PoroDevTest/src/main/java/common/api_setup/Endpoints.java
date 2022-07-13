package common.api_setup;

public class Endpoints {
    public static String BASE_URI = "https://localhost:7101/";
    public static String BASE_PATH = "api/";

    public static String CREATE_USER = "User/CreateUser";
    public static String UPDATE_USER = "User/UpdateUser";
    public static String REGISTER_USER = "User/register/user";

    public static String REGISTER_ADMIN = "User/register/admin";
    public static String GET_USER_BY_EMAIL = "User/ReadUserByEmail";
    public static String EMAIL_PATH = "?email=";
    public static String USER_LOGIN = "User/LoginUser";
    public static String USER_DELETE = "User/DeleteUser";

    public static String GET_USER_BY_ID = "User/ReadUserById";
    public static String ID_PATH = "?Id=";
    public static String RUNTIME = "Runtime/ExecuteProject";

    //STORAGE
    public static String UPLOAD_FILE = "Storage/Upload";
    public static String DOWNLOAD_FILE = "Storage/Download";
    public static String READ_FILE = "Storage/Read";
    public static String DELETE_FILE = "Storage/Delete";
}
