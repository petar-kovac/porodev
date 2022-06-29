package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.UserDetailsGenerator;
import org.hamcrest.core.AnyOf;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.*;

public class Runtime extends ApiConfig {





    @Test(dataProvider = "invalidJwtHeaderTokenList", dataProviderClass = UserDetailsGenerator.class)
    public void runtimeWithInvalidJwtHeaderTokenList(String invalidJwtHeaderTokenList) {
        String jsonRequestWithFileId = "{\n" +
                "  \"fileId\": \"e06246ba-d280-48bd-ab60-1739cee98c74\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .header("Bearer",
                        invalidJwtHeaderTokenList
                        + UserDetailsGenerator.incorectJwtPayload
                        + UserDetailsGenerator.incorectJwtSignature)
                .when()
                .body(jsonRequestWithFileId)
                .post(Endpoints.RUNTIME)
                .then()
                .statusCode(anyOf(is(400), is(500), is(401)));
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
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));

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
                .then()
                .statusCode(anyOf(is(400), is(404), is(500)));
    }

    @Test(description = "Sending the request for runtime with added characters to jwt token")
    public void runtimeWithAddedValuesToValidToken() {
        String token = UserDetailsGenerator
                .takeTokenValueFromJson("john.dean@boing.rs", "Pass$$444");
        String jsonRequestWithFileId = "{\n" +
                "  \"fileId\": \"e06246ba-d280-48bd-ab60-1739cee98c74\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .header("Bearer", token+"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
                .when()
                .body(jsonRequestWithFileId)
                .post(Endpoints.RUNTIME)
                .then().statusCode(401);
    }

    @Test(dataProvider = "invalidFileIdList", dataProviderClass = UserDetailsGenerator.class,
            description = "Sending the request for runtime with list of invalid file id's")
        public  void runtimeWithInvalidFileID(String invalidFileIdList){
        String token = UserDetailsGenerator
                .takeTokenValueFromJson("john.dean@boing.rs", "Pass$$444");
        String jsonRequestWithFileId = "{\n" +
                "  \"fileId\": \""+invalidFileIdList+"\"\n" +
                "}";
        given().relaxedHTTPSValidation()
                .header("Bearer", token)
                .when()
                .body(jsonRequestWithFileId)
                .post(Endpoints.RUNTIME)
                .then().statusCode(500);
    }



}




