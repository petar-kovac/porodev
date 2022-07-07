import { FC, useEffect, useState } from 'react';

import { usePageContext } from 'context/PageContext';
import ListCards from 'components/card/ListCards';
import GridCards from 'components/card/GridCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFolders from 'components/folders/PFolders';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
import {
  PFilterWrapper,
  StyledContent,
  StyledFilesWrapper,
  StyledFoldersWrapper,
  StyledFilesContainer,
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
                showFilterBySize: true,
              }}
            />
          </PFilterWrapper>

          <StyledFoldersContainer>
            <h2>Folders</h2>
            <StyledFoldersWrapper>
              <PFolders
                cardData={cardData}
                setIsSiderVisible={setIsSiderVisible}
                setIsModalVisible={setIsModalVisible}
                setCardData={setCardData}
                selectedCardId={selectedCardId}
                setSelectedCardId={setSelectedCardId}
              />
            </StyledFoldersWrapper>
          </StyledFoldersContainer>
          <StyledFilesContainer>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between',
              }}
            >
              <h2>Files</h2>
              <PFilter
                isList={isListView}
                setIsList={setIsListView}
                activeFilters={{
                  showSortByTime: true,
                  showSortByType: true,
                  showToggleButton: true,
                }}
              />
            </div>
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
          </StyledFilesContainer>
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
