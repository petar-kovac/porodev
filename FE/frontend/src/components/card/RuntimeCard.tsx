import { Card } from 'antd';
import styled from 'styled-components';
import { FileZipOutlined } from '@ant-design/icons';
import { FC, ReactNode, RefObject, useRef } from 'react';

import useDoubleClick from 'hooks/useDoubleClick';
import { formatDate } from 'util/helpers/date-formaters';
import theme from 'theme/theme';

const { Meta } = Card;

interface IRuntimeCardProps {
  title: ReactNode;
  createdAt: string;
  selected: boolean;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
}

const RuntimeCard: FC<IRuntimeCardProps> = ({
  title,
  createdAt,
  selected,
  onClick,
  onDoubleClick = () => undefined,
}) => {
  const ref = useRef<HTMLDivElement>(null);

  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <StyledCard
      ref={ref}
      selected={selected}
      hoverable
      cover={
        <div className="card-cover">
          <FileZipOutlined />
        </div>
      }
    >
      <Meta
        data-testid="title"
        title={title}
        description={[
          <StyledMetaCardDescription>
            {formatDate(createdAt)}
            <span className="show-more">&rarr; Show more</span>
          </StyledMetaCardDescription>,
        ]}
      />
    </StyledCard>
  );
};

const StyledCard = styled(Card).attrs({
  'data-testid': 'runtime-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  max-width: 24rem;
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

  .card-cover {
    width: 100%;
    height: 100%;

    span {
      font-size: 10rem;
      width: 100%;
      height: 100%;
      padding: 2rem;
    }
  }
`;

const StyledMetaCardDescription = styled.div`
  .show-more {
    display: block;
    font-weight: bold;
  }
`;

export default RuntimeCard;
