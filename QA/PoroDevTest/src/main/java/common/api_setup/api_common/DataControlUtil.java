package common.api_setup.api_common;

import common.api_setup.Endpoints;
import common.ui_setup.FileControlUtil;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

import static io.restassured.RestAssured.given;

public class DataControlUtil {
    private static final FileControlUtil fileControl;
    private static final FileControlUtil filePath;
    private static final ArrayList<String> fileIds = new ArrayList<>();

    static {
        try {
            fileControl = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
            filePath = new FileControlUtil(FileControlUtil.FILE_PATH_PROPERTIES);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }


    public static void uploadThenDeleteFiles(int numOfFiles) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(fileControl.getValue("VALID_EMAIL"), fileControl.getValue( "VALID_PASS_CREATED_USER"));

        for (int i = 0; i <= numOfFiles; i++) {
            given().relaxedHTTPSValidation()
                    .header("Content-Type", "multipart/form-data")
                    .header("Authorization","Bearer " + token)
                    .multiPart(new File(filePath.getValue("FILEPATH" + i)))
                    .when()
                    .post(Endpoints.UPLOAD_FILE)
                    .then().statusCode(200);

            fileIds.add(UserDetailsGenerator.takeValueFromResponseWithToken(Endpoints.READ_FILE, token,i,"fileId"));
        }
        for (int i = 0; i <= numOfFiles; i++) {
            given().relaxedHTTPSValidation()
                    .header("Authorization","Bearer " + token)
                    .when()
                    .delete(Endpoints.DELETE_FILE + Endpoints.DELETE_FILE_PATH + fileIds.get(i))
                    .then().statusCode(200);
            //.then().statusCode(200);
        }
    }
}
