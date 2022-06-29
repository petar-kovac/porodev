import axios from 'axios';
import GridCard from 'components/card/GridCard';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import ListCard from 'components/card/ListCard';
import PFilter from 'components/filter/PFilter';
import PFolders from 'components/folders/PFolders';
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
  const [data, setData] = useState<IFilesCard[] | undefined>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/files`);
        setData(res.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <StyledPageWrapper>
      <StyledContent>
        <StyledStaticContent
          onClick={() => {
            setIsSiderVisible(false);
            setCardData(null); // to trigger rerender, simulating onBlur effect
          }}
        >
          <StyledFoldersContainer>
            {data?.slice(0, 4).map((value: any) => (
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
            ))}
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
            {data?.map((value: any) => (
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
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          cardData={cardData}
          setCardData={setCardData}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
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
