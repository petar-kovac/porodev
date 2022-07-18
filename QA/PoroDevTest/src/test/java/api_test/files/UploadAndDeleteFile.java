package api_test.files;

import common.api_setup.ApiConfig;
import common.api_setup.api_common.DataControlUtil;
import common.ui_setup.FileControlUtil;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.annotations.Test;
import java.io.IOException;


public class UploadAndDeleteFile extends ApiConfig {
    private final FileControlUtil fileLogin = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
    private final FileControlUtil filePath = new FileControlUtil(FileControlUtil.FILE_PATH_PROPERTIES);
    private final Logger logger = LoggerFactory.getLogger(UploadAndDeleteFile.class);

    public UploadAndDeleteFile() throws IOException {
    }

    @Test(priority = 1)
    public void upload_xml_file() {
        DataControlUtil.uploadFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                filePath.getValue("POM_FILE"));
        logger.info("Xml file is uploaded, status code is 200");
    }

    @Test(priority = 2)
    public void delete_xml_file() {
        DataControlUtil.deleteFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                DataControlUtil.getFileIdFromFileName(
                        fileLogin.getValue("VALID_EMAIL"),
                        fileLogin.getValue("VALID_PASS_CREATED_USER"),
                        0));
        logger.info("Xml file is deleted, status code is 200");
    }

    @Test(priority = 3)
    public void uploadThenDeleteUploadedFiles() {
        DataControlUtil.uploadThenDeleteFiles(15);
        logger.info("Files are uploaded then deleted, status code is 200");
    }

    @Test(priority = 4)
    public void upload_jpeg_file() {
        DataControlUtil.uploadFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                filePath.getValue("JPG_IMAGE_FILE"));
        logger.info("Jpg file is uploaded, status code is 200");
    }

    @Test(priority = 5)
    public void delete_jpeg_file() {
        DataControlUtil.deleteFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                DataControlUtil.getFileIdFromFileName(
                        fileLogin.getValue("VALID_EMAIL"),
                        fileLogin.getValue("VALID_PASS_CREATED_USER"),
                        0));
        logger.info("Jpg file is deleted, status code is 200");
    }

    @Test(priority = 6)
    public void upload_100mb_file() {
        DataControlUtil.uploadFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                filePath.getValue("ONE_HUNDRED_MB_FILE"));
        logger.info("One hundred mb file is uploaded, status code is 200");
    }

    @Test(priority = 7)
    public void delete_100mb_file() {
        DataControlUtil.deleteFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                DataControlUtil.getFileIdFromFileName(
                        fileLogin.getValue("VALID_EMAIL"),
                        fileLogin.getValue("VALID_PASS_CREATED_USER"),
                        0));
        logger.info("One hundred mb file is deleted, status code is 200");
    }

    @Test(priority = 8)
    public void upload_500mb_file() {
        DataControlUtil.uploadFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                filePath.getValue("FIVE_HUNDRED_MB_FILE"));
        logger.info("Five hundred mb file is uploaded, status code is 200");
    }

    @Test(priority = 9)
    public void delete_500mb_file() {
        DataControlUtil.deleteFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                DataControlUtil.getFileIdFromFileName(
                        fileLogin.getValue("VALID_EMAIL"),
                        fileLogin.getValue("VALID_PASS_CREATED_USER"),
                        0));
        logger.info("Five hundred mb file is deleted, status code is 200");
    }
}
