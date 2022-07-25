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
  isAdmin?: boolean;
  cardData?: IFilesCard | null;
  data?: IFilesCard[] | null;
  selected?: boolean;
  selectedCardId?: number | null;
  onClick?: (event: MouseEvent) => unknown;
  onDoubleClick?: (event: MouseEvent) => unknown;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;

  searchTerm?: any;
  searchRes?: any;
  filteredResults?: any;
}

const ListCards: FC<IListCardProps> = ({
  isAdmin,
  selectedCardId,
  setCardData = () => undefined,
  setSelectedCardId = () => undefined,
  data,

  searchTerm,
  searchRes,
  filteredResults,
}) => {
  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  // console.log(data);

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

  return searchRes.map((value: any) => {
    return (
      <ListCard
        data={data}
        value={value}
        isAdmin={isAdmin}
        fileId={value.id}
        fileName={value.filename}
        userName={value.userName}
        userLastName={value.userLastname}
        selected={selectedCardId === value.id}
        key={value.id}
        setSelectedCardId={setSelectedCardId}
        onClick={() => handleClick(value)}
        onDoubleClick={() => handleDoubleClick(value)}
      />
    );
  });

  // <>
  //   {searchTerm.length > 0
  //     ? searchRes.map((value: any) => {
  //         return (
  //           <ListCard
  //             data={data}
  //             value={value}
  //             isAdmin={isAdmin}
  //             fileId={value.id}
  //             fileName={value.filename}
  //             userName={value.userName}
  //             userLastName={value.userLastname}
  //             selected={selectedCardId === value.id}
  //             key={value.id}
  //             setSelectedCardId={setSelectedCardId}
  //             onClick={() => handleClick(value)}
  //             onDoubleClick={() => handleDoubleClick(value)}
  //           />
  //         );
  //       })
  //     : // data
  //       //     ?.map((value: any) => (
  //       //       <ListCard
  //       //         isAdmin={isAdmin}
  //       //         fileId={value.fileId}
  //       //         fileName={value.fileName}
  //       //         userName={value.userName}
  //       //         userLastName={value.userLastName}
  //       //         data={data}
  //       //         value={value}
  //       //         selected={selectedCardId === value.fileId}
  //       //         key={value.fileId}
  //       //         setSelectedCardId={setSelectedCardId}
  //       //         onClick={() => handleClick(value)}
  //       //         onDoubleClick={() => handleDoubleClick(value)}
  //       //       />
  //       //     ))
  //       //     .reverse()}

  //       searchRes
  //         ?.map((value: any) => (
  //           <ListCard
  //             isAdmin={isAdmin}
  //             fileId={value.id}
  //             fileName={value.filename}
  //             userName={value.userName}
  //             userLastName={value.userLastname}
  //             data={data}
  //             value={value}
  //             selected={selectedCardId === value.id}
  //             key={value.id}
  //             setSelectedCardId={setSelectedCardId}
  //             onClick={() => handleClick(value)}
  //             onDoubleClick={() => handleDoubleClick(value)}
  //           />
  //         ))
  //         .reverse()}
  // </>
};

export default ListCards;
