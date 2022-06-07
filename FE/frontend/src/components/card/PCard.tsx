import { Card } from 'antd';
import { FC, useEffect } from 'react';
import styled from 'styled-components';
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

  return (
    <StyledCard hoverable cover={<img alt="example" src={`${image}`} />}>
      <Meta title={heading} description={description} />
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
  overflow: hidden;
  max-width: 240px;
  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

export default PCard;
