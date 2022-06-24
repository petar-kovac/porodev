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
  const [cardData, setCardData] = useState<IFilesCard | undefined>(undefined);

  const count = useRef(0);

  const onClick = useCallback((value: any) => {
    count.current += 1;
    setCardData(value);
    setTimeout(() => {
      if (count.current === 1) {
        setIsSiderVisible(true);
      }

      if (count.current === 2) {
        setIsSiderVisible(false);
        setIsModalVisible(true);
      }

      count.current = 0;
    }, 250);
  }, []);

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
        <StyledStaticContent>
          <StyledFoldersContainer>
            {data?.slice(0, 4).map((value: any) => (
              <PFolders
                heading={value?.name}
                description={value?.description}
                onClick={() => onClick(value)}
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
                    image={value?.image}
                    heading={value?.name}
                    description={value?.description}
                    onClick={() => onClick(value)}
                  />
                ) : (
                  <GridCard
                    image={value?.image}
                    heading={value?.name}
                    description={value?.description}
                    onClick={() => onClick(value)}
                  />
                )}
              </>
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          cardData={cardData}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
          type="folder"
        />
      </StyledContent>

      <PModal
        isModalVisible={isModalVisible}
        cardData={cardData}
        setIsModalVisible={setIsModalVisible}
      />
    </StyledPageWrapper>
  );
};

export default Files;
