import { Card } from 'antd';
import useDoubleClick from 'hooks/useDoubleClick';
import { FC, RefObject, useRef } from 'react';
import styled from 'styled-components';
import theme from 'theme/theme';

const { Meta } = Card;
interface IGridCardProps {
  id?: string;
  heading?: string;
  description?: string;
  image?: string;
  selected: boolean;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
}

const GridCard: FC<IGridCardProps> = ({
  id,
  heading,
  description,
  image,
  selected,
  onClick,
  onDoubleClick = () => undefined,
}) => {
  const ref = useRef<HTMLDivElement>(null);

  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <StyledGridCard
      ref={ref}
      hoverable
      cover={<img alt="example" src={`${image}`} />}
      role="button"
      selected={selected}
    >
      <Meta
        title={heading}
        description={[
          <StyledMetaCardDescription>
            <span>{description?.slice(0, 60)}...</span>
            <span className="show-more">&rarr; Show more</span>
          </StyledMetaCardDescription>,
        ]}
      />
    </StyledGridCard>
  );
};
const StyledGridCard = styled(Card).attrs({
  'data-testid': 'grid-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  max-width: 24rem;
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
    width: 24rem;
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
