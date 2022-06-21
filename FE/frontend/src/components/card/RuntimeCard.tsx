import { FileZipOutlined } from '@ant-design/icons';
import { Card } from 'antd';
import { FC, ReactNode } from 'react';
import styled from 'styled-components';
import dayjs from 'dayjs';

const { Meta } = Card;

interface IRuntimeCardProps {
  title: ReactNode;
  createdAt: string;
  onClick?: any;
  onDoubleClick?: any;
}

const RuntimeCard: FC<IRuntimeCardProps> = ({
  title,
  createdAt,
  onClick,
  onDoubleClick,
}) => {
  return (
    <>
      <StyledCard
        hoverable
        cover={
          <div className="card-cover">
            <FileZipOutlined />
          </div>
        }
        role="button"
        onDoubleClick={onDoubleClick}
        onClick={onClick}
      >
        <Meta
          title={title}
          description={[
            <StyledMetaCardDescription>
              {dayjs.unix(Date.parse(createdAt)).format('D MMMM')}
              <span className="show-more">&rarr; Show more</span>
            </StyledMetaCardDescription>,
          ]}
        />
      </StyledCard>
    </>
  );
};

const StyledCard = styled(Card).attrs({
  'data-testid': 'files-card',
})`
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
