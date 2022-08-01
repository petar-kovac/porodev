import { FC, useState } from 'react';

import GridCards from 'components/cards/grid/GridCards';
import ListCards from 'components/cards/list/ListCards';
import PFilter from 'components/filter/PFilter';
import { usePageContext } from 'context/PageContext';
import { useParams } from 'react-router-dom';
import { IFilesCard } from 'types/card-data';

import { PlusOutlined } from '@ant-design/icons';
import useFilesData from './hooks/useFilesData';
import FileModal from './modal/FileModal';
import FileSider from './sider/FileSider';
import {
  StyledContent,
  StyledFilesContainer,
  StyledFilesWrapper,
  StyledGridCardsWrapper,
  StyledListCardsWrapper,
  StyledPageWrapper,
  StyledPlusCircle,
  StyledStaticContent,
} from './styles/files-styled';

const SingleGroup: FC = () => {
  const [isListView, setIsListView] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible, setIsModalVisible, isCollapsed, userTrigger } =
    usePageContext();
  const { id } = useParams();
  const { data, isLoading, error, setData } = useFilesData(id as string);

  // search filter
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [searchRes, setSearchRes] = useState<any>([]);

  const onAddFile = () => {
    setIsModalVisible(true);
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
          <StyledFilesContainer>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between',
              }}
            >
              <h2>Shared space files</h2>
              <StyledPlusCircle
                onClick={onAddFile}
                type="primary"
                shape="circle"
                icon={
                  <PlusOutlined style={{ color: '#fff', fontSize: '2.4rem' }} />
                }
              />

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
                    isSharedSpace
                    searchTerm={searchTerm}
                    searchRes={searchRes}
                    data={data}
                    setData={setData}
                    cardData={cardData}
                    setCardData={setCardData}
                    selectedCardId={selectedCardId}
                    setSelectedCardId={setSelectedCardId}
                  />
                </StyledListCardsWrapper>
              ) : (
                <StyledGridCardsWrapper>
                  <GridCards
                    isSharedSpace
                    searchTerm={searchTerm}
                    searchRes={searchRes}
                    data={data}
                    setData={setData}
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

export default SingleGroup;
