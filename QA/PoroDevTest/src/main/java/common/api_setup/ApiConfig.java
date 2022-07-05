package common.api_setup;

import io.restassured.RestAssured;
import io.restassured.builder.RequestSpecBuilder;
import io.restassured.builder.ResponseSpecBuilder;
import io.restassured.filter.log.RequestLoggingFilter;
import io.restassured.filter.log.ResponseLoggingFilter;
import io.restassured.specification.RequestSpecification;
import io.restassured.specification.ResponseSpecification;
import org.testng.annotations.BeforeClass;

public class ApiConfig {

    public static RequestSpecification  api_requestSpec;
    public static ResponseSpecification api_responseSpec;

    @BeforeClass
    public static void setup() {

        api_requestSpec = new RequestSpecBuilder()
                .setBaseUri(Endpoints.BASE_URI)
                .setBasePath(Endpoints.BASE_PATH)
                .addHeader("Content-Type", "application/json")
                .addHeader("Accept", "text/plain")
                .addFilter(new RequestLoggingFilter())
                .addFilter(new ResponseLoggingFilter())
                .build();

        api_responseSpec = new ResponseSpecBuilder()
                .build();

        RestAssured.requestSpecification = api_requestSpec;
        RestAssured.responseSpecification = api_responseSpec;
    }


}
