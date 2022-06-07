import { Card } from 'antd';
import { FC, useEffect } from 'react';
import styled from 'styled-components';
import PUpload from '../upload/PUpload';
import { useAuthStateValue } from '../../context/AuthContext';

const { Meta } = Card;

const PCard: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return (
    <StyledCard
      hoverable
      style={{ width: 240 }}
      cover={<img alt="example" src={`${process.env.REACT_APP_IMAGE_CARD}`} />}
    >
      <Meta title="Europe Street beat" description="www.instagram.com" />
    </StyledCard>
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
`;

export default PCard;
