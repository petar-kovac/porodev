package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import common.ui_setup.FileControlUtil;
import io.qameta.allure.Feature;
import org.testng.annotations.Test;

import java.io.IOException;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.*;

@Feature("Runtime / Negative test cases")
public class Runtime extends ApiConfig {


    private final FileControlUtil file = new FileControlUtil(FileControlUtil.END_TO_END_PROPERTIES);

    public Runtime() throws IOException {
    }





    // Sending the runtime request with invalid token (header part with invalid list)
    @Test(dataProvider = "invalidJwtHeaderTokenList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the runtime request with invalid Header part of token")
    public void runtimeWithInvalidJwtHeaderTokenList(String invalidJwtHeaderTokenList) {
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer"
                        + invalidJwtHeaderTokenList
                        + UserDetailsGenerator.incorectJwtPayload
                        + UserDetailsGenerator.incorectJwtSignature)
                .when()
                .body(UserDetailsGenerator.createRuntimeJsonReq(
                        "e06246ba-d280-48bd-ab60-1739cee98c74",
                        "1",
                        "2"))
                .post(Endpoints.RUNTIME)
                .then()
                .statusCode(anyOf(is(400), is(500), is(401)));
    }


    // Sending the runtime request with invalid token (payload part with invalid list)
    @Test(dataProvider = "invalidJwtPayloadTokenList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the runtime request with invalid Payload part of token")
    public void runtimeWithInvalidJwtPayloadTokenList(String invalidJwtPayloadTokenList) {
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer"
                        + UserDetailsGenerator.validJwtHeader
                        + invalidJwtPayloadTokenList
                        + UserDetailsGenerator.incorectJwtSignature)
                .when()
                .body(UserDetailsGenerator.createRuntimeJsonReq(
                        "e06246ba-d280-48bd-ab60-1739cee98c74",
                        "3",
                        "3"))
                .post(Endpoints.RUNTIME)
                .then()
                .statusCode(anyOf(is(400), is(404), is(401), is(500)));

    }



    // Sending the runtime request with invalid token (signature part with invalid list)
    @Test(dataProvider = "invalidJwtSignatureTokenList", dataProviderClass = DataProviderBeUtil.class,
    description = "Sending the runtime request with invalid Signature part of token")
    public void runtimeWithInvalidJwtSignatureTokenList(String invalidJwtSignatureTokenList) {
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer"
                        + UserDetailsGenerator.validJwtHeader
                        + UserDetailsGenerator.incorectJwtPayload
                        + invalidJwtSignatureTokenList)
                .when()
                .body(UserDetailsGenerator.createRuntimeJsonReq(
                        "e06246ba-d280-48bd-ab60-1739cee98c74",
                        "2",
                        "2"))
                .post(Endpoints.RUNTIME)
                .then()
                .statusCode(anyOf(is(400), is(404), is(500), is(401)));
    }

    // Sending the runtime request with valid token + added characters
    @Test(description = "Sending the request for runtime with added characters to jwt token")
    public void runtimeWithAddedValuesToValidToken() {
        String token = UserDetailsGenerator
                .takeTokenValueFromJson(file.getValue("VALID_EMAIL"),
                        file.getValue("VALID_PASS"));
        given().relaxedHTTPSValidation()
                .header("authorization", "Bearer"+token+"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
                .when()
                .body(UserDetailsGenerator.createRuntimeJsonReq(
                        "e06246ba-d280-48bd-ab60-1739cee98c74",
                        "1",
                        "2"))
                .post(Endpoints.RUNTIME)
                .then().statusCode(anyOf(is(400), is(401)));
    }

    // Sending the runtime request with invalid id (invalid id list from dataprovider)
    @Test(dataProvider = "invalidFileIdList", dataProviderClass = DataProviderBeUtil.class,
            description = "Sending the request for runtime with invalid file ID attribute")
        public  void runtimeWithInvalidFileID(String invalidFileIdList){
        String token = UserDetailsGenerator
                .takeTokenValueFromJson(file.getValue("VALID_EMAIL"), file.getValue("VALID_PASS"));
        given().relaxedHTTPSValidation()
                .header("Bearer", token)
                .when()
                .body(UserDetailsGenerator.createRuntimeJsonReq(
                        invalidFileIdList,
                        "1",
                        "2"))
                .post(Endpoints.RUNTIME)
                .then().statusCode(400);
    }



}



