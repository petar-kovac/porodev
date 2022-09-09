import { FC, useEffect, useState } from 'react';

import { searchFiles } from 'service/files/files';

import { usePageContext } from 'context/PageContext';
import ListCards from 'components/cards/list/ListCards';
import GridCards from 'components/cards/grid/GridCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFolders from 'components/folders/PFolders';
import { IFilesCard } from 'types/card-data';
import { Spin } from 'antd';
import {
  PFilterWrapper,
  StyledContent,
  StyledFilesWrapper,
  StyledFoldersWrapper,
  StyledFilesContainer,
  StyledFoldersContainer,
  StyledPageWrapper,
  StyledStaticContent,
  StyledGridCardsWrapper,
  StyledListCardsWrapper,
} from './styles/files-styled';
import FileSider from './sider/FileSider';
import FileModal from './modal/FileModal';
import useFilesData from './hooks/useFilesData';

const Files: FC = () => {
  const [isListView, setIsListView] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const {
    setIsSiderVisible,
    setIsModalVisible,
    isCollapsed,
    isLoading,
    setIsLoading,
  } = usePageContext();
  const { data, error, setData } = useFilesData();

  console.log(data);
  // search filter
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [searchRes, setSearchRes] = useState<[]>();

  useEffect(() => {
    setIsLoading(true);
    searchFiles(searchTerm)
      .then((res) => {
        console.log(res);
        setSearchRes(res);

        setIsLoading(false);
      })
      .catch((err) => console.log(err));
  }, [searchTerm]);

  return (
    <StyledPageWrapper>
      <StyledContent
        onClick={() => {
          setSelectedCardId(null);
          setIsSiderVisible(false);
          setCardData(null);
        }}
      >
        <StyledStaticContent isCollapsed={isCollapsed}>
          <PFilterWrapper>
            <PFilter
              searchTerm={searchTerm}
              setSearchTerm={setSearchTerm}
              isList={isListView}
              setIsList={setIsListView}
              activeFilters={{
                showFilterByDate: true,
                showFilterBySize: true,
              }}
            />
          </PFilterWrapper>

          {/* <StyledFoldersContainer>
             <h2>Folders</h2> 
            <StyledFoldersWrapper>
               <PFolders
                cardData={cardData}
                setCardData={setCardData}
                selectedCardId={selectedCardId}
                setSelectedCardId={setSelectedCardId}
              /> 
            </StyledFoldersWrapper>
          </StyledFoldersContainer> */}
          <StyledFilesContainer>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between',
              }}
            >
              <h2 style={{ flexBasis: '44%' }}>Files</h2>

              {/* {!isListView && <h4>Uploaded</h4>} */}

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
            {isLoading ? (
              <Spin />
            ) : (
              <>
                {searchRes?.length === 0 ? (
                  <p>No files found</p>
                ) : (
                  <>
                    <StyledFilesWrapper>
                      {!isListView ? (
                        <StyledListCardsWrapper>
                          <ListCards
                            searchTerm={searchTerm}
                            searchRes={searchRes}
                            data={data}
                            cardData={cardData}
                            setCardData={setCardData}
                            selectedCardId={selectedCardId}
                            setSelectedCardId={setSelectedCardId}
                          />
                        </StyledListCardsWrapper>
                      ) : (
                        <StyledGridCardsWrapper>
                          <GridCards
                            searchTerm={searchTerm}
                            searchRes={searchRes}
                            data={data}
                            cardData={cardData}
                            setCardData={setCardData}
                            selectedCardId={selectedCardId}
                            setSelectedCardId={setSelectedCardId}
                          />
                        </StyledGridCardsWrapper>
                      )}
                    </StyledFilesWrapper>
                  </>
                )}
              </>
            )}
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
