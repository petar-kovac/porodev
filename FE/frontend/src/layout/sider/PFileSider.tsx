import { Button, Layout } from 'antd';
import { Dispatch, FC, SetStateAction, useState } from 'react';
import styled from 'styled-components';

import {
  CloseOutlined,
  CreditCardOutlined,
  FileOutlined,
  FolderFilled,
} from '@ant-design/icons';
import dayjs from 'dayjs';
import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';

const { Sider } = Layout;

interface IPFileSiderProps {
  isSiderVisible?: boolean;
  cardData?: IGroupCard | IFilesCard;
  type: 'folder' | 'file';
  setIsSiderVisible: Dispatch<SetStateAction<boolean>>;
}
export interface IGroupCard {
  titleGroup?: string;
  title: string;
  avatar?: string;
  createdAt?: string;
  groupName?: string;
  id?: number;
  isModerator?: boolean;
  moderatorName?: string;
  numberOfFiles?: number;
  numberOfUsers?: number;
  uuid?: string;
}
export interface IFilesCard {
  titleString?: string;
  title: string;
  avatar?: string;
  createdAt?: string;
  description?: string;
  id?: number;
  image?: string;
  name?: string;
}

enum ApiTranslation {
  createdAt = 'Created at',
  groupName = 'Group name',
  isModerator = 'Is user moderator',
  numberOfFiles = 'Number of files',
  numberOfUsers = 'Number of users',
  moderatorName = 'Moderator name',
  uuid = 'UUID',
  title = 'Title',
  id = 'ID',
  name = 'Name',
  description = 'Description',
  image = 'Image',
  ERROR = 4,
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
          {type === 'folder' ? <StyledFolder /> : <StyledFile />}
          <StyledClose onClick={() => handleClose()} />
        </StyledRow>
        <StyledTitle>{cardData?.title}</StyledTitle>
        <StyledContent>
          {cardData &&
            Object.entries(cardData as object).map((value: any) => {
              if (value[0] === 'createdAt') {
                value[1] = dayjs.unix(Date.parse(value[1])).format('D MMMM');
              }
              if (Object.keys(ApiTranslation).includes(value[0])) {
                value[0] = ApiTranslation[value[0]];
              }

              // ApiTranslation[value[0]] !== null
              return (
                <StyledContentRow>
                  <StyledText>{value[0]}:</StyledText>
                  <StyledText>{value[1]}</StyledText>
                </StyledContentRow>
              );
            })}
        </StyledContent>
        <PButton
          text={type === 'folder' ? 'Show folder' : 'Show file'}
          color="#fff"
          borderRadius="12px"
          background={theme.colors.primary}
        />
      </StyledColumn>
    </StyledFileSider>
  );
};

const StyledText = styled.div`
  display: flex;
  flex: 1;
`;
const StyledContentRow = styled.div`
  display: flex;
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
const StyledFolder = styled(FolderFilled)`
  color: ${({ theme: { colors } }) => colors.primary};
  font-size: 100px;
`;
const StyledFile = styled(FileOutlined)`
  color: ${({ theme: { colors } }) => colors.primary};
  font-size: 100px;
`;
const StyledClose = styled(CloseOutlined)`
  color: ${({ theme: { colors } }) => colors.primary};
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
