import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';
import { Card, Button, Modal } from 'antd';
import PUpload from '../upload/PUpload';
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
        <Meta title={heading} description={description} />
      </StyledCard>
      {/* <Button type="primary" onClick={showModal}>
        Open Modal
      </Button> */}
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
  .ant-card-cover {
    height: 120px;
    width: 240px;
    overflow: hidden;
    border-top-left-radius: 15px;
    border-top-right-radius: 15px;
  }
  height: 200px;
  border-radius: 15px;
  overflow: hidden;
  max-width: 240px;
  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

export default PCard;
