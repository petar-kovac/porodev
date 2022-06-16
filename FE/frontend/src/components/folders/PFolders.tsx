import { FolderFilled } from '@ant-design/icons';
import { Card } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

interface IPFoldersProps {
  heading: string;
  description: string;
  onClick?: any;
}

const PFolders: FC<IPFoldersProps> = ({ heading, description, onClick }) => {
  return (
    <StyledCard onClick={onClick} hoverable>
      <FolderFilled />
      <StyledFolderHeading>{heading}</StyledFolderHeading>
      <p>{description.slice(0, 10)}</p>
    </StyledCard>
  );
};

const StyledCard = styled(Card)`
  border: 1px solid #ccc;
  padding: 2rem;
  width: 22rem;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 14rem;
  border-radius: 1.5rem;
  overflow: hidden;
  background-color: white;
  cursor: pointer;
  transition: all 0.3s;
  border: none;

  &:hover {
    background-color: ${({ theme: { colors } }) => colors.primary};
    border: none;
  }

  &:hover .ant-card-body > * {
    color: #ffffff;
    transition: all 0.3s;
  }

  .ant-card-body {
    padding: 0.8rem 1rem;
  }

  .anticon {
    font-size: 4rem;
    color: ${({ theme: { colors } }) => colors.primary};
  }
`;

const StyledFolderHeading = styled.div`
  font-size: 1.7rem;
  font-weight: bold;
  color: #555;
`;

export default PFolders;
