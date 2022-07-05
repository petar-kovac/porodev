import { FC, useState } from 'react';

import ListCards from 'components/card/ListCards';
import GridCards from 'components/card/GridCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFolders from 'components/folders/PFolders';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
import { useFetchData } from 'hooks/useFetchData';
import {
  PFilterWrapper,
  StyledContent,
  StyledFilesWrapper,
  StyledFoldersContainer,
  StyledPageWrapper,
  StyledStaticContent,
} from './files-styled';

const Files: FC = () => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  return (
    <StyledPageWrapper>
      <StyledContent
        onClick={() => {
          setSelectedCardId(null);
          setIsSiderVisible(false);
          setCardData(null);
        }}
      >
        <StyledStaticContent>
          <PFilterWrapper>
            <PFilter
              isList={isList}
              setIsList={setIsList}
              activeFilters={{
                showFilterByDate: true,
                showSortByTime: true,
                showSortByType: true,
                showFilterBySize: true,
                showToggleButton: true,
              }}
            />
          </PFilterWrapper>
          <StyledFoldersContainer>
            <PFolders
              cardData={cardData}
              setIsSiderVisible={setIsSiderVisible}
              setIsModalVisible={setIsModalVisible}
              setCardData={setCardData}
              selectedCardId={selectedCardId}
              setSelectedCardId={setSelectedCardId}
            />
          </StyledFoldersContainer>

          <StyledFilesWrapper>
            {isList ? (
              <ListCards
                cardData={cardData}
                setIsSiderVisible={setIsSiderVisible}
                setIsModalVisible={setIsModalVisible}
                setCardData={setCardData}
                selectedCardId={selectedCardId}
                setSelectedCardId={setSelectedCardId}
              />
            ) : (
              <GridCards
                cardData={cardData}
                setCardData={setCardData}
                setIsSiderVisible={setIsSiderVisible}
                setIsModalVisible={setIsModalVisible}
                selectedCardId={selectedCardId}
                setSelectedCardId={setSelectedCardId}
              />
            )}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          cardData={cardData}
          setCardData={setCardData}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
          setSelectedCardId={setSelectedCardId}
          type="folder"
        />
      </StyledContent>

      <PModal
        isModalVisible={isModalVisible}
        cardData={cardData}
        setIsModalVisible={setIsModalVisible}
        setCardData={setCardData}
      />
    </StyledPageWrapper>
  );
};

export default Files;
