import { FC, useState } from 'react';

import ListCards from 'components/card/ListCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
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
          <StyledFoldersContainer>
            {/* {data?.slice(0, 4).map((value: any) => (
              <PFolders
                key={value.id}
                heading={value.name}
                description={value.description}
                selected={value.id === cardData?.id}
                onClick={(e) => {
                  setCardData(value);
                  e.stopPropagation();
                  setIsSiderVisible(true);
                }}
                onDoubleClick={(e) => {
                  e.stopPropagation();
                  setCardData(value);
                  setIsSiderVisible(false);
                  setIsModalVisible(true);
                }}
              />
            ))} */}
          </StyledFoldersContainer>
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
          <StyledFilesWrapper>
            {/* {data?.map((value: any) => (
              <>
                {isList ? (
                  <ListCard
                    key={value.id}
                    image={value.image}
                    heading={value.name}
                    description={value.description}
                    selected={value.id === cardData?.id}
                    onClick={(e) => {
                      setCardData(value);
                      e.stopPropagation();
                      setIsSiderVisible(true);
                    }}
                    onDoubleClick={(e) => {
                      e.stopPropagation();
                      setCardData(value);
                      setIsSiderVisible(false);
                      setIsModalVisible(true);
                    }}
                    setIsSiderVisible={setIsSiderVisible}
                  />
                ) : (
                  <GridCard
                    key={value.id}
                    image={value.image}
                    heading={value.name}
                    description={value.description}
                    selected={value.id === cardData?.id}
                    onClick={(e) => {
                      setCardData(value);
                      e.stopPropagation();
                      setIsSiderVisible(true);
                    }}
                    onDoubleClick={(e) => {
                      e.stopPropagation();
                      setCardData(value);
                      setIsSiderVisible(false);
                      setIsModalVisible(true);
                    }}
                  />
                )}
              </>
            ))} */}
            <ListCards
              cardData={cardData}
              setIsSiderVisible={setIsSiderVisible}
              setIsModalVisible={setIsModalVisible}
              setCardData={setCardData}
              selectedCardId={selectedCardId}
              setSelectedCardId={setSelectedCardId}

              // onClick={(e) => {
              //   setCardData(value);
              //   e.stopPropagation();
              //   setIsSiderVisible(true);
              // }}
              // onDoubleClick={(e) => {
              //   e.stopPropagation();
              //   setCardData(value);
              //   setIsSiderVisible(false);
              //   setIsModalVisible(true);
              // }}
              // setIsSiderVisible={setIsSiderVisible}
            />
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
