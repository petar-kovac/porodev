import {
  Dispatch,
  FC,
  MouseEvent,
  SetStateAction,
  useEffect,
  useState,
} from 'react';

import { IFilesCard } from 'types/card-data';
import { useFetchData } from 'hooks/useFetchData';
import { usePageContext } from 'context/PageContext';

import { findFiles, downloadFile } from 'service/files/files';

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
  setSelectedCardId = () => undefined,
}) => {
  const [data, setData] = useState<any>(undefined);

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  useEffect(() => {
    const fetchFiles = async () => {
      const res = await findFiles();
      setData(res);
    };

    fetchFiles();
  }, []);

  const handleClick = (value: any) => {
    setSelectedCardId(value.fileId);
    setCardData(value);
    setIsSiderVisible(true);
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
          <ListCard
            fileId={value.fileId}
            fileName={value.fileName}
            data={data}
            value={value}
            selected={selectedCardId === value.fileId}
            key={value.fileId}
            setSelectedCardId={setSelectedCardId}
            onClick={() => handleClick(value)}
            onDoubleClick={() => handleDoubleClick(value)}
          />
        ))
        .reverse()}
    </>
  );
};

export default ListCards;
