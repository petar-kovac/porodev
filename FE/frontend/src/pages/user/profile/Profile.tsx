import { FC, useState, ReactNode, ReactElement, useEffect } from 'react';
import styled from 'styled-components';
import { StorageKey } from 'util/enums/storage-keys';

import { findUserById } from 'service/authorization/authorization';

import JwtDecode from 'jwt-decode';

import type { UploadProps } from 'antd';
import { Avatar, Input, Upload, message, Button } from 'antd';
import {
  EditOutlined,
  MinusCircleOutlined,
  UploadOutlined,
} from '@ant-design/icons';

import PModal from 'components/modal/PModal';
import { usePageContext } from 'context/PageContext';

export interface IModalTitleProps {
  title?: string;
  content?: ReactNode;
  avatarUrl?: string;
  email?: string;
  name?: string;
  lastname?: string;
  passwordUnhashed?: string;
  position?: string;
  role?: 0;
}

const Profile: FC = () => {
  const [modalData, setModalData] = useState<IModalTitleProps | undefined>(
    undefined,
  );
  const [passData, setPassData] = useState<any>(null);

  const [inputField, setInputField] = useState<string>('');
  const [userData, setUserData] = useState<any>(null);

  const { isModalVisible, setIsModalVisible } = usePageContext();

  const accessToken = localStorage.getItem(StorageKey.ACCESS_TOKEN);

  const allowed = [
    'avatarUrl',
    'department',
    'email',
    'name',
    'lastname',
    'position',
    'role',
  ];

  useEffect(() => {
    if (accessToken) {
      const { Id }: any = JwtDecode(accessToken);
      findUserById(Id)
        .then((res) => {
          const { data } = res;
          setUserData(data);
          const filteredResponse: any = Object.keys(data)
            .filter((key) => {
              return allowed.includes(key);
            })
            .reduce((obj, key) => {
              return { ...obj, [key]: data[key] };
            }, {});
          localStorage.setItem(StorageKey.NAME, filteredResponse.name);
          localStorage.setItem(StorageKey.LASTNAME, filteredResponse.lastname);

          setModalData(filteredResponse);
        })
        .catch((error) => console.log(error));
    }
  }, [isModalVisible]);

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
              <StyledPContent>{userData?.name}</StyledPContent>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setInputField('firstname');
                  // setModalData({
                  //   title: 'Change Your Firstname',
                  //   content: localStorage.getItem(StorageKey.NAME) as string,
                  // });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading>Last name: </StyledPHeading>
              <StyledPContent>{userData?.lastname}</StyledPContent>

              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setInputField('lastname');
                  // setModalData({
                  //   title: 'Change Your Lastname',
                  //   content: localStorage.getItem(
                  //     StorageKey.LASTNAME,
                  //   ) as string,
                  // });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading>Position</StyledPHeading>
              <StyledPContent>position</StyledPContent>
              <StyledProfileIcon
                onClick={() => {
                  setIsModalVisible(true);
                  setInputField('position');
                  // setModalData({
                  //   title: 'Change Your Password',
                  // });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
            <StyledProfileCardItem>
              <StyledPHeading>Email</StyledPHeading>
              <StyledPContent>
                {/* {localStorage.getItem(StorageKey.EMAIL)} */}
                {userData?.email}
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
                  setPassData({ email: modalData?.email });
                  setIsModalVisible(true);
                  setInputField('password');

                  // setModalData({
                  //   title: 'Change Your Password',
                  // });
                }}
              >
                <StyledEditIcon />
              </StyledProfileIcon>
            </StyledProfileCardItem>
          </StyledProfileCardContent>
        </StyledProfileCard>
      </StyledPage>
      <PModal
        // title={modalData?.title}
        title={`Change your ${inputField}`}
        inputField={inputField}
        modalData={modalData}
        setModalData={setModalData}
        passData={passData}
        setPassData={setPassData}
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
