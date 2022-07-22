package api_test.files;

import common.api_setup.ApiConfig;
import common.api_setup.api_common.DataControlUtil;
import common.api_setup.api_common.DataProviderBeUtil;
import common.ui_setup.FileControlUtil;
import org.json.simple.parser.ParseException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;
import java.io.IOException;


public class UploadAndDeleteFile extends ApiConfig {
    private final FileControlUtil fileLogin = new FileControlUtil(FileControlUtil.LOGIN_DATA_PROPERTIES);
    private final FileControlUtil filePath = new FileControlUtil(FileControlUtil.FILE_PATH_PROPERTIES);
    private final Logger logger = LoggerFactory.getLogger(UploadAndDeleteFile.class);

    public UploadAndDeleteFile() throws IOException {
    }

    @Test(priority = 3, dataProvider = "uploadFilePaths", dataProviderClass = DataProviderBeUtil.class, description = "Uploads files from Data provider list of files.")
    public void upload_files_test(String filePath) throws ParseException {
        String currentFileName = DataControlUtil.uploadFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                filePath);

        boolean isUploaded = DataControlUtil.fileExists(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                currentFileName);

        Assert.assertTrue(isUploaded, "File is not uploaded");
        logger.info("Files are uploaded, status code is 200");
    }

    @Test(priority = 1, dataProvider = "uploadFilePaths", dataProviderClass = DataProviderBeUtil.class, description = "Uploads one file at the time then deletes it.")
    public void delete_files_test(String filePath) throws ParseException {
        String currentFileName = DataControlUtil.uploadFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                filePath);

        DataControlUtil.deleteFile(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                DataControlUtil.getFileIdFromFileName(
                        fileLogin.getValue("VALID_EMAIL"),
                        fileLogin.getValue("VALID_PASS_CREATED_USER"),
                        currentFileName));

        boolean fileExist = DataControlUtil.fileExists(
                fileLogin.getValue("VALID_EMAIL"),
                fileLogin.getValue("VALID_PASS_CREATED_USER"),
                currentFileName
        );
        Assert.assertFalse(fileExist, "File is not deleted, it still exists in the database.");

        logger.info("File is not deleted, still exists in the database");
    }

    @Test(priority = 2, description = "First uploads 15 files, after that deletes that uploaded files")
    public void uploadThenDeleteUploadedFiles_test() throws ParseException {
        DataControlUtil.uploadThenDeleteFiles(15);
        logger.info("Files are uploaded then deleted, status code is 200");
    }
}
