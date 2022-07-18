package common.api_setup.api_common;

import com.jayway.jsonpath.JsonPath;
import common.api_setup.Endpoints;
import common.ui_setup.FileControlUtil;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import static io.restassured.RestAssured.given;

public class DataControlUtil {
    private static final FileControlUtil fileLogin;
    private static final FileControlUtil filePath;
    private static final ArrayList<String> fileIds = new ArrayList<>();

    static {
        try {
            fileLogin = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
            filePath = new FileControlUtil(FileControlUtil.FILE_PATH_PROPERTIES);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    //This method upload number of files that are located in "filePath.properties" file, located in TestResources
    public static void uploadThenDeleteFiles(int numOfFiles) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(fileLogin.getValue("VALID_EMAIL"), fileLogin.getValue( "VALID_PASS_CREATED_USER"));

        for (int i = 0; i <= numOfFiles; i++) {
            given().relaxedHTTPSValidation()
                    .header("Content-Type", "multipart/form-data")
                    .header("Authorization","Bearer " + token)
                    .multiPart(new File(filePath.getValue("FILEPATH" + i)))
            .when()
                    .post(Endpoints.UPLOAD_FILE)
            .then()
                    .statusCode(200);

            fileIds.add(UserDetailsGenerator.takeValueFromResponseWithToken(Endpoints.READ_FILE, token,i,"fileId"));
        }
        for (int i = 0; i <= numOfFiles; i++) {
            given().relaxedHTTPSValidation()
                    .header("Authorization","Bearer " + token)
            .when()
                    .delete(Endpoints.DELETE_FILE + Endpoints.DELETE_FILE_PATH + fileIds.get(i))
            .then()
                    .statusCode(200);
        }
    }

    public static void uploadFile(String email, String pass, String pathToFile) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(email, pass);

        given().relaxedHTTPSValidation()
                .header("Content-Type", "multipart/form-data")
                .header("Authorization", "Bearer " + token)
                .multiPart(new File(pathToFile))
        .when()
                .post(Endpoints.UPLOAD_FILE)
        .then()
                .statusCode(200);
    }

    public static void deleteFile(String email, String pass, String fileId) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(email, pass);

        given().relaxedHTTPSValidation()
                .header("Authorization","Bearer " + token)
        .when()
                .delete(Endpoints.DELETE_FILE + Endpoints.DELETE_FILE_PATH + fileId)
        .then()
                .statusCode(200);
    }

    public static String getFileIdFromFileName(String email, String pass, int fileIndexPosition) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(email, pass);

        String json = given().relaxedHTTPSValidation().header("Authorization", "Bearer " + token).when().get(Endpoints.READ_FILE).asString();

        return JsonPath.read(json, "$.content.[" + fileIndexPosition + "]" + "fileId").toString();
    }
}
