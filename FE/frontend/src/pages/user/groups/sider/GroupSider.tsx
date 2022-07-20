import { Layout } from 'antd';
import { Dispatch, FC, SetStateAction } from 'react';
import styled from 'styled-components';

import PButton from 'components/buttons/PButton';
import { usePageContext } from 'context/PageContext';
import { StyledFile } from 'styles/icons/styled-icons';
import { IFilesCard } from 'types/card-data';
import SiderDataMapper from 'util/mappers/SiderDataMapper';

const { Sider } = Layout;

interface IPFileSiderProps {
  cardData?: IFilesCard | null;
  data?: IFilesCard[] | null;
  selectedCardId?: number | null;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setData?: Dispatch<SetStateAction<IFilesCard[] | null>>;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
}

const GroupSider: FC<IPFileSiderProps> = ({
  cardData,
  data,
  setSelectedCardId = () => undefined,
  selectedCardId,
}) => {
  const { isLoading, isSiderVisible, setIsSiderVisible } = usePageContext();

  const handleClose = () => {
    setIsSiderVisible(false);
    // setCardData(null);
    setSelectedCardId(null);
  };

  const onClick = () => {
    console.log('Group sider');
  };

  return (
    <StyledGroupSider
      collapsedWidth={0}
      collapsed={!isSiderVisible}
      width={300}
      onClick={(e) => e.stopPropagation()}
    >
      <StyledColumn>
        <StyledFirstPart>
          <StyledRow>
            <StyledFile />
          </StyledRow>
          <StyledTitle>Info</StyledTitle>
          <StyledContent>
            {cardData && <SiderDataMapper data={cardData} />}
          </StyledContent>
        </StyledFirstPart>
        <PButton text="Show files" onClick={onClick} isLoading={isLoading} />
      </StyledColumn>
    </StyledGroupSider>
  );
};

const StyledFirstPart = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

const StyledContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 10px;
  word-wrap: break-word;
`;
const StyledRow = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  justify-content: space-between;
  align-items: center;
`;
const StyledTitle = styled.div`
  font-size: 24px;
`;

const StyledColumn = styled.div`
  display: flex;
  flex-direction: column;
  align-items: space-around;
  width: 275px;
  gap: 15px;
  height: 95%;
  justify-content: space-between;
  padding-bottom: 20px;
`;

const StyledGroupSider = styled(Sider).attrs({
  'data-testid': 'file-sider',
})`
  // change parent component if you know what are you doing, usually change .children class
  background-color: #fff;
  overflow-x: hidden;
  display: flex;
  width: 380px;

  flex-direction: column;
  box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);

  .ant-layout-sider-trigger {
    box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);
  }

  .ant-layout-sider-children {
    display: flex;
    flex-direction: column;
    width: 380px;

    position: fixed;
    padding: 20px;
  }

  & li span {
    font-size: 1.9rem;
  }

  & li span svg {
    font-size: 1.9rem;
  }

  .ant-layout-sider-trigger {
    background-color: ${({ theme: { colors } }) => colors.primary};
  }
`;

export default GroupSider;
