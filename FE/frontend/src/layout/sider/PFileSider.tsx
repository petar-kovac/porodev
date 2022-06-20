import { Layout } from 'antd';
import { Dispatch, FC, SetStateAction } from 'react';
import styled from 'styled-components';

import { IGroupCard, IFilesCard } from 'types/card-data';
import { CloseOutlined, FileOutlined, FolderFilled } from '@ant-design/icons';
import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';
import StyledIcon from 'styles/icons/StyledIcons';
import { StyledClose } from 'styles/icons/styled-icons';
import SiderData from './util/SiderData';

const { Sider } = Layout;

interface IPFileSiderProps {
  isSiderVisible?: boolean;
  cardData?: IGroupCard | IFilesCard;
  type: 'folder' | 'file';
  setIsSiderVisible: Dispatch<SetStateAction<boolean>>;
}

const PFileSider: FC<IPFileSiderProps> = ({
  isSiderVisible,
  setIsSiderVisible,
  cardData,
  type,
}) => {
  const handleClose = () => {
    setIsSiderVisible(false);
  };

  return (
    <StyledFileSider collapsedWidth={0} collapsed={!isSiderVisible} width={320}>
      <StyledColumn>
        <StyledRow>
          <StyledIcon type={type} />
          <StyledClose onClick={() => handleClose()} />
        </StyledRow>
        <StyledTitle>{cardData?.title}</StyledTitle>
        <StyledContent>
          {cardData && <SiderData data={cardData} />}
        </StyledContent>
        <PButton
          text={`Show ${type}`}
          color="#fff"
          borderRadius="12px"
          background={theme.colors.primary}
        />
      </StyledColumn>
    </StyledFileSider>
  );
};

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
  align-items: flex-start;
`;
const StyledTitle = styled.div`
  font-size: 24px;
`;

const StyledColumn = styled.div`
  display: flex;
  flex-direction: column;
  align-items: space-around;
  width: 290px;
  flex: 1;
  gap: 15px;
`;

const StyledFileSider = styled(Sider).attrs({
  'data-testid': 'file-sider',
})`
  // change parent component if you know what are you doing, usually change .children class
  background-color: #fff;
  overflow-x: hidden;
  display: flex;
  width: 400px;

  flex-direction: column;
  box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);

  .ant-layout-sider-trigger {
    box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);
  }

  .ant-layout-sider-children {
    display: flex;
    flex-direction: column;
    width: 400px;

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

export default PFileSider;
