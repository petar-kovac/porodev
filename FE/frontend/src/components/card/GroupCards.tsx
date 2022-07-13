import { FC, Dispatch, SetStateAction } from 'react';

import { useFetchData } from 'hooks/useFetchData';

import { IFilesCard } from 'types/card-data';

import GroupCard from 'components/card/GroupCard';
import { usePageContext } from 'context/PageContext';

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

const GroupCards: FC<IGroupCardProps> = ({
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
        <GroupCard
          groupName={value.groupName}
          isModerator={value.isModerator}
          moderatorName={value.moderatorName}
          numberOfFiles={value.numberOfFiles}
          numberOfUsers={value.numberOfUsers}
          selected={selectedCardId === value.fileId}
          onClick={() => handleClick(value)}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default GroupCards;
