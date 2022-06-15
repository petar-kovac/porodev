import { Card } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

const { Meta } = Card;
interface IPCardProps {
  heading: string;
  description: string;
  image: string;
  onClick?: any;
  onDoubleClick?: any;
}

const PCard: FC<IPCardProps> = ({
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
            <div>
              <span>{description.slice(0, 60)}...</span>
              <span
                style={{
                  fontWeight: 'bold',
                  marginLeft: '2rem',
                }}
              >
                &rarr; Show more
              </span>
            </div>,
          ]}
        />
      </StyledCard>
    </>
  );
};

const StyledCard = styled(Card)`
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

export default PCard;
