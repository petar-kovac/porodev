import { InboxOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';
import { message, Upload } from 'antd';
import React from 'react';
import styled from 'styled-components';

const { Dragger } = Upload;

const props: UploadProps = {
  name: 'file',
  multiple: true,
  accept: '*',
  action: `${process.env.REACT_APP_IMAGE_URL}`,
  onChange(info) {
    const { status } = info.file;
    if (status !== 'uploading') {
      console.log('cdccd');
    }
    if (status === 'done') {
      message.success(`${info.file.name} file uploaded successfully.`);
    } else if (status === 'error') {
      message.error(`${info.file.name} file upload failed.`);
    }
  },
  onDrop(e) {},
};

const PUpload: React.FC = () => {
  return (
    <StyledUpload>
      <StyledDragger {...props}>
        <p className="ant-upload-drag-icon">
          <InboxOutlined />
        </p>
        <p className="ant-upload-text">
          Click or drag file to this area to upload
        </p>
        <p className="ant-upload-hint">
          Support for a single or bulk upload. Max 50 files, 10 MB per file,
          total 500 MB
        </p>
      </StyledDragger>
    </StyledUpload>
  );
};

const StyledDragger = styled(Dragger).attrs({
  'data-testid': 'upload',
})`
  display: flex;
  border-radius: 15px !important;
`;
const StyledUpload = styled.div`
  margin: 0px 20vw;
  height: 300px;
`;

export default PUpload;
