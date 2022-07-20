package common.api_setup.api_common;

import common.api_setup.Endpoints;
import common.ui_setup.FileControlUtil;
import io.restassured.path.json.JsonPath;
import io.restassured.response.Response;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.testng.Assert;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

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
    public static void uploadThenDeleteFiles(int numOfFiles) throws ParseException {

        List<String> fileNames = new ArrayList<>();
        for (int i = 0; i <= numOfFiles; i++) {
            fileNames.add(uploadFile(
                    fileLogin.getValue("VALID_EMAIL"),
                    fileLogin.getValue("VALID_PASS_CREATED_USER"),
                    filePath.getValue("FILEPATH" + i)));

            fileIds.add(
                    getFileIdFromFileName(
                            fileLogin.getValue("VALID_EMAIL"),
                            fileLogin.getValue("VALID_PASS_CREATED_USER"),
                            fileNames.get(i)));

            boolean isUploaded = fileExists(
                    fileLogin.getValue("VALID_EMAIL"),
                    fileLogin.getValue("VALID_PASS_CREATED_USER"),
                    fileNames.get(i));

            Assert.assertTrue(isUploaded, "File is not uploaded.");
        }

        for (int i = 0; i <= numOfFiles; i++) {
            deleteFile(
                    fileLogin.getValue("VALID_EMAIL"),
                    fileLogin.getValue("VALID_PASS_CREATED_USER"),
                    fileIds.get(i));

            boolean fileExist = fileExists(
                    fileLogin.getValue("VALID_EMAIL"),
                    fileLogin.getValue("VALID_PASS_CREATED_USER"),
                    fileNames.get(i));

            Assert.assertFalse(fileExist, "File is not deleted, still exists in database.");
        }
    }

    public static String uploadFile(String email, String pass, String pathToFile) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(email, pass);

        Response response = given().relaxedHTTPSValidation()
                .header("Content-Type", "multipart/form-data")
                .header("Authorization", "Bearer " + token)
                .multiPart(new File(pathToFile))
                .when()
                .post(Endpoints.UPLOAD_FILE)
                .then()
                .statusCode(200).extract().response();

        return JsonPath.from(response.asString()).get("fileName");
    }

    public static void deleteFile(String email, String pass, String fileId) {
        String token = UserDetailsGenerator.takeTokenValueFromJson(email, pass);

        given().relaxedHTTPSValidation()
                .header("Authorization", "Bearer " + token)
                .when()
                .delete(Endpoints.DELETE_FILE + Endpoints.DELETE_FILE_PATH + fileId)
                .then();
    }

    public static String getFileIdFromFileName(String email, String pass, String fileName) throws ParseException {
        String token = UserDetailsGenerator.takeTokenValueFromJson(email, pass);

        Response response = given().relaxedHTTPSValidation().header("Authorization", "Bearer " + token).when().get(Endpoints.READ_FILE).then().extract().response();
        JSONParser jsonParser = new JSONParser();
        JSONObject object = (JSONObject) jsonParser.parse(response.asString());
        JSONArray jsonArray = (JSONArray) object.get("content");

        String currentId = "";

        for (Object obj : jsonArray) {
            if (!JsonPath.from(obj.toString()).get("fileName").equals("") && JsonPath.from(obj.toString()).get("fileName").equals(fileName)) {
                currentId = JsonPath.from(obj.toString()).get("fileId");
                break;
            }
        }
           return currentId;
    }

    public static boolean fileExists(String email, String pass, String fileName) throws ParseException {

        return !getFileIdFromFileName(email, pass, fileName).equals("");
    }
}
