import { Card } from 'antd';
import useDoubleClick from 'hooks/useDoubleClick';
import { FC, RefObject, useRef, MouseEventHandler } from 'react';
import styled from 'styled-components';
import theme from 'theme/theme';

interface IGridCardProps {
  fileId?: any;
  fileName?: string;
  id?: string;
  data?: any;
  value?: any;
  heading?: string;
  description?: string;
  image?: string;
  selected: boolean;
  fileExtension?: string;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
}

const GridCard: FC<IGridCardProps> = ({
  fileId,
  fileName,
  data,
  value,
  heading,
  description,
  selected,
  image,
  fileExtension,
  onClick,
  onDoubleClick,
}) => {
  const ref = useRef<HTMLDivElement>(null);
  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  console.log(fileExtension);

  return (
    <StyledGridCard
      ref={ref}
      selected={selected}
      hoverable
      cover={
        <img
          alt="example"
          src={`https://pro.alchemdigital.com/api/extension-image/${fileExtension}`}
          // src="https://pro.alchemdigital.com/api/extension-image/exe"
          style={{ width: '100%', height: '100%' }}
        />
      }
      role="button"
    >
      <StyledMetaCardDescription>
        <h4>{heading}</h4>
        {/* <span>{value.uploadTime?.slice(0, 30)}...</span>
        <span className="show-more">&rarr; Show more</span> */}
      </StyledMetaCardDescription>
    </StyledGridCard>
  );
};

const StyledGridCard = styled(Card).attrs({
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
    height: 14rem;
    width: 23rem;
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

export default GridCard;
