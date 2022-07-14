import { FC, useEffect, useState } from 'react';

import GroupCards from 'components/cards/group/GroupCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import { IFilesCard } from 'types/card-data';
import { usePageContext } from 'context/PageContext';

import RuntimeCards from 'components/cards/runtime/RuntimeCards';
import {
  StyledPageWrapper,
  StyledContent,
  StyledFilterWrapper,
  StyledFilesWrapper,
  StyledStaticContent,
} from './runtime-styled';

const Runtime: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  return (
    <StyledPageWrapper>
      <StyledContent
        onClick={() => {
          setSelectedCardId(null);
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
            <RuntimeCards
              cardData={cardData}
              selectedCardId={selectedCardId}
              setCardData={setCardData}
              setSelectedCardId={setSelectedCardId}
              // setIsModalVisible={setIsModalVisible}
              // setIsSiderVisible={setIsSiderVisible}
            />
          </StyledFilesWrapper>
        </StyledStaticContent>
      </StyledContent>
      <PModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Runtime;
