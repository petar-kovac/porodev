import { FC, useEffect, useState } from 'react';

import PFilter from 'components/filter/PFilter';
import { usePageContext } from 'context/PageContext';
import { IFilesCard } from 'types/card-data';

import RuntimeCards from 'components/card/RuntimeCards';
import SiderContextProvider from 'context/SiderContext';
import { findFiles } from 'service/files/files';
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
            type="runtime"
          />
        </SiderContextProvider>
      </StyledContent>
      <RuntimeModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Runtime;
