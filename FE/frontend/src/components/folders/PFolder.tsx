import { FolderFilled } from '@ant-design/icons';
import { Card } from 'antd';
import useDoubleClick from 'hooks/useDoubleClick';
import { FC, RefObject, useRef, MouseEventHandler } from 'react';
import styled from 'styled-components';

interface IPFolderProps {
  value?: any;
  heading?: string;
  description?: string;
  selected: boolean;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
}

const PFolder: FC<IPFolderProps> = ({
  selected,
  heading,
  description,
  onClick,
  onDoubleClick,
}) => {
  const ref = useRef<HTMLDivElement>(null);
  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <StyledCard ref={ref} selected={selected} hoverable>
      <FolderFilled />
      <StyledFolderHeading>{heading}</StyledFolderHeading>
      <p>{description?.slice(0, 10)}</p>
    </StyledCard>
  );
};

const StyledCard = styled(Card)<{
  selected: boolean;
  ref: RefObject<HTMLDivElement | null>;
}>`
  border: 1px solid #ccc;
  padding: 2rem;
  width: 23rem;
  box-shadow: 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 14rem;
  border-radius: 1.5rem;
  overflow: hidden;
  background-color: white;
  cursor: pointer;
  transition: all 0.3s;
  border: none;
  cursor: pointer;
  border: 2px solid ${({ selected }) => (selected ? '#47a6ff' : 'transparent')};
  background-color: ${({ selected }) =>
    selected ? 'rgba(167, 187, 224, 0.1)' : '#fff'};
  &:hover,
  &:active,
  &:focus {
    border: 2px solid
      ${({ selected }) => (selected ? '#47a6ff' : 'transparent')};
  }

  &:hover {
    background-color: ${({ theme: { colors } }) => colors.primary};
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

export default PFolder;
