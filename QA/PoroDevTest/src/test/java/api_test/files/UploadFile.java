package api_test.files;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import io.restassured.RestAssured;
import io.restassured.response.Response;
import org.testng.annotations.Test;

import java.io.File;

public class UploadFile extends ApiConfig {

    @Test
    public void uploadFile() {
        File uploadedFile = new File("C:\\Users\\User\\IdeaProjects\\porodev\\QA\\PoroDevTest\\pom.xml");

        Response response = RestAssured
                .given().relaxedHTTPSValidation()
                .header("Content-type", "multipart/form-data")
                .multiPart("uploadFile", uploadedFile, "multipart/form-data")
                .post(Endpoints.UPLOAD_FILE)
                .thenReturn();

        System.out.println(response.prettyPrint());
    }
}
