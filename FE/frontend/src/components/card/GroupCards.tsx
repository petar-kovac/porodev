import { FC, Dispatch, SetStateAction } from 'react';

import { useFetchData } from 'hooks/useFetchData';

import { IFilesCard } from 'types/card-data';

import GroupCard from 'components/card/GroupCard';

interface IGroupCardProps {
  selected?: boolean | null;
  selectedCardId?: number | null;
  cardData?: IFilesCard | null;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
  setIsSiderVisible?: Dispatch<SetStateAction<boolean>>;
  setIsModalVisible?: Dispatch<SetStateAction<boolean>>;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
}

const GroupCards: FC<IGroupCardProps> = ({
  selectedCardId,
  setCardData = () => undefined,
  setSelectedCardId = () => undefined,
  setIsModalVisible = () => undefined,
  setIsSiderVisible = () => undefined,
}) => {
  const url = `${process.env.REACT_APP_MOCK_URL}/groups`;
  const { data } = useFetchData(url);
  console.log(data);

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
        <GroupCard
          groupName={value.groupName}
          isModerator={value.isModerator}
          moderatorName={value.moderatorName}
          numberOfFiles={value.numberOfFiles}
          numberOfUsers={value.numberOfUsers}
          selected={selectedCardId === value.id}
          onClick={() => handleClick(value)}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default GroupCards;
