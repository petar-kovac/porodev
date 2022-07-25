import { FC, useEffect, useState } from 'react';

import { searchFiles } from 'service/files/files';

import { usePageContext } from 'context/PageContext';
import ListCards from 'components/cards/list/ListCards';
import GridCards from 'components/cards/grid/GridCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFolders from 'components/folders/PFolders';
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

  const { setIsSiderVisible, setIsModalVisible, isCollapsed } =
    usePageContext();
  const { data, isLoading, error } = useFilesData();

  // search filter
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [searchRes, setSearchRes] = useState<any>([]);
  const [filteredResults, setFilteredResults] = useState([]);

  useEffect(() => {
    searchFiles(searchTerm)
      .then((res) => {
        setSearchRes(res);
        console.log(res);
      })
      .catch((err) => console.log(err));
  }, [searchTerm]);

  const handleFilter = (e: any) => {
    const searchWord = e.target.value;
    setSearchTerm(searchWord);

    // const filteredData = searchRes.filter((value: any) => {
    //   // return value.filename.toLowerCase().includes(searchTerm.toLowerCase());
    //   return value.filename.toLowerCase().startsWith(searchTerm.toLowerCase());
    // });

    // if (searchWord === '') {
    //   setFilteredResults([]);
    // } else {
    //   setFilteredResults(filteredData);
    // }
  };

  const clearInput = () => {
    setFilteredResults([]);
    setSearchTerm('');
  };

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
              handleFilter={handleFilter}
              searchTerm={searchTerm}
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
              {/* <PFolders
                cardData={cardData}
                setCardData={setCardData}
                selectedCardId={selectedCardId}
                setSelectedCardId={setSelectedCardId}
              /> */}
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
                <StyledListCardsWrapper>
                  <ListCards
                    searchTerm={searchTerm}
                    searchRes={searchRes}
                    filteredResults={filteredResults}
                    data={data}
                    cardData={cardData}
                    setCardData={setCardData}
                    selectedCardId={selectedCardId}
                    setSelectedCardId={setSelectedCardId}
                  />
                  {/* <ListCards
                    data={data}
                    cardData={cardData}
                    setCardData={setCardData}
                    selectedCardId={selectedCardId}
                    setSelectedCardId={setSelectedCardId}
                  /> */}
                </StyledListCardsWrapper>
              ) : (
                <StyledGridCardsWrapper>
                  <GridCards
                    data={data}
                    cardData={cardData}
                    setCardData={setCardData}
                    selectedCardId={selectedCardId}
                    setSelectedCardId={setSelectedCardId}
                  />
                </StyledGridCardsWrapper>
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
