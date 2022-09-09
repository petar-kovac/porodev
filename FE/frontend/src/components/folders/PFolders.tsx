import { FC, MouseEventHandler, Dispatch, SetStateAction } from 'react';
import { useFetchData } from 'hooks/useFetchData';
// import { IPFoldersProps } from 'types/folder-props';
import { usePageContext } from 'context/PageContext';
import { IFilesCard } from 'types/card-data';
import { useNavigate } from 'react-router-dom';
import PFolder from './PFolder';

interface IPFoldersProps {
  id?: string;
  heading?: string;
  description?: string;
  selected?: boolean | null;
  selectedCardId?: number | null;
  cardData?: IFilesCard | null;
  data?: any;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
  setIsSiderVisible?: Dispatch<SetStateAction<boolean>>;
  setIsModalVisible?: Dispatch<SetStateAction<boolean>>;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  onClick?: MouseEventHandler<HTMLElement>;
  onDoubleClick?: MouseEventHandler<HTMLElement>;
}

const PFolders: FC<IPFoldersProps> = ({
  selectedCardId,
  data,
  setCardData = () => undefined,
  setSelectedCardId = () => undefined,
}) => {
  const { setIsSiderVisible, setIsModalVisible, setSharedSpaceId } =
    usePageContext();
  const navigate = useNavigate();

  const handleClick = (value: any) => {
    setIsSiderVisible(true);
    setCardData(value);
    setSelectedCardId(value.sharedSpaceId);
    // setSharedSpaceId(value)
  };

  const handleDoubleClick = (value: any) => {
    setSelectedCardId(value.sharedSpaceId);
    setCardData(value);
    // setIsSiderVisible(false);
    navigate(`/shared-spaces/${value.sharedSpaceId}`);

    // setIsModalVisible(true);
  };

  return (
    <>
      {data?.map((value: any) => (
        <PFolder
          key={value.sharedSpaceId}
          value={value}
          heading={value?.sharedSpaceName}
          description={value?.description}
          selected={selectedCardId === value.sharedSpaceId}
          onClick={() => handleClick(value)}
          onDoubleClick={() => handleDoubleClick(value)}
        />
      ))}
    </>
  );
};

export default PFolders;
