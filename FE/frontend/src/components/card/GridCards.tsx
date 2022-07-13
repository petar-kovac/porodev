import {
  FC,
  Dispatch,
  MouseEvent,
  SetStateAction,
  useState,
  useEffect,
} from 'react';

import { useFetchData } from 'hooks/useFetchData';
import { IFilesCard } from 'types/card-data';

import { findFiles, downloadFile } from 'service/files/files';

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
  const [data, setData] = useState<any>(undefined);

  useEffect(() => {
    const fetchFiles = async () => {
      const res = await findFiles();
      setData(res);
    };

    fetchFiles();
  }, []);

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
      {data?.content
        .map((value: any) => (
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
        ))
        .reverse()}
    </>
  );
};

export default GridCards;
