import axios from 'axios';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';
import { Layout } from 'antd';

import GroupCard from 'components/card/GroupCard';
import ListCard from 'components/card/ListCard';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IGroupCard } from 'types/card-data';

const { Sider, Content } = Layout;

const Groups: FC = () => {
  const [data, setData] = useState<IGroupCard[] | undefined>();
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IGroupCard | undefined>(undefined);

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
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/groups`);
        setData(res.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <Layout>
      <StyledContent>
        <StyledHeadingWrapper>
          <StyledHeadingText>Your groups: </StyledHeadingText>
          <StyledFilterWrapper>
            <PFilter
              isList={isList}
              setIsList={setIsList}
              activeFilters={{
                showSortByType: true,
                showSortByTime: true,
              }}
            />
          </StyledFilterWrapper>
        </StyledHeadingWrapper>
        <StyledStaticContent>
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
                  <GroupCard
                    groupName={value.groupName}
                    isModerator={value.isModerator}
                    moderatorName={value.moderatorName}
                    numberOfFiles={value.numberOfFiles}
                    numberOfUsers={value.numberOfUsers}
                    onClick={() => onClick(value)}
                    uuid={value.uuid}
                  />
                )}
              </>
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PModal
          isModalVisible={isModalVisible}
          setIsModalVisible={setIsModalVisible}
          cardData={cardData}
        />
      </StyledContent>
      <PFileSider
        isSiderVisible={isSiderVisible}
        setIsSiderVisible={setIsSiderVisible}
        cardData={cardData}
        type="file"
      />
    </Layout>
  );
};
const StyledContent = styled(Content)`
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
`;

const StyledFilterWrapper = styled.div`
  display: flex;
  flex: 1;
  align-items: flex-end;
  justify-content: flex-end;
`;
const StyledHeading = styled.div`
  display: flex;
  flex: 1;
  justify-content: flex-end;
`;
export const StyledHeadingText = styled.div`
  font-size: 24px;
  color: ${({ theme: { colors } }) => colors.primary};
  font-weight: 600;
`;

const StyledHeadingWrapper = styled.div`
  width: 100%;
  padding: 20px;
  align-items: center;
  display: flex;
`;

const StyledPageWrapper = styled.div`
  display: flex;
`;

// const StyledContent = styled.div`
//   display: flex;
// `;

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
  width: 90%; // fix not to break card until grid has been implemented
  flex-wrap: wrap;
`;

export default Groups;