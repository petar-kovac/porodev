package common.ui_setup;

import java.io.IOException;
import java.util.Properties;

public class FileControlUtil {
    public static String LOGIN_DATA_PROPERTIES = "login_data.properties";
    public static String REGISTRATION_DATA_PROPERTIES = "registration_data.properties";
    public static String END_TO_END_PROPERTIES = "end_to_end.properties";

    public static String RUNTIME_DATA_PROPERTIES = "be_runtime_data.properties";

    public static String BE_REGISTER_PROPERTIES = "be_registration.properties";

    public static String FE_UPLOAD_PROPERTIES = "fe_upload.properties";
    public Properties properties;

    public FileControlUtil(String file) throws IOException {
        properties = new Properties();
        properties.load(getClass().getResourceAsStream("/" + file));
    }

    public String getValue(String propertyKey) {
        return properties.getProperty(propertyKey);
    }
}
