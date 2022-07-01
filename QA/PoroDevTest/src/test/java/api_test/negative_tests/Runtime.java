package api_test.negative_tests;

import common.api_setup.ApiConfig;
import common.api_setup.Endpoints;
import common.api_setup.api_common.DataProviderBeUtil;
import common.api_setup.api_common.UserDetailsGenerator;
import org.testng.annotations.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.*;

public class Runtime extends ApiConfig {





    @Test(dataProvider = "invalidJwtHeaderTokenList", dataProviderClass = DataProviderBeUtil.class)
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


    @Test(dataProvider = "invalidJwtPayloadTokenList", dataProviderClass = DataProviderBeUtil.class)
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




    @Test(dataProvider = "invalidJwtSignatureTokenList", dataProviderClass = DataProviderBeUtil.class)
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

    @Test(description = "Sending the request for runtime with added characters to jwt token")
    public void runtimeWithAddedValuesToValidToken() {
        String token = UserDetailsGenerator
                .takeTokenValueFromJson("jack.sparrow@boing.rs", "Pass##33");
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

    @Test(dataProvider = "invalidFileIdList", dataProviderClass = DataProviderBeUtil.class,
            description = "Sending the request for runtime with list of invalid file id's")
        public  void runtimeWithInvalidFileID(String invalidFileIdList){
        String token = UserDetailsGenerator
                .takeTokenValueFromJson("jack.sparrow@boing.rs", "Pass##33");
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




