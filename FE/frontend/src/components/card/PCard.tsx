import { DownloadOutlined } from '@ant-design/icons';
import { Card, Modal, Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { FC, useState } from 'react';
import styled from 'styled-components';

import { useAuthStateValue } from '../../context/AuthContext';

const { Meta } = Card;
interface IPCardProps {
  heading: string;
  description: string;
  image: string;
}

const PCard: FC<IPCardProps> = ({ heading, description, image }) => {
  const { isAuthenticated, testMessage } = useAuthStateValue();
  const [isModalVisible, setIsModalVisible] = useState(false);

  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleOk = () => {
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  return (
    <>
      <StyledFilesCard
        hoverable
        cover={<img alt="example" src={`${image}`} />}
        role="button"
        onClick={showModal}
      >
        <Meta
          title={heading}
          description={[
            <div>
              <span>{description.slice(0, 60)}...</span>
              <span
                style={{
                  fontWeight: 'bold',
                  marginLeft: '2rem',
                }}
              >
                &rarr; Show more
              </span>
            </div>,
          ]}
        />
      </StyledFilesCard>

      <StyledFilesModal
        title={heading}
        visible={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
        footer={[
          <div
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
            }}
          >
            <StyledFilesButton onClick={handleCancel}>Cancel</StyledFilesButton>
            <StyledFilesButton type="primary" icon={<DownloadOutlined />}>
              Download
            </StyledFilesButton>
          </div>,
        ]}
      >
        <p>{description}</p>
      </StyledFilesModal>
    </>
  );
};
const StyledFilesCard = styled(Card)`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  .ant-card-cover {
    height: 140px;
    width: 240px;
    overflow: hidden;
    border-top-left-radius: 15px;
    border-top-right-radius: 15px;
  }
  height: 250px;
  border-radius: 15px;
  overflow: hidden;
  max-width: 240px;
  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

const StyledFilesModal = styled(Modal)`
  .ant-modal-header {
    border-radius: 12px;
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
    border-radius: 12px;
    box-shadow: 1px 3px 4px rgba(255, 255, 255, 0.4);
  }

  .ant-modal-footer {
    padding: 14px 22px;
    border-radius: 16px;
  }
`;

const StyledFilesButton = styled(Button)`
  border-radius: 8px;
`;

export default PCard;
