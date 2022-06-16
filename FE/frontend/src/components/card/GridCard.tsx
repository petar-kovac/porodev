import { Card } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

const { Meta } = Card;
interface IGridCardProps {
  heading: string;
  description: string;
  image: string;
  onClick?: any;
  onDoubleClick?: any;
}

const GridCard: FC<IGridCardProps> = ({
  heading,
  description,
  image,
  onClick,
  onDoubleClick,
}) => {
  return (
    <>
      <StyledCard
        hoverable
        cover={<img alt="example" src={`${image}`} />}
        role="button"
        onDoubleClick={onDoubleClick}
        onClick={onClick}
      >
        <Meta
          title={heading}
          description={[
            <StyledMetaCardDescription>
              <span>{description.slice(0, 60)}...</span>
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
`;

const StyledMetaCardDescription = styled.div`
  .show-more {
    font-weight: bold;
    margin-left: 2rem;
  }
`;

export default GridCard;