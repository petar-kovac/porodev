import { Card } from 'antd';
import styled from 'styled-components';
import { FileZipOutlined } from '@ant-design/icons';
import { FC, ReactNode, RefObject, useRef } from 'react';

import useDoubleClick from 'hooks/useDoubleClick';
import { formatDate } from 'util/helpers/date-formaters';

const { Meta } = Card;

interface IRuntimeCardProps {
  title: ReactNode;
  createdAt: string;
  selected?: boolean;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
  keyless: string;
}

const RuntimeCard: FC<IRuntimeCardProps> = ({
  title,
  createdAt,
  onClick,
  selected = false,
  onDoubleClick = () => undefined,
  keyless,
}) => {
  const ref = useRef<HTMLDivElement>(null);

  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <StyledCard
      ref={ref}
      selected={selected}
      key={keyless}
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
  'data-testid': 'files-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  background-color: ${({ selected }) => (selected ? 'red' : 'blue')};
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 25rem;
  border-radius: 1.5rem;
  overflow: hidden;
  max-width: 24rem;

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
