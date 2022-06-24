import { Card } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';
import { StyledGridCard, StyledMetaCardDescription } from './card-styled';

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
      <StyledGridCard
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
      </StyledGridCard>
    </>
  );
};

export default GridCard;
