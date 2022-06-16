import axios from 'axios';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import GroupCard from 'components/card/GroupCard';
import ListCard from 'components/card/ListCard';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';

const Groups: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [cardData, setCardData] = useState<any>();
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);

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
    }, 300);
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
    <StyledPageWrapper>
      <StyledContent>
        <StyledStaticContent>
          <PFilter isList={isList} setIsList={setIsList} />
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

        <PFileSider
          title={cardData?.groupName}
          content={cardData?.moderatorName}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
        />
      </StyledContent>

      <PModal
        isModalVisible={isModalVisible}
        title={cardData?.groupName}
        content={cardData?.moderatorName}
        setIsModalVisible={setIsModalVisible}
      />
    </StyledPageWrapper>
  );
};

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

export default Groups;
