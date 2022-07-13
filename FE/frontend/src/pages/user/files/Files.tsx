import { FC, useEffect, useState } from 'react';

import { usePageContext } from 'context/PageContext';
import ListCards from 'components/cards/list/ListCards';
import GridCards from 'components/cards/grid/GridCards';
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
} from './styles/files-styled';
import FileSider from './sider/FileSider';
import FileModal from './modal/FileModal';
import useFilesData from './hooks/useFilesData';

const Files: FC = () => {
  const [isListView, setIsListView] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();
  const { data, isLoading, error } = useFilesData();

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
              <h2 style={{ flexBasis: '44%' }}>Files</h2>
              {!isListView && <h4>Uploaded</h4>}
              <div>
                <PFilter
                  isList={!isListView}
                  setIsList={setIsListView}
                  activeFilters={{
                    showSortByTime: true,
                    showSortByType: true,
                    showToggleButton: true,
                  }}
                />
              </div>
            </div>
            <StyledFilesWrapper>
              {!isListView ? (
                <ListCards
                  data={data}
                  cardData={cardData}
                  setCardData={setCardData}
                  selectedCardId={selectedCardId}
                  setSelectedCardId={setSelectedCardId}
                />
              ) : (
                <GridCards
                  data={data}
                  cardData={cardData}
                  setCardData={setCardData}
                  selectedCardId={selectedCardId}
                  setSelectedCardId={setSelectedCardId}
                />
              )}
            </StyledFilesWrapper>
          </StyledFilesContainer>
        </StyledStaticContent>

        <FileSider
          cardData={cardData}
          setCardData={setCardData}
          setSelectedCardId={setSelectedCardId}
          type="folder"
        />
      </StyledContent>

      <FileModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Files;
