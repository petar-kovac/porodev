package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;

public class Runtime extends ApiConfig {


    @Test(dataProvider = "invalidJwtHeaderTokenList", dataProviderClass = UserDetailsGenerator.class)
    public void runtimeWithInvalidJwtHeaderTokenList(String invalidJwtHeaderTokenList) {
        String jsonRequestWithFileId = "{\n" +
                "  \"fileId\": \"e06246ba-d280-48bd-ab60-1739cee98c74\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer " + invalidJwtHeaderTokenList
                        + UserDetailsGenerator.incorectJwtPayload
                        + UserDetailsGenerator.incorectJwtSignature)
                .when()
                .body(jsonRequestWithFileId)
                .post(Endpoints.RUNTIME)
                .then().statusCode(500);
    }


    @Test(dataProvider = "invalidJwtPayloadTokenList", dataProviderClass = UserDetailsGenerator.class)
    public void runtimeWithInvalidJwtPayloadTokenList(String invalidJwtPayloadTokenList) {
        String jsonRequestWithFileId = "{\n" +
                "  \"fileId\": \"e06246ba-d280-48bd-ab60-1739cee98c74\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer"
                        + UserDetailsGenerator.validJwtHeader
                        + invalidJwtPayloadTokenList
                        + UserDetailsGenerator.incorectJwtSignature)
                .when()
                .body(jsonRequestWithFileId)
                .post(Endpoints.RUNTIME)
                .then().statusCode(500);

    }




    @Test(dataProvider = "invalidJwtSignatureTokenList", dataProviderClass = UserDetailsGenerator.class)
    public void runtimeWithInvalidJwtSignatureTokenList(String invalidJwtSignatureTokenList) {
        String jsonRequestWithFileId = "{\n" +
                "  \"fileId\": \"e06246ba-d280-48bd-ab60-1739cee98c74\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer"
                        + UserDetailsGenerator.validJwtHeader
                        + UserDetailsGenerator.incorectJwtPayload
                        + invalidJwtSignatureTokenList)
                .when()
                .body(jsonRequestWithFileId)
                .post(Endpoints.RUNTIME)
                .then().statusCode(500);
    }


}




