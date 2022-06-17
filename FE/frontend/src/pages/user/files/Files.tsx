import axios from 'axios';
import GridCard from 'components/card/GridCard';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import ListCard from 'components/card/ListCard';
import PFilter from 'components/filter/PFilter';
import PFolders from 'components/folders/PFolders';
import PModal from 'components/modal/PModal';
import PFileSider, { IFilesCard } from 'layout/sider/PFileSider';

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
const PFilterWrapper = styled.div`
  width: 100%;
  display: flex;
  align-items: flex-end;
  gap: 1rem;
  padding: 2rem 0;
  justify-content: space-around;
  flex-wrap: wrap;
  background-color: #fcfcfc;
  border-radius: 3rem;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-bottom: 2px solid #ddd;
`;

const StyledPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const StyledContent = styled.div`
  display: flex;
`;

const StyledStaticContent = styled.div`
  display: flex;
  flex-direction: column;
  width: 80%;
  margin: 0 auto;
  align-items: flex-start;
  gap: 5rem;
`;

const StyledFoldersContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  margin-top: 3rem;
  gap: 2rem;
`;

const StyledFilesWrapper = styled.div`
  padding: 0 2rem;
  display: flex;
  justify-content: center;
  gap: 2.5rem;
  flex-wrap: wrap;
`;

export default Files;
