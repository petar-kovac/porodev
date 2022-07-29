import { FC, useState } from 'react';

import GroupCards from 'components/cards/group/GroupCards';
import PFilter from 'components/filter/PFilter';
import Spinner from 'components/spinner/Spinner';
import { usePageContext } from 'context/PageContext';
import Error from 'pages/error/ErrorPage';
import { StyledSpinnerWrapper } from 'styles/shared-styles';
import { IFilesCard } from 'types/card-data';

import PFolders from 'components/folders/PFolders';
import PModal from 'components/modal/PModal';

import {
  CloseOutlined,
  FileAddOutlined,
  FileZipFilled,
  PlusCircleOutlined,
  PlusOutlined,
} from '@ant-design/icons';
import GroupsContextProvider from 'context/GroupsContext';
import GroupModal from './modal/GroupModal';
import GroupSider from './sider/GroupSider';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledFilterWrapper,
  StyledPageWrapper,
  StyledStaticContent,
  StyledFoldersContainer,
  StyledFoldersWrapper,
  StyledFilesContainer,
  StyledHeadingWrapper,
  StyledPlusCircle,
} from './styles/groups-styled';
import useGroupData from './hooks/useGroupData';
import { IModalTitleProps } from '../profile/Profile';

const Groups: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);
  const [modalData, setModalData] = useState<IModalTitleProps | undefined>(
    undefined,
  );

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();
  const { data, isLoading, error } = useGroupData();

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
          <StyledFoldersContainer>
            <StyledHeadingWrapper>
              <h2>Shared spaces</h2>
              {/* <PlusCircleOutlined
                onClick={() => {
                  setIsModalVisible(true);
                }}
              /> */}
              <StyledPlusCircle
                onClick={() => {
                  setIsModalVisible(true);
                }}
                type="primary"
                shape="circle"
                icon={
                  <PlusOutlined style={{ color: '#fff', fontSize: '2.4rem' }} />
                }
              />
            </StyledHeadingWrapper>
            <div>
              <StyledFoldersWrapper>
                <PFolders
                  data={data}
                  cardData={cardData}
                  setCardData={setCardData}
                  selectedCardId={selectedCardId}
                  setSelectedCardId={setSelectedCardId}
                />
              </StyledFoldersWrapper>
            </div>
          </StyledFoldersContainer>
          <StyledFilesContainer>
            {/* <StyledFilterWrapper>
              <PFilter
                isList={isList}
                setIsList={setIsList}
                activeFilters={{
                  showSortByType: true,
                  showSortByTime: true,
                }}
              />
            </StyledFilterWrapper> */}
            {/* <h2>Latest 5 files</h2>
            <StyledFilesWrapper>
              <GroupCards
                data={data}
                cardData={cardData}
                setCardData={setCardData}
                selectedCardId={selectedCardId}
                setSelectedCardId={setSelectedCardId}
              />
            </StyledFilesWrapper> */}
          </StyledFilesContainer>
        </StyledStaticContent>

        <GroupsContextProvider>
          <GroupSider cardData={cardData} selectedCardId={selectedCardId} />
        </GroupsContextProvider>
      </StyledContent>
      <GroupModal cardData={cardData} setCardData={setCardData} />
      <PModal
        data={data}
        title="Create shared space"
        inputField="sharedspace"
        modalData={modalData}
        setModalData={setModalData}
      />
    </StyledPageWrapper>
  );
};

export default Groups;
