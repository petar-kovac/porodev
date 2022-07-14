import { UserOutlined } from '@ant-design/icons';
import { Button, Modal, Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import { Dispatch, FC, SetStateAction, ReactNode } from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import styled from 'styled-components';
import { IFilesCard } from 'types/card-data';

interface IPModalProps {
  title?: string;
  content?: ReactNode;
  onOk?: any;
  onCancel?: any;
  isSiderModalVisible: boolean;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setIsSiderModalVisible?: Dispatch<SetStateAction<boolean>>;
  cardData?: IFilesCard | null;
}

const ChoseImageModal: FC<IPModalProps> = ({
  title,
  content,
  setIsSiderModalVisible = () => undefined,
  isSiderModalVisible,
}) => {
  const handleOk = () => {
    setIsSiderModalVisible(false);
  };

  const handleCancel = () => {
    setIsSiderModalVisible(false);
  };

  return (
    <StyledImageModal
      title={title}
      visible={isSiderModalVisible}
      onOk={handleOk}
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
      <div>{content}</div>
    </StyledImageModal>
  );
};

const StyledImageModal = styled(Modal).attrs({
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

export default ChoseImageModal;
