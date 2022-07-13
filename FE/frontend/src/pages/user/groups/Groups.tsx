import { FC, useEffect, useState } from 'react';

import GroupCards from 'components/card/GroupCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
import { usePageContext } from 'context/PageContext';
import { findFiles } from 'service/files/files';

import {
  StyledPageWrapper,
  StyledContent,
  StyledFilterWrapper,
  StyledFilesWrapper,
  StyledStaticContent,
} from './styles/groups-styled';
import GroupSider from './sider/GroupSider';
import GroupModal from './modal/GroupModal';

const Groups: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [data, setData] = useState<any>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible } = usePageContext();

  useEffect(() => {
    setIsSiderVisible(false);
    const fetchFiles = async () => {
      try {
        const res = await findFiles();
        setData(res.content);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

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
              data={data}
              cardData={cardData}
              setCardData={setCardData}
              selectedCardId={selectedCardId}
              setSelectedCardId={setSelectedCardId}
            />
          </StyledFilesWrapper>
        </StyledStaticContent>

        <GroupSider cardData={cardData} type="file" />
      </StyledContent>
      <GroupModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Groups;
