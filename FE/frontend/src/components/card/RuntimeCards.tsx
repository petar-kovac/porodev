import { FC, Dispatch, SetStateAction } from 'react';

import { useFetchData } from 'hooks/useFetchData';

import { IFilesCard } from 'types/card-data';
import { usePageContext } from 'context/PageContext';

import GroupCard from 'components/card/GroupCard';
import RuntimeCard from './RuntimeCard';

interface IGroupCardProps {
  selected?: boolean | null;
  selectedCardId?: number | null;
  cardData?: IFilesCard | null;
  data?: IFilesCard[] | null;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
  setIsSiderVisible?: Dispatch<SetStateAction<boolean>>;
  setIsModalVisible?: Dispatch<SetStateAction<boolean>>;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
}

const RuntimeCards: FC<IGroupCardProps> = ({
  selectedCardId,
  data,
  setCardData = () => undefined,
  setSelectedCardId = () => undefined,
  setIsModalVisible = () => undefined,
  setIsSiderVisible = () => undefined,
}) => {
  const {
    setInputParameters,
    setNumberOfInputFields,
    setProjectId,
    setImageParameters,
  } = usePageContext();

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
      {data?.slice(0, 3).map((value: any) => (
        <RuntimeCard
          value={value}
          key={value.fileId}
          image={value.image}
          heading={value.fileName}
          description={value.description}
          selected={selectedCardId === value.fileId}
          onClick={() => {
            handleClick(value);
            setNumberOfInputFields(1);
            setInputParameters([]);
            setImageParameters([]);
            setProjectId(value.fileId);
          }}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default RuntimeCards;
