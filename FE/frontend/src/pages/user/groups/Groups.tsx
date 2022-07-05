import { FC, useEffect, useState } from 'react';

import GroupCards from 'components/card/GroupCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
import { usePageContext } from 'context/PageContext';

import {
  StyledPageWrapper,
  StyledContent,
  StyledFilterWrapper,
  StyledFilesWrapper,
  StyledStaticContent,
} from './groups-styled';

const Groups: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  useEffect(() => {
    setIsSiderVisible(false);
  }, []);

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

        <PFileSider cardData={cardData} type="file" />
      </StyledContent>
      <PModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Groups;
