import { Layout } from 'antd';
import axios from 'axios';
import { FC, useCallback, useEffect, useRef, useState } from 'react';

import GroupCards from 'components/card/GroupCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';

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
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

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
            <GroupCards
              cardData={cardData}
              selectedCardId={selectedCardId}
              setCardData={setCardData}
              setSelectedCardId={setSelectedCardId}
              setIsModalVisible={setIsModalVisible}
              setIsSiderVisible={setIsSiderVisible}
            />
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
