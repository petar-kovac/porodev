import { Card, Button } from 'antd';
import styled from 'styled-components';
import { FC, useRef, MouseEventHandler, RefObject } from 'react';

import theme from 'theme/theme';

import useDoubleClick from 'hooks/useDoubleClick';

import DownloadButton from 'components/buttons/DownloadButton';

interface IListCardProps {
  value: any;
  selected: boolean;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
  setSelectedCardId: (value: number | null) => unknown;
  setIsSiderVisible: (value: boolean) => unknown;
}

const ListCard: FC<IListCardProps> = ({
  value,
  selected,
  onClick = () => undefined,
  onDoubleClick = () => undefined,
  setSelectedCardId,
  setIsSiderVisible,
}) => {
  const ref = useRef<HTMLDivElement>(null);

  useDoubleClick({ ref, onClick, onDoubleClick, stopPropagation: true });

  return (
    <StyledListCardContainer>
      <StyledListCard ref={ref} hoverable selected={selected}>
        <StyledDescription>
          <div>
            <h3>{value.name}</h3>
            <span>Click/DoubleClick for more!</span>
          </div>
          <StyledDescriptionUploadDetails>
            <h4>Uploaded:</h4>
            <span>{value.description.slice(0, 12)}</span>
          </StyledDescriptionUploadDetails>
          <StyledDescriptionButtons>
            <StyledFilesButton
              onClickCapture={(e) => {
                console.log(e);
              }}
            >
              Remove file
            </StyledFilesButton>

            <DownloadButton
              onClickCapture={(e) => {
                e.stopPropagation();
                setSelectedCardId(value.id);
                setIsSiderVisible(false);
              }}
            />
          </StyledDescriptionButtons>
        </StyledDescription>
      </StyledListCard>
    </StyledListCardContainer>
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

const StyledDescription = styled.div`
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
