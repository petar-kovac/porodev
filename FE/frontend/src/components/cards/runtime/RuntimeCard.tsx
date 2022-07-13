import { FileZipOutlined } from '@ant-design/icons';
import { Card } from 'antd';
import useDoubleClick from 'hooks/useDoubleClick';
import { FC, RefObject, useRef, MouseEventHandler } from 'react';
import styled from 'styled-components';
import theme from 'theme/theme';

interface IGridCardProps {
  value?: any;
  id?: string;
  heading?: string;
  description?: string;
  image?: string;
  selected: boolean;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
}

const RuntimeCard: FC<IGridCardProps> = ({
  heading,
  description,
  selected,
  image,
  onClick,
  onDoubleClick,
}) => {
  const ref = useRef<HTMLDivElement>(null);
  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <StyledRuntimeCard
      ref={ref}
      selected={selected}
      hoverable
      cover={<StyledZip />}
      role="button"
    >
      <StyledMetaCardDescription>
        <h4>{heading}</h4>
        {/* <span>{description?.slice(0, 30)}...</span>
        <span className="show-more">&rarr; Show more</span> */}
      </StyledMetaCardDescription>
    </StyledRuntimeCard>
  );
};

const StyledRuntimeCard = styled(Card).attrs({
  'data-testid': 'grid-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 24rem;
  border-radius: 1.5rem;
  overflow: hidden;
  width: 23rem;
  cursor: pointer;
  border: 2px solid
    ${({ selected }) => (selected ? `${theme.colors.selected}` : '#fff')};
  background-color: ${({ selected }) =>
    selected ? `${theme.colors.selectedBackground}` : '#fff'};
  &:hover,
  &:active,
  &:focus {
    border: 2px solid
      ${({ selected }) => (selected ? `${theme.colors.selected}` : '#fff')};
  }

  .ant-card-cover {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 14rem;
    max-width: 23rem;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;
  }

  .ant-card-body {
    padding: 1rem 2rem;
  }
`;

const StyledMetaCardDescription = styled.div`
  .show-more {
    font-weight: bold;
    margin-left: 2rem;
  }
`;
const StyledZip = styled(FileZipOutlined)`
  font-size: 70px;
`;

export default RuntimeCard;
