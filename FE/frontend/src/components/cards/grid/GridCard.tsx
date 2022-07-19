import { Card } from 'antd';
import useDoubleClick from 'hooks/useDoubleClick';
import { FC, RefObject, useRef, MouseEventHandler } from 'react';
import styled from 'styled-components';
import theme from 'theme/theme';

import { usePageContext } from 'context/PageContext';

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
  isCollapsed?: boolean;
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

  const { isCollapsed } = usePageContext();
  console.log(isCollapsed);

  return (
    <StyledGridCard
      ref={ref}
      selected={selected}
      isCollapsed={isCollapsed}
      hoverable
      cover={
        <img
          alt="example"
          src={`https://pro.alchemdigital.com/api/extension-image/${fileExtension}`}
          // src="https://pro.alchemdigital.com/api/extension-image/exe"
          style={{ width: '8rem', height: '8rem' }}
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
})<{
  selected: boolean;
  ref: RefObject<HTMLDivElement | null>;
  isCollapsed: boolean;
}>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 21.5rem;
  border-radius: 1.5rem;
  overflow: hidden;
  max-width: auto;
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
    max-width: auto;
    overflow: hidden;
    border-top-left-radius: 1.5rem;
    border-top-right-radius: 1.5rem;

    display: flex;
    justify-content: center;
    align-items: center;
  }

  .ant-card-body {
    padding: 1rem 2rem;
    word-break: break-all;
  }
`;

const StyledMetaCardDescription = styled.div`
  .show-more {
    font-weight: bold;
    margin-left: 2rem;
  }
`;

export default GridCard;
