import { Card, Modal } from 'antd';
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
      <StyledCard
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
      </StyledCard>

      <Modal
        title="Basic Modal"
        visible={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <p>{heading}</p>
        <p>{description}</p>
      </Modal>
    </>
  );
};
const StyledCard = styled(Card)`
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

export default PCard;
