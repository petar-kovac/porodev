import { FC, useEffect, useState } from 'react';

import { usePageContext } from 'context/PageContext';
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
  const [isListView, setIsListView] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  useEffect(() => {
    setIsSiderVisible(false);
  }, []);

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
              isList={isListView}
              setIsList={setIsListView}
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
            {isListView ? (
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
          setSelectedCardId={setSelectedCardId}
          type="folder"
        />
      </StyledContent>

      <PModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Files;
