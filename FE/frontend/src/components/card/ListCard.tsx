import { Card, Button } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import { FC } from 'react';
import styled from 'styled-components';

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
      <StyledCard
        hoverable
        role="button"
        onDoubleClick={onDoubleClick}
        onClick={onClick}
      >
        <Meta
          description={[
            <div
              style={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                paddingTop: '5px',
              }}
            >
              <div>
                <h3>{heading}</h3>
                <span>Click/DoubleClick for more!</span>
              </div>
              <div style={{ textAlign: 'center' }}>
                <h4>Uploaded:</h4>
                <span>{description.slice(0, 15)}</span>
              </div>
              <div style={{ display: 'flex', gap: '1rem' }}>
                <StyledFilesButton>Remove file</StyledFilesButton>
                <StyledFilesButton type="primary" icon={<DownloadOutlined />}>
                  Download
                </StyledFilesButton>
              </div>
            </div>,
          ]}
        />
      </StyledCard>
    </>
  );
};

const StyledCard = styled(Card)`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 7.5rem;
  border-radius: 1.5rem;
  overflow: hidden;
  width: 70vw;

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

const StyledFilesButton = styled(Button)`
  border-radius: 0.8rem;
`;

export default ListCard;
