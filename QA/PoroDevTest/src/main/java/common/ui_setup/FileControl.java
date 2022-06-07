package common.ui_setup;

import java.io.IOException;
import java.util.Properties;

public class FileControl {
    public static String LOGIN_DATA_PROPERTIES = "login_data.properties";
    public static String REGISTRATION_DATA_PROPERTIES = "registration_data.properties";
    public Properties properties;

    public FileControl(String file) throws IOException {
        properties = new Properties();
        properties.load(getClass().getResourceAsStream("/" + file));
    }

    public String getValue(String propertyKey) {
        return properties.getProperty(propertyKey);
    }
}
