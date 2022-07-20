import { FC, useState } from 'react';

import GroupCards from 'components/cards/group/GroupCards';
import PFilter from 'components/filter/PFilter';
import Spinner from 'components/spinner/Spinner';
import { usePageContext } from 'context/PageContext';
import Error from 'pages/error/ErrorPage';
import { StyledSpinnerWrapper } from 'styles/shared-styles';
import { IFilesCard } from 'types/card-data';

import useRuntimeData from '../runtime/hooks/useRuntimeData';
import GroupModal from './modal/GroupModal';
import GroupSider from './sider/GroupSider';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledFilterWrapper,
  StyledPageWrapper,
  StyledStaticContent,
} from './styles/groups-styled';

const SingleGroup: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible } = usePageContext();
  const { data, isLoading, error } = useRuntimeData();

  if (isLoading) {
    return (
      <StyledSpinnerWrapper>
        <Spinner color="#000" size={42} speed={1.2} />
      </StyledSpinnerWrapper>
    );
  }

  if (error) {
    return <Error message={error} />;
  }

  return (
    <StyledPageWrapper>
      <StyledContent
        onClick={() => {
          setIsSiderVisible(false);
          setSelectedCardId(null);
          setCardData(null);
        }}
      >
        <StyledStaticContent>
          <StyledFilesWrapper>
            <GroupCards
              data={data}
              cardData={cardData}
              setCardData={setCardData}
              selectedCardId={selectedCardId}
              setSelectedCardId={setSelectedCardId}
            />
          </StyledFilesWrapper>
        </StyledStaticContent>

        <GroupSider cardData={cardData} />
      </StyledContent>
      <GroupModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default SingleGroup;
