import { FC, Dispatch, MouseEvent, SetStateAction } from 'react';

import { useFetchData } from 'hooks/useFetchData';
import { IFilesCard } from 'types/card-data';

import GridCard from './GridCard';

interface IGridCardProps {
  selected?: boolean;
  cardData?: IFilesCard | null;
  selectedCardId?: number | null;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
  setIsSiderVisible?: Dispatch<SetStateAction<boolean>>;
  setIsModalVisible?: Dispatch<SetStateAction<boolean>>;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
}

const GridCards: FC<IGridCardProps> = ({
  selectedCardId,
  setCardData = () => undefined,
  setIsSiderVisible = () => undefined,
  setSelectedCardId = () => undefined,
  setIsModalVisible = () => undefined,
}) => {
  const url = `${process.env.REACT_APP_MOCK_URL}/files`;
  const { data } = useFetchData(url);

  const handleClick = (value: any) => {
    setIsSiderVisible(true);
    setCardData(value);
    setSelectedCardId(value.id);
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
        <GridCard
          key={value.id}
          value={value}
          selected={selectedCardId === value.id}
          setIsSiderVisible={setIsSiderVisible}
          setSelectedCardId={setSelectedCardId}
          onClick={() => handleClick(value)}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default GridCards;
