import { Button, Card, Modal } from 'antd';
import { FC, useState } from 'react';
import styled from 'styled-components';

import { useAuthStateValue } from '../../context/AuthContext';

const { Meta } = Card;
interface IPCardProps {
  heading: string;
  description: string;
  image: string;
  onClick?: any;
  onDoubleClick?: any;
}

const PCard: FC<IPCardProps> = ({
  heading,
  description,
  image,
  onClick,
  onDoubleClick,
}) => {
  const { isAuthenticated, testMessage } = useAuthStateValue();
  const [isModalVisible, setIsModalVisible] = useState(false);

  return (
    <>
      <StyledCard
        hoverable
        cover={<img alt="example" src={`${image}`} />}
        role="button"
        onDoubleClick={onDoubleClick}
        onClick={onClick}
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
    </>
  );
};
const StyledCard = styled(Card)`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  .ant-card-cover {
    height: 14rem;
    width: 24rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  max-width: 24rem;
  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

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

export default PCard;
