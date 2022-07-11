import { InboxOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';
import { message, Upload } from 'antd';
import { STATUS_CODES } from 'http';
import React, { useState } from 'react';
import { findFiles } from 'service/files/files';
import styled from 'styled-components';
import { StatusCode } from 'util/enums/status-codes';
import api from '../../service/base';

const { Dragger } = Upload;

interface PUploadProps {
  setFiles: (data: any) => unknown;
}

const usePUploadProps = (setFiles: (data: any) => unknown) => {
  const props: UploadProps = {
    multiple: true,
    accept: '*',
    onChange(info) {
      const { status } = info.file;
      if (status !== 'uploading') {
        console.log('Uploading');
      }
      if (status === 'done') {
        message.success(`${info.file.name} file uploaded successfully.`);
      } else if (status === 'error') {
        message.error(`${info.file.name} file upload failed.`);
      }
    },
    onDrop(e) {
      console.log(e);
    },
    async customRequest(options) {
      const { onError, file, onProgress, onSuccess, method } = options;
      const fmData = new FormData();
      const config = {
        headers: { 'content-type': 'multipart/form-data' },
      };
      fmData.append('file', file);

      try {
        const res = await api
          .service()
          .post('/api/Storage/Upload', fmData, config);

        const files = await findFiles();
        setFiles(files);

        console.log(res);
        if (onSuccess) {
          onSuccess('Ok');
        }
      } catch (error: any) {
        if (onError) {
          onError({
            status: error.status as any,
            method,
            url: '/api/Storage/Upload',
            message: 'Error uploading file',
            name: 'Error has happened',
          });
        }
      }
    },
    onRemove(file) {
      console.log('on remove');
    },
  };
  return props;
};

const PUpload: React.FC<PUploadProps> = ({ setFiles }) => {
  const props = usePUploadProps(setFiles);

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
  height: 250px;
  margin: 0px 20vw;
  span {
  }
  .ant-upload-list-text {
    max-height: 120px;
    overflow-y: scroll;
    padding-left: 10px;
  }
`;

export default PUpload;
