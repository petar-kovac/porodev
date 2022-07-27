import { Dispatch, FC, MouseEvent, SetStateAction } from 'react';

import { IFilesCard } from 'types/card-data';

import { usePageContext } from 'context/PageContext';
import GridCard from './GridCard';

interface IGridCardProps {
  home?: boolean;
  selected?: boolean;
  cardData?: IFilesCard | null;
  data?: IFilesCard[] | null;
  selectedCardId?: number | null;
  searchTerm?: string;
  searchRes?: any;
  isSharedSpace?: boolean;
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
  searchTerm,
  isSharedSpace,
  searchRes,
  home,
}) => {
  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  const handleClick = (value: any) => {
    setIsSiderVisible(true);
    setCardData(value);
    if (isSharedSpace) {
      setSelectedCardId(value.fileId);
    } else {
      setSelectedCardId(value.id);
    }
  };

  const handleDoubleClick = (value: any) => {
    setSelectedCardId(value.fileId);
    setCardData(value);
    setIsSiderVisible(false);
    setIsModalVisible(true);
  };

  return !isSharedSpace ? (
    <>
      {searchRes
        ?.map((value: any) => (
          <GridCard
            home
            value={value}
            key={value.id}
            fileId={value.id}
            // image={value.image}
            fileName={value.filename}
            // description={value.description}
            time={value.uploadDateTime}
            selected={selectedCardId === value.id}
            fileExtension={value.filename.split('.')[1]}
            onClick={() => handleClick(value)}
            onDoubleClick={() => handleDoubleClick(value)}
          />
        ))
        .reverse()}
    </>
  ) : (
    <>
      {data
        ?.map((value: any) => (
          <GridCard
            value={value}
            key={value.fileId}
            fileId={value.fileId}
            // image={value.image}
            fileName={value.filename}
            // description={value.description}
            selected={selectedCardId === value.fileId}
            fileExtension={value.filename?.split('.')[1]}
            onClick={() => handleClick(value)}
            onDoubleClick={() => handleDoubleClick(value)}
          />
        ))
        .reverse()}
    </>
  );
};

export default GridCards;
