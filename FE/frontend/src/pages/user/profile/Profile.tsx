import { FC, useState } from 'react';
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

interface IModalTitleProps {
  title: string;
}

const Profile: FC = () => {
  const [isModalVisible, setIsModalVisible] = useState<boolean>(false);
  const [modalTitle, setModalTitle] = useState<IModalTitleProps | undefined>(
    undefined,
  );

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
              <p className="name">First name: </p>
              <p> {localStorage.getItem(StorageKey.NAME)}</p>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setModalTitle({
                    title: 'Change Your Firstname',
                  });
                }}
              >
                <EditOutlined />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <p>Last name: </p>
              <p> {localStorage.getItem(StorageKey.LASTNAME)}</p>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setModalTitle({
                    title: 'Change Your Lastname',
                  });
                }}
              >
                <EditOutlined />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <p>Email</p>
              <p>{localStorage.getItem(StorageKey.EMAIL)}</p>
              <StyledProfileIcon>
                <MinusCircleOutlined />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <p>Password</p>
              <p>random</p>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setModalTitle({
                    title: 'Change Your Password',
                  });
                }}
              >
                <EditOutlined />
              </StyledProfileIcon>
            </StyledProfileCardItem>
          </StyledProfileCardContent>
        </StyledProfileCard>
      </StyledPage>
      <PModal
        title={modalTitle?.title}
        isModalVisible={isModalVisible}
        setIsModalVisible={setIsModalVisible}
        content={<Input />}
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

export default Profile;
