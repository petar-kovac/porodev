import { DownloadOutlined } from '@ant-design/icons';
import { Button, Modal } from 'antd';
import { FC, useState } from 'react';
import styled from 'styled-components';

const PModal: FC<{ heading?: string; description?: string }> = ({
  heading,
  description,
}) => {
  const [isModalVisible, setIsModalVisible] = useState(false);

  const showModal = (e: any) => {
    console.log(e.target, 'e');

    setIsModalVisible(true);
  };

  const handleOk = () => {
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };
  return (
    <div>
      <StyledFilesModal
        title={heading}
        visible={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
        footer={[
          <div className="footer-content">
            <StyledFilesButton onClick={handleCancel}>Cancel</StyledFilesButton>
            <StyledFilesButton type="primary" icon={<DownloadOutlined />}>
              Download
            </StyledFilesButton>
          </div>,
        ]}
      >
        <p>{description}</p>
      </StyledFilesModal>
    </div>
  );
};

const StyledFilesModal = styled(Modal)`
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

    .footer-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;
const StyledFilesButton = styled(Button)`
  border-radius: 0.8rem;
`;

export default PModal;
