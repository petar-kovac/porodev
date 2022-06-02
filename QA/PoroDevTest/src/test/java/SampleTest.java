import TestSetup.SampleTestSetup;
import org.testng.annotations.Test;

public class SampleTest extends SampleTestSetup {

    @Test
    public void openGoogleTest() {
        System.out.println("Google is open");
    }
}
