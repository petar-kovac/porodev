import { UserOutlined } from '@ant-design/icons';
import { Button, Modal, Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import { Dispatch, FC, SetStateAction, ReactNode } from 'react';
import { handleDelete } from 'util/helpers/files-functions';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import styled from 'styled-components';
import { IFilesCard } from 'types/card-data';

interface IPModalProps {
  title?: string;
  content?: ReactNode;
  onOk?: any;
  onCancel?: any;
  isRemoveModalVisible?: boolean;
  sharedSpaceId?: string;
  data?: any;
  setData?: any;
  fileName?: string;
  fileId?: string;
  setSearchRes?: any;
  searchRes?: any;
  isSharedSpaceFile?: boolean;
  setIsRemoveModalVisible?: Dispatch<SetStateAction<boolean>>;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  handleCancel?: any;
  cardData?: IFilesCard | null;
}

const RemoveModal: FC<IPModalProps> = ({
  title,
  setCardData,
  handleCancel,
  fileName,
  fileId,
  data,
  setData,
  isRemoveModalVisible,
  isSharedSpaceFile,
  sharedSpaceId,
  setIsRemoveModalVisible = () => undefined,
}) => {
  const { setUserTrigger, userTrigger } = usePageContext();

  return (
    <>
      <StyledFilesModal
        okText="Yes"
        cancelText="No"
        title={title}
        visible={isRemoveModalVisible}
        onOk={() => {
          handleDelete(fileId, isSharedSpaceFile, sharedSpaceId, data);
          setIsRemoveModalVisible(false);
          setData([...data.filter((value: any) => value.fileId !== fileId)]);
        }}
        onCancel={handleCancel}

        // footer={[
        //   <div className="footer-content">
        //     <StyledFilesButton onClick={handleCancel}>Cancel</StyledFilesButton>
        //     <StyledFilesButton type="primary" icon={<DownloadOutlined />}>
        //       Download
        //     </StyledFilesButton>
        //   </div>,
        // ]}
      >
        <div>
          <p>Are you sure you want to delete this file? </p>
          <span>{fileName}</span>
        </div>
      </StyledFilesModal>
    </>
  );
};

const StyledFilesModal = styled(Modal).attrs({
  'data-testid': 'modal',
})`
  .ant-modal-header {
    border-radius: 1.2rem;
    background-color: rgba(220, 220, 220, 0.1);
    border: 1px solid #fff;
    border-bottom: 1px solid #eee;
  }

  .ant-modal-title {
    color: #555;
    letter-spacing: 0.5px;
    font-size: 1.8rem;
  }

  .ant-modal-content {
    border-radius: 1.2rem;
    box-shadow: 1px 3px 4px rgba(255, 255, 255, 0.4);
  }

  .ant-modal-footer {
    padding: 1.4rem 2.2rem;
    border-radius: 1.6rem;

    button {
      border-radius: 0.8rem;
    }

    .footer-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;

export default RemoveModal;
