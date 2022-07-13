import { Dispatch, FC, MouseEvent, SetStateAction } from 'react';

import { IFilesCard } from 'types/card-data';

import { usePageContext } from 'context/PageContext';
import GridCard from './GridCard';

interface IGridCardProps {
  selected?: boolean;
  cardData?: IFilesCard | null;
  data?: IFilesCard[] | null;
  selectedCardId?: number | null;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
}

const GridCards: FC<IGridCardProps> = ({
  selectedCardId,
  setCardData = () => undefined,
  setSelectedCardId = () => undefined,
  data,
}) => {
  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  const handleClick = (value: any) => {
    setIsSiderVisible(true);
    setCardData(value);
    setSelectedCardId(value.fileId);
  };

  const handleDoubleClick = (value: any) => {
    setSelectedCardId(value.fileId);
    setCardData(value);
    setIsSiderVisible(false);
    setIsModalVisible(true);
  };

  return (
    <>
      {data?.map((value: any) => (
        <GridCard
          value={value}
          key={value.fileId}
          image={value.image}
          heading={value.fileName}
          description={value.description}
          selected={selectedCardId === value.fileId}
          fileExtension={value.fileName.split('.')[1]}
          onClick={() => handleClick(value)}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default GridCards;
