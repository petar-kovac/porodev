package api_test.files;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import io.restassured.RestAssured;
import io.restassured.config.HeaderConfig;
import io.restassured.config.RestAssuredConfig;
import io.restassured.response.Response;
import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;

public class UploadFile extends ApiConfig {
    private final FileControlUtil fileControl = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);

    public UploadFile() throws IOException {
    }

    @Test
    public void uploadFile() {
        File uploadedFile = new File("C:\\Users\\User\\IdeaProjects\\porodev\\QA\\PoroDevTest\\pom.xml");
        String token = UserDetailsGenerator.takeTokenValueFromJson(fileControl.getValue("VALID_EMAIL"), fileControl.getValue( "VALID_PASS_CREATED_USER"));

        Response response = RestAssured
                .given().relaxedHTTPSValidation()
                //.config(RestAssuredConfig.config().headerConfig(HeaderConfig.headerConfig().overwriteHeadersWithName("Content-Type")))
                .header("Content-Type", "multipart/form-data")
                .header("Authorization","Bearer " + token)
                .multiPart(uploadedFile)
                .when()
                .post(Endpoints.UPLOAD_FILE);

        String fileId = UserDetailsGenerator.takeValueFromResponseWithToken(Endpoints.READ_FILE, token,1,"fileId");
        System.out.println("This is file id: " + fileId);
    }
}