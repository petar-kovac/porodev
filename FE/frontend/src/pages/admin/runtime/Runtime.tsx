import { FC, useState } from 'react';

import PFilter from 'components/filter/PFilter';
import { usePageContext } from 'context/PageContext';
import { IFilesCard } from 'types/card-data';

import RuntimeCards from 'components/cards/runtime/RuntimeCards';
import Spinner from 'components/spinner/Spinner';
import SiderContextProvider, { useSiderContext } from 'context/SiderContext';
import Error from 'pages/error/ErrorPage';
import { StyledSpinnerWrapper } from 'styles/shared-styles';
import useRuntimeData from './hooks/useRuntimeData';
import RuntimeModal from './modal/RuntimeModal';
import RuntimeSider from './sider/RuntimeSider';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledFilterWrapper,
  StyledPageWrapper,
  StyledStaticContent,
} from './styles/runtime-styled';

const Runtime: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible } = usePageContext();

  const { data, isLoading, error } = useRuntimeData();

  // if (isLoading) {
  //   return (
  //     <StyledSpinnerWrapper>
  //       <Spinner color="#000" size={42} speed={1.2} />
  //     </StyledSpinnerWrapper>
  //   );
  // }
  // if (error) {
  //   return <Error message={error} />;
  // }

  return (
    <StyledPageWrapper>
      <StyledContent
        onClick={() => {
          setSelectedCardId(null);
          setCardData(null);
          setIsSiderVisible(false);
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
              data={data}
              cardData={cardData}
              selectedCardId={selectedCardId}
              setCardData={setCardData}
              setSelectedCardId={setSelectedCardId}
            />
          </StyledFilesWrapper>
        </StyledStaticContent>
        <SiderContextProvider>
          <RuntimeSider
            data={data}
            cardData={cardData}
            selectedCardId={selectedCardId}
          />
        </SiderContextProvider>
      </StyledContent>
      <RuntimeModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Runtime;
