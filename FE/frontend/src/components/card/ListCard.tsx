import { Card, Button } from 'antd';
import { useFetchData } from 'hooks/useFetchData';
import React, {
  useState,
  Dispatch,
  FC,
  RefObject,
  useRef,
  SetStateAction,
  MouseEvent,
} from 'react';
import styled from 'styled-components';
import DownloadButton from 'components/buttons/DownloadButton';
import useDoubleClick from 'hooks/useDoubleClick';
import { IFilesCard } from 'types/card-data';
import theme from 'theme/theme';

const { Meta } = Card;

interface IListCardProps {
  heading?: string;
  description?: string;
  image?: string;
  cardData?: IFilesCard | null;
  selected?: boolean;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setIsSiderVisible?: Dispatch<SetStateAction<boolean>>;
  setIsModalVisible?: Dispatch<SetStateAction<boolean>>;
}

const ListCard: FC<IListCardProps> = ({
  cardData,
  onClick = () => undefined,
  onDoubleClick = () => undefined,
  setCardData = () => undefined,
  setIsSiderVisible = () => undefined,
  setIsModalVisible = () => undefined,
}) => {
  const url = `${process.env.REACT_APP_MOCK_URL}/files`;
  const { data } = useFetchData(url);

  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const handleOnClick = (
    event: MouseEvent<HTMLElement, globalThis.MouseEvent>,
    value: any,
  ) => {
    event.stopPropagation();
    setCardData(value);
    setIsSiderVisible(true);
  };

  const handleOnDoubleClick = (
    event: MouseEvent<HTMLElement, globalThis.MouseEvent>,
    value: any,
  ) => {
    event.stopPropagation();
    setCardData(value);
    setIsSiderVisible(false);
    setIsModalVisible(false);
  };

  const ref = useRef<HTMLDivElement>(null);
  useDoubleClick({ ref, onDoubleClick, onClick, stopPropagation: true });

  return (
    <>
      {data?.map((value: any) => (
        <StyledListCardContainer>
          <StyledListCard
            ref={ref}
            hoverable
            key={value.id}
            selected={value?.id === selectedCardId}
            onClick={(event) => {
              setSelectedCardId(value.id);
              handleOnClick(event, value);
            }}
            onDoubleClick={(event) => {
              setSelectedCardId(value.id);
              handleOnDoubleClick(event, value);
            }}
          >
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
                    if (setIsSiderVisible) {
                      setIsSiderVisible(false);
                    }
                  }}
                />
              </StyledDescriptionButtons>
            </StyledDescription>
          </StyledListCard>
        </StyledListCardContainer>
      ))}
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
