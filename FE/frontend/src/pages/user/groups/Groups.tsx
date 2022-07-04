import { Layout } from 'antd';
import axios from 'axios';
import { FC, useCallback, useEffect, useRef, useState } from 'react';

import GroupCard from 'components/card/GroupCard';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IGroupCard } from 'types/card-data';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledFilterWrapper,
  StyledHeadingText,
  StyledHeadingWrapper,
  StyledStaticContent,
} from './groups-styled';

const { Sider, Content } = Layout;

const Groups: FC = () => {
  const [data, setData] = useState<IGroupCard[] | undefined>();
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IGroupCard | null>(null);

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
        <StyledStaticContent
          onClick={() => {
            setIsSiderVisible(false);
            setCardData(null); // to trigger rerender, simulating onBlur effect
          }}
        >
          <StyledFilesWrapper>
            {data?.map((value: any) => (
              <>
                <GroupCard
                  groupName={value.groupName}
                  isModerator={value.isModerator}
                  moderatorName={value.moderatorName}
                  numberOfFiles={value.numberOfFiles}
                  numberOfUsers={value.numberOfUsers}
                  selected={value?.id === cardData?.id}
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
                  uuid={value.uuid}
                />
              </>
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PModal
          isModalVisible={isModalVisible}
          setIsModalVisible={setIsModalVisible}
          cardData={cardData}
          setCardData={setCardData}
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

export default Groups;
