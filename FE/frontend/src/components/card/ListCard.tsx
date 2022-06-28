import { Card, Button } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import { FC, RefObject, useRef } from 'react';
import styled from 'styled-components';
import DownloadButton from 'components/buttons/DownloadButton';
import useDoubleClick from 'hooks/useDoubleClick';
import theme from 'theme/theme';

const { Meta } = Card;

interface IListCardProps {
  heading: string;
  description: string;
  image: string;
  selected: boolean;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
}

const ListCard: FC<IListCardProps> = ({
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
    <>
      <StyledListCardContainer>
        <StyledListCard ref={ref} hoverable selected={selected}>
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

export const StyledListCard = styled(Card).attrs({
  'data-testid': 'list-card',
})<{ selected: boolean; ref: RefObject<HTMLDivElement | null> }>`
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  height: 7.5rem;
  border-radius: 1.5rem;
  overflow: hidden;
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
const StyledListCardContainer = styled.div`
  width: 100%;
`;

const StyledFilesButton = styled(Button)`
  border-radius: 0.8rem;
`;

const StyledMetaDescription = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-top: 0.5rem;
`;

const StyledDescriptionButtons = styled.div`
  display: flex;
  gap: 1rem;
`;

const StyledDescriptionUploadDetails = styled.div`
  text-align: center;
`;

export default ListCard;
