import { Card, Button } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import { FC } from 'react';
import styled from 'styled-components';
import DownloadButton from 'components/buttons/DownloadButton';
import {
  StyledDescriptionButtons,
  StyledDescriptionUploadDetails,
  StyledFilesButton,
  StyledListCard,
  StyledListCardContainer,
  StyledMetaDescription,
} from './card-styled';

const { Meta } = Card;

interface IListCardProps {
  heading: string;
  description: string;
  image: string;
  onClick?: any;
  onDoubleClick?: any;
}

const ListCard: FC<IListCardProps> = ({
  heading,
  description,
  image,
  onClick,
  onDoubleClick,
}) => {
  return (
    <>
      <StyledListCardContainer>
        <StyledListCard
          hoverable
          role="button"
          onDoubleClick={onDoubleClick}
          onClick={onClick}
        >
          <Meta
            description={[
              <StyledMetaDescription>
                <div>
                  <h3>{heading}</h3>
                  <span>Click/DoubleClick for more!</span>
                </div>
                <StyledDescriptionUploadDetails>
                  <h4>Uploaded:</h4>
                  <span>{description.slice(0, 15)}</span>
                </StyledDescriptionUploadDetails>
                <StyledDescriptionButtons>
                  <StyledFilesButton>Remove filee</StyledFilesButton>
                  {/* <StyledFilesButton type="primary" icon={<DownloadOutlined />}>
                    Download
                  </StyledFilesButton> */}
                  <DownloadButton />
                </StyledDescriptionButtons>
              </StyledMetaDescription>,
            ]}
          />
        </StyledListCard>
      </StyledListCardContainer>
    </>
  );
};

export default ListCard;
