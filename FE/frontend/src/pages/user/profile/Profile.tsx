import { FC, useState, ReactNode, ReactElement } from 'react';
import styled from 'styled-components';
import { StorageKey } from 'util/enums/storage-keys';

import type { UploadProps } from 'antd';
import { Avatar, Input, Upload, message, Button } from 'antd';
import {
  EditOutlined,
  MinusCircleOutlined,
  UploadOutlined,
} from '@ant-design/icons';

import PModal from 'components/modal/PModal';
import { usePageContext } from 'context/PageContext';

interface IModalTitleProps {
  title?: string;
  content?: ReactNode;
}

const Profile: FC = () => {
  const [modalData, setModalData] = useState<IModalTitleProps | undefined>(
    undefined,
  );
  const { setIsModalVisible } = usePageContext();

  return (
    <>
      <StyledPage>
        <StyledProfileCard>
          <h2>Profile Info</h2>
          <StyledProfileCardContent>
            <StyledProfileCardItem avatar>
              <p>Profile picture</p>
              <Avatar size={48} src="https://joeschmoe.io/api/v1/random" />
              <StyledProfileIcon style={{ marginBottom: '.8rem' }}>
                <UploadOutlined />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading className="name">First name: </StyledPHeading>
              <StyledPContent>
                {localStorage.getItem(StorageKey.NAME)}
              </StyledPContent>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setModalData({
                    title: 'Change Your Firstname',
                    content: localStorage.getItem(StorageKey.NAME) as string,
                  });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading>Last name: </StyledPHeading>
              <StyledPContent>
                {localStorage.getItem(StorageKey.LASTNAME)}
              </StyledPContent>

              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setModalData({
                    title: 'Change Your Lastname',
                    content: localStorage.getItem(
                      StorageKey.LASTNAME,
                    ) as string,
                  });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading>Email</StyledPHeading>
              <StyledPContent>
                {localStorage.getItem(StorageKey.EMAIL)}
              </StyledPContent>
              <StyledProfileIcon>
                <StyledDisableIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading>Password</StyledPHeading>
              <StyledPContent>******</StyledPContent>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setModalData({
                    title: 'Change Your Password',
                  });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
          </StyledProfileCardContent>
        </StyledProfileCard>
      </StyledPage>
      <PModal
        title={modalData?.title}
        content={
          <Input
            onChange={(e) =>
              setModalData({ ...modalData, content: e.target.value })
            }
            value={modalData?.content as string}
          />
        }
      />
    </>
  );
};

const StyledPage = styled.div`
  margin-top: 5rem; ;
`;

const StyledProfileCard = styled.div`
  max-width: 80rem;
  margin: 0 auto;
  padding: 3rem 8rem;
  background-color: #fcfcfc;
  border-radius: 30px;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 3rem;
`;

const StyledProfileCardContent = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

const StyledProfileCardItem = styled.div<{ avatar?: boolean }>`
  display: flex;
  justify-content: space-between;
  align-items: ${({ avatar }) => (avatar ? 'flex-end' : 'center')};
  border-bottom: 1px solid #ddd;
  padding: 0 1.5rem;
  border-bottom-left-radius: 0.8rem;
  border-bottom-right-radius: 0.8rem;

  .ant-avatar {
    border: 1px solid #999;
    margin-bottom: 0.5rem;
  }
`;

const StyledProfileIcon = styled.div`
  cursor: pointer;
`;

const StyledPHeading = styled.p.attrs(() => ({
  'data-testid': 'paragraph-heading',
}))``;

const StyledPContent = styled.p.attrs(() => ({
  'data-testid': 'paragraph-description',
}))``;

const StyledEditIcon = styled(EditOutlined).attrs(() => ({
  'data-testid': 'edit-icon',
}))``;

const StyledDisableIcon = styled(MinusCircleOutlined).attrs(() => ({
  'data-testid': 'disable-icon',
}))``;

export default Profile;
