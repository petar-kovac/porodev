import { Dispatch, FC, MouseEvent, SetStateAction, useState } from 'react';

import { IFilesCard } from 'types/card-data';
import { useFetchData } from 'hooks/useFetchData';

import ListCard from './ListCard';

interface IListCardProps {
  cardData?: IFilesCard | null;
  selected?: boolean;
  selectedCardId?: number | null;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setIsSiderVisible?: Dispatch<SetStateAction<boolean>>;
  setIsModalVisible?: Dispatch<SetStateAction<boolean>>;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
}

const ListCards: FC<IListCardProps> = ({
  selectedCardId,
  setCardData = () => undefined,
  setIsSiderVisible = () => undefined,
  setIsModalVisible = () => undefined,
  setSelectedCardId = () => undefined,
}) => {
  const url = `${process.env.REACT_APP_MOCK_URL}/files`;
  const { data } = useFetchData(url);

  const handleClick = (value: any) => {
    setSelectedCardId(value.id);
    setCardData(value);
    setIsSiderVisible(true);
  };

  const handleDoubleClick = (value: any) => {
    setSelectedCardId(value.id);
    setCardData(value);
    setIsSiderVisible(false);
    setIsModalVisible(true);
  };

  return (
    <>
      {data?.map((value: any) => (
        <ListCard
          value={value}
          selected={selectedCardId === value.id}
          key={value.id}
          setSelectedCardId={setSelectedCardId}
          setIsSiderVisible={setIsSiderVisible}
          onClick={() => handleClick(value)}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default ListCards;
